namespace ODrive.CodeGeneration.DeviceSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal static class DeviceSchemaParser
    {
        public static IDeviceMember ParseMember(DeviceObject parentObject, JObject inputNode)
        {
            switch (inputNode.Value<string>("type"))
            {
                case "object":
                    return DeviceObject.CreateFrom(parentObject, inputNode);
                case "function":
                    return DeviceFunction.CreateFrom(inputNode);
                default:
                    return DeviceProperty.CreateFrom(inputNode);
            }
        }

        public static DeviceObject Parse(string rootObjectName, string jsonInput)
        {
            dynamic nodes = JArray.Parse(jsonInput);
            return DeviceObject.CreateFrom(null, null, rootObjectName, nodes);
        }
    }
}
