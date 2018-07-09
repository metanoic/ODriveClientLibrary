namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class AxisController : RemoteObject
    {
        public AxisController(Connection connection): base(connection)
        {
            Config = new ControllerConfig(connection);
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
                var result = FetchEndpointSync<float>(89);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            private set
            {
                FetchEndpointSync<float>(89, value);
                this.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = FetchEndpointSync<float>(90);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            private set
            {
                FetchEndpointSync<float>(90, value);
                this.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = FetchEndpointSync<float>(91);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            private set
            {
                FetchEndpointSync<float>(91, value);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = FetchEndpointSync<float>(92);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            private set
            {
                FetchEndpointSync<float>(92, value);
                this.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            FetchEndpointSync<float>(99, pos_setpoint);
            FetchEndpointSync<float>(100, vel_feed_forward);
            FetchEndpointSync<float>(101, current_feed_forward);
            FetchEndpointSync<byte>(98);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            FetchEndpointSync<float>(103, vel_setpoint);
            FetchEndpointSync<float>(104, current_feed_forward);
            FetchEndpointSync<byte>(102);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            FetchEndpointSync<float>(106, current_setpoint);
            FetchEndpointSync<byte>(105);
        }

        public void StartAnticoggingCalibration()
        {
            FetchEndpointSync<byte>(107);
        }
    }
}