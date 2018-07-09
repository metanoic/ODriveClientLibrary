namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceSystemStats : RemoteObject
    {
        public DeviceSystemStats(Device ODriveDevice): base(ODriveDevice)
        {
            Usb = new SystemStatsUsb(ODriveDevice);
            I2c = new SystemStatsI2c(ODriveDevice);
        }

        public SystemStatsUsb Usb
        {
            get;
            private set;
        }

        public SystemStatsI2c I2c
        {
            get;
            private set;
        }

        private uint uptime;
        public uint Uptime
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(12);
                this.RaiseAndSetIfChanged(ref uptime, result);
                return uptime;
            }
        }

        private uint minHeapSpace;
        public uint MinHeapSpace
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(13);
                this.RaiseAndSetIfChanged(ref minHeapSpace, result);
                return minHeapSpace;
            }
        }

        private uint minStackSpaceAxis0;
        public uint MinStackSpaceAxis0
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(14);
                this.RaiseAndSetIfChanged(ref minStackSpaceAxis0, result);
                return minStackSpaceAxis0;
            }
        }

        private uint minStackSpaceAxis1;
        public uint MinStackSpaceAxis1
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(15);
                this.RaiseAndSetIfChanged(ref minStackSpaceAxis1, result);
                return minStackSpaceAxis1;
            }
        }

        private uint minStackSpaceComms;
        public uint MinStackSpaceComms
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(16);
                this.RaiseAndSetIfChanged(ref minStackSpaceComms, result);
                return minStackSpaceComms;
            }
        }

        private uint minStackSpaceUsb;
        public uint MinStackSpaceUsb
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(17);
                this.RaiseAndSetIfChanged(ref minStackSpaceUsb, result);
                return minStackSpaceUsb;
            }
        }

        private uint minStackSpaceUart;
        public uint MinStackSpaceUart
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(18);
                this.RaiseAndSetIfChanged(ref minStackSpaceUart, result);
                return minStackSpaceUart;
            }
        }

        private uint minStackSpaceUsbIrq;
        public uint MinStackSpaceUsbIrq
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(19);
                this.RaiseAndSetIfChanged(ref minStackSpaceUsbIrq, result);
                return minStackSpaceUsbIrq;
            }
        }

        private uint minStackSpaceStartup;
        public uint MinStackSpaceStartup
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(20);
                this.RaiseAndSetIfChanged(ref minStackSpaceStartup, result);
                return minStackSpaceStartup;
            }
        }
    }
}