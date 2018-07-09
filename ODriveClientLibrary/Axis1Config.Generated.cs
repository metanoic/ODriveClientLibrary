namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Config : RemoteObject
    {
        public Axis1Config(Device device): base(device)
        {
        }

        private bool startupMotorCalibration;
        public bool StartupMotorCalibration
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(141);
                this.RaiseAndSetIfChanged(ref startupMotorCalibration, result);
                return startupMotorCalibration;
            }

            set
            {
                device.FetchEndpointSync<bool>(141, value);
                this.RaiseAndSetIfChanged(ref startupMotorCalibration, value);
            }
        }

        private bool startupEncoderIndexSearch;
        public bool StartupEncoderIndexSearch
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(142);
                this.RaiseAndSetIfChanged(ref startupEncoderIndexSearch, result);
                return startupEncoderIndexSearch;
            }

            set
            {
                device.FetchEndpointSync<bool>(142, value);
                this.RaiseAndSetIfChanged(ref startupEncoderIndexSearch, value);
            }
        }

        private bool startupEncoderOffsetCalibration;
        public bool StartupEncoderOffsetCalibration
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(143);
                this.RaiseAndSetIfChanged(ref startupEncoderOffsetCalibration, result);
                return startupEncoderOffsetCalibration;
            }

            set
            {
                device.FetchEndpointSync<bool>(143, value);
                this.RaiseAndSetIfChanged(ref startupEncoderOffsetCalibration, value);
            }
        }

        private bool startupClosedLoopControl;
        public bool StartupClosedLoopControl
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(144);
                this.RaiseAndSetIfChanged(ref startupClosedLoopControl, result);
                return startupClosedLoopControl;
            }

            set
            {
                device.FetchEndpointSync<bool>(144, value);
                this.RaiseAndSetIfChanged(ref startupClosedLoopControl, value);
            }
        }

        private bool startupSensorlessControl;
        public bool StartupSensorlessControl
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(145);
                this.RaiseAndSetIfChanged(ref startupSensorlessControl, result);
                return startupSensorlessControl;
            }

            set
            {
                device.FetchEndpointSync<bool>(145, value);
                this.RaiseAndSetIfChanged(ref startupSensorlessControl, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(146);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            set
            {
                device.FetchEndpointSync<bool>(146, value);
                this.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private float countsPerStep;
        public float CountsPerStep
        {
            get
            {
                var result = device.FetchEndpointSync<float>(147);
                this.RaiseAndSetIfChanged(ref countsPerStep, result);
                return countsPerStep;
            }

            set
            {
                device.FetchEndpointSync<float>(147, value);
                this.RaiseAndSetIfChanged(ref countsPerStep, value);
            }
        }

        private float rampUpTime;
        public float RampUpTime
        {
            get
            {
                var result = device.FetchEndpointSync<float>(148);
                this.RaiseAndSetIfChanged(ref rampUpTime, result);
                return rampUpTime;
            }

            set
            {
                device.FetchEndpointSync<float>(148, value);
                this.RaiseAndSetIfChanged(ref rampUpTime, value);
            }
        }

        private float rampUpDistance;
        public float RampUpDistance
        {
            get
            {
                var result = device.FetchEndpointSync<float>(149);
                this.RaiseAndSetIfChanged(ref rampUpDistance, result);
                return rampUpDistance;
            }

            set
            {
                device.FetchEndpointSync<float>(149, value);
                this.RaiseAndSetIfChanged(ref rampUpDistance, value);
            }
        }

        private float spinUpCurrent;
        public float SpinUpCurrent
        {
            get
            {
                var result = device.FetchEndpointSync<float>(150);
                this.RaiseAndSetIfChanged(ref spinUpCurrent, result);
                return spinUpCurrent;
            }

            set
            {
                device.FetchEndpointSync<float>(150, value);
                this.RaiseAndSetIfChanged(ref spinUpCurrent, value);
            }
        }

        private float spinUpAcceleration;
        public float SpinUpAcceleration
        {
            get
            {
                var result = device.FetchEndpointSync<float>(151);
                this.RaiseAndSetIfChanged(ref spinUpAcceleration, result);
                return spinUpAcceleration;
            }

            set
            {
                device.FetchEndpointSync<float>(151, value);
                this.RaiseAndSetIfChanged(ref spinUpAcceleration, value);
            }
        }

        private float spinUpTargetVel;
        public float SpinUpTargetVel
        {
            get
            {
                var result = device.FetchEndpointSync<float>(152);
                this.RaiseAndSetIfChanged(ref spinUpTargetVel, result);
                return spinUpTargetVel;
            }

            set
            {
                device.FetchEndpointSync<float>(152, value);
                this.RaiseAndSetIfChanged(ref spinUpTargetVel, value);
            }
        }
    }
}