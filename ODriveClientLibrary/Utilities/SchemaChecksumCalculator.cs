namespace ODriveClientLibrary.Utilities
{
    using System.Linq;

    public static class SchemaChecksumCalculator
    {
        private static CRC<ushort> schemaChecksumCalculator = new CRC<ushort>(sizeof(ushort) * 8, Config.CANONICAL_CRC16_POLYNOMIAL, Config.USB_PROTOCOL_VERSION);

        public static ushort CalculateChecksum(string jsonInput)
        {
            var jsonBytes = System.Text.Encoding.ASCII.GetBytes(jsonInput);
            return CalculateChecksum(jsonBytes);
        }

        public static ushort CalculateChecksum(byte[] jsonBytes)
        {
            ushort result = jsonBytes.Any() ? schemaChecksumCalculator.CalculateAsNumeric(jsonBytes) : (ushort)0;
            return result;
        }
    }
}
