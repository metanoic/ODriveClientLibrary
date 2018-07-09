namespace ODrive
{
    using ReactiveUI;

    public abstract class RemoteObject : ReactiveObject
    {
        protected Device device { get; set; }

        public RemoteObject(Device device)
        {
            this.device = device;
        }

        public RemoteObject()
        {
        }
    }
}
