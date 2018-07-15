namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor1GateDriver : RemoteObject
    {
        public Motor1GateDriver(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private int drvFault;
        public int DrvFault
        {
            get
            {
                var result = FetchEndpointSync<int>(158);
                this.RaiseAndSetIfChanged(ref drvFault, result);
                return drvFault;
            }

            set
            {
                SetPropertySync<int>(158, value);
                ODriveDevice.RaiseAndSetIfChanged(ref drvFault, value);
            }
        }

        private ushort statusReg1;
        public ushort StatusReg1
        {
            get
            {
                var result = FetchEndpointSync<ushort>(159);
                this.RaiseAndSetIfChanged(ref statusReg1, result);
                return statusReg1;
            }

            set
            {
                SetPropertySync<ushort>(159, value);
                ODriveDevice.RaiseAndSetIfChanged(ref statusReg1, value);
            }
        }

        private ushort statusReg2;
        public ushort StatusReg2
        {
            get
            {
                var result = FetchEndpointSync<ushort>(160);
                this.RaiseAndSetIfChanged(ref statusReg2, result);
                return statusReg2;
            }

            set
            {
                SetPropertySync<ushort>(160, value);
                ODriveDevice.RaiseAndSetIfChanged(ref statusReg2, value);
            }
        }

        private ushort ctrlReg1;
        public ushort CtrlReg1
        {
            get
            {
                var result = FetchEndpointSync<ushort>(161);
                this.RaiseAndSetIfChanged(ref ctrlReg1, result);
                return ctrlReg1;
            }

            set
            {
                SetPropertySync<ushort>(161, value);
                ODriveDevice.RaiseAndSetIfChanged(ref ctrlReg1, value);
            }
        }

        private ushort ctrlReg2;
        public ushort CtrlReg2
        {
            get
            {
                var result = FetchEndpointSync<ushort>(162);
                this.RaiseAndSetIfChanged(ref ctrlReg2, result);
                return ctrlReg2;
            }

            set
            {
                SetPropertySync<ushort>(162, value);
                ODriveDevice.RaiseAndSetIfChanged(ref ctrlReg2, value);
            }
        }
    }
}