namespace ODrive.DeviceGenerator.CodeSchema
{
    using ODrive.DeviceGenerator.DeviceSchema;

    public class CodeArgument
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int EndpointID { get; private set; }

        public static CodeArgument CreateFrom(DeviceProperty deviceProperty)
        {
            var codeArgument = new CodeArgument();

            codeArgument.Name = Helpers.ToCamelCase(deviceProperty.Name);
            codeArgument.Type = Helpers.DataTypeToString(deviceProperty.Type);
            codeArgument.EndpointID = deviceProperty.ID;

            return codeArgument;
        }
    }
}
