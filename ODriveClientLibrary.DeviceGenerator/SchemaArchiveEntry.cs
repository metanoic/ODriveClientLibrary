namespace ODrive.DeviceGenerator
{
    public class SchemaArchiveEntry
    {
        public string SchemaBase64 { get; set; }
        public ushort SchemaChecksum { get; set; }
        public byte? FirmwareVersionMajor { get; set; }
        public byte? FirmwareVersionMinor { get; set; }
        public byte? FirmwareVersionRevision { get; set; }
        public byte? FirmwareVersionUnreleased { get; set; }
    }
}
