namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class EncoderConfig : RemoteObject
    {
        public EncoderConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private bool useIndex;
        public bool UseIndex
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(66);
                this.RaiseAndSetIfChanged(ref useIndex, result);
                return useIndex;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(66, value);
                ODriveDevice.RaiseAndSetIfChanged(ref useIndex, value);
            }
        }

        private bool manuallyCalibrated;
        public bool ManuallyCalibrated
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(67);
                this.RaiseAndSetIfChanged(ref manuallyCalibrated, result);
                return manuallyCalibrated;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(67, value);
                ODriveDevice.RaiseAndSetIfChanged(ref manuallyCalibrated, value);
            }
        }

        private float idxSearchSpeed;
        public float IdxSearchSpeed
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(68);
                this.RaiseAndSetIfChanged(ref idxSearchSpeed, result);
                return idxSearchSpeed;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(68, value);
                ODriveDevice.RaiseAndSetIfChanged(ref idxSearchSpeed, value);
            }
        }

        private int cpr;
        public int Cpr
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(69);
                this.RaiseAndSetIfChanged(ref cpr, result);
                return cpr;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(69, value);
                ODriveDevice.RaiseAndSetIfChanged(ref cpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(70);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(70, value);
                ODriveDevice.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private int motorDir;
        public int MotorDir
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(71);
                this.RaiseAndSetIfChanged(ref motorDir, result);
                return motorDir;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(71, value);
                ODriveDevice.RaiseAndSetIfChanged(ref motorDir, value);
            }
        }
    }
}