namespace DeviceMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Windows.Forms;
    using ODriveClientLibrary;

    public class Program
    {
        public static void Main(string[] args)
        {
            var deviceMonitor = DeviceMonitor.Instance;

            PrintDeviceInfos(deviceMonitor.AvailableDevices.ToList());

            deviceMonitor.AvailableDevices.CountChanged.Subscribe(evt =>
            {
                PrintDeviceInfos(deviceMonitor.AvailableDevices.ToList());
            });

            deviceMonitor.AvailableDevices.ItemChanged.Subscribe(evt =>
            {
                PrintDeviceInfos(deviceMonitor.AvailableDevices.ToList());
            });

            while (!Console.KeyAvailable)
            {
                Application.DoEvents();
            }
        }

        private static void PrintDeviceInfos(List<BasicDeviceInfo> deviceInfos)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            deviceInfos.ForEach(deviceInfo =>
            {
                Console.WriteLine(deviceInfo.ToString());
                Console.WriteLine();
            });
            Console.Write("[Press any key to exit]");
        }
    }
}
