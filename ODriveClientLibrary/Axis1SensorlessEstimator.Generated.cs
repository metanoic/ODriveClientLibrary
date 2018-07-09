namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1SensorlessEstimator : RemoteObject
    {
        public Axis1SensorlessEstimator(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private byte error;
        public byte Error
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(232);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(232, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(233);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(233, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float pllPos;
        public float PllPos
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(234);
                this.RaiseAndSetIfChanged(ref pllPos, result);
                return pllPos;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(234, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllPos, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(235);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(235, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(236);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(236, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(237);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(237, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}