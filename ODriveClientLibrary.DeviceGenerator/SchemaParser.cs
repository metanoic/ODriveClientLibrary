namespace ODrive.DeviceGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ODrive.DeviceGenerator.CodeSchema;
    using ODrive.DeviceGenerator.DeviceSchema;
    using ODriveClientLibrary.DeviceGenerator;

    public static class SchemaParser
    {
        public static ParsedSchema ParseFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return Parse(json);
        }

        public static ParsedSchema Parse(string jsonInput)
        {
            var rootDeviceObject = DeviceSchemaParser.Parse("Device", jsonInput);

            var deviceObjects = rootDeviceObject.Members
                .Flatten(x => x is DeviceObject ? ((DeviceObject)x).Members : null)
                .Where(x => x is DeviceObject)
                .Cast<DeviceObject>()
                .ToList();

            deviceObjects.Insert(0, rootDeviceObject);

            var codeClasses = deviceObjects.Select(deviceObject => CodeClass.CreateFrom(deviceObject)).ToList();

            codeClasses.ForEach(codeClass => codeClass.Generate());

            var parsedSchema = new ParsedSchema(codeClasses, jsonInput);
            return parsedSchema;
        }

        public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        {
            var flattened = source.ToList();

            var children = source.Select(recursion).Where(x => x != null);

            if (children != null)
            {
                foreach (var child in children)
                {
                    flattened.AddRange(child.Flatten(recursion));
                }
            }

            return flattened;
        }
    }
}
