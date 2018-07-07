namespace ODrive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal struct ODriveVendorProductPair
    {
        public int VendorID;
        public int ProductID;
    }

    internal static class Config
    {
        public const byte USB_MAX_PACKET_SIZE = 128;

        public const int USB_PROTOCOL_VERSION = 1;

        public const int USB_READ_TIMEOUT = 1000;
        public const int USB_WRITE_TIMEOUT = 1000;

        public static IReadOnlyList<int> USB_READ_ENDPOINT_ADDRESSES { get; private set; } = new List<int>() { 0x83, 0x81 };
        public static IReadOnlyList<int> USB_WRITE_ENDPOINT_ADDRESSES { get; private set; } = new List<int>() { 0x03, 0x01 };

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
