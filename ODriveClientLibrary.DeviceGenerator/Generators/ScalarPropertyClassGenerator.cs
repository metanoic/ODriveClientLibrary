namespace ODriveClientLibrary.DeviceGenerator.Generators
{
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODriveClientLibrary.DeviceGenerator.CodeSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    internal static class ScalarPropertyClassGenerator
    {
        public static ClassDeclarationSyntax Generate(CodeProperty codeProperty)
        {
            string className = $"{codeProperty.Name}Property";

            var classDeclaration = ClassDeclaration(className)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword))
                .AddBaseListTypes(SimpleBaseType(ParseTypeName($"IReadablePropertyMember<{codeProperty.Type}>")))
                .AddMembers(GenerateGetPropertyMember(codeProperty.Type, codeProperty.EndpointID.Value));

            if (codeProperty.CanSet)
            {
                classDeclaration = classDeclaration.AddBaseListTypes(SimpleBaseType(ParseTypeName($"IWriteablePropertyMember<{codeProperty.Type}>")))
                    .AddMembers(GenerateSetPropertyMember(codeProperty.Type, codeProperty.EndpointID.Value));
            }

            return classDeclaration;
        }

        private static MethodDeclarationSyntax GenerateGetPropertyMember(string propertyType, int endpointID)
        {
            string template = $@"
                public async Task<{propertyType}> GetProperty(IDevice oDrive) {{
                    return await oDrive.RequestValue<{propertyType}>({endpointID});
                }}
            ";

            return ((CompilationUnitSyntax)ParseSyntaxTree(template).GetRoot()).Members[0] as MethodDeclarationSyntax;
        }

        private static MethodDeclarationSyntax GenerateSetPropertyMember(string propertyType, int endpointID)
        {
            string template = $@"
                public async Task SetProperty(IDevice oDrive, {propertyType} newValue) {{
                    await oDrive.PushValue<{propertyType}>({endpointID}, newValue);
                }}
            ";

            return ((CompilationUnitSyntax)ParseSyntaxTree(template).GetRoot()).Members[0] as MethodDeclarationSyntax;
        }
    }
}
