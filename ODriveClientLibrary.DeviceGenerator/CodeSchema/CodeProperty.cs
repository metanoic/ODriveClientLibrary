﻿namespace ODrive.DeviceGenerator.CodeSchema
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.DeviceGenerator.DeviceSchema;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public class CodeProperty
    {
        public int? EndpointID { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool CanSet { get; private set; }

        public static CodeProperty CreateFrom(DeviceProperty deviceProperty)
        {
            var codeProperty = new CodeProperty();

            codeProperty.EndpointID = deviceProperty.ID;
            codeProperty.Name = Helpers.ToPascalCase(deviceProperty.Name);
            codeProperty.Type = Helpers.DataTypeToString(deviceProperty.Type);
            codeProperty.CanSet = deviceProperty.Access.HasFlag(AccessMode.CanWrite);

            return codeProperty;
        }

        public static CodeProperty CreateFrom(DeviceObject deviceObject)
        {
            var codeProperty = new CodeProperty();

            codeProperty.Name = Helpers.ToPascalCase(deviceObject.Name);
            codeProperty.Type = CodeClass.GetObjectClassName(deviceObject);

            return codeProperty;
        }

        public IEnumerable<MemberDeclarationSyntax> GenerateObject()
        {
            var propertyDeclaration = PropertyDeclaration(ParseTypeName(Type), Name)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
                .AddAccessorListAccessors(AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

            return new List<MemberDeclarationSyntax>()
            {
                propertyDeclaration
            };
        }

        public IEnumerable<MemberDeclarationSyntax> GenerateScalar()
        {
            var fieldDeclaration = FieldDeclaration(
                VariableDeclaration(ParseTypeName(Type),
                SeparatedList(new[] { VariableDeclarator(Identifier(Helpers.ToCamelCase(Name))) })
            )).AddModifiers(Token(SyntaxKind.PrivateKeyword));

            var propertyDeclaration = PropertyDeclaration(ParseTypeName(Type), Name)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.GetAccessorDeclaration, Block(
                        List(new[] {
                            ParseStatement(
                                $"var result = FetchEndpointSync<{Type}>({EndpointID});"
                            ),
                            ParseStatement(
                                $"this.RaiseAndSetIfChanged(ref {Helpers.ToCamelCase(Name)}, result);"
                            ),
                            ParseStatement(
                                $"return {Helpers.ToCamelCase(Name)};"
                            )
                        })
                    ))
                );

            if (CanSet)
            {
                propertyDeclaration = propertyDeclaration.AddAccessorListAccessors(
                    AccessorDeclaration(SyntaxKind.SetAccessorDeclaration, Block(
                        List(new[] {
                            ParseStatement(
                                $"FetchEndpointSync<{Type}>({EndpointID}, value);"
                            ),
                            ParseStatement(
                                $"this.RaiseAndSetIfChanged(ref {Helpers.ToCamelCase(Name)}, value);"
                            )
                        })
                    )).AddModifiers(Token(SyntaxKind.PrivateKeyword))
                );
            }

            return new List<MemberDeclarationSyntax>() {
                fieldDeclaration,
                propertyDeclaration
            };
        }
    }
}