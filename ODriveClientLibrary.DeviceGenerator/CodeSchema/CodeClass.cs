namespace ODrive.DeviceGenerator.CodeSchema
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.DeviceGenerator.DeviceSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public class CodeClass
    {
        public string FileName { get; private set; }
        public string ClassName { get; private set; }
        public string Source { get; private set; }
        public IReadOnlyList<CodeProperty> ScalarProperties { get; private set; }
        public IReadOnlyList<CodeFunction> Functions { get; private set; }
        public IReadOnlyList<CodeProperty> ObjectProperties { get; private set; }

        public static CodeClass CreateFrom(DeviceObject deviceObject)
        {
            var codeClass = new CodeClass();

            codeClass.ClassName = GetObjectClassName(deviceObject);
            codeClass.FileName = GetObjectClassName(deviceObject) + ".Generated.cs";
            var deviceProperties = deviceObject.Members.Where(x => x is DeviceProperty || x is DeviceObject).ToList();

            codeClass.Functions = deviceObject.Members.Where(x => x is DeviceFunction).Select(x => CodeFunction.CreateFrom((DeviceFunction)x)).ToList();
            codeClass.ScalarProperties = deviceProperties.Where(x => x.Type != DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceProperty)x)).ToList();
            codeClass.ObjectProperties = deviceProperties.Where(x => x.Type == DataType.Object).Select(x => CodeProperty.CreateFrom((DeviceObject)x)).ToList();

            return codeClass;
        }

        public static string GetObjectClassName(DeviceObject deviceObject)
        {
            var parentName = deviceObject.Parent != null ? deviceObject.Parent.Name : string.Empty;
            var objectName = deviceObject.Name;

            return Helpers.ToPascalCase(parentName) + Helpers.ToPascalCase(objectName);
        }

        public void Generate()
        {
            var compilationUnit = CompilationUnit();

            var namespaceDeclaration = NamespaceDeclaration(ParseName("ODrive"))
                .AddUsings(
                    UsingDirective(ParseName("System")),
                    UsingDirective(ParseName("ReactiveUI"))
                );

            var constructorStatements = new List<StatementSyntax>();

            foreach (var objectProperty in ObjectProperties)
            {
                if (ClassName == "Device")
                {
                    constructorStatements.Add(ParseStatement($"{objectProperty.Name} = new {objectProperty.Type}(this);"));
                }
                else
                {
                    constructorStatements.Add(ParseStatement($"{objectProperty.Name} = new {objectProperty.Type}(device);"));
                }
            }

            var classConstructor = ConstructorDeclaration(Identifier(ClassName))
                .WithBody(Block(constructorStatements));

            if (ClassName != "Device")
            {
                classConstructor = classConstructor.WithParameterList(
                    ParameterList(SingletonSeparatedList(Parameter(Identifier("device")).WithType(IdentifierName("Device")))))
                .WithInitializer(
                    ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, ArgumentList(SingletonSeparatedList(Argument(IdentifierName("device"))))))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
            }
            else
            {
                classConstructor = classConstructor.WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)));
            }

            var classDeclaration = ClassDeclaration(ClassName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword))
                .AddBaseListTypes(SimpleBaseType(ParseTypeName("RemoteObject")))
                .AddMembers(classConstructor);

            foreach (var objectProperty in ObjectProperties)
            {
                foreach (var objectMember in objectProperty.GenerateObject())
                {
                    classDeclaration = classDeclaration.AddMembers(objectMember);
                }
            }

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

            var source = compilationUnit.NormalizeWhitespace().ToFullString();

            Source = source;
        }
    }
}
