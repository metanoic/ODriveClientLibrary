namespace ODrive.DeviceGenerator.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using ODrive.DeviceGenerator.DeviceSchema;

    public class CodeClass
    {
        public string FileName { get; private set; }
        public string ClassName { get; private set; }
        public string Source { get; private set; }
        public IReadOnlyList<CodeProperty> ScalarProperties { get; private set; }
        public IReadOnlyList<CodeFunction> Functions { get; private set; }
        public IReadOnlyList<CodeProperty> ObjectProperties { get; private set; }

        public static CodeClass CreateFrom(DeviceObject deviceObject)
        {
            var codeClass = new CodeClass
            {
                ClassName = GetObjectClassName(deviceObject),
                FileName = GetObjectClassName(deviceObject) + ".Generated.cs"
            };

            var deviceProperties = deviceObject.Members.Where(x => x is DeviceProperty || x is DeviceObject).ToList();

            codeClass.Functions = deviceObject.Members.Where(x => x is DeviceFunction).Select(x => CodeFunction.CreateFrom((DeviceFunction)x)).ToList();
            codeClass.ScalarProperties = deviceProperties.Where(x => x.Type != DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceProperty)x)).ToList();
            codeClass.ObjectProperties = deviceProperties.Where(x => x.Type == DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceObject)x)).ToList();

            return codeClass;
        }

        public static string GetObjectClassName(DeviceObject deviceObject)
        {
            var parentName = deviceObject.Parent != null ? deviceObject.Parent.Name : string.Empty;
            var objectName = deviceObject.Name;

            return Helpers.ReplaceIllegals(Helpers.ToPascalCase(parentName) + Helpers.ToPascalCase(objectName));
        }
    }
}
