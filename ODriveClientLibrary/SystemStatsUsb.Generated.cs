namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class SystemStatsUsb : RemoteObject
    {
        public SystemStatsUsb(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private uint rxCnt;
        public uint RxCnt
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(21);
                this.RaiseAndSetIfChanged(ref rxCnt, result);
                return rxCnt;
            }
        }

        private uint txCnt;
        public uint TxCnt
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(22);
                this.RaiseAndSetIfChanged(ref txCnt, result);
                return txCnt;
            }
        }

        private uint txOverrunCnt;
        public uint TxOverrunCnt
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(23);
                this.RaiseAndSetIfChanged(ref txOverrunCnt, result);
                return txOverrunCnt;
            }
        }
    }
}