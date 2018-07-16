namespace ODrive.DeviceGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
        public static GenerationResult GenerateFromString(string schemaJson)
        {
            var schemaBytes = Encoding.ASCII.GetBytes(schemaJson);
            var schemaBase64 = Convert.ToBase64String(schemaBytes);
            var schemaChecksum = SchemaChecksumCalculator.CalculateChecksum(schemaBytes);

            var codeClasses = SchemaParser.Parse(schemaJson);

            var namespaceDeclaration = NamespaceDeclaration(ParseName("ODrive.Schema"))
                .AddUsings(
                    UsingDirective(ParseName("System")),
                    UsingDirective(ParseName("System.Threading.Tasks")),
                    UsingDirective(ParseName("ODrive.Utilities"))
                );

            var alreadyGeneratedClassNames = new List<string>();
            codeClasses.ForEach(codeClass =>
            {
                // Don't generate duplicate classes - e.g. Axis0.Something as well as Axis1.Something
                // The class name is prefixed with the parent object's type, which work for now in 
                // making actual unique types have unique names.
                if (!alreadyGeneratedClassNames.Contains(codeClass.ClassName))
                {
                    var classDeclaration = Generators.CodeClassGenerator.Generate(codeClass, schemaChecksum);
                    namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);
                    alreadyGeneratedClassNames.Add(codeClass.ClassName);
                }
            });

            //var firmwareVersionProperties = codeClasses
            //    .ToList()
            //    .SelectMany(item => item.ScalarProperties)
            //    .Where(item => item.Name.StartsWith("FwVersion"))
            //    .ToList();

            //byte? majorVersion = null;
            //byte? minorVersion = null;
            //byte? revisionVersion = null;
            //byte? unreleasedVersion = null;

            //if (firmwareVersionProperties.Count >= 4)
            //{
            //    var majorEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionMajor").EndpointID.Value;
            //    var minorEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionMinor").EndpointID.Value;
            //    var revisionEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionRevision").EndpointID.Value;
            //    var unreleasedEndpointID = firmwareVersionProperties.Find(item => item.Name == "FwVersionUnreleased").EndpointID.Value;

            //    majorVersion = oDrive.FetchEndpoint<byte>((ushort)majorEndpointID).Result;
            //    minorVersion = oDrive.FetchEndpoint<byte>((ushort)minorEndpointID).Result;
            //    revisionVersion = oDrive.FetchEndpoint<byte>((ushort)revisionEndpointID).Result;
            //    unreleasedVersion = oDrive.FetchEndpoint<byte>((ushort)unreleasedEndpointID).Result;
            //}


            var code = ((SyntaxNode)namespaceDeclaration).NormalizeWhitespace().ToFullString();

            var generationResult = new GenerationResult(code, new SchemaArchiveEntry()
            {
                SchemaBase64 = schemaBase64,
                SchemaChecksum = schemaChecksum
                //FirmwareVersionMajor = majorVersion,
                //FirmwareVersionMinor = minorVersion,
                //FirmwareVersionRevision = revisionVersion,
                //FirmwareVersionUnreleased = unreleasedVersion
            });

            return generationResult;
        }

        public static GenerationResult GenerateFromSchemaFile(string filePath)
        {
            return GenerateFromString(File.ReadAllText(filePath));
        }

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

            using (var oDrive = new Device(foundDeviceInfo, 1))
            {
                var connectResult = oDrive.Connect(true).Result;
                var schemaJson = oDrive.DownloadSchema().Result;
                return GenerateFromString(schemaJson);
            }
        }
    }
}
