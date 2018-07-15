namespace ODrive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using LibUsbDotNet.Info;
    using LibUsbDotNet.Main;
    using ODrive.Utilities;
    using Nito.AsyncEx;
    using Nito.Disposables;
    using Nito;
    using System.Collections.Concurrent;
    using ODrive.Exceptions;

    internal class Connection
    {
        private const int REQUEST_TIMEOUT_MS = 1 * 1000;
        private const int SCHEMA_FETCH_TIMEOUT_SECONDS = 30;

        private readonly ThreadSafeCounter sequenceCounter = new ThreadSafeCounter();
        private readonly ConcurrentDictionary<ushort, Request> pendingRequests = new ConcurrentDictionary<ushort, Request>();
        private readonly ConcurrentDictionary<ushort, Request> cancelledRequests = new ConcurrentDictionary<ushort, Request>();

        private readonly UsbDevice usbDevice;

        private UsbInterfaceInfo readInterfaceInfo;
        private UsbEndpointInfo readEndpointInfo;

        private UsbInterfaceInfo writeInterfaceInfo;
        private UsbEndpointInfo writeEndpointInfo;

        private UsbEndpointReader endpointReader;
        private UsbEndpointWriter endpointWriter;

        // For endpoint 0 the protocol version is used, for all others we need the actual CRC16 of the JSON endpoints definition
        public ushort? SchemaChecksum { get; private set; }

        public bool IsConnected { get => endpointWriter != null && endpointWriter != null; }

        public Connection(UsbDevice usbDevice, ushort schemaChecksum)
        {
            this.usbDevice = usbDevice;
            SchemaChecksum = schemaChecksum;
        }

        ////// Determine if the supplied checksum is valid for the device we're connected to.
        ////// Since the device won't reply to non-zero endpoints if the checksum is incorrect,
        ////// we first verify it will reply at all by fetching a small bit of endpoint 0 and 
        ////// then endpoint 1 (which should be the bus voltage endpoint)
        ////public async Task<bool> ValidateChecksum(ushort checksumValue)
        ////{
        ////    throw new NotImplementedException();
        ////}

        // TODO: Need proper return type
        // TODO: Timeout? What are possible results of OpenEndpointReader/Writer?
        public bool Connect()
        {
            if (IsConnected)
            {
                throw new Exception("Attempted to Connect an already connected connection.");
            }

            // There should be only one config, but whatevs.
            foreach (var config in usbDevice.Configs)
            {
                foreach (var interfaceInfo in config.InterfaceInfoList)
                {
                    foreach (var endpointInfo in interfaceInfo.EndpointInfoList)
                    {
                        if ((readInterfaceInfo == null && readEndpointInfo == null)
                            && Config.USB_READ_ENDPOINT_ADDRESSES.Contains(endpointInfo.Descriptor.EndpointID))
                        {
                            readInterfaceInfo = interfaceInfo;
                            readEndpointInfo = endpointInfo;
                        }

                        if ((writeInterfaceInfo == null && writeEndpointInfo == null)
                            && Config.USB_WRITE_ENDPOINT_ADDRESSES.Contains(endpointInfo.Descriptor.EndpointID))
                        {
                            writeInterfaceInfo = interfaceInfo;
                            writeEndpointInfo = endpointInfo;
                        }
                    }
                }
            }

            if (readEndpointInfo == null)
            {
                throw new Exception("Failed to locate read endpoint.");
            }

            if (writeEndpointInfo == null)
            {
                throw new Exception("Failed to locate write endpoint.");
            }

            if (writeInterfaceInfo.Descriptor.InterfaceID != readInterfaceInfo.Descriptor.InterfaceID)
            {
                throw new Exception("Read and write endpoints must be on the same interface.");
            }

            endpointReader = usbDevice.OpenEndpointReader(
                (ReadEndpointID)readEndpointInfo.Descriptor.EndpointID,
                readEndpointInfo.Descriptor.MaxPacketSize,
                (EndpointType)(readEndpointInfo.Descriptor.Attributes & 0x03));

            endpointReader.ReadThreadPriority = ThreadPriority.AboveNormal;
            endpointReader.DataReceivedEnabled = true;

            endpointReader.DataReceived -= EndpointReader_DataReceived;
            endpointReader.DataReceived += EndpointReader_DataReceived;

            endpointWriter = usbDevice.OpenEndpointWriter(
                (WriteEndpointID)writeEndpointInfo.Descriptor.EndpointID,
                (EndpointType)(writeEndpointInfo.Descriptor.Attributes & 0x03));

            return true;
        }

        // TODO: Need proper return type
        public bool Disconnect()
        {
            if (IsConnected == false)
            {
                throw new Exception("Attempted to Disconnect and already disconnected connection.");
            }
            endpointReader.DataReceived -= EndpointReader_DataReceived;
            endpointReader = null;
            endpointWriter = null;

            return true;
        }

        private int SendRequest(Request request)
        {
            AssertConnected();

            if (request.Body.Data.Length + 2 >= Config.USB_MAX_PACKET_SIZE)
            {
                throw new NotSupportedException("Packets larger than 127 bytes are not currently supported.");
            }

            // TODO: Is the compare to USB_PROTOCOL_VERSION safe? Is there any chance the CRC could be 1?
            if (request.EndpointID != 0 && request.Signature == Config.USB_PROTOCOL_VERSION)
            {
                throw new ArgumentException("Packet signature must be provided if requesting endpoints other than 0.");
            }

            int transferLength = 0;

            if (!request.CancellationToken.IsCancellationRequested)
            {
                var requestBytes = request.ToByteArray();

                ErrorCode err = endpointWriter.Write(requestBytes, Config.USB_WRITE_TIMEOUT, out transferLength);

                if (err != ErrorCode.None)
                {
                    throw new UsbLibraryException($"Error {UsbDevice.LastErrorNumber} occurred in USB library: {UsbDevice.LastErrorString}.");
                }

                pendingRequests.TryAdd(request.SequenceNumber, request);
            }

            return transferLength;
        }

        private void EndpointReader_DataReceived(object sender, EndpointDataEventArgs e)
        {
            var response = new Response(e.Buffer, e.Count);
            var sequenceNumber = response.SequenceNumber;

            pendingRequests.TryGetValue(sequenceNumber, out Request pendingRequest);

            if (pendingRequest != null)
            {
                if (pendingRequest.CancellationToken.IsCancellationRequested)
                {
                    // Move the request into the cancelled list, we may still receive
                    // the response for it, although we will discard the response
                    pendingRequests.TryRemove(sequenceNumber, out _);
                    cancelledRequests.TryAdd(sequenceNumber, pendingRequest);
                    pendingRequest.CancellationToken.ThrowIfCancellationRequested();
                }
                else
                {
                    // TODO: Log on failure??
                    pendingRequests.TryRemove(sequenceNumber, out _);
                    pendingRequest.ResponseCallback?.Invoke(pendingRequest, response);
                }
            }
            else
            {
                // If it's not pending, maybe it was cancelled?  If it was then we
                // know it can be discarded.
                cancelledRequests.TryGetValue(sequenceNumber, out Request cancelledRequest);
                if (cancelledRequest != null)
                {
                    pendingRequests.TryRemove(sequenceNumber, out _);
                    cancelledRequests.TryRemove(sequenceNumber, out _);
                }

                // Received data we have no knoweldge of asking for... log it?
            }
        }

        public async Task<T> FetchEndpointScalar<T>(
            ushort endpointID,
            T? newValue = null,
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null) where T : struct
        {
            var timeoutDuration = timeoutOverride ?? TimeSpan.FromMilliseconds(REQUEST_TIMEOUT_MS);

            using (NormalizedCancellationToken timeoutTokenSource = NormalizedCancellationToken.Timeout(timeoutDuration),
                cancelAndTimeoutTokenSource = NormalizedCancellationToken.Normalize(cancellationToken, timeoutTokenSource.Token))
            {
                var cancelAndTimeoutToken = cancelAndTimeoutTokenSource.Token;

                var taskCompletionSource = new TaskCompletionSource<T>();

                var dataSize = Marshal.SizeOf(typeof(T));

                WireBuffer requestBuffer = null;

                if (newValue.HasValue)
                {
                    requestBuffer = new WireBuffer(dataSize);
                    requestBuffer.Write(newValue.Value);
                }

                var request = new Request(
                    endpointID: endpointID,
                    expectedResponseSize: 32,
                    requestACK: true,
                    populateBody: () => requestBuffer,
                    responseCallback: (req, res) =>
                    {
                        var responseData = res.Body.Read<T>();
                        taskCompletionSource.SetResult(responseData);
                    },
                    signature: SchemaChecksum ?? Config.USB_PROTOCOL_VERSION,
                    cancellationToken: cancelAndTimeoutToken
                );

                SendRequest(request);

                T result = default(T);

                try
                {
                    result = await taskCompletionSource.Task.WaitAsync(cancelAndTimeoutToken);
                }
                catch (OperationCanceledException ex)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw new RequestCancelledException($"Cancellation signal received during {nameof(FetchEndpointScalar)}.", ex);
                    }
                    if (timeoutTokenSource.Token.IsCancellationRequested)
                    {
                        throw new RequestTimeoutException($"Timeout occurred during {nameof(FetchEndpointScalar)}.", ex);
                    }
                }

                return result;
            }
        }

        public async Task<byte[]> FetchEndpointBuffer(CancellationToken parentCancellationToken = default(CancellationToken), TimeSpan? timeoutOverride = null)
        {
            var fetchEndpointsTimeoutDuration = timeoutOverride ?? TimeSpan.FromSeconds(SCHEMA_FETCH_TIMEOUT_SECONDS);
            using (CancellationTokenSource requestTimeoutTokenSource = new CancellationTokenSource(fetchEndpointsTimeoutDuration),
                timeoutOrParentCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(parentCancellationToken, requestTimeoutTokenSource.Token))
            {
                var timeoutOrParentCancelToken = timeoutOrParentCancelTokenSource.Token;
                var taskCompletionSource = new TaskCompletionSource<byte[]>();
                byte[] cumulativeResponse = new byte[0];
                uint totalBytesReceived = 0;

                // TODO: Ensure that timing out on this inner request triggers a retry on THIS area of code, not the parent
                while (true)
                {
                    using (CancellationTokenSource byteRequestTimeoutTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(REQUEST_TIMEOUT_MS)),
                        timeoutOrCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(timeoutOrParentCancelToken, byteRequestTimeoutTokenSource.Token))
                    {
                        var timeoutOrCancelToken = timeoutOrCancelTokenSource.Token;
                        try
                        {
                            var data = await FetchEndpointBuffer(totalBytesReceived, parentCancellationToken: timeoutOrCancelToken, timeoutOverride: timeoutOverride)
                                .WaitAsync(byteRequestTimeoutTokenSource.Token);

                            if (data.Length == 0)
                            {
                                SchemaChecksum = SchemaChecksumCalculator.CalculateChecksum(cumulativeResponse);
                                taskCompletionSource.SetResult(cumulativeResponse);

                                break;
                            }
                            else
                            {
                                totalBytesReceived += (uint)data.Length;
                            }

                            cumulativeResponse = ConcatByteArrays(cumulativeResponse, data);
                        }
                        catch (OperationCanceledException ex)
                        {
                            // Indicates we timed out on the fetch inside the loop
                            if (byteRequestTimeoutTokenSource.IsCancellationRequested)
                            {
                                throw new RequestTimeoutException($"Timeout occurred during {nameof(FetchEndpointBuffer)}.", ex);
                            }
                            // Indicates we timed out waiting for the loop
                            if (requestTimeoutTokenSource.IsCancellationRequested)
                            {
                                throw new RequestTimeoutException($"Timeout occurred during {nameof(FetchEndpointBuffer)}.", ex);
                            }
                            // Indicates that the parent cancelled
                            if (parentCancellationToken.IsCancellationRequested)
                            {
                                throw new RequestCancelledException($"Cancellation signal received during {nameof(FetchEndpointBuffer)}.", ex);
                            }
                        }
                    }
                }
                return await taskCompletionSource.Task;
            }
        }

        // TODO: Reconnect()?
        // TODO: Use this to verify successful connection prior to attempting a non-zero endpoint fetch with a questionable CRC
        internal async Task<byte[]> FetchEndpointBuffer(
            uint payloadOffset = 0,
            ushort? schemaChecksum = null,
            CancellationToken parentCancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null)
        {
            var timeoutDuration = timeoutOverride ?? TimeSpan.FromMilliseconds(REQUEST_TIMEOUT_MS);
            using (CancellationTokenSource requestTimeoutTokenSource = new CancellationTokenSource(timeoutDuration),
                timeoutOrParentCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(parentCancellationToken, requestTimeoutTokenSource.Token))
            {
                var timeoutOrParentCancelToken = timeoutOrParentCancelTokenSource.Token;

                var taskCompletionSource = new TaskCompletionSource<byte[]>();

                byte[] cumulativeResponse = new byte[0];

                var request = new Request(
                    endpointID: 0,
                    expectedResponseSize: 32,
                    requestACK: true,
                    populateBody: () =>
                    {
                        var wireBuffer = new WireBuffer(4);
                        wireBuffer.Write(payloadOffset);
                        return wireBuffer;
                    },
                    responseCallback: (req, res) =>
                    {
                        var responseBytes = res.Body.Data;
                        taskCompletionSource.SetResult(responseBytes);
                    },
                    signature: Config.USB_PROTOCOL_VERSION,
                    cancellationToken: timeoutOrParentCancelToken
                );


                byte[] result = null;

                try
                {
                    SendRequest(request);

                    result = await taskCompletionSource.Task.WaitAsync(timeoutOrParentCancelToken);
                }
                catch (OperationCanceledException ex)
                {
                    if (parentCancellationToken.IsCancellationRequested)
                    {
                        throw new RequestCancelledException($"Cancellation signal received during {nameof(FetchEndpointBuffer)}.", ex);
                    }

                    if (requestTimeoutTokenSource.Token.IsCancellationRequested)
                    {
                        throw new RequestTimeoutException($"Timeout occurred during {nameof(FetchEndpointBuffer)}.", ex);
                    }
                }

                return result;
            }
        }

        private void AssertConnected()
        {
            if (endpointReader == null || endpointWriter == null)
            {
                throw new InvalidOperationException("Attempted to read or write while connection is not open.");
            }
        }

        private static byte[] ConcatByteArrays(byte[] arrayA, byte[] arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }
    }
}
