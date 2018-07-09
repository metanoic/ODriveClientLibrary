namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Encoder : RemoteObject
    {
        public Axis1Encoder(Device device): base(device)
        {
            Config = new EncoderConfig(device);
        }

        public EncoderConfig Config
        {
            get;
            private set;
        }

        private byte error;
        public byte Error
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(210);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                device.FetchEndpointSync<byte>(210, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool isReady;
        public bool IsReady
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(211);
                this.RaiseAndSetIfChanged(ref isReady, result);
                return isReady;
            }
        }

        private bool indexFound;
        public bool IndexFound
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(212);
                this.RaiseAndSetIfChanged(ref indexFound, result);
                return indexFound;
            }
        }

        private int shadowCount;
        public int ShadowCount
        {
            get
            {
                var result = device.FetchEndpointSync<int>(213);
                this.RaiseAndSetIfChanged(ref shadowCount, result);
                return shadowCount;
            }

            set
            {
                device.FetchEndpointSync<int>(213, value);
                this.RaiseAndSetIfChanged(ref shadowCount, value);
            }
        }

        private int countInCpr;
        public int CountInCpr
        {
            get
            {
                var result = device.FetchEndpointSync<int>(214);
                this.RaiseAndSetIfChanged(ref countInCpr, result);
                return countInCpr;
            }

            set
            {
                device.FetchEndpointSync<int>(214, value);
                this.RaiseAndSetIfChanged(ref countInCpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = device.FetchEndpointSync<int>(215);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            set
            {
                device.FetchEndpointSync<int>(215, value);
                this.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private float interpolation;
        public float Interpolation
        {
            get
            {
                var result = device.FetchEndpointSync<float>(216);
                this.RaiseAndSetIfChanged(ref interpolation, result);
                return interpolation;
            }

            set
            {
                device.FetchEndpointSync<float>(216, value);
                this.RaiseAndSetIfChanged(ref interpolation, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = device.FetchEndpointSync<float>(217);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            set
            {
                device.FetchEndpointSync<float>(217, value);
                this.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float posEstimate;
        public float PosEstimate
        {
            get
            {
                var result = device.FetchEndpointSync<float>(218);
                this.RaiseAndSetIfChanged(ref posEstimate, result);
                return posEstimate;
            }

            set
            {
                device.FetchEndpointSync<float>(218, value);
                this.RaiseAndSetIfChanged(ref posEstimate, value);
            }
        }

        private float posCpr;
        public float PosCpr
        {
            get
            {
                var result = device.FetchEndpointSync<float>(219);
                this.RaiseAndSetIfChanged(ref posCpr, result);
                return posCpr;
            }

            set
            {
                device.FetchEndpointSync<float>(219, value);
                this.RaiseAndSetIfChanged(ref posCpr, value);
            }
        }

        private byte hallState;
        public byte HallState
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(220);
                this.RaiseAndSetIfChanged(ref hallState, result);
                return hallState;
            }

            set
            {
                device.FetchEndpointSync<byte>(220, value);
                this.RaiseAndSetIfChanged(ref hallState, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = device.FetchEndpointSync<float>(221);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                device.FetchEndpointSync<float>(221, value);
                this.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = device.FetchEndpointSync<float>(222);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                device.FetchEndpointSync<float>(222, value);
                this.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = device.FetchEndpointSync<float>(223);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                device.FetchEndpointSync<float>(223, value);
                this.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}