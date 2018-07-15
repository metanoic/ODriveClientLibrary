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
                var result = FetchEndpointSync<bool>(110);
                this.RaiseAndSetIfChanged(ref enableControlAtStart, result);
                return enableControlAtStart;
            }

            set
            {
                SetPropertySync<bool>(110, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableControlAtStart, value);
            }
        }

        private bool doCalibrationAtStart;
        public bool DoCalibrationAtStart
        {
            get
            {
                var result = FetchEndpointSync<bool>(111);
                this.RaiseAndSetIfChanged(ref doCalibrationAtStart, result);
                return doCalibrationAtStart;
            }

            set
            {
                SetPropertySync<bool>(111, value);
                ODriveDevice.RaiseAndSetIfChanged(ref doCalibrationAtStart, value);
            }
        }
    }
}