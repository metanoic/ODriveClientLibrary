namespace ODrive
{
    using ReactiveUI;

    public abstract class RemoteObject : ReactiveObject
    {
        protected Device ODriveDevice { get; set; }

        public RemoteObject(Device device)
        {
            this.ODriveDevice = device;
        }

        public RemoteObject()
        {
        }
    }
}
