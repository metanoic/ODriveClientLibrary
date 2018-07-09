namespace Demo.DeviceGenerator
{
    using System;
    using System.Windows.Forms;

    public class Program
    {
        public static void Main(string[] args)
        {

            ODrive.DeviceGenerator.SchemaParser.Test();

            while (!Console.KeyAvailable)
            {
                Application.DoEvents();
            }
        }

    }
}
