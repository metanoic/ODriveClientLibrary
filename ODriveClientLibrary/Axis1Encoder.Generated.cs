namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Encoder : RemoteObject
    {
        public Axis1Encoder(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new EncoderConfig(ODriveDevice);
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
                var result = ODriveDevice.FetchEndpointSync<byte>(210);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(210, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool isReady;
        public bool IsReady
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(211);
                this.RaiseAndSetIfChanged(ref isReady, result);
                return isReady;
            }
        }

        private bool indexFound;
        public bool IndexFound
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(212);
                this.RaiseAndSetIfChanged(ref indexFound, result);
                return indexFound;
            }
        }

        private int shadowCount;
        public int ShadowCount
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(213);
                this.RaiseAndSetIfChanged(ref shadowCount, result);
                return shadowCount;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(213, value);
                ODriveDevice.RaiseAndSetIfChanged(ref shadowCount, value);
            }
        }

        private int countInCpr;
        public int CountInCpr
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(214);
                this.RaiseAndSetIfChanged(ref countInCpr, result);
                return countInCpr;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(214, value);
                ODriveDevice.RaiseAndSetIfChanged(ref countInCpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(215);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(215, value);
                ODriveDevice.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private float interpolation;
        public float Interpolation
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(216);
                this.RaiseAndSetIfChanged(ref interpolation, result);
                return interpolation;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(216, value);
                ODriveDevice.RaiseAndSetIfChanged(ref interpolation, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(217);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(217, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float posEstimate;
        public float PosEstimate
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(218);
                this.RaiseAndSetIfChanged(ref posEstimate, result);
                return posEstimate;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(218, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posEstimate, value);
            }
        }

        private float posCpr;
        public float PosCpr
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(219);
                this.RaiseAndSetIfChanged(ref posCpr, result);
                return posCpr;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(219, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posCpr, value);
            }
        }

        private byte hallState;
        public byte HallState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(220);
                this.RaiseAndSetIfChanged(ref hallState, result);
                return hallState;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(220, value);
                ODriveDevice.RaiseAndSetIfChanged(ref hallState, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(221);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(221, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(222);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(222, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(223);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(223, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}