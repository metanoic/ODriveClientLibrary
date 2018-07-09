namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class AxisConfig : RemoteObject
    {
        public AxisConfig(Connection connection): base(connection)
        {
        }

        private bool startupMotorCalibration;
        public bool StartupMotorCalibration
        {
            get
            {
                var result = FetchEndpointSync<bool>(39);
                this.RaiseAndSetIfChanged(ref startupMotorCalibration, result);
                return startupMotorCalibration;
            }

            private set
            {
                FetchEndpointSync<bool>(39, value);
                this.RaiseAndSetIfChanged(ref startupMotorCalibration, value);
            }
        }

        private bool startupEncoderIndexSearch;
        public bool StartupEncoderIndexSearch
        {
            get
            {
                var result = FetchEndpointSync<bool>(40);
                this.RaiseAndSetIfChanged(ref startupEncoderIndexSearch, result);
                return startupEncoderIndexSearch;
            }

            private set
            {
                FetchEndpointSync<bool>(40, value);
                this.RaiseAndSetIfChanged(ref startupEncoderIndexSearch, value);
            }
        }

        private bool startupEncoderOffsetCalibration;
        public bool StartupEncoderOffsetCalibration
        {
            get
            {
                var result = FetchEndpointSync<bool>(41);
                this.RaiseAndSetIfChanged(ref startupEncoderOffsetCalibration, result);
                return startupEncoderOffsetCalibration;
            }

            private set
            {
                FetchEndpointSync<bool>(41, value);
                this.RaiseAndSetIfChanged(ref startupEncoderOffsetCalibration, value);
            }
        }

        private bool startupClosedLoopControl;
        public bool StartupClosedLoopControl
        {
            get
            {
                var result = FetchEndpointSync<bool>(42);
                this.RaiseAndSetIfChanged(ref startupClosedLoopControl, result);
                return startupClosedLoopControl;
            }

            private set
            {
                FetchEndpointSync<bool>(42, value);
                this.RaiseAndSetIfChanged(ref startupClosedLoopControl, value);
            }
        }

        private bool startupSensorlessControl;
        public bool StartupSensorlessControl
        {
            get
            {
                var result = FetchEndpointSync<bool>(43);
                this.RaiseAndSetIfChanged(ref startupSensorlessControl, result);
                return startupSensorlessControl;
            }

            private set
            {
                FetchEndpointSync<bool>(43, value);
                this.RaiseAndSetIfChanged(ref startupSensorlessControl, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = FetchEndpointSync<bool>(44);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            private set
            {
                FetchEndpointSync<bool>(44, value);
                this.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private float countsPerStep;
        public float CountsPerStep
        {
            get
            {
                var result = FetchEndpointSync<float>(45);
                this.RaiseAndSetIfChanged(ref countsPerStep, result);
                return countsPerStep;
            }

            private set
            {
                FetchEndpointSync<float>(45, value);
                this.RaiseAndSetIfChanged(ref countsPerStep, value);
            }
        }

        private float rampUpTime;
        public float RampUpTime
        {
            get
            {
                var result = FetchEndpointSync<float>(46);
                this.RaiseAndSetIfChanged(ref rampUpTime, result);
                return rampUpTime;
            }

            private set
            {
                FetchEndpointSync<float>(46, value);
                this.RaiseAndSetIfChanged(ref rampUpTime, value);
            }
        }

        private float rampUpDistance;
        public float RampUpDistance
        {
            get
            {
                var result = FetchEndpointSync<float>(47);
                this.RaiseAndSetIfChanged(ref rampUpDistance, result);
                return rampUpDistance;
            }

            private set
            {
                FetchEndpointSync<float>(47, value);
                this.RaiseAndSetIfChanged(ref rampUpDistance, value);
            }
        }

        private float spinUpCurrent;
        public float SpinUpCurrent
        {
            get
            {
                var result = FetchEndpointSync<float>(48);
                this.RaiseAndSetIfChanged(ref spinUpCurrent, result);
                return spinUpCurrent;
            }

            private set
            {
                FetchEndpointSync<float>(48, value);
                this.RaiseAndSetIfChanged(ref spinUpCurrent, value);
            }
        }

        private float spinUpAcceleration;
        public float SpinUpAcceleration
        {
            get
            {
                var result = FetchEndpointSync<float>(49);
                this.RaiseAndSetIfChanged(ref spinUpAcceleration, result);
                return spinUpAcceleration;
            }

            private set
            {
                FetchEndpointSync<float>(49, value);
                this.RaiseAndSetIfChanged(ref spinUpAcceleration, value);
            }
        }

        private float spinUpTargetVel;
        public float SpinUpTargetVel
        {
            get
            {
                var result = FetchEndpointSync<float>(50);
                this.RaiseAndSetIfChanged(ref spinUpTargetVel, result);
                return spinUpTargetVel;
            }

            private set
            {
                FetchEndpointSync<float>(50, value);
                this.RaiseAndSetIfChanged(ref spinUpTargetVel, value);
            }
        }
    }
}