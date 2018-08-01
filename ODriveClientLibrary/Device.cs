namespace ODriveClientLibrary
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using LibUsbDotNet;
    using ODriveClientLibrary.Exceptions;
    using ODriveClientLibrary.Utilities;
    using ODriveClientLibrary.Common;
    using System.ComponentModel;
    using LibUsbDotNet.Main;

    public partial class Device : IDevice, IDisposable
    {
        private readonly BasicDeviceInfo deviceInfo;

        private UsbDevice usbDevice;
        private Connection deviceConnection;
        private ManualResetEventSlim readyEvent = new ManualResetEventSlim();
        private bool isDisposed = false;

        public bool IsConnected { get; private set; }

        public ushort? SchemaChecksum { get; private set; }

        private Func<BasicDeviceInfo, bool> DeviceIdentifyingPredicate { get; set; }

        public Device(BasicDeviceInfo deviceInfo, ushort? schemaChecksum = null)
        {
            this.deviceInfo = deviceInfo;
            usbDevice = deviceInfo.Device;

            UsbDevice.UsbErrorEvent += UsbDevice_UsbErrorEvent;

            SchemaChecksum = schemaChecksum;


            // If we get disconnected, we'll use this to find the device later
            // so we can reconnect to it.
            DeviceIdentifyingPredicate = PredicateBuilder.True<BasicDeviceInfo>()
                    .And(inputDeviceInfo => inputDeviceInfo.VendorID == deviceInfo.VendorID)
                    .And(inputDeviceInfo => inputDeviceInfo.ProductID == deviceInfo.ProductID)
                    .And(inputDeviceInfo => inputDeviceInfo.SerialNumber == deviceInfo.SerialNumber)
                    .Compile();

            // Create a listener on DeviceMonitor for this device so we can tell when it gets disconnected


        }

        private void UsbDevice_UsbErrorEvent(object sender, UsbError e)
        {
            if (ReferenceEquals(sender, usbDevice)
                || (sender is UsbEndpointBase
                    && (ReferenceEquals(((UsbEndpointBase)sender).Device, usbDevice))
                    )
                )
            {
                try
                {
                    Disconnect();
                }
                catch { };
            }

            throw new UsbLibraryException($"Error {UsbDevice.LastErrorNumber} occurred in USB library: {UsbDevice.LastErrorString}.");
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

        public async Task<bool> Connect(ushort? schemaChecksum = null)
        {
            AssertNotDisposed();

            if (readyEvent.IsSet)
            {
                readyEvent.Reset();
            }

            SchemaChecksum = schemaChecksum ?? SchemaChecksum;

            var deviceOpenResult = usbDevice.IsOpen ? true : usbDevice.Open();
            if (!deviceOpenResult)
            {
                throw new Exception("Failed to open USBDevice");
            }

            // Open usb read and write endpoints
            deviceConnection = new Connection(usbDevice, SchemaChecksum);

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

            IsConnected = true;

            if (SchemaChecksum.HasValue)
            {
                var retrievedChecksum = await deviceConnection.RequestSchemaChecksum();
                if (SchemaChecksum.Value != retrievedChecksum)
                {
                    try
                    {
                        Disconnect();
                    }
                    catch { };

                    throw new InvalidChecksumException($"Invalid checksum.  Device has {retrievedChecksum} but expected {SchemaChecksum.Value}");
                }
            }

            return true;
        }

        public bool Disconnect()
        {
            AssertNotDisposed();

            bool disconnectSuccessful = deviceConnection.Disconnect();

            if (disconnectSuccessful || deviceConnection.IsConnected == false)
            {
                IsConnected = false;
            }

            return disconnectSuccessful;
        }

        public async Task<string> DownloadSchema(CancellationToken cancellationToken = default(CancellationToken), bool setSchemaChecksum = true)
        {
            AssertNotDisposed();
            AssertConnected();

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
            AssertConnected();

            await deviceConnection.RequestInvoke(endpointID, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> RequestValue<T>(ushort endpointID, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();
            AssertConnected();

            return await deviceConnection.RequestResponse<T>(endpointID, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> PushValue<T>(ushort endpointID, T newValue, CancellationToken cancellationToken = default(CancellationToken)) where T : struct
        {
            AssertNotDisposed();
            AssertConnected();

            return await deviceConnection.RequestResponse(endpointID, newValue, cancellationToken).ConfigureAwait(false);
        }

        private void AssertConnected()
        {
            if (usbDevice.IsOpen == false)
            {
                throw new NotConnectedException("Not connected to device");
            }
        }

        private void AssertNotDisposed()
        {
            if (isDisposed == true)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed == false)
            {
                if (disposing)
                {
                    Disconnect();
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
