namespace ODriveClientLibrary
{
    using LibUsbDotNet;

    /// <summary>
    /// This class represents USB devices in what can be considered lowest-common-denominator fashion.  That is, its primary purpose is to expose
    /// just enough information about the device to allow a consumer to confidently determine whether it is the one they want to connect to.
    /// </summary>
    public class BasicDeviceInfo : PropertyNotifierBase
    {
        /// <summary>
        /// Gets the device's vendor identifier.
        /// </summary>
        /// <value>Unless you're dealing with <see cref="DeviceMonitor.AllDevices" />, then this value will always be one of <see cref="DeviceMonitor.vendorProductPairs"/>.
        public int VendorID { get; private set; }

        /// <summary>
        /// Gets the device's product identifier.
        /// </summary>
        /// <value>Unless you're dealing with <see cref="DeviceMonitor.AllDevices" />, then this value will always be one of <see cref="DeviceMonitor.vendorProductPairs"/>.
        public int ProductID { get; private set; }

        /// <summary>
        /// Gets the device's serial number, if one exists
        /// </summary>
        /// <value>The device's serial number, if one exists</value>
        public string SerialNumber { get; private set; }

        /// <summary>
        /// Gets the manufacturer of the device
        /// </summary>
        /// <value>The device manufacturer's name</value>
        public string Manufacturer { get; private set; }

        /// <summary>
        /// Gets the product name of the device
        /// </summary>
        /// <value>The device's product name.</value>
        public string ProductName { get; private set; }

        /// <summary>
        /// Gets the USB device that was used to populate the instance
        /// </summary>
        /// <value>The USB device.</value>
        public UsbDevice Device { get; private set; }

        private bool isConnected;

        /// <summary>
        /// Gets or sets a value indicating whether the USB device this instance was populated from is currently connected to the computer.
        /// </summary>
        /// <value><c>true</c> if the underlying device is connected to the computer; otherwise, <c>false</c>.</value>
        public bool IsConnected
        {
            get => isConnected;
            set => RaiseAndSetIfChanged(ref isConnected, value);
        }

        private BasicDeviceInfo(int vendorID, int productID, string serialNumber, string manufacturer, string productName, UsbDevice device)
        {
            VendorID = vendorID;
            ProductID = productID;
            SerialNumber = serialNumber;
            Manufacturer = manufacturer;
            ProductName = productName;
            Device = device;
        }

        /// <summary>
        /// Creates an instance of <see cref="BasicDeviceInfo"/> using the <see cref="UsbDevice"/> parameter as a source.
        /// </summary>
        /// <param name="device">The USB device whose values the <see cref="BasicDeviceInfo"/> will be populated with.</param>
        /// <returns><see cref="BasicDeviceInfo"/> instances that represents minimum information about the USB device.</returns>
        public static BasicDeviceInfo CreateFrom(UsbDevice device)
        {
            if (device == null)
            {
                return null;
            }

            return new BasicDeviceInfo(
               device.UsbRegistryInfo.Vid,
               device.UsbRegistryInfo.Pid,
               device.Info.SerialString,
               device.Info.ManufacturerString,
               device.Info.ProductString,
               device
            );
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"VendorID: {VendorID}\n" +
            $"ProductID: {ProductID}\n" +
            $"SerialNumber: {SerialNumber}\n" +
            $"Manufacturer: {Manufacturer}\n" +
            $"ProductName: {ProductName}\n" +
            $"IsConnected: {IsConnected}";
    }
}
