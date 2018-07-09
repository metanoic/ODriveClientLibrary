namespace ODrive
{
    using System;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using ODrive.Utilities;

    public partial class Device : RemoteObject
    {
        public const string TARGET_FIRMWARE_VERSION = "4.2.2";

        private readonly BasicDeviceInfo deviceInfo;
        private UsbDevice usbDevice;
        private Connection deviceConnection;

        private readonly Func<BasicDeviceInfo, bool> deviceIdentifyingPredicate;

        public Device(BasicDeviceInfo deviceInfo) : this()
        {
            this.deviceInfo = deviceInfo;
            usbDevice = deviceInfo.Device;

            //Play nice with generated partial
            device = this;

            // Open usb read and write endpoints
            deviceConnection = new Connection(usbDevice);

            // If we get disconnected, we'll use this to find the device later
            // so we can reconnect to it.
            deviceIdentifyingPredicate = PredicateBuilder.True<BasicDeviceInfo>()
                    .And(inputDeviceInfo => inputDeviceInfo.VendorID == deviceInfo.VendorID)
                    .And(inputDeviceInfo => inputDeviceInfo.ProductID == deviceInfo.ProductID)
                    .And(inputDeviceInfo => inputDeviceInfo.SerialNumber == deviceInfo.SerialNumber)
                    .Compile();

            deviceConnection.JsonCRC = 9455;
            //deviceConnection.EndpointJSON = FetchSchemaSync();
            //var x = 1;
        }

        public async Task<string> FetchSchema()
        {
            byte[] schemaBytes = await deviceConnection.FetchEndpointBuffer();
            string schema = System.Text.Encoding.UTF8.GetString(schemaBytes, 0, schemaBytes.Length);
            return schema;
        }

        public string FetchSchemaSync()
        {
            return Task.Run(async () => await FetchSchema()).Result;
        }

        internal T FetchEndpointSync<T>(ushort endpointID, T? newValue = null) where T : struct
        {
            return Task.Run(async () => await deviceConnection.FetchEndpointScalar(endpointID, newValue)).Result;
        }
    }
}
