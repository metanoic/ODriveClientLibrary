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
        public Connection deviceConnection;
        private ManualResetEventSlim readyEvent = new ManualResetEventSlim();

        public DeviceStatus Status { get; private set; } = DeviceStatus.Unknown;

        private ushort? schemaChecksum;
        public ushort SchemaChecksum
        {
            get => schemaChecksum ?? originSchemaChecksum;
            private set => schemaChecksum = value;
        }

        private Func<BasicDeviceInfo, bool> DeviceIdentifyingPredicate { get; set; }

        public Device(BasicDeviceInfo deviceInfo, ushort? schemaChecksum = null) : this()
        {
            Status = DeviceStatus.Initializing;

            this.deviceInfo = deviceInfo;
            usbDevice = deviceInfo.Device;

            if (schemaChecksum.HasValue)
            {
                originSchemaChecksum = schemaChecksum.Value;
            }

            // Play nice with generated partial
            // TODO: Convert to partial method?
            ODriveDevice = this;
        }

        public bool WaitUntilReady(TimeSpan? timeout = null)
        {
            return readyEvent.Wait(timeout ?? Timeout.InfiniteTimeSpan);
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
            deviceConnection = new Connection(usbDevice, SchemaChecksum);

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

            readyEvent.Set();

            ////deviceConnection.ValidateChecksum(OriginSchemaChecksum).ContinueWith(validationTask =>
            ////{
            ////    if (validationTask.Result)
            ////    {
            ////        Status = DeviceStatus.Ready;
            ////        readyEvent.Set();
            ////    }
            ////});

            return connectSuccessful;
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

        public async Task<string> FetchSchema(CancellationToken cancellationToken = default(CancellationToken), TimeSpan? timeoutOverride = null)
        {
            AssertNotDisposed();
            byte[] schemaBytes = await deviceConnection.FetchEndpointBuffer(cancellationToken, timeoutOverride);
            string schemaJson = System.Text.Encoding.UTF8.GetString(schemaBytes, 0, schemaBytes.Length);
            return schemaJson;
        }

        public string FetchSchemaSync(CancellationToken cancellationToken = default(CancellationToken), TimeSpan? timeoutOverride = null)
        {
            AssertNotDisposed();
            return Task.Run(async () => await FetchSchema(cancellationToken)).Result;
        }

        public async Task<T> FetchEndpoint<T>(
            ushort endpointID,
            T? newValue = null,
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null) where T : struct
        {
            AssertNotDisposed();

            T result = default(T);

            try
            {
                result = await deviceConnection.FetchEndpointScalar(endpointID, newValue, cancellationToken, timeoutOverride);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public T FetchEndpointSync<T>(
            ushort endpointID,
            T? newValue = null,
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null
            ) where T : struct
        {
            AssertNotDisposed();
            return Task.Run(async () => await deviceConnection.FetchEndpointScalar(endpointID, newValue, cancellationToken, timeoutOverride)).Result;
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
                    Disconnect();
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
