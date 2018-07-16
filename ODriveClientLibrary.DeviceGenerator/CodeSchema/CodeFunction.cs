namespace ODrive.DeviceGenerator.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using ODrive.DeviceGenerator.DeviceSchema;

    public class CodeFunction
    {
        public string EndpointID { get; private set; }
        public string Name { get; private set; }
        public string ReturnType { get; private set; }
        public IReadOnlyList<CodeArgument> Arguments { get; private set; } = new List<CodeArgument>();

        public static CodeFunction CreateFrom(DeviceFunction deviceFunction)
        {
            var codeFunction = new CodeFunction
            {
                Name = Helpers.ToPascalCase(Helpers.ReplaceIllegals(deviceFunction.Name)),
                EndpointID = deviceFunction.ID.ToString()
            };

            var arguments = deviceFunction.Arguments.Concat(deviceFunction.Inputs).ToList();

            codeFunction.Arguments = arguments.Select(argument => CodeArgument.CreateFrom(argument)).ToList();

            if (deviceFunction.Outputs.Any())
            {
                codeFunction.ReturnType = Helpers.DataTypeToString(deviceFunction.Outputs.First().Type);
            }

            return codeFunction;
        }
    }
}
