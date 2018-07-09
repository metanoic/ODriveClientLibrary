namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class ControllerConfig : RemoteObject
    {
        public ControllerConfig(Connection connection): base(connection)
        {
        }

        private byte controlMode;
        public byte ControlMode
        {
            get
            {
                var result = FetchEndpointSync<byte>(93);
                this.RaiseAndSetIfChanged(ref controlMode, result);
                return controlMode;
            }

            private set
            {
                FetchEndpointSync<byte>(93, value);
                this.RaiseAndSetIfChanged(ref controlMode, value);
            }
        }

        private float posGain;
        public float PosGain
        {
            get
            {
                var result = FetchEndpointSync<float>(94);
                this.RaiseAndSetIfChanged(ref posGain, result);
                return posGain;
            }

            private set
            {
                FetchEndpointSync<float>(94, value);
                this.RaiseAndSetIfChanged(ref posGain, value);
            }
        }

        private float velGain;
        public float VelGain
        {
            get
            {
                var result = FetchEndpointSync<float>(95);
                this.RaiseAndSetIfChanged(ref velGain, result);
                return velGain;
            }

            private set
            {
                FetchEndpointSync<float>(95, value);
                this.RaiseAndSetIfChanged(ref velGain, value);
            }
        }

        private float velIntegratorGain;
        public float VelIntegratorGain
        {
            get
            {
                var result = FetchEndpointSync<float>(96);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, result);
                return velIntegratorGain;
            }

            private set
            {
                FetchEndpointSync<float>(96, value);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, value);
            }
        }

        private float velLimit;
        public float VelLimit
        {
            get
            {
                var result = FetchEndpointSync<float>(97);
                this.RaiseAndSetIfChanged(ref velLimit, result);
                return velLimit;
            }

            private set
            {
                FetchEndpointSync<float>(97, value);
                this.RaiseAndSetIfChanged(ref velLimit, value);
            }
        }
    }
}