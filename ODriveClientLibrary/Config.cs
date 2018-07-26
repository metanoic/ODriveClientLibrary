namespace ODrive
{
    using System.Collections.Generic;

    internal struct ODriveVendorProductPair
    {
        public int VendorID;
        public int ProductID;
    }

    internal static class Config
    {
        public const byte USB_MAX_PACKET_SIZE = 128;

        public const int USB_PROTOCOL_VERSION = 1;

        public const int USB_READ_TIMEOUT = 10000;
        public const int USB_WRITE_TIMEOUT = 10000;

        public static IReadOnlyList<int> USB_READ_ENDPOINT_ADDRESSES { get; private set; } = new List<int>() { 0x83, 0x81 };
        public static IReadOnlyList<int> USB_WRITE_ENDPOINT_ADDRESSES { get; private set; } = new List<int>() { 0x03, 0x01 };

        // See https://github.com/madcowswe/ODrive/blob/b6aca99d6f7fe033a5554ed847eb8331a69ea235/docs/interfaces.md#usb
        // Ordered by preference.  Firmware 3.4 and prior only expose 0x81 and 0x01.
        internal static IEnumerable<ODriveVendorProductPair> KnownVendorProductPairs = new List<ODriveVendorProductPair>()
        {
            new ODriveVendorProductPair() { VendorID = 0x1209, ProductID = 0x0D31 },
            new ODriveVendorProductPair() { VendorID = 0x1209, ProductID = 0x0D32 },
            new ODriveVendorProductPair() { VendorID = 0x1209, ProductID = 0x0D33 }
        };

        public const ushort CANONICAL_CRC8_POLYNOMIAL = 0x37;
        public const ushort CANONICAL_CRC8_INIT = 0x42;

        public const ushort CANONICAL_CRC16_POLYNOMIAL = 0x3d65;

        // This is used for stream format.  Packet signature uses protocol version as initial value
        public const ushort CANONICAL_CRC16_INIT = 0x1337;

        public const byte CANONICAL_PREFIX = 0xAA;
    }
}
