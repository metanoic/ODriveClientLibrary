using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODrive.Exceptions
{
    public class UsbLibraryException : Exception
    {
        public UsbLibraryException(string message) : base(message)
        {
        }
    }
}
