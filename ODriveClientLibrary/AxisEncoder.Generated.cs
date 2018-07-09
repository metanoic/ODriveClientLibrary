namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class AxisEncoder : RemoteObject
    {
        public AxisEncoder(Connection connection): base(connection)
        {
            Config = new EncoderConfig(connection);
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
                var result = FetchEndpointSync<byte>(108);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            private set
            {
                FetchEndpointSync<byte>(108, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool isReady;
        public bool IsReady
        {
            get
            {
                var result = FetchEndpointSync<bool>(109);
                this.RaiseAndSetIfChanged(ref isReady, result);
                return isReady;
            }
        }

        private bool indexFound;
        public bool IndexFound
        {
            get
            {
                var result = FetchEndpointSync<bool>(110);
                this.RaiseAndSetIfChanged(ref indexFound, result);
                return indexFound;
            }
        }

        private int shadowCount;
        public int ShadowCount
        {
            get
            {
                var result = FetchEndpointSync<int>(111);
                this.RaiseAndSetIfChanged(ref shadowCount, result);
                return shadowCount;
            }

            private set
            {
                FetchEndpointSync<int>(111, value);
                this.RaiseAndSetIfChanged(ref shadowCount, value);
            }
        }

        private int countInCpr;
        public int CountInCpr
        {
            get
            {
                var result = FetchEndpointSync<int>(112);
                this.RaiseAndSetIfChanged(ref countInCpr, result);
                return countInCpr;
            }

            private set
            {
                FetchEndpointSync<int>(112, value);
                this.RaiseAndSetIfChanged(ref countInCpr, value);
            }
        }

        private int offset;
        public int Offset
        {
            get
            {
                var result = FetchEndpointSync<int>(113);
                this.RaiseAndSetIfChanged(ref offset, result);
                return offset;
            }

            private set
            {
                FetchEndpointSync<int>(113, value);
                this.RaiseAndSetIfChanged(ref offset, value);
            }
        }

        private float interpolation;
        public float Interpolation
        {
            get
            {
                var result = FetchEndpointSync<float>(114);
                this.RaiseAndSetIfChanged(ref interpolation, result);
                return interpolation;
            }

            private set
            {
                FetchEndpointSync<float>(114, value);
                this.RaiseAndSetIfChanged(ref interpolation, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = FetchEndpointSync<float>(115);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            private set
            {
                FetchEndpointSync<float>(115, value);
                this.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float posEstimate;
        public float PosEstimate
        {
            get
            {
                var result = FetchEndpointSync<float>(116);
                this.RaiseAndSetIfChanged(ref posEstimate, result);
                return posEstimate;
            }

            private set
            {
                FetchEndpointSync<float>(116, value);
                this.RaiseAndSetIfChanged(ref posEstimate, value);
            }
        }

        private float posCpr;
        public float PosCpr
        {
            get
            {
                var result = FetchEndpointSync<float>(117);
                this.RaiseAndSetIfChanged(ref posCpr, result);
                return posCpr;
            }

            private set
            {
                FetchEndpointSync<float>(117, value);
                this.RaiseAndSetIfChanged(ref posCpr, value);
            }
        }

        private byte hallState;
        public byte HallState
        {
            get
            {
                var result = FetchEndpointSync<byte>(118);
                this.RaiseAndSetIfChanged(ref hallState, result);
                return hallState;
            }

            private set
            {
                FetchEndpointSync<byte>(118, value);
                this.RaiseAndSetIfChanged(ref hallState, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = FetchEndpointSync<float>(119);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            private set
            {
                FetchEndpointSync<float>(119, value);
                this.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = FetchEndpointSync<float>(120);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            private set
            {
                FetchEndpointSync<float>(120, value);
                this.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = FetchEndpointSync<float>(121);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            private set
            {
                FetchEndpointSync<float>(121, value);
                this.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}