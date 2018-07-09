namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class EncoderConfig : RemoteObject
    {
        public EncoderConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private byte mode;
        public byte Mode
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(122);
                this.RaiseAndSetIfChanged(ref mode, result);
                return mode;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(122, value);
                ODriveDevice.RaiseAndSetIfChanged(ref mode, value);
            }
        }

        private bool useIndex;
        public bool UseIndex
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(123);
                this.RaiseAndSetIfChanged(ref useIndex, result);
                return useIndex;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(123, value);
                ODriveDevice.RaiseAndSetIfChanged(ref useIndex, value);
            }
        }

        private bool preCalibrated;
        public bool PreCalibrated
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(124);
                this.RaiseAndSetIfChanged(ref preCalibrated, result);
                return preCalibrated;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(124, value);
                ODriveDevice.RaiseAndSetIfChanged(ref preCalibrated, value);
            }
        }

        private float idxSearchSpeed;
        public float IdxSearchSpeed
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(125);
                this.RaiseAndSetIfChanged(ref idxSearchSpeed, result);
                return idxSearchSpeed;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(125, value);
                ODriveDevice.RaiseAndSetIfChanged(ref idxSearchSpeed, value);
            }
        }

        private int cpr;
        public int Cpr
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(126);
                this.RaiseAndSetIfChanged(ref cpr, result);
                return cpr;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(126, value);
                ODriveDevice.RaiseAndSetIfChanged(ref cpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(127);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(127, value);
                ODriveDevice.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private float offsetFloat;
        public float OffsetFloat
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(128);
                this.RaiseAndSetIfChanged(ref offsetFloat, result);
                return offsetFloat;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(128, value);
                ODriveDevice.RaiseAndSetIfChanged(ref offsetFloat, value);
            }
        }

        private float calibRange;
        public float CalibRange
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(129);
                this.RaiseAndSetIfChanged(ref calibRange, result);
                return calibRange;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(129, value);
                ODriveDevice.RaiseAndSetIfChanged(ref calibRange, value);
            }
        }
    }
}