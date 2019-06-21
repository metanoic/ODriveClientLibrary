namespace ODriveClientLibrary.Exceptions
{
    using System;

    public class NotConnectedException : Exception
    {
        public NotConnectedException(string message) : base(message)
        {
        }
    }
}
