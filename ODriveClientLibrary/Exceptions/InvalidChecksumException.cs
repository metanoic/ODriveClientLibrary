namespace ODriveClientLibrary.Exceptions
{
    using System;

    public class InvalidChecksumException : Exception
    {
        public InvalidChecksumException(string message) : base(message)
        {
        }
    }
}
