namespace DeviceMonitor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Forms;
    using ODriveClientLibrary;

    public class Program
    {
        public static void Main(string[] args)
        {
            var deviceMonitor = DeviceMonitor.Instance;

            PrintDeviceInfos(deviceMonitor.AvailableDevices.ToList());


            deviceMonitor.AvailableDevices.CollectionChanged += AvailableDevices_CollectionChanged;

            while (!Console.KeyAvailable)
            {
                Application.DoEvents();
            }
        }

        private static void AvailableDevices_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var availableDevices = sender as ObservableCollection<BasicDeviceInfo>;
            PrintDeviceInfos(availableDevices.ToList());
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
