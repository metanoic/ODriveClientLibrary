namespace ODrive.DeviceGenerator.DeviceSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public class DeviceFunction : IDeviceMember
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        public IEnumerable<DeviceProperty> Arguments { get; private set; }
        public IEnumerable<DeviceProperty> Inputs { get; private set; }
        public IEnumerable<DeviceProperty> Outputs { get; private set; }

        public static DeviceFunction CreateFrom(int id, string name, JArray argumentNodes, JArray inputNodes, JArray outputNodes)
        {
            var deviceFunction = new DeviceFunction();

            deviceFunction.ID = id;
            deviceFunction.Name = name;
            deviceFunction.Type = DataType.Function;

            var arguments = new List<DeviceProperty>();
            var inputs = new List<DeviceProperty>();
            var outputs = new List<DeviceProperty>();

            if (argumentNodes != null)
            {
                foreach (dynamic node in argumentNodes)
                {
                    arguments.Add(DeviceProperty.CreateFrom(node));
                }
            }

            if (inputNodes != null)
            {
                foreach (dynamic node in inputNodes)
                {
                    inputs.Add(DeviceProperty.CreateFrom(node));
                }
            }

            if (outputNodes != null)
            {
                foreach (dynamic node in outputNodes)
                {
                    outputs.Add(DeviceProperty.CreateFrom(node));
                }
            }

            deviceFunction.Arguments = arguments;
            deviceFunction.Inputs = inputs;
            deviceFunction.Outputs = outputs;

            return deviceFunction;
        }

        public static DeviceFunction CreateFrom(JObject inputNode)
        {
            int id = inputNode.Value<int>("id");
            string name = inputNode.Value<string>("name");

            var argumentNodes = inputNode.Value<JArray>("arguments");
            var inputNodes = inputNode.Value<JArray>("inputs");
            var outputNodes = inputNode.Value<JArray>("outputs");

            return CreateFrom(id, name, argumentNodes, inputNodes, outputNodes);
        }
    }
}
