namespace ODrive.CodeGeneration.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using ODrive.CodeGeneration.DeviceSchema;

    internal class CodeClass
    {
        public string FileName { get; private set; }
        public string Namespace { get; private set; }
        public string ClassName { get; private set; }
        public IEnumerable<CodeProperty> ScalarProperties { get; private set; }
        public IEnumerable<CodeFunction> Functions { get; private set; }
        public IEnumerable<CodeProperty> ObjectProperties { get; private set; }

        public static CodeClass CreateFrom(DeviceObject deviceObject)
        {
            var codeClass = new CodeClass();

            codeClass.ClassName = GetObjectClassName(deviceObject);
            codeClass.FileName = GetObjectPath(deviceObject.Parent, null) + codeClass.ClassName + ".Generated.cs";
            var deviceProperties = deviceObject.Members.Where(x => x is DeviceProperty || x is DeviceObject).ToList();

            codeClass.Functions = deviceObject.Members.Where(x => x is DeviceFunction).Select(x => CodeFunction.CreateFrom((DeviceFunction)x)).ToList();
            codeClass.ScalarProperties = deviceProperties.Where(x => x.Type != DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceProperty)x)).ToList();
            codeClass.ObjectProperties = deviceProperties.Where(x => x.Type == DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceObject)x)).ToList();

            return codeClass;
        }

        public static string GetObjectClassName(DeviceObject deviceObject)
        {
            return Helpers.ToPascalCase(deviceObject.Parent != null ? deviceObject.Parent.Name : string.Empty)
                + Helpers.ToPascalCase(deviceObject.Name);
        }

        private static string GetObjectPath(DeviceObject deviceObject, string pathSoFar)
        {
            if (deviceObject?.Parent != null)
            {
                return GetObjectPath(deviceObject.Parent, Helpers.ToPascalCase(deviceObject.Name) + @"\" + pathSoFar);
            }
            else
            {
                return pathSoFar;
            }
        }
    }
}
