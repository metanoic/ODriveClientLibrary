namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor1Encoder : RemoteObject
    {
        public Motor1Encoder(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new EncoderConfig(ODriveDevice);
        }

        public EncoderConfig Config
        {
            get;
            private set;
        }

        private float phase;
        public float Phase
        {
            get
            {
                var result = FetchEndpointSync<float>(173);
                this.RaiseAndSetIfChanged(ref phase, result);
                return phase;
            }
        }

        private float pllPos;
        public float PllPos
        {
            get
            {
                var result = FetchEndpointSync<float>(174);
                this.RaiseAndSetIfChanged(ref pllPos, result);
                return pllPos;
            }

            set
            {
                SetPropertySync<float>(174, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllPos, value);
            }
        }

        private float pllVel;
        public float PllVel
        {
            get
            {
                var result = FetchEndpointSync<float>(175);
                this.RaiseAndSetIfChanged(ref pllVel, result);
                return pllVel;
            }

            set
            {
                SetPropertySync<float>(175, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllVel, value);
            }
        }

        private float pllKp;
        public float PllKp
        {
            get
            {
                var result = FetchEndpointSync<float>(176);
                this.RaiseAndSetIfChanged(ref pllKp, result);
                return pllKp;
            }

            set
            {
                SetPropertySync<float>(176, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKp, value);
            }
        }

        private float pllKi;
        public float PllKi
        {
            get
            {
                var result = FetchEndpointSync<float>(177);
                this.RaiseAndSetIfChanged(ref pllKi, result);
                return pllKi;
            }

            set
            {
                SetPropertySync<float>(177, value);
                ODriveDevice.RaiseAndSetIfChanged(ref pllKi, value);
            }
        }

        private int encoderOffset;
        public int EncoderOffset
        {
            get
            {
                var result = FetchEndpointSync<int>(178);
                this.RaiseAndSetIfChanged(ref encoderOffset, result);
                return encoderOffset;
            }

            set
            {
                SetPropertySync<int>(178, value);
                ODriveDevice.RaiseAndSetIfChanged(ref encoderOffset, value);
            }
        }

        private int encoderState;
        public int EncoderState
        {
            get
            {
                var result = FetchEndpointSync<int>(179);
                this.RaiseAndSetIfChanged(ref encoderState, result);
                return encoderState;
            }

            set
            {
                SetPropertySync<int>(179, value);
                ODriveDevice.RaiseAndSetIfChanged(ref encoderState, value);
            }
        }

        private int motorDir;
        public int MotorDir
        {
            get
            {
                var result = FetchEndpointSync<int>(180);
                this.RaiseAndSetIfChanged(ref motorDir, result);
                return motorDir;
            }

            set
            {
                SetPropertySync<int>(180, value);
                ODriveDevice.RaiseAndSetIfChanged(ref motorDir, value);
            }
        }
    }
}