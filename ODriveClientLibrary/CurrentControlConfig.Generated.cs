namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class CurrentControlConfig : RemoteObject
    {
        public CurrentControlConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private float currentLim;
        public float CurrentLim
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(47);
                this.RaiseAndSetIfChanged(ref currentLim, result);
                return currentLim;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(47, value);
                ODriveDevice.RaiseAndSetIfChanged(ref currentLim, value);
            }
        }
    }
}