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

    internal class Connection
    {
        private readonly ThreadSafeCounter sequenceCounter = new ThreadSafeCounter();
        private readonly Dictionary<ushort, Request> pendingRequests = new Dictionary<ushort, Request>();
        private readonly Dictionary<ushort, Response> queuedResponses = new Dictionary<ushort, Response>();

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

        // Determine if the supplied checksum is valid for the device we're connected to.
        // Since the device won't reply to non-zero endpoints if the checksum is incorrect,
        // we first verify it will reply at all by fetching a small bit of endpoint 0 and 
        // then endpoint 1 (which should be the bus voltage endpoint)
        public async Task<bool> ValidateChecksum(ushort checksumValue)
        {
            throw new NotImplementedException();
        }

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

        // TODO: Need propert return type
        public bool Disconnect()
        {
            if (IsConnected == false)
            {
                throw new Exception("Attempted to Disconnect and already disconnected connection");
            }
            endpointReader.DataReceived -= EndpointReader_DataReceived;
            endpointReader = null;
            endpointWriter = null;

            return true;
        }

        public async Task<T> FetchEndpointScalar<T>(ushort endpointID, T? newValue = null, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            //if (endpointID != 0 && SchemaChecksum.HasValue == false)
            //{
            //    throw new Exception("Must set SchemaChecksum to request non-zero endpoints");
            //}
            var tcs = new TaskCompletionSource<T>();

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
                    tcs.SetResult(responseData);
                },
                signature: SchemaChecksum.HasValue ? SchemaChecksum.Value : (ushort)Config.USB_PROTOCOL_VERSION
            );

            cancellationToken.Register(() =>
            {
                request.CancelRequest();
            });

            SendRequest(request);

            return await tcs.Task;
        }

        private static byte[] ConcatByteArrays(byte[] arrayA, byte[] arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }

        public async Task<byte[]> FetchEndpointBuffer(CancellationToken cancellationToken = default(CancellationToken))
        {
            byte[] cumulativeResponse = new byte[0];
            uint totalBytesReceived = 0;

            while (cancellationToken.IsCancellationRequested == false)
            {
                var data = await FetchEndpointBuffer(totalBytesReceived, cancellationToken);
                if (data.Length == 0)
                {
                    break;
                }
                else
                {
                    totalBytesReceived += (uint)data.Length;
                }

                cumulativeResponse = ConcatByteArrays(cumulativeResponse, data);
            }

            // We could be done with the loop 'cause user cancelled
            if (cancellationToken.IsCancellationRequested == false)
            {
                SchemaChecksum = SchemaChecksumCalculator.CalculateChecksum(cumulativeResponse);
            }

            return cumulativeResponse;
        }

        // TODO: Retry
        // TODO: Timeout
        // TODO: Reconnect()?
        // TODO: Use this to verify successful connection prior to attempting a non-zero endpoint fetch with a questionable CRC
        internal async Task<byte[]> FetchEndpointBuffer(
            uint payloadOffset = 0,
            CancellationToken cancellationToken = default(CancellationToken),
            ushort? schemaChecksum = null)
        {
            var tcs = new TaskCompletionSource<byte[]>();

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
                    tcs.SetResult(responseBytes);
                },
                signature: Config.USB_PROTOCOL_VERSION
            );

            cancellationToken.Register(() =>
            {
                request.CancelRequest();
            });

            SendRequest(request);

            return await tcs.Task;
        }

        private void AssertConnected()
        {
            if (endpointReader == null || endpointWriter == null)
            {
                throw new InvalidOperationException("Attempted to read or write while connection is not open");
            }
        }

        private int SendRequest(Request request, CancellationToken cancellationToken = default(CancellationToken))
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

            var requestBytes = request.ToByteArray();
            ErrorCode err = endpointWriter.Write(requestBytes, Config.USB_WRITE_TIMEOUT, out int transferLength);

            if (err != ErrorCode.None)
            {
                throw new Exception(err.ToString());
            }

            pendingRequests.Add(request.SequenceNumber, request);

            return transferLength;
        }

        private void EndpointReader_DataReceived(object sender, EndpointDataEventArgs e)
        {
            var response = new Response(e.Buffer, e.Count);
            var sequenceNumber = response.SequenceNumber;

            pendingRequests.TryGetValue(response.SequenceNumber, out Request request);

            if (!(request is null))
            {
                System.Diagnostics.Debug.WriteLine("Got response for endpoint " + request.EndpointID.ToString());
                if (!request.CancellationRequested)
                {
                    request.ResponseCallback?.Invoke(request, response);
                }
                pendingRequests.Remove(sequenceNumber);
                queuedResponses.Remove(sequenceNumber);
            }
        }
    }
}
