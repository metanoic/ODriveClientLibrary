namespace ODrive.DeviceGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ODrive.DeviceGenerator.CodeSchema;
    using ODrive.DeviceGenerator.DeviceSchema;

    public static class SchemaParser
    {
        public static void Test()
        {
            var json = File.ReadAllText(@"H:\repos\ODriveClientLibrary\ODriveClientLibrary.DeviceGenerator\DeviceSchema\DefinitionArchive\3.5.json");
            var x = Parse(json);
        }

        public static IEnumerable<CodeClass> Parse(string jsonInput)
        {
            var rootDeviceObject = DeviceSchemaParser.Parse("Device", jsonInput);

            var deviceObjects = rootDeviceObject.Members
                .Flatten(x => x is DeviceObject ? ((DeviceObject)x).Members : null)
                .Where(x => x is DeviceObject)
                .Cast<DeviceObject>()
                .ToList();

            deviceObjects.Insert(0, rootDeviceObject);

            var uniqueObjectKeys = deviceObjects.Select(deviceObject => deviceObject.GetObjectKey()).Distinct().ToList();
            var uniqueDeviceObjects = new List<DeviceObject>();

            foreach (var objectKey in uniqueObjectKeys)
            {
                uniqueDeviceObjects.Add(deviceObjects.Find(x => x.GetObjectKey() == objectKey));
            }

            var codeClasses = uniqueDeviceObjects.Select(deviceObject => CodeClass.CreateFrom(deviceObject)).ToList();

            codeClasses.ForEach(codeClass => codeClass.Generate());

            return codeClasses;
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
