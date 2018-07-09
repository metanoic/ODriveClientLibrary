namespace ODrive.DeviceGenerator.DeviceSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class DeviceObject : IDeviceMember
    {
        public int? ID { get; private set; }
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        public DeviceObject Parent { get; private set; }
        public IReadOnlyList<IDeviceMember> Members { get; private set; }

        public static DeviceObject CreateFrom(DeviceObject parent, int? id, string name, JArray inputNodes)
        {
            var deviceObject = new DeviceObject();

            deviceObject.Parent = parent;
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

                // Ignore test entries
                if (node.Value<string>("name") == "test_function" || node.Value<string>("name") == "test_property")
                {
                    continue;
                }

                var deviceMember = DeviceSchemaParser.ParseMember(deviceObject, node);
                memberList.Add(deviceMember);
            }

            deviceObject.Members = memberList;

            return deviceObject;
        }

        public static DeviceObject CreateFrom(DeviceObject parent, string name, JObject inputNode)
        {
            int? id = inputNode.Value<int?>("id");
            var members = inputNode.Value<JArray>("members");
            return CreateFrom(parent, id, name, members);
        }

        public static DeviceObject CreateFrom(DeviceObject parent, JObject inputNode)
        {
            string name = inputNode.Value<string>("name");
            return CreateFrom(parent, name, inputNode);
        }

        // Since the schema doesn't tell us the class type, we just uniquely identify the object type by its exposed
        // properties and such...
        public string GetObjectKey()
        {
            string objectKey = string.Empty;

            foreach (var member in Members)
            {
                if (member is DeviceObject)
                {
                    var deviceObject = (DeviceObject)member;
                    objectKey += deviceObject.Name;
                    objectKey += Helpers.DataTypeToString(deviceObject.Type);
                    objectKey += "|";
                }
                else if (member is DeviceProperty)
                {
                    var deviceProperty = (DeviceProperty)member;
                    objectKey += deviceProperty.Name;
                    objectKey += Helpers.DataTypeToString(deviceProperty.Type);
                    objectKey += "|";
                }
                else if (member is DeviceFunction)
                {
                    var deviceFunction = (DeviceFunction)member;
                    objectKey += deviceFunction.Name;
                    objectKey += Helpers.DataTypeToString(deviceFunction.Type);
                    objectKey += "|";
                }
            }

            return objectKey;
        }
    }
}
