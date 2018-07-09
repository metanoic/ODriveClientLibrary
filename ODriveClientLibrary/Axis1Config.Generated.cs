namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Config : RemoteObject
    {
        public Axis1Config(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private bool startupMotorCalibration;
        public bool StartupMotorCalibration
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(141);
                this.RaiseAndSetIfChanged(ref startupMotorCalibration, result);
                return startupMotorCalibration;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(141, value);
                ODriveDevice.RaiseAndSetIfChanged(ref startupMotorCalibration, value);
            }
        }

        private bool startupEncoderIndexSearch;
        public bool StartupEncoderIndexSearch
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(142);
                this.RaiseAndSetIfChanged(ref startupEncoderIndexSearch, result);
                return startupEncoderIndexSearch;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(142, value);
                ODriveDevice.RaiseAndSetIfChanged(ref startupEncoderIndexSearch, value);
            }
        }

        private bool startupEncoderOffsetCalibration;
        public bool StartupEncoderOffsetCalibration
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(143);
                this.RaiseAndSetIfChanged(ref startupEncoderOffsetCalibration, result);
                return startupEncoderOffsetCalibration;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(143, value);
                ODriveDevice.RaiseAndSetIfChanged(ref startupEncoderOffsetCalibration, value);
            }
        }

        private bool startupClosedLoopControl;
        public bool StartupClosedLoopControl
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(144);
                this.RaiseAndSetIfChanged(ref startupClosedLoopControl, result);
                return startupClosedLoopControl;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(144, value);
                ODriveDevice.RaiseAndSetIfChanged(ref startupClosedLoopControl, value);
            }
        }

        private bool startupSensorlessControl;
        public bool StartupSensorlessControl
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(145);
                this.RaiseAndSetIfChanged(ref startupSensorlessControl, result);
                return startupSensorlessControl;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(145, value);
                ODriveDevice.RaiseAndSetIfChanged(ref startupSensorlessControl, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(146);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(146, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private float countsPerStep;
        public float CountsPerStep
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(147);
                this.RaiseAndSetIfChanged(ref countsPerStep, result);
                return countsPerStep;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(147, value);
                ODriveDevice.RaiseAndSetIfChanged(ref countsPerStep, value);
            }
        }

        private float rampUpTime;
        public float RampUpTime
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(148);
                this.RaiseAndSetIfChanged(ref rampUpTime, result);
                return rampUpTime;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(148, value);
                ODriveDevice.RaiseAndSetIfChanged(ref rampUpTime, value);
            }
        }

        private float rampUpDistance;
        public float RampUpDistance
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(149);
                this.RaiseAndSetIfChanged(ref rampUpDistance, result);
                return rampUpDistance;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(149, value);
                ODriveDevice.RaiseAndSetIfChanged(ref rampUpDistance, value);
            }
        }

        private float spinUpCurrent;
        public float SpinUpCurrent
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(150);
                this.RaiseAndSetIfChanged(ref spinUpCurrent, result);
                return spinUpCurrent;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(150, value);
                ODriveDevice.RaiseAndSetIfChanged(ref spinUpCurrent, value);
            }
        }

        private float spinUpAcceleration;
        public float SpinUpAcceleration
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(151);
                this.RaiseAndSetIfChanged(ref spinUpAcceleration, result);
                return spinUpAcceleration;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(151, value);
                ODriveDevice.RaiseAndSetIfChanged(ref spinUpAcceleration, value);
            }
        }

        private float spinUpTargetVel;
        public float SpinUpTargetVel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(152);
                this.RaiseAndSetIfChanged(ref spinUpTargetVel, result);
                return spinUpTargetVel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(152, value);
                ODriveDevice.RaiseAndSetIfChanged(ref spinUpTargetVel, value);
            }
        }
    }
}