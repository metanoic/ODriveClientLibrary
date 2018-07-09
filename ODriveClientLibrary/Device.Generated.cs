namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Device : RemoteObject
    {
        internal Device()
        {
            SystemStats = new DeviceSystemStats(this);
            Config = new DeviceConfig(this);
            Axis0 = new DeviceAxis(this);
            Axis1 = new DeviceAxis(this);
            Can = new DeviceCan(this);
        }

        public DeviceSystemStats SystemStats
        {
            get;
            private set;
        }

        public DeviceConfig Config
        {
            get;
            private set;
        }

        public DeviceAxis Axis0
        {
            get;
            private set;
        }

        public DeviceAxis Axis1
        {
            get;
            private set;
        }

        public DeviceCan Can
        {
            get;
            private set;
        }

        private float vbusVoltage;
        public float VbusVoltage
        {
            get
            {
                var result = device.FetchEndpointSync<float>(1);
                this.RaiseAndSetIfChanged(ref vbusVoltage, result);
                return vbusVoltage;
            }
        }

        private ulong serialNumber;
        public ulong SerialNumber
        {
            get
            {
                var result = device.FetchEndpointSync<ulong>(2);
                this.RaiseAndSetIfChanged(ref serialNumber, result);
                return serialNumber;
            }
        }

        private byte hwVersionMajor;
        public byte HwVersionMajor
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(3);
                this.RaiseAndSetIfChanged(ref hwVersionMajor, result);
                return hwVersionMajor;
            }
        }

        private byte hwVersionMinor;
        public byte HwVersionMinor
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(4);
                this.RaiseAndSetIfChanged(ref hwVersionMinor, result);
                return hwVersionMinor;
            }
        }

        private byte hwVersionVariant;
        public byte HwVersionVariant
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(5);
                this.RaiseAndSetIfChanged(ref hwVersionVariant, result);
                return hwVersionVariant;
            }
        }

        private byte fwVersionMajor;
        public byte FwVersionMajor
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(6);
                this.RaiseAndSetIfChanged(ref fwVersionMajor, result);
                return fwVersionMajor;
            }
        }

        private byte fwVersionMinor;
        public byte FwVersionMinor
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(7);
                this.RaiseAndSetIfChanged(ref fwVersionMinor, result);
                return fwVersionMinor;
            }
        }

        private byte fwVersionRevision;
        public byte FwVersionRevision
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(8);
                this.RaiseAndSetIfChanged(ref fwVersionRevision, result);
                return fwVersionRevision;
            }
        }

        private byte fwVersionUnreleased;
        public byte FwVersionUnreleased
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(9);
                this.RaiseAndSetIfChanged(ref fwVersionUnreleased, result);
                return fwVersionUnreleased;
            }
        }

        private bool userConfigLoaded;
        public bool UserConfigLoaded
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(10);
                this.RaiseAndSetIfChanged(ref userConfigLoaded, result);
                return userConfigLoaded;
            }
        }

        private bool brakeResistorArmed;
        public bool BrakeResistorArmed
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(11);
                this.RaiseAndSetIfChanged(ref brakeResistorArmed, result);
                return brakeResistorArmed;
            }
        }

        public float GetOscilloscopeVal(uint index)
        {
            device.FetchEndpointSync<uint>(250, index);
            return device.FetchEndpointSync<float>(249);
        }

        public float GetAdcVoltage(uint gpio)
        {
            device.FetchEndpointSync<uint>(253, gpio);
            return device.FetchEndpointSync<float>(252);
        }

        public void SaveConfiguration()
        {
            device.FetchEndpointSync<byte>(255);
        }

        public void EraseConfiguration()
        {
            device.FetchEndpointSync<byte>(256);
        }

        public void Reboot()
        {
            device.FetchEndpointSync<byte>(257);
        }

        public void EnterDfuMode()
        {
            device.FetchEndpointSync<byte>(258);
        }
    }
}