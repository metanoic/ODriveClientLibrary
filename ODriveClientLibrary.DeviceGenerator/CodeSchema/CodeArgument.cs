namespace ODriveClientLibrary.DeviceGenerator.CodeSchema
{
    using ODriveClientLibrary.DeviceGenerator.DeviceSchema;

    public class CodeArgument
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int EndpointID { get; private set; }

        public static CodeArgument CreateFrom(DeviceProperty deviceProperty)
        {
            var codeArgument = new CodeArgument
            {
                Name = Helpers.ToCamelCase(Helpers.ReplaceIllegals(deviceProperty.Name)),
                Type = Helpers.DataTypeToString(deviceProperty.Type),
                EndpointID = deviceProperty.ID
            };

            return codeArgument;
        }
    }
}
