namespace ODrive.CodeGeneration.DeviceSchema
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    internal class DevicePropertyType : Attribute
    {
        public string DeviceTypeString { get; set; }
        public Type SystemType { get; set; }

        public DevicePropertyType(string deviceTypeString, Type systemType)
        {
            DeviceTypeString = deviceTypeString;
            SystemType = systemType;
        }
    }
}
