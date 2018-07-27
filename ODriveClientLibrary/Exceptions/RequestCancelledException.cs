namespace ODrive.Exceptions
{
    using System;

    public class RequestCancelledException : Exception
    {
        public RequestCancelledException(string message) : base(message)
        {
        }

        public RequestCancelledException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
