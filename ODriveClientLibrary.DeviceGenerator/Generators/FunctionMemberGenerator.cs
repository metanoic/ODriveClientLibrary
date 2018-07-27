namespace ODrive.DeviceGenerator.Generators
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.DeviceGenerator.CodeSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    internal static class FunctionMemberGenerator
    {
        public static MemberDeclarationSyntax Generate(CodeFunction codeFunction)
        {
            string methodOrFunction = string.IsNullOrEmpty(codeFunction.ReturnType) ? "Method" : "Function";
            string fieldName = $"{codeFunction.Name}{methodOrFunction}";

            var fieldDeclaration = GenerateFieldMember(fieldName, codeFunction.Name);
            var currentCode = ((SyntaxNode)fieldDeclaration).NormalizeWhitespace().ToFullString();

            return fieldDeclaration;
        }

        private static FieldDeclarationSyntax GenerateFieldMember(string propertyType, string propertyName)
        {
            string template = $@"public {propertyType} {propertyName} = new {propertyType}();";
            return ((CompilationUnitSyntax)ParseSyntaxTree(template).GetRoot()).Members[0] as FieldDeclarationSyntax;
        }
    }
}
