namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceAxis1 : RemoteObject
    {
        public DeviceAxis1(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new Axis1Config(ODriveDevice);
        }

        public Axis1Config Config
        {
            get;
            private set;
        }
    }
}