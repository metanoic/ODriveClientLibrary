namespace ODrive
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using ODrive.Exceptions;
    using ODrive.Utilities;

    public partial class Device : RemoteObject, IDisposable
    {
        private const int RETRY_DELAY_MS = 1 * 1000;
        private const int RETRY_ATTEMPTS = 5;

        private readonly BasicDeviceInfo deviceInfo;

        private UsbDevice usbDevice;
        private Connection deviceConnection;
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

            bool connectSuccessful = false;

            try
            {
                connectSuccessful = deviceConnection.Connect();
            }
            catch
            {
                try { deviceConnection.Disconnect(); } catch { }
                try { Disconnect(); } catch { }
                throw;
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

            bool disconnectSuccessful = deviceConnection.Disconnect();

            if (disconnectSuccessful)
            {
                Status = DeviceStatus.Disconnected;
            }

            return disconnectSuccessful;
        }

        public async Task<string> FetchSchema(
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null,
            int retryAttempts = RETRY_ATTEMPTS,
            TimeSpan? retryDelayOverride = null)
        {
            AssertNotDisposed();

            string schemaJson = string.Empty;

            try
            {
                byte[] schemaBytes = await RetryHelper.ExecuteWithRetry(() =>
                {
                    return deviceConnection.FetchEndpointBuffer(cancellationToken, timeoutOverride);
                }, retryAttempts, retryDelayOverride ?? TimeSpan.FromMilliseconds(RETRY_DELAY_MS));

                schemaJson = System.Text.Encoding.UTF8.GetString(schemaBytes, 0, schemaBytes.Length);
            }
            catch
            {
                throw;
            }

            return schemaJson;
        }

        // TODO: Remove.  If consumer wants synchronous, they can implement themselves.
        public string FetchSchemaSync(
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null,
            int retryAttempts = RETRY_ATTEMPTS,
            TimeSpan? retryDelayOverride = null)
        {
            AssertNotDisposed();
            return Task.Run(async () => await FetchSchema(cancellationToken)).Result;
        }

        public async Task<T> FetchEndpoint<T>(
            ushort endpointID,
            T? newValue = null,
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null,
            int retryAttempts = RETRY_ATTEMPTS,
            TimeSpan? retryDelayOverride = null) where T : struct
        {
            AssertNotDisposed();

            T result = default(T);

            try
            {
                result = await RetryHelper.ExecuteWithRetry(() =>
                 {
                     return deviceConnection.FetchEndpointScalar(endpointID, newValue, cancellationToken, timeoutOverride);
                 }, retryAttempts, retryDelayOverride ?? TimeSpan.FromMilliseconds(RETRY_DELAY_MS));
            }
            catch
            {
                throw;
            }

            return result;
        }

        // TODO: Remove.  If consumer wants synchronous, they can implement themselves.
        public T FetchEndpointSync<T>(
            ushort endpointID,
            T? newValue = null,
            CancellationToken cancellationToken = default(CancellationToken),
            TimeSpan? timeoutOverride = null,
            int retryAttempts = RETRY_ATTEMPTS,
            TimeSpan? retryDelayOverride = null) where T : struct
        {
            AssertNotDisposed();
            return Task.Run(async () => await FetchEndpoint(endpointID, newValue, cancellationToken, timeoutOverride, retryAttempts, retryDelayOverride)).Result;
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
