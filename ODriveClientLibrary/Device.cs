namespace ODrive
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using ODrive.Utilities;

    public partial class Device : RemoteObject, IDisposable
    {
        private readonly BasicDeviceInfo deviceInfo;

        private UsbDevice usbDevice;
        private Connection deviceConnection;
        private ManualResetEventSlim readyEvent = new ManualResetEventSlim();

        public DeviceStatus Status { get; private set; } = DeviceStatus.Unknown;

        // TODO: Assign the json definition CRC value to this property during generation
        // and then check the device's CRC at runtime and error if they don't match.
        public ushort GeneratedForCRC { get; private set; }

        private Func<BasicDeviceInfo, bool> DeviceIdentifyingPredicate { get; set; }

        public Device(BasicDeviceInfo deviceInfo) : this()
        {
            Status = DeviceStatus.Initializing;

            this.deviceInfo = deviceInfo;
            usbDevice = deviceInfo.Device;

            // Play nice with generated partial
            // TODO: Convert to partial method?
            ODriveDevice = this;
        }

        public bool Disconnect()
        {
            AssertNotDisposed();

            // TODO: Throws on failure - try/catch/finally
            var result = deviceConnection.Disconnect();

            if (result)
            {
                Status = DeviceStatus.Disconnected;
            }

            return result;
        }

        public bool Connect()
        {
            AssertNotDisposed();

            if (readyEvent.IsSet)
            {
                readyEvent.Reset();
            }

            Status = DeviceStatus.Connecting;

            var deviceOpenResult = usbDevice.IsOpen ? true : usbDevice.Open();
            if (!deviceOpenResult)
            {
                throw new Exception("Failed to open USBDevice");
            }

            // Open usb read and write endpoints
            deviceConnection = new Connection(usbDevice);

            // If we get disconnected, we'll use this to find the device later
            // so we can reconnect to it.
            DeviceIdentifyingPredicate = PredicateBuilder.True<BasicDeviceInfo>()
                    .And(inputDeviceInfo => inputDeviceInfo.VendorID == deviceInfo.VendorID)
                    .And(inputDeviceInfo => inputDeviceInfo.ProductID == deviceInfo.ProductID)
                    .And(inputDeviceInfo => inputDeviceInfo.SerialNumber == deviceInfo.SerialNumber)
                    .Compile();

            Status = DeviceStatus.Connecting;

            // TODO: Throws on failure - try/catch/finally
            var connectSuccessful = deviceConnection.Connect();

            if (connectSuccessful)
            {
                Status = DeviceStatus.Ready;
            }

            // You can assign the CRC immediately and bypass the Schema fetch
            // deviceConnection.JsonCRC = 9455;
            FetchSchema().ContinueWith(jsonTask =>
            {
                deviceConnection.EndpointJSON = jsonTask.Result;

                ////if (deviceConnection.JsonCRC != GeneratedForCRC)
                ////{
                ////    throw new Exception("Device schema does not match the schema this library was generated against");
                ////}

                Status = DeviceStatus.Ready;
                readyEvent.Set();
            });

            return connectSuccessful;
        }

        public bool WaitUntilReady(TimeSpan? timeout = null)
        {
            return readyEvent.Wait(timeout ?? Timeout.InfiniteTimeSpan);
        }

        public async Task<string> FetchSchema(CancellationToken cancellationToken = default(CancellationToken))
        {
            AssertNotDisposed();
            byte[] schemaBytes = await deviceConnection.FetchEndpointBuffer(cancellationToken);
            string schema = System.Text.Encoding.UTF8.GetString(schemaBytes, 0, schemaBytes.Length);
            return schema;
        }

        public string FetchSchemaSync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AssertNotDisposed();
            return Task.Run(async () => await FetchSchema(cancellationToken)).Result;
        }

        public async Task<T> FetchEndpoint<T>(ushort endpointID, T? newValue = null, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();
            return await deviceConnection.FetchEndpointScalar(endpointID, newValue, cancellationToken);
        }

        public T FetchEndpointSync<T>(ushort endpointID, T? newValue = null, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();
            return Task.Run(async () => await deviceConnection.FetchEndpointScalar(endpointID, newValue, cancellationToken)).Result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Status != DeviceStatus.Disposed)
            {
                if (disposing)
                {
                    deviceConnection.Disconnect();
                    deviceConnection.EndpointJSON = string.Empty;
                }

                Status = DeviceStatus.Disposed;
            }
        }

        private void AssertNotDisposed()
        {
            if (Status == DeviceStatus.Disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
