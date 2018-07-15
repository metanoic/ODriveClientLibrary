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

    class Program
    {
        static void Main(string[] args)
        {
            MyAsyncFunc();
        }

        static async Task MyAsyncFunc()
        {
            var deviceMonitor = DeviceMonitor.Instance;
            var foundDevice = deviceMonitor.AvailableDevices.FirstOrDefault();

            if (foundDevice == null)
            {
                throw new Exception("Could not find any suitable devices to connect to");
            }

            using (var oDrive = new Device(foundDevice))
            {
                try
                {
                    await oDrive.Connect();
                    var z = 1;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debugger.Break();
                }

                Console.WriteLine($"Serial Number: {(oDrive.SerialNumber.ToString("X2"))}");
                Console.WriteLine($"Bus Voltage: {oDrive.VbusVoltage}V");
            }

            while (!Console.KeyAvailable)
            {
                Application.DoEvents();
            }
        }
    }
}
