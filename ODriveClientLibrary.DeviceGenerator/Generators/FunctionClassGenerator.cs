namespace ODrive.DeviceGenerator.Generators
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    internal static class FunctionClassGenerator
    {
        public static ClassDeclarationSyntax Generate(CodeSchema.CodeFunction codeFunction)
        {
            string methodOrFunction = string.IsNullOrEmpty(codeFunction.ReturnType) ? "Method" : "Function";
            string className = $"{codeFunction.Name}{methodOrFunction}";

            var classDeclaration = ClassDeclaration(className)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword))
                .AddBaseListTypes(SimpleBaseType(ParseTypeName($"IExecutableMember<{className}.ExecutionDelegate>")))
                .AddMembers(GenerateDelegateMember(codeFunction))
                .AddMembers(GenerateGetExecutorMember(codeFunction));
            
            // var currentCode = ((SyntaxNode)classDeclaration).NormalizeWhitespace().ToFullString();

            return classDeclaration;
        }

        private static List<string> GetParameterList(IEnumerable<CodeSchema.CodeArgument> arguments)
        {
            var result = new List<string>();

            foreach (var argument in arguments)
            {
                result.Add($"{argument.Type} {argument.Name}");
            }

            return result;
        }

        private static MemberDeclarationSyntax GenerateDelegateMember(CodeSchema.CodeFunction codeFunction)
        {
            var returnType = string.IsNullOrEmpty(codeFunction.ReturnType) ? "Task" : $"Task<{codeFunction.ReturnType}>";
            var parameters = string.Join(",", GetParameterList(codeFunction.Arguments));
            string template = $@"public delegate {returnType} ExecutionDelegate({parameters});";

            return ((CompilationUnitSyntax)ParseSyntaxTree(template).GetRoot()).Members[0] as MemberDeclarationSyntax;
        }

        private static MethodDeclarationSyntax GenerateGetExecutorMember(CodeSchema.CodeFunction codeFunction)
        {
            List<string> argumentFetches = new List<string>();

            foreach (var argument in codeFunction.Arguments)
            {
                argumentFetches.Add($"await oDrive.FetchEndpoint<{argument.Type}>({argument.EndpointID}, {argument.Name});");
            }

            var returnType = string.IsNullOrEmpty(codeFunction.ReturnType) ? "Task" : $"Task<{codeFunction.ReturnType}>";
            bool hasReturn = !string.IsNullOrEmpty(codeFunction.ReturnType);
            var parameters = string.Join(",", GetParameterList(codeFunction.Arguments));

            // TODO: Figure out why this template is putting the last semicolon on a new line...
            // TODO: Could parallel the fetches if there are multiple arguments
            string template = $@"
                public ExecutionDelegate GetExecutor(Device oDrive) {{
                    return async ({parameters}) => 
                    {{
                        {string.Join(Environment.NewLine, argumentFetches)}
                        {(hasReturn ? "return " : "")} await oDrive.FetchEndpoint{(hasReturn ? $"<{codeFunction.ReturnType}>" : "")}({codeFunction.EndpointID});
                    }};
                }}
            ";

            return ((CompilationUnitSyntax)ParseSyntaxTree(template).GetRoot()).Members[0] as MethodDeclarationSyntax;
        }
    }
}
