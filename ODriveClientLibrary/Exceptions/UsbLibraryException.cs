namespace ODrive.Exceptions
{
    using System;

    public class UsbLibraryException : Exception
    {
        public UsbLibraryException(string message) : base(message)
        {
        }
    }
}
