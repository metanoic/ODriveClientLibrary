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
        private Connection Connection;

        public RemoteObject(Connection connection)
        {
            Connection = connection;
        }

        protected T FetchEndpointSync<T>(ushort endpointID, T? newValue = null) where T : struct
        {
            return Task.Run(async () => await Connection.FetchEndpointScalar<T>(endpointID, newValue)).Result;
        }
    }
}
