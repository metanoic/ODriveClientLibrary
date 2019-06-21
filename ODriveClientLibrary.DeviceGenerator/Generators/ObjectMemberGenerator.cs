namespace ODriveClientLibrary.DeviceGenerator.Generators
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODriveClientLibrary.DeviceGenerator.CodeSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    internal static class ObjectMemberGenerator
    {
        public static MemberDeclarationSyntax Generate(CodeProperty codeProperty)
        {
            var fieldDeclaration = GenerateFieldMember(codeProperty.Type, codeProperty.Name);
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
