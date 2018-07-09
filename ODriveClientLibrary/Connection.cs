namespace ODrive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using LibUsbDotNet.Info;
    using LibUsbDotNet.Main;
    using ODrive.Utilities;

    public class Connection
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
        public ushort JsonCRC { get; set; } = Config.USB_PROTOCOL_VERSION;

        private CRC<ushort> CRC16 = new CRC<ushort>(16, Config.CANONICAL_CRC16_POLYNOMIAL, Config.USB_PROTOCOL_VERSION);

        private string endpointJSON = string.Empty;
        public string EndpointJSON
        {
            get => endpointJSON;

            set
            {
                endpointJSON = value;

                var jsonBytes = System.Text.Encoding.ASCII.GetBytes(endpointJSON);
                if (jsonBytes.Any())
                {
                    JsonCRC = CRC16.CalculateAsNumeric(jsonBytes);
                }
            }
        }

        public bool Connect()
        {
            if (usbDevice.IsOpen == true)
            {
                return false;
            }

            var openResult = usbDevice.Open();

            if (!openResult)
            {
                throw new Exception("Failed to open device.");
            }

            return openResult;
        }

        public bool Disconnect()
        {
            if (usbDevice.IsOpen == false)
            {
                return false;
            }

            var closeResult = usbDevice.Close();

            if (!closeResult)
            {
                throw new Exception("Failed to close device");
            }

            return closeResult;
        }

        public Connection(UsbDevice usbDevice)
        {
            this.usbDevice = usbDevice;

            Connect();

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

            endpointReader.ReadThreadPriority = System.Threading.ThreadPriority.AboveNormal;
            endpointReader.DataReceivedEnabled = true;

            endpointReader.DataReceived += EndpointReader_DataReceived;

            endpointWriter = usbDevice.OpenEndpointWriter(
                (WriteEndpointID)writeEndpointInfo.Descriptor.EndpointID,
                (EndpointType)(writeEndpointInfo.Descriptor.Attributes & 0x03));
        }

        public async Task<byte[]> FetchEndpointBuffer()
        {
            byte[] cumulativeResponse = new byte[0];
            uint totalBytesReceived = 0;

            while (true)
            {
                var data = await FetchEndpointBuffer(totalBytesReceived);
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

            return cumulativeResponse;
        }

        public async Task<T> FetchEndpointScalar<T>(ushort endpointID, T? newValue = null) where T : struct
        {
            var tcs = new TaskCompletionSource<T>();

            var dataSize = Marshal.SizeOf(typeof(T));

            WireBuffer requestBuffer = null;

            if (newValue.HasValue)
            {
                requestBuffer = new WireBuffer(dataSize);
                requestBuffer.Write(newValue.Value);
            }

            SendRequest(new Request(
                endpointID: endpointID,
                expectedResponseSize: 32,
                requestACK: true,
                populateBody: () => requestBuffer,
                responseCallback: (req, res) =>
                {
                    var responseData = res.Body.Read<T>();
                    tcs.SetResult(responseData);
                },
                signature: JsonCRC
            ));

            return await tcs.Task;
        }

        private static byte[] ConcatByteArrays(byte[] arrayA, byte[] arrayB)
        {
            byte[] outputBytes = new byte[arrayA.Length + arrayB.Length];
            Buffer.BlockCopy(arrayA, 0, outputBytes, 0, arrayA.Length);
            Buffer.BlockCopy(arrayB, 0, outputBytes, arrayA.Length, arrayB.Length);
            return outputBytes;
        }

        // TODO: Retry
        // TODO: Timeout
        // TODO: Cancel
        // TODO: Disconnect() Reconnect()?
        private async Task<byte[]> FetchEndpointBuffer(uint payloadOffset = 0)
        {
            var tcs = new TaskCompletionSource<byte[]>();

            byte[] cumulativeResponse = new byte[0];

            SendRequest(new Request(
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
            ));

            return await tcs.Task;
        }

        private int SendRequest(Request request)
        {
            if (request.Body.Data.Length + 2 >= Config.USB_MAX_PACKET_SIZE)
            {
                throw new NotSupportedException("Packets larger than 127 bytes are not currently supported.");
            }

            if (request.EndpointID != 0 && request.Signature == Config.USB_PROTOCOL_VERSION)
            {
                throw new ArgumentException("Packet CRC must be provided if requesting endpoints other than 0.");
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
                request.ResponseCallback?.Invoke(request, response);
                pendingRequests.Remove(sequenceNumber);
                queuedResponses.Remove(sequenceNumber);
            }
        }
    }
}
