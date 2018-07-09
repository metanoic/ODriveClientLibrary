namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Controller : RemoteObject
    {
        public Axis1Controller(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new ControllerConfig(ODriveDevice);
        }

        public ControllerConfig Config
        {
            get;
            private set;
        }

        private float posSetpoint;
        public float PosSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(191);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(191, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(192);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(192, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(193);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(193, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(194);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(194, value);
                ODriveDevice.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            ODriveDevice.FetchEndpointSync<float>(201, pos_setpoint);
            ODriveDevice.FetchEndpointSync<float>(202, vel_feed_forward);
            ODriveDevice.FetchEndpointSync<float>(203, current_feed_forward);
            ODriveDevice.FetchEndpointSync<byte>(200);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            ODriveDevice.FetchEndpointSync<float>(205, vel_setpoint);
            ODriveDevice.FetchEndpointSync<float>(206, current_feed_forward);
            ODriveDevice.FetchEndpointSync<byte>(204);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            ODriveDevice.FetchEndpointSync<float>(208, current_setpoint);
            ODriveDevice.FetchEndpointSync<byte>(207);
        }

        public void StartAnticoggingCalibration()
        {
            ODriveDevice.FetchEndpointSync<byte>(209);
        }
    }
}