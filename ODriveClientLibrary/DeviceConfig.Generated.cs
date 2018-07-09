namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceConfig : RemoteObject
    {
        public DeviceConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private float brakeResistance;
        public float BrakeResistance
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(28);
                this.RaiseAndSetIfChanged(ref brakeResistance, result);
                return brakeResistance;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(28, value);
                ODriveDevice.RaiseAndSetIfChanged(ref brakeResistance, value);
            }
        }

        private bool enableUart;
        public bool EnableUart
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(29);
                this.RaiseAndSetIfChanged(ref enableUart, result);
                return enableUart;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(29, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableUart, value);
            }
        }

        private bool enableI2cInsteadOfCan;
        public bool EnableI2cInsteadOfCan
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(30);
                this.RaiseAndSetIfChanged(ref enableI2cInsteadOfCan, result);
                return enableI2cInsteadOfCan;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(30, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableI2cInsteadOfCan, value);
            }
        }

        private bool enableAsciiProtocolOnUsb;
        public bool EnableAsciiProtocolOnUsb
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(31);
                this.RaiseAndSetIfChanged(ref enableAsciiProtocolOnUsb, result);
                return enableAsciiProtocolOnUsb;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(31, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableAsciiProtocolOnUsb, value);
            }
        }

        private float dcBusUndervoltageTripLevel;
        public float DcBusUndervoltageTripLevel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(32);
                this.RaiseAndSetIfChanged(ref dcBusUndervoltageTripLevel, result);
                return dcBusUndervoltageTripLevel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(32, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dcBusUndervoltageTripLevel, value);
            }
        }

        private float dcBusOvervoltageTripLevel;
        public float DcBusOvervoltageTripLevel
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(33);
                this.RaiseAndSetIfChanged(ref dcBusOvervoltageTripLevel, result);
                return dcBusOvervoltageTripLevel;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(33, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dcBusOvervoltageTripLevel, value);
            }
        }
    }
}