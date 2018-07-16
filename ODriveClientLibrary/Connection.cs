namespace ODrive
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using LibUsbDotNet.Info;
    using LibUsbDotNet.Main;
    using ODrive.Exceptions;
    using ODrive.Utilities;
    using Polly;

    internal class Connection
    {
        private static readonly Lazy<Policy> StandardWaitAndRetryPolicy = new Lazy<Policy>(() =>
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(1),
                onRetry: (exception, span, retryCount, context) =>
                {
                    System.Diagnostics.Debug.WriteLine($"Polly onRetry[{retryCount}]: {exception.Message}");
                });
        }, isThreadSafe: true);

        private static readonly Lazy<Policy> StandardTimeoutPolicy = new Lazy<Policy>(() =>
        {
            return Policy.TimeoutAsync(
                timeout: TimeSpan.FromSeconds(10),
                timeoutStrategy: Polly.Timeout.TimeoutStrategy.Optimistic,
                onTimeoutAsync: (context, timespan, task) =>
                {
                    System.Diagnostics.Debug.WriteLine("Timeout occurred");
                    return task;
                });
        }, isThreadSafe: true);

        private static readonly Lazy<Policy> StandardTimeoutWaitAndRetryPolicy = new Lazy<Policy>(() =>
        {
            return Policy.WrapAsync(StandardTimeoutPolicy.Value, StandardWaitAndRetryPolicy.Value);
        }, isThreadSafe: true);

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

        public async Task<bool> TestConnection()
        {
            // Don't retry on this request
            var testBytes = await RequestBufferSegment(payloadOffset: 0, requestPolicy: StandardTimeoutPolicy.Value).ConfigureAwait(false);
            return testBytes.Length >= 30;
        }

        public async Task<bool> ValidateChecksum(ushort? checksumValue)
        {
            var checksumToValidate = checksumValue ?? SchemaChecksum;

            if (!checksumToValidate.HasValue)
            {
                throw new ArgumentNullException("No checksum provided.");
            }
            // Don't retry on this request
            await RequestInvoke(endpointID: 1, requestPolicy: StandardTimeoutPolicy.Value).ConfigureAwait(false);

            return true;
        }

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

            int transferLength = 0;

            if (!request.CancellationToken.IsCancellationRequested)
            {
                var requestBytes = request.ToByteArray();
                System.Diagnostics.Debug.WriteLine($"Sending seqNo {request.SequenceNumber}");
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

            System.Diagnostics.Debug.WriteLine($"Received seqNo {response.SequenceNumber}...");

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

        public async Task<T> RequestResponse<T>(
            ushort endpointID,
            CancellationToken parentCancellationToken = default(CancellationToken),
            Policy requestPolicy = null)
        {
            var policyToUse = requestPolicy ?? StandardTimeoutWaitAndRetryPolicy.Value;
            return await policyToUse.ExecuteAsync(async cancellationToken =>
            {
                var taskCompletionSource = new TaskCompletionSource<T>();

                WireBuffer requestBuffer = null;
                requestBuffer = new WireBuffer(0);

                var request = new Request(
                    endpointID: endpointID,
                    expectedResponseSize: 0,
                    requestACK: true,
                    populateBody: () => requestBuffer,
                    responseCallback: (req, res) =>
                    {
                        var responseData = res.Body.Read<T>();
                        taskCompletionSource.SetResult(responseData);
                    },
                    signature: SchemaChecksum ?? Config.USB_PROTOCOL_VERSION,
                    cancellationToken: cancellationToken
                );

                SendRequest(request);

                return await taskCompletionSource.Task.ConfigureAwait(false);
            }, parentCancellationToken).ConfigureAwait(false);
        }

        public async Task<T> RequestResponse<T>(
            ushort endpointID,
            T value,
            CancellationToken parentCancellationToken = default(CancellationToken),
            Policy requestPolicy = null)
        {
            var policyToUse = requestPolicy ?? StandardTimeoutWaitAndRetryPolicy.Value;
            return await policyToUse.ExecuteAsync(async cancellationToken =>
            {
                var taskCompletionSource = new TaskCompletionSource<T>();

                var dataSize = Marshal.SizeOf(typeof(T));

                WireBuffer requestBuffer = null;
                requestBuffer = new WireBuffer(dataSize);
                requestBuffer.Write(value);

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
                    cancellationToken: cancellationToken
                );

                SendRequest(request);

                return await taskCompletionSource.Task.ConfigureAwait(false);
            }, parentCancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> RequestInvoke(
            ushort endpointID,
            CancellationToken parentCancellationToken = default(CancellationToken),
            Policy requestPolicy = null)
        {
            var policyToUse = requestPolicy ?? StandardTimeoutWaitAndRetryPolicy.Value;
            return await policyToUse.ExecuteAsync(async cancellationToken =>
            {
                var taskCompletionSource = new TaskCompletionSource<bool>();

                WireBuffer requestBuffer = null;
                requestBuffer = new WireBuffer(0);

                var request = new Request(
                    endpointID: endpointID,
                    expectedResponseSize: 0,
                    requestACK: true,
                    populateBody: () => requestBuffer,
                    responseCallback: (req, res) =>
                    {
                        taskCompletionSource.SetResult(true);
                    },
                    signature: SchemaChecksum ?? Config.USB_PROTOCOL_VERSION,
                    cancellationToken: cancellationToken
                );

                SendRequest(request);

                return await taskCompletionSource.Task.ConfigureAwait(false);
            }, parentCancellationToken).ConfigureAwait(false);
        }

        public async Task<byte[]> RequestBuffer(
            CancellationToken parentCancellationToken = default(CancellationToken),
            Policy requestPolicy = null)
        {
            var policyToUse = requestPolicy ?? StandardTimeoutWaitAndRetryPolicy.Value;
            return await policyToUse.ExecuteAsync(async cancellationToken =>
            {
                var taskCompletionSource = new TaskCompletionSource<byte[]>();
                byte[] cumulativeResponse = new byte[0];
                uint totalBytesReceived = 0;

                while (true)
                {
                    var data = await RequestBufferSegment(totalBytesReceived, parentCancellationToken: cancellationToken, requestPolicy: requestPolicy).ConfigureAwait(false);
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

                return await taskCompletionSource.Task.ConfigureAwait(false);
            }, parentCancellationToken).ConfigureAwait(false);
        }

        public async Task<byte[]> RequestBufferSegment(
            uint payloadOffset = 0,
            CancellationToken parentCancellationToken = default(CancellationToken),
            Policy requestPolicy = null)
        {
            var policyToUse = requestPolicy ?? StandardTimeoutWaitAndRetryPolicy.Value;
            return await policyToUse.ExecuteAsync(async cancellationToken =>
            {
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
                    cancellationToken: cancellationToken
                );

                SendRequest(request);

                return await taskCompletionSource.Task.ConfigureAwait(false);
            }, parentCancellationToken).ConfigureAwait(false);
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
