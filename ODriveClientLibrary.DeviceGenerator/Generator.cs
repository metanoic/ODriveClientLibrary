namespace ODrive.DeviceGenerator
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ODrive;
    using ODrive.Utilities;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public static class Generator
    {
        public static GenerationResult GenerateFromDevice(string serialNumber = null)
        {
            var deviceMonitor = DeviceMonitor.Instance;

            if (!string.IsNullOrEmpty(serialNumber))
            {
                deviceMonitor.DeviceAvailabilityPredicate = PredicateBuilder.True<BasicDeviceInfo>()
                .And(deviceInfo => deviceInfo.SerialNumber == serialNumber)
                .And(deviceInfo => deviceInfo.IsConnected);
            }

            var foundDeviceInfo = deviceMonitor.AvailableDevices.FirstOrDefault();

            if (foundDeviceInfo == null)
            {
                throw new Exception($"Could not find any ODrive boards with SerialNumber: {serialNumber}");
            }

            using (var oDrive = new Device(foundDeviceInfo))
            {
                oDrive.Connect();

                var schemaJson = oDrive.FetchSchemaSync();

                var codeClasses = SchemaParser.Parse(schemaJson);

                codeClasses.ForEach(codeClass => codeClass.Generate());

                var firmwareVersionProperties = codeClasses
                    .ToList()
                    .SelectMany(item => item.ScalarProperties)
                    .Where(item => item.Name.StartsWith("FwVersion"))
                    .ToList();

                byte? majorVersion = null;
                byte? minorVersion = null;
                byte? revisionVersion = null;
                byte? unreleasedVersion = null;

                if (firmwareVersionProperties.Count >= 4)
                {
                    var majorEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionMajor").EndpointID.Value;
                    var minorEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionMinor").EndpointID.Value;
                    var revisionEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionRevision").EndpointID.Value;
                    var unreleasedEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionUnreleased").EndpointID.Value;

                    majorVersion = oDrive.FetchEndpointSync<byte>((ushort)majorEndpointID);
                    minorVersion = oDrive.FetchEndpointSync<byte>((ushort)minorEndpointID);
                    revisionVersion = oDrive.FetchEndpointSync<byte>((ushort)revisionEndpointID);
                    unreleasedVersion = oDrive.FetchEndpointSync<byte>((ushort)unreleasedEndpointID);
                }

                var schemaBytes = Encoding.ASCII.GetBytes(schemaJson);
                var schemaBase64 = Convert.ToBase64String(schemaBytes);
                var schemaChecksum = Utilities.SchemaChecksumCalculator.CalculateChecksum(schemaBytes);

                // Regenerate the device class to include the schema checksum as a private property (we expose
                // it via a public get accessor in the partial).
                codeClasses.Find(x => x.ClassName == "Device").Generate(compilationUnit =>
                {
                    var namespaceDeclaration = (NamespaceDeclarationSyntax)compilationUnit.Members.FirstOrDefault(x => x.IsKind(SyntaxKind.NamespaceDeclaration));
                    var classDeclaration = (ClassDeclarationSyntax)namespaceDeclaration.Members.FirstOrDefault(x => x.IsKind(SyntaxKind.ClassDeclaration));
                    var replacementClassDeclaration = classDeclaration
                        .AddMembers(
                            FieldDeclaration(
                                VariableDeclaration(
                                    ParseTypeName("ushort")
                                ).AddVariables(
                                    VariableDeclarator("originSchemaChecksum")
                                    .WithInitializer(
                                        EqualsValueClause(ParseExpression(schemaChecksum.ToString()))))));
                    var replacementNamespaceDeclaration = namespaceDeclaration.ReplaceNode(classDeclaration, replacementClassDeclaration);
                    return compilationUnit.ReplaceNode(namespaceDeclaration, replacementNamespaceDeclaration);
                });

                var generationResult = new GenerationResult(codeClasses, new SchemaArchiveEntry()
                {
                    SchemaBase64 = schemaBase64,
                    SchemaChecksum = schemaChecksum,
                    FirmwareVersionMajor = majorVersion,
                    FirmwareVersionMinor = minorVersion,
                    FirmwareVersionRevision = revisionVersion,
                    FirmwareVersionUnreleased = unreleasedVersion
                });

                return generationResult;
            }
        }
    }
}
