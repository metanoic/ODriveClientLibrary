namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorCurrentControl : RemoteObject
    {
        public MotorCurrentControl(Device device): base(device)
        {
        }

        private float pGain;
        public float PGain
        {
            get
            {
                var result = device.FetchEndpointSync<float>(59);
                this.RaiseAndSetIfChanged(ref pGain, result);
                return pGain;
            }

            set
            {
                device.FetchEndpointSync<float>(59, value);
                this.RaiseAndSetIfChanged(ref pGain, value);
            }
        }

        private float iGain;
        public float IGain
        {
            get
            {
                var result = device.FetchEndpointSync<float>(60);
                this.RaiseAndSetIfChanged(ref iGain, result);
                return iGain;
            }

            set
            {
                device.FetchEndpointSync<float>(60, value);
                this.RaiseAndSetIfChanged(ref iGain, value);
            }
        }

        private float vCurrentControlIntegralD;
        public float VCurrentControlIntegralD
        {
            get
            {
                var result = device.FetchEndpointSync<float>(61);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, result);
                return vCurrentControlIntegralD;
            }

            set
            {
                device.FetchEndpointSync<float>(61, value);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, value);
            }
        }

        private float vCurrentControlIntegralQ;
        public float VCurrentControlIntegralQ
        {
            get
            {
                var result = device.FetchEndpointSync<float>(62);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, result);
                return vCurrentControlIntegralQ;
            }

            set
            {
                device.FetchEndpointSync<float>(62, value);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, value);
            }
        }

        private float ibus;
        public float Ibus
        {
            get
            {
                var result = device.FetchEndpointSync<float>(63);
                this.RaiseAndSetIfChanged(ref ibus, result);
                return ibus;
            }

            set
            {
                device.FetchEndpointSync<float>(63, value);
                this.RaiseAndSetIfChanged(ref ibus, value);
            }
        }

        private float finalVAlpha;
        public float FinalVAlpha
        {
            get
            {
                var result = device.FetchEndpointSync<float>(64);
                this.RaiseAndSetIfChanged(ref finalVAlpha, result);
                return finalVAlpha;
            }

            set
            {
                device.FetchEndpointSync<float>(64, value);
                this.RaiseAndSetIfChanged(ref finalVAlpha, value);
            }
        }

        private float finalVBeta;
        public float FinalVBeta
        {
            get
            {
                var result = device.FetchEndpointSync<float>(65);
                this.RaiseAndSetIfChanged(ref finalVBeta, result);
                return finalVBeta;
            }

            set
            {
                device.FetchEndpointSync<float>(65, value);
                this.RaiseAndSetIfChanged(ref finalVBeta, value);
            }
        }

        private float iqSetpoint;
        public float IqSetpoint
        {
            get
            {
                var result = device.FetchEndpointSync<float>(66);
                this.RaiseAndSetIfChanged(ref iqSetpoint, result);
                return iqSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(66, value);
                this.RaiseAndSetIfChanged(ref iqSetpoint, value);
            }
        }

        private float iqMeasured;
        public float IqMeasured
        {
            get
            {
                var result = device.FetchEndpointSync<float>(67);
                this.RaiseAndSetIfChanged(ref iqMeasured, result);
                return iqMeasured;
            }

            set
            {
                device.FetchEndpointSync<float>(67, value);
                this.RaiseAndSetIfChanged(ref iqMeasured, value);
            }
        }

        private float maxAllowedCurrent;
        public float MaxAllowedCurrent
        {
            get
            {
                var result = device.FetchEndpointSync<float>(68);
                this.RaiseAndSetIfChanged(ref maxAllowedCurrent, result);
                return maxAllowedCurrent;
            }

            set
            {
                device.FetchEndpointSync<float>(68, value);
                this.RaiseAndSetIfChanged(ref maxAllowedCurrent, value);
            }
        }
    }
}