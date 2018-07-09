namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class ControllerConfig : RemoteObject
    {
        public ControllerConfig(Device device): base(device)
        {
        }

        private byte controlMode;
        public byte ControlMode
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(93);
                this.RaiseAndSetIfChanged(ref controlMode, result);
                return controlMode;
            }

            set
            {
                device.FetchEndpointSync<byte>(93, value);
                this.RaiseAndSetIfChanged(ref controlMode, value);
            }
        }

        private float posGain;
        public float PosGain
        {
            get
            {
                var result = device.FetchEndpointSync<float>(94);
                this.RaiseAndSetIfChanged(ref posGain, result);
                return posGain;
            }

            set
            {
                device.FetchEndpointSync<float>(94, value);
                this.RaiseAndSetIfChanged(ref posGain, value);
            }
        }

        private float velGain;
        public float VelGain
        {
            get
            {
                var result = device.FetchEndpointSync<float>(95);
                this.RaiseAndSetIfChanged(ref velGain, result);
                return velGain;
            }

            set
            {
                device.FetchEndpointSync<float>(95, value);
                this.RaiseAndSetIfChanged(ref velGain, value);
            }
        }

        private float velIntegratorGain;
        public float VelIntegratorGain
        {
            get
            {
                var result = device.FetchEndpointSync<float>(96);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, result);
                return velIntegratorGain;
            }

            set
            {
                device.FetchEndpointSync<float>(96, value);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, value);
            }
        }

        private float velLimit;
        public float VelLimit
        {
            get
            {
                var result = device.FetchEndpointSync<float>(97);
                this.RaiseAndSetIfChanged(ref velLimit, result);
                return velLimit;
            }

            set
            {
                device.FetchEndpointSync<float>(97, value);
                this.RaiseAndSetIfChanged(ref velLimit, value);
            }
        }
    }
}