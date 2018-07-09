namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorGateDriver : RemoteObject
    {
        public MotorGateDriver(Device device): base(device)
        {
        }

        private ushort drvFault;
        public ushort DrvFault
        {
            get
            {
                var result = device.FetchEndpointSync<ushort>(69);
                this.RaiseAndSetIfChanged(ref drvFault, result);
                return drvFault;
            }
        }
    }
}