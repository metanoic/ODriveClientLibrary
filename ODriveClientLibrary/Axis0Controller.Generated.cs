namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis0Controller : RemoteObject
    {
        public Axis0Controller(Device ODriveDevice): base(ODriveDevice)
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
                var result = ODriveDevice.FetchEndpointSync<float>(89);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(89, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(90);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(90, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(91);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(91, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(92);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(92, value);
                ODriveDevice.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            ODriveDevice.FetchEndpointSync<float>(99, pos_setpoint);
            ODriveDevice.FetchEndpointSync<float>(100, vel_feed_forward);
            ODriveDevice.FetchEndpointSync<float>(101, current_feed_forward);
            ODriveDevice.FetchEndpointSync<byte>(98);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            ODriveDevice.FetchEndpointSync<float>(103, vel_setpoint);
            ODriveDevice.FetchEndpointSync<float>(104, current_feed_forward);
            ODriveDevice.FetchEndpointSync<byte>(102);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            ODriveDevice.FetchEndpointSync<float>(106, current_setpoint);
            ODriveDevice.FetchEndpointSync<byte>(105);
        }

        public void StartAnticoggingCalibration()
        {
            ODriveDevice.FetchEndpointSync<byte>(107);
        }
    }
}