namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceConfig : RemoteObject
    {
        public DeviceConfig(Device device): base(device)
        {
        }

        private float brakeResistance;
        public float BrakeResistance
        {
            get
            {
                var result = device.FetchEndpointSync<float>(28);
                this.RaiseAndSetIfChanged(ref brakeResistance, result);
                return brakeResistance;
            }

            private set
            {
                device.FetchEndpointSync<float>(28, value);
                this.RaiseAndSetIfChanged(ref brakeResistance, value);
            }
        }

        private bool enableUart;
        public bool EnableUart
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(29);
                this.RaiseAndSetIfChanged(ref enableUart, result);
                return enableUart;
            }

            private set
            {
                device.FetchEndpointSync<bool>(29, value);
                this.RaiseAndSetIfChanged(ref enableUart, value);
            }
        }

        private bool enableI2cInsteadOfCan;
        public bool EnableI2cInsteadOfCan
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(30);
                this.RaiseAndSetIfChanged(ref enableI2cInsteadOfCan, result);
                return enableI2cInsteadOfCan;
            }

            private set
            {
                device.FetchEndpointSync<bool>(30, value);
                this.RaiseAndSetIfChanged(ref enableI2cInsteadOfCan, value);
            }
        }

        private bool enableAsciiProtocolOnUsb;
        public bool EnableAsciiProtocolOnUsb
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(31);
                this.RaiseAndSetIfChanged(ref enableAsciiProtocolOnUsb, result);
                return enableAsciiProtocolOnUsb;
            }

            private set
            {
                device.FetchEndpointSync<bool>(31, value);
                this.RaiseAndSetIfChanged(ref enableAsciiProtocolOnUsb, value);
            }
        }

        private float dcBusUndervoltageTripLevel;
        public float DcBusUndervoltageTripLevel
        {
            get
            {
                var result = device.FetchEndpointSync<float>(32);
                this.RaiseAndSetIfChanged(ref dcBusUndervoltageTripLevel, result);
                return dcBusUndervoltageTripLevel;
            }

            private set
            {
                device.FetchEndpointSync<float>(32, value);
                this.RaiseAndSetIfChanged(ref dcBusUndervoltageTripLevel, value);
            }
        }

        private float dcBusOvervoltageTripLevel;
        public float DcBusOvervoltageTripLevel
        {
            get
            {
                var result = device.FetchEndpointSync<float>(33);
                this.RaiseAndSetIfChanged(ref dcBusOvervoltageTripLevel, result);
                return dcBusOvervoltageTripLevel;
            }

            private set
            {
                device.FetchEndpointSync<float>(33, value);
                this.RaiseAndSetIfChanged(ref dcBusOvervoltageTripLevel, value);
            }
        }
    }
}