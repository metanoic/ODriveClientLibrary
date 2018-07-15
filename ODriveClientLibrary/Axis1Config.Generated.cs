namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Config : RemoteObject
    {
        public Axis1Config(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private bool enableControlAtStart;
        public bool EnableControlAtStart
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(110);
                this.RaiseAndSetIfChanged(ref enableControlAtStart, result);
                return enableControlAtStart;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(110, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableControlAtStart, value);
            }
        }

        private bool doCalibrationAtStart;
        public bool DoCalibrationAtStart
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(111);
                this.RaiseAndSetIfChanged(ref doCalibrationAtStart, result);
                return doCalibrationAtStart;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(111, value);
                ODriveDevice.RaiseAndSetIfChanged(ref doCalibrationAtStart, value);
            }
        }
    }
}