namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor1CurrentControl : RemoteObject
    {
        public Motor1CurrentControl(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new CurrentControlConfig(ODriveDevice);
        }

        public CurrentControlConfig Config
        {
            get;
            private set;
        }

        private float pGain;
        public float PGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(149);
                this.RaiseAndSetIfChanged(ref pGain, result);
                return pGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(149, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pGain, value);
            }
        }

        private float iGain;
        public float IGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(150);
                this.RaiseAndSetIfChanged(ref iGain, result);
                return iGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(150, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iGain, value);
            }
        }

        private float vCurrentControlIntegralD;
        public float VCurrentControlIntegralD
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(151);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, result);
                return vCurrentControlIntegralD;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(151, value);
                ODriveDevice.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, value);
            }
        }

        private float vCurrentControlIntegralQ;
        public float VCurrentControlIntegralQ
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(152);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, result);
                return vCurrentControlIntegralQ;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(152, value);
                ODriveDevice.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, value);
            }
        }

        private float iqSetpoint;
        public float IqSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(153);
                this.RaiseAndSetIfChanged(ref iqSetpoint, result);
                return iqSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(153, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iqSetpoint, value);
            }
        }

        private float iqMeasured;
        public float IqMeasured
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(154);
                this.RaiseAndSetIfChanged(ref iqMeasured, result);
                return iqMeasured;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(154, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iqMeasured, value);
            }
        }

        private float ibus;
        public float Ibus
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(155);
                this.RaiseAndSetIfChanged(ref ibus, result);
                return ibus;
            }
        }
    }
}