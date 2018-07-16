namespace Demo.ReadSetProperties
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ODrive;
    using ODrive.DeviceGenerator;
    using ODrive.Enums;
    using ODrive.Schema;

    class Program
    {
        static void Main(string[] args)
        {
            MyAsyncFunc();
        }

        static async Task MyAsyncFunc()
        {
            //var result = Generator.GenerateFromDevice("385932683037");
            //var xx = 1;
            //Generator.GenerateFromSchemaFile(@"H:\repos\ODriveClientLibrary\ODriveClientLibrary.DeviceGenerator\DeviceSchema\DefinitionArchive\3.5.json");
            var deviceMonitor = DeviceMonitor.Instance;
            var foundDevice = deviceMonitor.AvailableDevices.FirstOrDefault();
            var schema = new DeviceSchema();

            if (foundDevice == null)
            {
                throw new Exception("Could not find any suitable devices to connect to");
            }

            using (var oDrive = new Device(foundDevice, DeviceSchema.SchemaChecksum))
            {
                bool connectSuccess = false;
                try
                {
                    connectSuccess = await oDrive.Connect(true);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debugger.Break();
                }

                //var download = await oDrive.FetchSchema();


                while (!Console.KeyAvailable)
                {
                    //await oDrive.SetProperty(schema.Motor0.Config.CalibrationCurrent, 1);
                    await oDrive.GetExecutionDelegate(schema.SaveConfiguration)();
                    Application.DoEvents();
                }
            }
        }
    }
}
