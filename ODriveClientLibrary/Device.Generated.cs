namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Device : RemoteObject
    {
        internal Device()
        {
            Config = new DeviceConfig(this);
            Axis0 = new DeviceAxis0(this);
            Motor0 = new DeviceMotor0(this);
            Axis1 = new DeviceAxis1(this);
            Motor1 = new DeviceMotor1(this);
        }

        public DeviceConfig Config
        {
            get;
            private set;
        }

        public DeviceAxis0 Axis0
        {
            get;
            private set;
        }

        public DeviceMotor0 Motor0
        {
            get;
            private set;
        }

        public DeviceAxis1 Axis1
        {
            get;
            private set;
        }

        public DeviceMotor1 Motor1
        {
            get;
            private set;
        }

        private float vbusVoltage;
        public float VbusVoltage
        {
            get
            {
                var result = FetchEndpointSync<float>(1);
                this.RaiseAndSetIfChanged(ref vbusVoltage, result);
                return vbusVoltage;
            }
        }

        private ulong serialNumber;
        public ulong SerialNumber
        {
            get
            {
                var result = FetchEndpointSync<ulong>(2);
                this.RaiseAndSetIfChanged(ref serialNumber, result);
                return serialNumber;
            }
        }

        public void RunAnticoggingCalibration()
        {
            FetchEndpointSync<byte>(3);
        }

        public void SaveConfiguration()
        {
            FetchEndpointSync<byte>(208);
        }

        public void EraseConfiguration()
        {
            FetchEndpointSync<byte>(210);
        }

        public void Reboot()
        {
            FetchEndpointSync<byte>(212);
        }

        public void EnterDfuMode()
        {
            FetchEndpointSync<byte>(214);
        }

        ushort originSchemaChecksum = 47683;
    }
}