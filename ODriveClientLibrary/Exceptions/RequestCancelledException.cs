using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODrive.Exceptions
{
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
