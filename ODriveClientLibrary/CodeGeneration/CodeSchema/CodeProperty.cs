namespace ODrive.CodeGeneration.CodeSchema
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive.CodeGeneration.DeviceSchema;

    internal class CodeProperty
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
            codeProperty.Type = typeof(ushort).Name;
            codeProperty.CanSet = deviceProperty.Access.HasFlag(AccessMode.CanWrite);

            return codeProperty;
        }

        public static CodeProperty CreateFrom(DeviceObject deviceObject)
        {
            var codeProperty = new CodeProperty();

            codeProperty.Name = Helpers.ToPascalCase(deviceObject.Name);
            codeProperty.Type = CodeClass.GetObjectClassName(deviceObject.Parent);

            return codeProperty;
        }

        public IEnumerable<MemberDeclarationSyntax> GenerateScalar()
        {
            var fieldDeclaration = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName(Type),
                SyntaxFactory.SeparatedList(new[] {
                    SyntaxFactory.VariableDeclarator(
                        SyntaxFactory.Identifier(
                            Helpers.ToCamelCase(Name)
                        )
                    )
                })
            )).AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PrivateKeyword
            ));

            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(Type), Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration, SyntaxFactory.Block(
                        SyntaxFactory.List(new[] {
                            SyntaxFactory.ParseStatement(
                                $"var result = FetchEndpointSync<{Type}>({EndpointID});"
                            ),
                            SyntaxFactory.ParseStatement(
                                $"this.RaiseAndSetIfChanged(ref {Helpers.ToCamelCase(Name)}, result);"
                            ),
                            SyntaxFactory.ParseStatement(
                                $"return {Helpers.ToCamelCase(Name)};"
                            )
                        })
                    ))
                );

            if (CanSet)
            {
                propertyDeclaration = propertyDeclaration.AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration, SyntaxFactory.Block(
                        SyntaxFactory.List(new[] {
                            SyntaxFactory.ParseStatement(
                                $"FetchEndpointSync<{Type}>({EndpointID}, value);"
                            ),
                            SyntaxFactory.ParseStatement(
                                $"this.RaiseAndSetIfChanged(ref {Helpers.ToCamelCase(Name)}, value);"
                            )
                        })
                    )).AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
                );
            }

            return new List<MemberDeclarationSyntax>() {
                fieldDeclaration,
                propertyDeclaration
            };
        }
    }
}
