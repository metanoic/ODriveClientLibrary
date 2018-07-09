namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class EncoderConfig : RemoteObject
    {
        public EncoderConfig(Device device): base(device)
        {
        }

        private byte mode;
        public byte Mode
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(122);
                this.RaiseAndSetIfChanged(ref mode, result);
                return mode;
            }

            private set
            {
                device.FetchEndpointSync<byte>(122, value);
                this.RaiseAndSetIfChanged(ref mode, value);
            }
        }

        private bool useIndex;
        public bool UseIndex
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(123);
                this.RaiseAndSetIfChanged(ref useIndex, result);
                return useIndex;
            }

            private set
            {
                device.FetchEndpointSync<bool>(123, value);
                this.RaiseAndSetIfChanged(ref useIndex, value);
            }
        }

        private bool preCalibrated;
        public bool PreCalibrated
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(124);
                this.RaiseAndSetIfChanged(ref preCalibrated, result);
                return preCalibrated;
            }

            private set
            {
                device.FetchEndpointSync<bool>(124, value);
                this.RaiseAndSetIfChanged(ref preCalibrated, value);
            }
        }

        private float idxSearchSpeed;
        public float IdxSearchSpeed
        {
            get
            {
                var result = device.FetchEndpointSync<float>(125);
                this.RaiseAndSetIfChanged(ref idxSearchSpeed, result);
                return idxSearchSpeed;
            }

            private set
            {
                device.FetchEndpointSync<float>(125, value);
                this.RaiseAndSetIfChanged(ref idxSearchSpeed, value);
            }
        }

        private int cpr;
        public int Cpr
        {
            get
            {
                var result = device.FetchEndpointSync<int>(126);
                this.RaiseAndSetIfChanged(ref cpr, result);
                return cpr;
            }

            private set
            {
                device.FetchEndpointSync<int>(126, value);
                this.RaiseAndSetIfChanged(ref cpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = device.FetchEndpointSync<int>(127);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            private set
            {
                device.FetchEndpointSync<int>(127, value);
                this.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private float offsetFloat;
        public float OffsetFloat
        {
            get
            {
                var result = device.FetchEndpointSync<float>(128);
                this.RaiseAndSetIfChanged(ref offsetFloat, result);
                return offsetFloat;
            }

            private set
            {
                device.FetchEndpointSync<float>(128, value);
                this.RaiseAndSetIfChanged(ref offsetFloat, value);
            }
        }

        private float calibRange;
        public float CalibRange
        {
            get
            {
                var result = device.FetchEndpointSync<float>(129);
                this.RaiseAndSetIfChanged(ref calibRange, result);
                return calibRange;
            }

            private set
            {
                device.FetchEndpointSync<float>(129, value);
                this.RaiseAndSetIfChanged(ref calibRange, value);
            }
        }
    }
}