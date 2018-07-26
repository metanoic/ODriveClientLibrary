namespace ODrive.DeviceGenerator.Generators
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.DeviceGenerator.CodeSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    internal static class CodeClassGenerator
    {
        public static ClassDeclarationSyntax Generate(CodeClass codeClass, ushort schemaChecksum)
        {
            string className = codeClass.ClassName;

            var classDeclaration = ClassDeclaration(className)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword))
                .AddBaseListTypes(SimpleBaseType(ParseTypeName("IDeviceSchema")));

            if (className == "DeviceSchema")
            {
                classDeclaration = classDeclaration.AddMembers(GenerateChecksumField(schemaChecksum));
            }

            // Generate members
            foreach (var codeFunction in codeClass.Functions)
            {
                classDeclaration = classDeclaration.AddMembers(FunctionMemberGenerator.Generate(codeFunction));
            }

            foreach (var codeProperty in codeClass.ObjectProperties)
            {
                classDeclaration = classDeclaration.AddMembers(ObjectMemberGenerator.Generate(codeProperty));
            }

            foreach (var codeProperty in codeClass.ScalarProperties)
            {
                classDeclaration = classDeclaration.AddMembers(ScalarMemberGenerator.Generate(codeProperty));
            }

            // Generate classes
            foreach (var codeFunction in codeClass.Functions)
            {
                classDeclaration = classDeclaration.AddMembers(FunctionClassGenerator.Generate(codeFunction));
            }

            foreach (var codeProperty in codeClass.ScalarProperties)
            {
                classDeclaration = classDeclaration.AddMembers(ScalarPropertyClassGenerator.Generate(codeProperty));
            }

            var currentCode = ((SyntaxNode)classDeclaration).NormalizeWhitespace().ToFullString();

            return classDeclaration;
        }

        private static MemberDeclarationSyntax GenerateChecksumField(ushort checksumValue)
        {
            string template = $@"
                public static ushort SchemaChecksum = {checksumValue};
            ";

            return ((CompilationUnitSyntax)ParseSyntaxTree(template).GetRoot()).Members[0] as MemberDeclarationSyntax;
        }
    }
}
