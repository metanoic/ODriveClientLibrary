namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class AxisSensorlessEstimator : RemoteObject
    {
        public AxisSensorlessEstimator(Connection connection): base(connection)
        {
        }

        private byte error;
        public byte Error
        {
            get
            {
                var result = FetchEndpointSync<byte>(130);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            private set
            {
                FetchEndpointSync<byte>(130, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = FetchEndpointSync<float>(131);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            private set
            {
                FetchEndpointSync<float>(131, value);
                this.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float pllPos;
        public float PllPos
        {
            get
            {
                var result = FetchEndpointSync<float>(132);
                this.RaiseAndSetIfChanged(ref pllPos, result);
                return pllPos;
            }

            private set
            {
                FetchEndpointSync<float>(132, value);
                this.RaiseAndSetIfChanged(ref pllPos, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = FetchEndpointSync<float>(133);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            private set
            {
                FetchEndpointSync<float>(133, value);
                this.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = FetchEndpointSync<float>(134);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            private set
            {
                FetchEndpointSync<float>(134, value);
                this.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = FetchEndpointSync<float>(135);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            private set
            {
                FetchEndpointSync<float>(135, value);
                this.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}