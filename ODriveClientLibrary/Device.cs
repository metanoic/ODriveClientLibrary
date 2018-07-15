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

        public async Task<bool> Connect(bool skipChecksumValidation = false)
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

            if (!connectSuccessful)
            {
                throw new UsbLibraryException("Failed to connect to USB Device.");
            }

            if (!skipChecksumValidation)
            {

                bool connectionActive = false;
                try
                {
                    connectionActive = await deviceConnection.TestConnection().ConfigureAwait(false);
                }
                catch { }

                if (!connectionActive)
                {
                    throw new RequestTimeoutException("Connected to USB Device successfully, but communication failed.");
                }

                bool checksumIsValid = false;
                try
                {
                    checksumIsValid = await deviceConnection.ValidateChecksum(SchemaChecksum).ConfigureAwait(false);
                }
                catch (RequestTimeoutException)
                {
                    throw new InvalidChecksumException($"The checksum provided ({SchemaChecksum.ToString("X2")}) does not match the device's checksum.");
                }

                if (checksumIsValid)
                {
                    readyEvent.Set();
                }

                return connectSuccessful && connectionActive && checksumIsValid;
            }
            else
            {
                readyEvent.Set();

                return connectSuccessful;
            }
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

        public async Task<string> FetchSchema(CancellationToken cancellationToken = default(CancellationToken))
        {
            AssertNotDisposed();

            byte[] schemaBytes = await deviceConnection.FetchEndpointBuffer(cancellationToken).ConfigureAwait(false);
            var schemaJson = System.Text.Encoding.UTF8.GetString(schemaBytes, 0, schemaBytes.Length);

            return schemaJson;
        }

        public async Task<T> FetchEndpoint<T>(ushort endpointID, T? newValue = null, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();

            return await deviceConnection.FetchEndpointScalar(endpointID, newValue, cancellationToken).ConfigureAwait(false);
        }

        private void AssertNotDisposed()
        {
            if (Status == DeviceStatus.Disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
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

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
