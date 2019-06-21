namespace ODriveClientLibrary.DeviceGenerator.CodeSchema
{
    using ODriveClientLibrary.DeviceGenerator.DeviceSchema;

    public class CodeProperty
    {
        public int? EndpointID { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool CanSet { get; private set; }

        public static CodeProperty CreateFrom(DeviceProperty deviceProperty)
        {
            var codeProperty = new CodeProperty
            {
                EndpointID = deviceProperty.ID,
                Name = Helpers.ToPascalCase(Helpers.ReplaceIllegals(deviceProperty.Name)),
                Type = Helpers.DataTypeToString(deviceProperty.Type),
                CanSet = deviceProperty.Access.HasFlag(AccessMode.CanWrite)
            };

            return codeProperty;
        }

        public static CodeProperty CreateFrom(DeviceObject deviceObject)
        {
            var codeProperty = new CodeProperty
            {
                Name = Helpers.ToPascalCase(deviceObject.Name),
                Type = CodeClass.GetObjectClassName(deviceObject)
            };

            return codeProperty;
        }
    }
}
