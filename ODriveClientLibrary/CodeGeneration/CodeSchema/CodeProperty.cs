namespace ODrive.CodeGeneration.CodeSchema
{
    using System;
    using ODrive.CodeGeneration.DeviceSchema;

    internal class CodeProperty
    {
        public int? EndpointID { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool CanSet { get; private set; }

        public static CodeProperty CreateFrom(DeviceProperty deviceProperty)
        {
            var codeProperty = new CodeProperty();

            codeProperty.EndpointID = deviceProperty.ID;
            codeProperty.Name = Helpers.ToPascalCase(deviceProperty.Name);
            codeProperty.Type = typeof(String).Name;
            codeProperty.CanSet = deviceProperty.Access.HasFlag(AccessMode.CanWrite);

            return codeProperty;
        }

        public static CodeProperty CreateFrom(DeviceObject deviceObject)
        {
            var codeProperty = new CodeProperty();

            codeProperty.Name = Helpers.ToPascalCase(deviceObject.Name);
            codeProperty.Type = CodeClass.GetObjectClassName(deviceObject.Parent);

            return codeProperty;
        }
    }
}
