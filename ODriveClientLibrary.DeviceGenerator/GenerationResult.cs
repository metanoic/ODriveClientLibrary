namespace ODrive.DeviceGenerator
{
    using System.Collections.Generic;
    using ODrive.DeviceGenerator.CodeSchema;

    public class GenerationResult
    {
        public List<CodeClass> CodeClasses { get; private set; }
        public SchemaArchiveEntry ArchiveEntry { get; private set; }

        public GenerationResult(List<CodeClass> codeClasses, SchemaArchiveEntry archiveEntry)
        {
            CodeClasses = codeClasses;
            ArchiveEntry = archiveEntry;
        }
    }
}
