namespace Demo.ReadSetProperties
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ODriveClientLibrary.DeviceSchema;
    using ODriveClientLibrary;

    class Program
    {
        static async Task Main(string[] args)
        {
            await MyAsyncFunc();
        }

        static async Task MyAsyncFunc()
        {
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
                    connectSuccess = await oDrive.Connect();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debugger.Break();
                }

                // var download = await oDrive.DownloadSchema();

                while (!Console.KeyAvailable)
                {
                    var x = await oDrive.GetProperty(schema.Config.Gpio1PwmMapping.Endpoint);
                    Console.WriteLine($"{x.EndpointID}, {x.JsonCRC}");
                    //await oDrive.SetProperty(schema.Motor0.Config.CalibrationCurrent, 1);
                    //await oDrive.GetExecutionDelegate(schema.SaveConfiguration)();
                    Application.DoEvents();
                }
            }
        }
    }
}
