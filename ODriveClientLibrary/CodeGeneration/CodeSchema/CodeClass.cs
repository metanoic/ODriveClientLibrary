namespace ODrive.CodeGeneration.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.CodeGeneration.DeviceSchema;

    internal class CodeClass
    {
        public string FileName { get; private set; }
        public string Namespace { get; private set; }
        public string ClassName { get; private set; }
        public IReadOnlyList<CodeProperty> ScalarProperties { get; private set; }
        public IReadOnlyList<CodeFunction> Functions { get; private set; }
        public IReadOnlyList<CodeProperty> ObjectProperties { get; private set; }

        public static CodeClass CreateFrom(DeviceObject deviceObject)
        {
            var codeClass = new CodeClass();

            codeClass.ClassName = GetObjectClassName(deviceObject);
            //codeClass.Namespace = GetObjectNamespace(deviceObject, null);
            codeClass.FileName = GetObjectPath(deviceObject.Parent, null) + codeClass.ClassName + ".Generated.cs";
            var deviceProperties = deviceObject.Members.Where(x => x is DeviceProperty || x is DeviceObject).ToList();

            codeClass.Functions = deviceObject.Members.Where(x => x is DeviceFunction).Select(x => CodeFunction.CreateFrom((DeviceFunction)x)).ToList();
            codeClass.ScalarProperties = deviceProperties.Where(x => x.Type != DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceProperty)x)).ToList();
            codeClass.ObjectProperties = deviceProperties.Where(x => x.Type == DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceObject)x)).ToList();

            return codeClass;
        }

        public static string GetObjectClassName(DeviceObject deviceObject)
        {
            return Helpers.ToPascalCase(deviceObject.Parent != null ? deviceObject.Parent.Name : string.Empty)
                + Helpers.ToPascalCase(deviceObject.Name);
        }

        private static string GetObjectPath(DeviceObject deviceObject, string resultSoFar)
        {
            if (deviceObject?.Parent != null)
            {
                return GetObjectPath(deviceObject.Parent, Helpers.ToPascalCase(deviceObject.Name) + @"\" + resultSoFar);
            }
            else
            {
                return resultSoFar;
            }
        }


        private static string GetObjectNamespace(DeviceObject deviceObject, string resultSoFar)
        {
            if (deviceObject?.Parent != null)
            {
                return GetObjectPath(deviceObject.Parent, Helpers.ToPascalCase(deviceObject.Name) + @"." + resultSoFar);
            }
            else
            {
                return resultSoFar;
            }
        }

        public ICompilationUnitSyntax Generate()
        {
            var compilationUnit = SyntaxFactory.CompilationUnit();

            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(Namespace ?? "ODrive"))
                .AddUsings(
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("ReactiveUI"))
                );

            var classDeclaration = SyntaxFactory.ClassDeclaration(ClassName)
                .AddModifiers(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(SyntaxKind.PartialKeyword)
                ).AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("RemoteObject"))
                ).AddMembers(
                    SyntaxFactory.ConstructorDeclaration(
                        SyntaxFactory.Identifier(ClassName))
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        SyntaxFactory.ParameterList(
                            SyntaxFactory.SingletonSeparatedList<ParameterSyntax>(
                                SyntaxFactory.Parameter(
                                    SyntaxFactory.Identifier("connection"))
                                .WithType(
                                    SyntaxFactory.IdentifierName("Connection")))))
                    .WithInitializer(
                        SyntaxFactory.ConstructorInitializer(
                            SyntaxKind.BaseConstructorInitializer,
                            SyntaxFactory.ArgumentList(
                                SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                    SyntaxFactory.Argument(
                                        SyntaxFactory.IdentifierName("connection"))))))
                    .WithBody(SyntaxFactory.Block())
                );

            foreach (var scalarProperty in ScalarProperties)
            {
                foreach (var scalarMember in scalarProperty.GenerateScalar())
                {
                    classDeclaration = classDeclaration.AddMembers(scalarMember);
                }
            }

            foreach (var function in Functions)
            {
                foreach (var functionMember in function.Generate())
                {
                    classDeclaration = classDeclaration.AddMembers(functionMember);
                }
            }

            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);
            compilationUnit = compilationUnit.AddMembers(namespaceDeclaration);

            var code = compilationUnit.NormalizeWhitespace().ToFullString();

            return compilationUnit;
        }
    }
}
