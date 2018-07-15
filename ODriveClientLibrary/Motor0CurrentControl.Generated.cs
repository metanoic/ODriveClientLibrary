namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor0CurrentControl : RemoteObject
    {
        public Motor0CurrentControl(Device ODriveDevice): base(ODriveDevice)
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
                var result = ODriveDevice.FetchEndpointSync<float>(49);
                this.RaiseAndSetIfChanged(ref pGain, result);
                return pGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(49, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pGain, value);
            }
        }

        private float iGain;
        public float IGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(50);
                this.RaiseAndSetIfChanged(ref iGain, result);
                return iGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(50, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iGain, value);
            }
        }

        private float vCurrentControlIntegralD;
        public float VCurrentControlIntegralD
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(51);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, result);
                return vCurrentControlIntegralD;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(51, value);
                ODriveDevice.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, value);
            }
        }

        private float vCurrentControlIntegralQ;
        public float VCurrentControlIntegralQ
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(52);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, result);
                return vCurrentControlIntegralQ;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(52, value);
                ODriveDevice.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, value);
            }
        }

        private float iqSetpoint;
        public float IqSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(53);
                this.RaiseAndSetIfChanged(ref iqSetpoint, result);
                return iqSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(53, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iqSetpoint, value);
            }
        }

        private float iqMeasured;
        public float IqMeasured
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(54);
                this.RaiseAndSetIfChanged(ref iqMeasured, result);
                return iqMeasured;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(54, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iqMeasured, value);
            }
        }

        private float ibus;
        public float Ibus
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(55);
                this.RaiseAndSetIfChanged(ref ibus, result);
                return ibus;
            }
        }
    }
}