namespace ODrive.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ODrive.CodeGeneration.CodeSchema;
    using ODrive.CodeGeneration.DeviceSchema;

    public static class SchemaParser
    {
        public static void Test()
        {

            var json = File.ReadAllText(@"H:\repos\ODriveClientLibrary\ODriveClientLibrary\CodeGeneration\DeviceSchema\DefinitionArchive\3.5.json");
            var x = Parse(json);

        }

        internal static IEnumerable<CodeClass> Parse(string jsonInput)
        {
            var rootDeviceObject = DeviceSchemaParser.Parse("Device", jsonInput);

            var uniqueDeviceObjects = rootDeviceObject.Members
                .Flatten(x => x is DeviceObject ? ((DeviceObject)x).Members : null)
                .Where(x => x is DeviceObject)
                .Cast<DeviceObject>()
                .ToList();

            uniqueDeviceObjects.Insert(0, rootDeviceObject);

            var codeClasses = uniqueDeviceObjects.Select(deviceObject => CodeClass.CreateFrom(deviceObject)).ToList();
            codeClasses.ForEach(x => x.Generate());
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
