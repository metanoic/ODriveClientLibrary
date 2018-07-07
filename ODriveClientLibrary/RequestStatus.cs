namespace ODrive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Flags]
    internal enum RequestStatus
    {
        Unknown = 0x0,
        Initialized = 0x1,
        Sent = 0x2,
        Received = 0x4,
        Replied = 0x8,
        Complete = 0x16
    }
}
