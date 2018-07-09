using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace ODrive
{
    public abstract class RemoteObject : ReactiveObject
    {
        protected Device device;

        public RemoteObject(Device device)
        {
            this.device = device;
        }

        public RemoteObject()
        {

        }
    }
}
