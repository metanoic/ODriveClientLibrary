﻿namespace ODrive
{
    using System;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using ODrive.Utilities;

    public partial class Device : RemoteObject, IDisposable
    {
        private readonly BasicDeviceInfo deviceInfo;
        private readonly Func<BasicDeviceInfo, bool> deviceIdentifyingPredicate;

        private bool disposedValue = false;
        private UsbDevice usbDevice;
        private Connection deviceConnection;

        // TODO: Assign the json definition CRC value to this property during generation
        // and then check the device's CRC at runtime and error if they don't match.
        public ushort GeneratedForCRC { get; private set; }

        public Device(BasicDeviceInfo deviceInfo) : this()
        {
            this.deviceInfo = deviceInfo;
            usbDevice = deviceInfo.Device;

            // Play nice with generated partial
            ODriveDevice = this;

            // Open usb read and write endpoints
            deviceConnection = new Connection(usbDevice);

            // If we get disconnected, we'll use this to find the device later
            // so we can reconnect to it.
            deviceIdentifyingPredicate = PredicateBuilder.True<BasicDeviceInfo>()
                    .And(inputDeviceInfo => inputDeviceInfo.VendorID == deviceInfo.VendorID)
                    .And(inputDeviceInfo => inputDeviceInfo.ProductID == deviceInfo.ProductID)
                    .And(inputDeviceInfo => inputDeviceInfo.SerialNumber == deviceInfo.SerialNumber)
                    .Compile();

            // You can assign the CRC immediately and bypass the Schema fetch
            // deviceConnection.JsonCRC = 9455;
            deviceConnection.EndpointJSON = FetchSchemaSync();

            ////if (deviceConnection.JsonCRC != GeneratedForCRC)
            ////{
            ////    throw new Exception("Device schema does not match the schema this library was generated against");
            ////}
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

        public async Task<T> FetchEndpoint<T>(ushort endpointID, T? newValue = null) where T : struct
        {
            return await deviceConnection.FetchEndpointScalar(endpointID, newValue);
        }

        public T FetchEndpointSync<T>(ushort endpointID, T? newValue = null) where T : struct
        {
            return Task.Run(async () => await deviceConnection.FetchEndpointScalar(endpointID, newValue)).Result;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    deviceConnection.Disconnect();
                    deviceConnection.EndpointJSON = string.Empty;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
