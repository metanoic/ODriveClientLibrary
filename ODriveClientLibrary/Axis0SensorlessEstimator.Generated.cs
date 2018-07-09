namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis0SensorlessEstimator : RemoteObject
    {
        public Axis0SensorlessEstimator(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private byte error;
        public byte Error
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(130);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(130, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(131);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(131, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float pllPos;
        public float PllPos
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(132);
                this.RaiseAndSetIfChanged(ref pllPos, result);
                return pllPos;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(132, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllPos, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(133);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(133, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(134);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(134, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(135);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(135, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}