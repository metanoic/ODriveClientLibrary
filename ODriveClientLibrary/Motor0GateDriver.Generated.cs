namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor0GateDriver : RemoteObject
    {
        public Motor0GateDriver(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private int drvFault;
        public int DrvFault
        {
            get
            {
                var result = FetchEndpointSync<int>(58);
                this.RaiseAndSetIfChanged(ref drvFault, result);
                return drvFault;
            }

            set
            {
                SetPropertySync<int>(58, value);
                ODriveDevice.RaiseAndSetIfChanged(ref drvFault, value);
            }
        }

        private ushort statusReg1;
        public ushort StatusReg1
        {
            get
            {
                var result = FetchEndpointSync<ushort>(59);
                this.RaiseAndSetIfChanged(ref statusReg1, result);
                return statusReg1;
            }

            set
            {
                SetPropertySync<ushort>(59, value);
                ODriveDevice.RaiseAndSetIfChanged(ref statusReg1, value);
            }
        }

        private ushort statusReg2;
        public ushort StatusReg2
        {
            get
            {
                var result = FetchEndpointSync<ushort>(60);
                this.RaiseAndSetIfChanged(ref statusReg2, result);
                return statusReg2;
            }

            set
            {
                SetPropertySync<ushort>(60, value);
                ODriveDevice.RaiseAndSetIfChanged(ref statusReg2, value);
            }
        }

        private ushort ctrlReg1;
        public ushort CtrlReg1
        {
            get
            {
                var result = FetchEndpointSync<ushort>(61);
                this.RaiseAndSetIfChanged(ref ctrlReg1, result);
                return ctrlReg1;
            }

            set
            {
                SetPropertySync<ushort>(61, value);
                ODriveDevice.RaiseAndSetIfChanged(ref ctrlReg1, value);
            }
        }

        private ushort ctrlReg2;
        public ushort CtrlReg2
        {
            get
            {
                var result = FetchEndpointSync<ushort>(62);
                this.RaiseAndSetIfChanged(ref ctrlReg2, result);
                return ctrlReg2;
            }

            set
            {
                SetPropertySync<ushort>(62, value);
                ODriveDevice.RaiseAndSetIfChanged(ref ctrlReg2, value);
            }
        }
    }
}