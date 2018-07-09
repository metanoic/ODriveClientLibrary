namespace ODrive.DeviceGenerator.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.DeviceGenerator.DeviceSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public class CodeArgument
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int EndpointID { get; private set; }

        public static CodeArgument CreateFrom(DeviceProperty deviceProperty)
        {
            var codeArgument = new CodeArgument();

            codeArgument.Name = Helpers.ToCamelCase(deviceProperty.Name);
            codeArgument.Type = Helpers.DataTypeToString(deviceProperty.Type);
            codeArgument.EndpointID = deviceProperty.ID;

            return codeArgument;
        }
    }
}
