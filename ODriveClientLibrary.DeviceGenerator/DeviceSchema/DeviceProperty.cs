namespace ODrive.DeviceGenerator.DeviceSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json.Linq;

    public class DeviceProperty : IDeviceMember
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        public AccessMode Access { get; private set; }

        public static readonly Lazy<Dictionary<string, DataType>> StringToDataTypeMap =
            new Lazy<Dictionary<string, DataType>>(() =>
            {
                return Enum.GetValues(typeof(DataType))
                    .Cast<DataType>()
                    .ToDictionary(key => Helpers.GetAttribute<DevicePropertyType>(key).DeviceTypeString, val => val);
            });


        public static DeviceProperty CreateFrom(JObject inputNode)
        {
            var deviceProperty = new DeviceProperty();

            deviceProperty.ID = inputNode.Value<int>("id");
            deviceProperty.Name = inputNode.Value<string>("name");
            deviceProperty.Type = StringToDataTypeMap.Value[inputNode.Value<string>("type")];

            string accessString = inputNode.Value<string>("access");
            deviceProperty.Access = AccessMode.None;

            if (accessString.Contains("r"))
            {
                deviceProperty.Access |= AccessMode.CanRead;
            }

            if (accessString.Contains("rw"))
            {
                deviceProperty.Access |= AccessMode.CanWrite;
            }

            return deviceProperty;
        }

    }
}
