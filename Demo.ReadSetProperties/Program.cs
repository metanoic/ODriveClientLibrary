using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ODrive;

namespace Demo.ReadSetProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            var deviceMonitor = DeviceMonitor.Instance;
            var oDrive = new Device(deviceMonitor.AvailableDevices.First());

            Console.WriteLine(oDrive.SerialNumber.ToString("X2"));

            while (!Console.KeyAvailable)
            {
                Application.DoEvents();
            }
        }
    }
}
