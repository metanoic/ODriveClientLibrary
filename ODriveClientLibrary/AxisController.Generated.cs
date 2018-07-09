namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class AxisController : RemoteObject
    {
        public AxisController(Device device): base(device)
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
                var result = device.FetchEndpointSync<float>(89);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(89, value);
                this.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = device.FetchEndpointSync<float>(90);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(90, value);
                this.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = device.FetchEndpointSync<float>(91);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            set
            {
                device.FetchEndpointSync<float>(91, value);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = device.FetchEndpointSync<float>(92);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            set
            {
                device.FetchEndpointSync<float>(92, value);
                this.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            device.FetchEndpointSync<float>(99, pos_setpoint);
            device.FetchEndpointSync<float>(100, vel_feed_forward);
            device.FetchEndpointSync<float>(101, current_feed_forward);
            device.FetchEndpointSync<byte>(98);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            device.FetchEndpointSync<float>(103, vel_setpoint);
            device.FetchEndpointSync<float>(104, current_feed_forward);
            device.FetchEndpointSync<byte>(102);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            device.FetchEndpointSync<float>(106, current_setpoint);
            device.FetchEndpointSync<byte>(105);
        }

        public void StartAnticoggingCalibration()
        {
            device.FetchEndpointSync<byte>(107);
        }
    }
}