namespace ODrive
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using ODrive.Exceptions;
    using ODrive.Schema;
    using ODrive.Utilities;

    public partial class Device : IDisposable
    {
        private readonly BasicDeviceInfo deviceInfo;

        private UsbDevice usbDevice;
        private Connection deviceConnection;
        private ManualResetEventSlim readyEvent = new ManualResetEventSlim();

        public DeviceStatus Status { get; private set; } = DeviceStatus.Unknown;

        public ushort? SchemaChecksum { get; private set; }

        private Func<BasicDeviceInfo, bool> DeviceIdentifyingPredicate { get; set; }

        public Device(BasicDeviceInfo deviceInfo, ushort? schemaChecksum = null)
        {
            Status = DeviceStatus.Initializing;

            this.deviceInfo = deviceInfo;
            usbDevice = deviceInfo.Device;

            SchemaChecksum = schemaChecksum;
        }

        public async Task<T> GetProperty<T>(IReadablePropertyMember<T> readablePropertyMember)
        {
            return await readablePropertyMember.GetProperty(this);
        }

        public async Task SetProperty<T>(IWriteablePropertyMember<T> writeablePropertyMember, T newValue)
        {
            await writeablePropertyMember.SetProperty(this, newValue);
        }

        public T GetExecutionDelegate<T>(IExecutableMember<T> executableMember)
        {
            return executableMember.GetExecutor(this);
        }

        public async Task<bool> Connect(bool skipChecksumValidation = false)
        {
            AssertNotDisposed();

            if (readyEvent.IsSet)
            {
                readyEvent.Reset();
            }

            skipChecksumValidation = skipChecksumValidation || SchemaChecksum.HasValue == false;

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
                    throw new InvalidChecksumException($"The checksum provided ({SchemaChecksum.Value.ToString("X2")}) does not match the device's checksum.");
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

        public async Task<string> DownloadSchema(CancellationToken cancellationToken = default(CancellationToken), bool setSchemaChecksum = true)
        {
            AssertNotDisposed();

            byte[] schemaBytes = await deviceConnection.RequestBuffer(cancellationToken).ConfigureAwait(false);

            if (setSchemaChecksum)
            {
                var schemaChecksum = SchemaChecksumCalculator.CalculateChecksum(schemaBytes);
                SchemaChecksum = schemaChecksum;
                deviceConnection.SchemaChecksum = schemaChecksum;
            }

            var schemaJson = System.Text.Encoding.UTF8.GetString(schemaBytes, 0, schemaBytes.Length);

            return schemaJson;
        }

        public async Task InvokeEndpoint(ushort endpointID, CancellationToken cancellationToken = default(CancellationToken))
        {
            AssertNotDisposed();

            await deviceConnection.RequestInvoke(endpointID, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> RequestValue<T>(ushort endpointID, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();

            return await deviceConnection.RequestResponse<T>(endpointID, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> PushValue<T>(ushort endpointID, T newValue, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();

            return await deviceConnection.RequestResponse(endpointID, newValue, cancellationToken).ConfigureAwait(false);
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
