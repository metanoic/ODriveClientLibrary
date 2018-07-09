namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorCurrentControl : RemoteObject
    {
        public MotorCurrentControl(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private float pGain;
        public float PGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(59);
                this.RaiseAndSetIfChanged(ref pGain, result);
                return pGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(59, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pGain, value);
            }
        }

        private float iGain;
        public float IGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(60);
                this.RaiseAndSetIfChanged(ref iGain, result);
                return iGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(60, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iGain, value);
            }
        }

        private float vCurrentControlIntegralD;
        public float VCurrentControlIntegralD
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(61);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, result);
                return vCurrentControlIntegralD;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(61, value);
                ODriveDevice.RaiseAndSetIfChanged(ref vCurrentControlIntegralD, value);
            }
        }

        private float vCurrentControlIntegralQ;
        public float VCurrentControlIntegralQ
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(62);
                this.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, result);
                return vCurrentControlIntegralQ;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(62, value);
                ODriveDevice.RaiseAndSetIfChanged(ref vCurrentControlIntegralQ, value);
            }
        }

        private float ibus;
        public float Ibus
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(63);
                this.RaiseAndSetIfChanged(ref ibus, result);
                return ibus;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(63, value);
                ODriveDevice.RaiseAndSetIfChanged(ref ibus, value);
            }
        }

        private float finalVAlpha;
        public float FinalVAlpha
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(64);
                this.RaiseAndSetIfChanged(ref finalVAlpha, result);
                return finalVAlpha;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(64, value);
                ODriveDevice.RaiseAndSetIfChanged(ref finalVAlpha, value);
            }
        }

        private float finalVBeta;
        public float FinalVBeta
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(65);
                this.RaiseAndSetIfChanged(ref finalVBeta, result);
                return finalVBeta;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(65, value);
                ODriveDevice.RaiseAndSetIfChanged(ref finalVBeta, value);
            }
        }

        private float iqSetpoint;
        public float IqSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(66);
                this.RaiseAndSetIfChanged(ref iqSetpoint, result);
                return iqSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(66, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iqSetpoint, value);
            }
        }

        private float iqMeasured;
        public float IqMeasured
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(67);
                this.RaiseAndSetIfChanged(ref iqMeasured, result);
                return iqMeasured;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(67, value);
                ODriveDevice.RaiseAndSetIfChanged(ref iqMeasured, value);
            }
        }

        private float maxAllowedCurrent;
        public float MaxAllowedCurrent
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(68);
                this.RaiseAndSetIfChanged(ref maxAllowedCurrent, result);
                return maxAllowedCurrent;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(68, value);
                ODriveDevice.RaiseAndSetIfChanged(ref maxAllowedCurrent, value);
            }
        }
    }
}