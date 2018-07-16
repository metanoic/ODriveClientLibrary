namespace ODrive.DeviceGenerator
{
    public class GenerationResult
    {
        public string Code { get; private set; }
        public SchemaArchiveEntry ArchiveEntry { get; private set; }

        public GenerationResult(string code, SchemaArchiveEntry archiveEntry)
        {
            Code = code;
            ArchiveEntry = archiveEntry;
        }
    }
}
