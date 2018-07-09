namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorGateDriver : RemoteObject
    {
        public MotorGateDriver(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private ushort drvFault;
        public ushort DrvFault
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(69);
                this.RaiseAndSetIfChanged(ref drvFault, result);
                return drvFault;
            }
        }
    }
}