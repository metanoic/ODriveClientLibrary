namespace ODrive.CodeGeneration.DeviceSchema
{
    using System;

    [Flags]
    internal enum AccessMode
    {
        None = 0x0,
        CanRead = 0x1,
        CanWrite = 0x2
    }

    internal enum DataType
    {
        [DevicePropertyType("json", typeof(object))]
        JSON,
        [DevicePropertyType("float", typeof(float))]
        Float,
        [DevicePropertyType("bool", typeof(bool))]
        Bool,
        [DevicePropertyType("int8", typeof(sbyte))]
        Int8,
        [DevicePropertyType("uint8", typeof(byte))]
        UInt8,
        [DevicePropertyType("int16", typeof(short))]
        Int16,
        [DevicePropertyType("uint16", typeof(ushort))]
        UInt16,
        [DevicePropertyType("int32", typeof(int))]
        Int32,
        [DevicePropertyType("uint32", typeof(uint))]
        UInt32,
        [DevicePropertyType("int64", typeof(long))]
        Int64,
        [DevicePropertyType("uint64", typeof(ulong))]
        UInt64,
        [DevicePropertyType("function", typeof(DeviceFunction))]
        Function,
        [DevicePropertyType("object", typeof(DeviceObject))]
        Object,
    }
}
