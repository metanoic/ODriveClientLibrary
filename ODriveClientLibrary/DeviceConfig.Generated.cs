namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceConfig : RemoteObject
    {
        public DeviceConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private float brakeResistance;
        public float BrakeResistance
        {
            get
            {
                var result = FetchEndpointSync<float>(6);
                this.RaiseAndSetIfChanged(ref brakeResistance, result);
                return brakeResistance;
            }

            set
            {
                SetPropertySync<float>(6, value);
                ODriveDevice.RaiseAndSetIfChanged(ref brakeResistance, value);
            }
        }
    }
}