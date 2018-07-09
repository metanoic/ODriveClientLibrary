namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class ControllerConfig : RemoteObject
    {
        public ControllerConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private byte controlMode;
        public byte ControlMode
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(93);
                this.RaiseAndSetIfChanged(ref controlMode, result);
                return controlMode;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(93, value);
                ODriveDevice.RaiseAndSetIfChanged(ref controlMode, value);
            }
        }

        private float posGain;
        public float PosGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(94);
                this.RaiseAndSetIfChanged(ref posGain, result);
                return posGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(94, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posGain, value);
            }
        }

        private float velGain;
        public float VelGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(95);
                this.RaiseAndSetIfChanged(ref velGain, result);
                return velGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(95, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velGain, value);
            }
        }

        private float velIntegratorGain;
        public float VelIntegratorGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(96);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, result);
                return velIntegratorGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(96, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorGain, value);
            }
        }

        private float velLimit;
        public float VelLimit
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(97);
                this.RaiseAndSetIfChanged(ref velLimit, result);
                return velLimit;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(97, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velLimit, value);
            }
        }
    }
}