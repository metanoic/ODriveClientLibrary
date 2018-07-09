﻿namespace ODrive.DeviceGenerator.CodeSchema
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

            parentName = parentName.Replace("axis0", "axis").Replace("axis1", "axis");
            objectName = objectName.Replace("axis0", "axis").Replace("axis1", "axis");

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
                constructorStatements.Add(ParseStatement($"{objectProperty.Name} = new {objectProperty.Type}(connection);"));
            }

            var classConstructor = ConstructorDeclaration(Identifier(ClassName))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(
                    ParameterList(SingletonSeparatedList(Parameter(Identifier("connection")).WithType(IdentifierName("Connection")))))
                .WithInitializer(
                    ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, ArgumentList(SingletonSeparatedList(Argument(IdentifierName("connection"))))))
                .WithBody(Block(constructorStatements));


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