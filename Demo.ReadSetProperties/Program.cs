namespace Demo.ReadSetProperties
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using ODrive;
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
                oDrive.Connect();

                var tokenSource = new CancellationTokenSource();
                var result = oDrive.deviceConnection.FetchEndpointBuffer();

                try
                {
                    await result;
                }
                catch (Exception ex)
                {
                    var sadfasd = 1;
                }
                var zz = 1;
                //Console.WriteLine($"Firmware Version: {oDrive.FwVersionMajor}.{oDrive.FwVersionMinor}.{oDrive.FwVersionRevision}.{oDrive.FwVersionUnreleased}");
                //Console.WriteLine($"Hardware Version: {oDrive.HwVersionMajor}.{oDrive.HwVersionMinor}.{oDrive.HwVersionVariant}");
                //Console.WriteLine($"Serial Number: {(oDrive.SerialNumber.ToString("X2"))}");
                //Console.WriteLine($"Bus Voltage: {oDrive.VbusVoltage}V");

                //Console.WriteLine("Press any key to begin calibration...");
                //Console.ReadKey();

                //// Casting to byte is totally hackish.  Will improve when schema is improved or better partials with generated code.
                //oDrive.Axis0.RequestedState = (byte)AxisState.AXIS_STATE_FULL_CALIBRATION_SEQUENCE;

                //while (oDrive.Axis0.CurrentState != (byte)AxisState.AXIS_STATE_IDLE)
                //{
                //    System.Threading.Thread.Sleep(500);
                //    Application.DoEvents();
                //}

                Console.WriteLine("Calibration complete");
            }

            while (!Console.KeyAvailable)
            {
                Application.DoEvents();
            }
        }
    }
}
