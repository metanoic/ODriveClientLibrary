namespace ODrive.DeviceGenerator.DeviceSchema
{
    using System;
    using System.ComponentModel;

    [Flags]
    public enum AccessMode
    {
        None = 0x0,
        CanRead = 0x1,
        CanWrite = 0x2
    }

    public enum DataType
    {
        [Description("string")]
        [DevicePropertyType("json", typeof(string))]
        JSON,
        [Description("float")]
        [DevicePropertyType("float", typeof(float))]
        Float,
        [Description("bool")]
        [DevicePropertyType("bool", typeof(bool))]
        Bool,
        [Description("sbyte")]
        [DevicePropertyType("int8", typeof(sbyte))]
        Int8,
        [Description("byte")]
        [DevicePropertyType("uint8", typeof(byte))]
        UInt8,
        [Description("short")]
        [DevicePropertyType("Int16", typeof(short))]
        Int16,
        [Description("ushort")]
        [DevicePropertyType("uint16", typeof(ushort))]
        UInt16,
        [Description("int")]
        [DevicePropertyType("int32", typeof(int))]
        Int32,
        [Description("uint")]
        [DevicePropertyType("uint32", typeof(uint))]
        UInt32,
        [Description("long")]
        [DevicePropertyType("int64", typeof(long))]
        Int64,
        [Description("ulong")]
        [DevicePropertyType("uint64", typeof(ulong))]
        UInt64,
        [Description("DeviceFunction")]
        [DevicePropertyType("function", typeof(DeviceFunction))]
        Function,
        [Description("DeviceObject")]
        [DevicePropertyType("object", typeof(DeviceObject))]
        Object,
    }
}
