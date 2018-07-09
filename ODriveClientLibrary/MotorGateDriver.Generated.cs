namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorGateDriver : RemoteObject
    {
        public MotorGateDriver(Connection connection): base(connection)
        {
        }

        private ushort drvFault;
        public ushort DrvFault
        {
            get
            {
                var result = FetchEndpointSync<ushort>(69);
                this.RaiseAndSetIfChanged(ref drvFault, result);
                return drvFault;
            }
        }
    }
}