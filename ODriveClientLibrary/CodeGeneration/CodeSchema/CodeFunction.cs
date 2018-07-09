namespace ODrive.CodeGeneration.CodeSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ODrive.CodeGeneration.DeviceSchema;

    internal class CodeFunction
    {
        public string Name { get; private set; }
        public Dictionary<string, DataType> Arguments { get; private set; }

        public static CodeFunction CreateFrom(DeviceFunction deviceFunction)
        {
            var codeFunction = new CodeFunction();

            codeFunction.Name = Helpers.ToPascalCase(deviceFunction.Name);

            var arguments = deviceFunction.Arguments.Concat(deviceFunction.Inputs);
            codeFunction.Arguments = new Dictionary<string, DataType>();

            foreach (var argument in arguments)
            {
                codeFunction.Arguments.Add(argument.Name, argument.Type);
            }

            return codeFunction;
        }
    }
}
