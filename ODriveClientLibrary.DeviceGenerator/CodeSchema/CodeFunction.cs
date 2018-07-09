namespace ODrive.DeviceGenerator.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.DeviceGenerator.DeviceSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public class CodeFunction
    {
        public string EndpointID { get; private set; }
        public string Name { get; private set; }
        public string ReturnType { get; private set; }
        public IReadOnlyList<CodeArgument> Arguments { get; private set; } = new List<CodeArgument>();

        public static CodeFunction CreateFrom(DeviceFunction deviceFunction)
        {
            var codeFunction = new CodeFunction();

            codeFunction.Name = Helpers.ToPascalCase(deviceFunction.Name);
            codeFunction.EndpointID = deviceFunction.ID.ToString();

            var arguments = deviceFunction.Arguments.Concat(deviceFunction.Inputs).ToList();

            codeFunction.Arguments = arguments.Select(argument => CodeArgument.CreateFrom(argument)).ToList();

            if (deviceFunction.Outputs.Any())
            {
                codeFunction.ReturnType = Helpers.DataTypeToString(deviceFunction.Outputs.First().Type);
            }

            return codeFunction;
        }

        public IEnumerable<MemberDeclarationSyntax> Generate()
        {
            var methodDeclaration = MethodDeclaration(IdentifierName(ReturnType ?? "void"), Name)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));

            var methodStatements = new List<StatementSyntax>();

            foreach (var argument in Arguments)
            {
                methodStatements.Add(ParseStatement(
                    $"FetchEndpointSync<{argument.Type}>({argument.EndpointID}, {Helpers.ToCamelCase(argument.Name)});"
                ));

                methodDeclaration = methodDeclaration.AddParameterListParameters(
                    Parameter(Identifier(Helpers.ToCamelCase(argument.Name))).WithType(ParseTypeName(argument.Type))
                ).WithBody(Block());
            }


            if (ReturnType != null)
            {
                methodStatements.Add(ParseStatement(
                    $"return FetchEndpointSync<{ReturnType}>({EndpointID});"
                ));
            }
            else
            {
                methodStatements.Add(ParseStatement(
                   $"FetchEndpointSync<byte>({EndpointID});"
               ));
            }


            methodDeclaration = methodDeclaration.WithBody(Block(methodStatements));

            return new List<MemberDeclarationSyntax>()
            {
                methodDeclaration
            };
        }
    }
}
