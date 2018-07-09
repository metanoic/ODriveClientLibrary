namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Controller : RemoteObject
    {
        public Axis1Controller(Device device): base(device)
        {
            Config = new ControllerConfig(device);
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
                var result = device.FetchEndpointSync<float>(191);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(191, value);
                this.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = device.FetchEndpointSync<float>(192);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(192, value);
                this.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = device.FetchEndpointSync<float>(193);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            set
            {
                device.FetchEndpointSync<float>(193, value);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = device.FetchEndpointSync<float>(194);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(194, value);
                this.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            device.FetchEndpointSync<float>(201, pos_setpoint);
            device.FetchEndpointSync<float>(202, vel_feed_forward);
            device.FetchEndpointSync<float>(203, current_feed_forward);
            device.FetchEndpointSync<byte>(200);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            device.FetchEndpointSync<float>(205, vel_setpoint);
            device.FetchEndpointSync<float>(206, current_feed_forward);
            device.FetchEndpointSync<byte>(204);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            device.FetchEndpointSync<float>(208, current_setpoint);
            device.FetchEndpointSync<byte>(207);
        }

        public void StartAnticoggingCalibration()
        {
            device.FetchEndpointSync<byte>(209);
        }
    }
}