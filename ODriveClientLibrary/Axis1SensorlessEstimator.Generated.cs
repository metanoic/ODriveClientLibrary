namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1SensorlessEstimator : RemoteObject
    {
        public Axis1SensorlessEstimator(Device device): base(device)
        {
        }

        private byte error;
        public byte Error
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(232);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                device.FetchEndpointSync<byte>(232, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = device.FetchEndpointSync<float>(233);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }

            set
            {
                device.FetchEndpointSync<float>(233, value);
                this.RaiseAndSetIfChanged(ref phase, value);
            }
        }

        private float pllPos;
        public float PllPos
        {
            get
            {
                var result = device.FetchEndpointSync<float>(234);
                this.RaiseAndSetIfChanged(ref pllPos, result);
                return pllPos;
            }

            set
            {
                device.FetchEndpointSync<float>(234, value);
                this.RaiseAndSetIfChanged(ref pllPos, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = device.FetchEndpointSync<float>(235);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                device.FetchEndpointSync<float>(235, value);
                this.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = device.FetchEndpointSync<float>(236);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                device.FetchEndpointSync<float>(236, value);
                this.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = device.FetchEndpointSync<float>(237);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                device.FetchEndpointSync<float>(237, value);
                this.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }
    }
}