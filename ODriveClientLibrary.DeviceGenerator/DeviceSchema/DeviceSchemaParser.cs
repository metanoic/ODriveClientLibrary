namespace ODrive.DeviceGenerator.DeviceSchema
{
    using Newtonsoft.Json.Linq;

    internal class DeviceSchemaParser
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
