namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceAxis0 : RemoteObject
    {
        public DeviceAxis0(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new Axis0Config(ODriveDevice);
        }

        public Axis0Config Config
        {
            get;
            private set;
        }
    }
}