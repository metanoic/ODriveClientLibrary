namespace ODrive.CodeGeneration.DeviceSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal static class Parser
    {
        public static IDeviceMember ParseMember(JObject inputNode)
        {
            switch (inputNode.Value<string>("type"))
            {
                case "object":
                    return DeviceObject.CreateFrom(inputNode);
                case "function":
                    return DeviceFunction.CreateFrom(inputNode);
                default:
                    return DeviceProperty.CreateFrom(inputNode);
            }
        }

        public static DeviceObject Parse(string jsonInput)
        {
            dynamic nodes = JArray.Parse(jsonInput);
            return DeviceObject.CreateFrom(null, "Root", nodes);
        }
    }
}
