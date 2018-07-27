namespace ODriveClientLibrary.Exceptions
{
    using System;

    public class RequestTimeoutException : Exception
    {
        public RequestTimeoutException(string message) : base(message)
        {
        }

        public RequestTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
