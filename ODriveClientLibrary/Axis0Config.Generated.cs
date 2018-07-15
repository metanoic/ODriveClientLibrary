namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis0Config : RemoteObject
    {
        public Axis0Config(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private bool enableControlAtStart;
        public bool EnableControlAtStart
        {
            get
            {
                var result = FetchEndpointSync<bool>(10);
                this.RaiseAndSetIfChanged(ref enableControlAtStart, result);
                return enableControlAtStart;
            }

            set
            {
                SetPropertySync<bool>(10, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableControlAtStart, value);
            }
        }

        private bool doCalibrationAtStart;
        public bool DoCalibrationAtStart
        {
            get
            {
                var result = FetchEndpointSync<bool>(11);
                this.RaiseAndSetIfChanged(ref doCalibrationAtStart, result);
                return doCalibrationAtStart;
            }

            set
            {
                SetPropertySync<bool>(11, value);
                ODriveDevice.RaiseAndSetIfChanged(ref doCalibrationAtStart, value);
            }
        }
    }
}