namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis0Encoder : RemoteObject
    {
        public Axis0Encoder(Device ODriveDevice): base(ODriveDevice)
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
                var result = ODriveDevice.FetchEndpointSync<byte>(108);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(108, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool isReady;
        public bool IsReady
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(109);
                this.RaiseAndSetIfChanged(ref isReady, result);
                return isReady;
            }
        }

        private bool indexFound;
        public bool IndexFound
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(110);
                this.RaiseAndSetIfChanged(ref indexFound, result);
                return indexFound;
            }
        }

        private int shadowCount;
        public int ShadowCount
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(111);
                this.RaiseAndSetIfChanged(ref shadowCount, result);
                return shadowCount;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(111, value);
                ODriveDevice.RaiseAndSetIfChanged(ref shadowCount, value);
            }
        }

        private int countInCpr;
        public int CountInCpr
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(112);
                this.RaiseAndSetIfChanged(ref countInCpr, result);
                return countInCpr;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(112, value);
                ODriveDevice.RaiseAndSetIfChanged(ref countInCpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(113);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(113, value);
                ODriveDevice.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private float interpolation;
        public float Interpolation
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(114);
                this.RaiseAndSetIfChanged(ref interpolation, result);
                return interpolation;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(114, value);
                ODriveDevice.RaiseAndSetIfChanged(ref interpolation, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(115);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(115, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float posEstimate;
        public float PosEstimate
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(116);
                this.RaiseAndSetIfChanged(ref posEstimate, result);
                return posEstimate;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(116, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posEstimate, value);
            }
        }

        private float posCpr;
        public float PosCpr
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(117);
                this.RaiseAndSetIfChanged(ref posCpr, result);
                return posCpr;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(117, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posCpr, value);
            }
        }

        private byte hallState;
        public byte HallState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(118);
                this.RaiseAndSetIfChanged(ref hallState, result);
                return hallState;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(118, value);
                ODriveDevice.RaiseAndSetIfChanged(ref hallState, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(119);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(119, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(120);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(120, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(121);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(121, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}