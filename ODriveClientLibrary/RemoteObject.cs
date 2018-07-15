namespace ODrive
{
    using System.Threading.Tasks;
    using ReactiveUI;

    public abstract class RemoteObject : ReactiveObject
    {
        protected Device ODriveDevice { get; set; }

        public RemoteObject(Device device)
        {
            this.ODriveDevice = device;
        }

        internal T FetchEndpointSync<T>(ushort endpointID) where T : struct
        {
            return Task.Run(() => ODriveDevice.FetchEndpoint<T>(endpointID)).Result;
        }

        internal T FetchEndpointSync<T>(ushort endpointID, T? newValue) where T : struct
        {
            return Task.Run(() => ODriveDevice.FetchEndpoint(endpointID, newValue)).Result;
        }

        internal void SetPropertySync<T>(ushort endpointID, T? newValue) where T : struct
        {
            var result = Task.Run(() => ODriveDevice.FetchEndpoint(endpointID, newValue)).Result;
        }

        public RemoteObject()
        {
        }
    }
}
