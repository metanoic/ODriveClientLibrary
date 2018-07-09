namespace ODrive.CodeGeneration.CodeSchema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
    using ODrive.CodeGeneration.DeviceSchema;

    internal class CodeFunction
    {
        public string Name { get; private set; }
        public string ReturnType { get; private set; } = "void";
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

        public IEnumerable<MemberDeclarationSyntax> Generate()
        {
            var methodDeclaration = MethodDeclaration(IdentifierName(ReturnType), Name)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(
                    ParameterList(SingletonSeparatedList(
                            Parameter(Identifier("deviceFunction"))
                            .WithType(IdentifierName("DeviceFunction")))))
                .WithBody(Block());

            return new List<MemberDeclarationSyntax>()
            {
                methodDeclaration
            };
        }
    }
}
