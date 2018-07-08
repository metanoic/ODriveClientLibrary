namespace ODrive.CodeGeneration.DeviceSchema
{
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    internal class DeviceObject : IDeviceMember
    {
        public int? ID { get; private set; }
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        public IEnumerable<IDeviceMember> Members { get; private set; }

        public static DeviceObject CreateFrom(int? id, string name, JArray inputNodes)
        {
            var deviceObject = new DeviceObject();

            deviceObject.ID = id;
            deviceObject.Name = name;
            deviceObject.Type = DataType.Object;

            var memberList = new List<IDeviceMember>();

            foreach (dynamic node in inputNodes)
            {
                // Ignore the endpoint entry
                if (string.IsNullOrEmpty(node.Value<string>("name")) && node.Value<int>("id") == 0)
                {
                    continue;
                }

                var deviceMember = Parser.ParseMember(node);
                memberList.Add(deviceMember);
            }

            deviceObject.Members = memberList;

            return deviceObject;
        }

        public static DeviceObject CreateFrom(string name, JObject inputNode)
        {
            int? id = inputNode.Value<int?>("id");
            var members = inputNode.Value<JArray>("members");
            return CreateFrom(id, name, members);
        }

        public static DeviceObject CreateFrom(JObject inputNode)
        {
            string name = inputNode.Value<string>("name");
            return CreateFrom(name, inputNode);
        }
    }
}
