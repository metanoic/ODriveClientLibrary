namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class SystemStatsI2c : RemoteObject
    {
        public SystemStatsI2c(Connection connection): base(connection)
        {
        }

        private byte addr;
        public byte Addr
        {
            get
            {
                var result = FetchEndpointSync<byte>(24);
                this.RaiseAndSetIfChanged(ref addr, result);
                return addr;
            }
        }

        private uint addrMatchCnt;
        public uint AddrMatchCnt
        {
            get
            {
                var result = FetchEndpointSync<uint>(25);
                this.RaiseAndSetIfChanged(ref addrMatchCnt, result);
                return addrMatchCnt;
            }
        }

        private uint rxCnt;
        public uint RxCnt
        {
            get
            {
                var result = FetchEndpointSync<uint>(26);
                this.RaiseAndSetIfChanged(ref rxCnt, result);
                return rxCnt;
            }
        }

        private uint errorCnt;
        public uint ErrorCnt
        {
            get
            {
                var result = FetchEndpointSync<uint>(27);
                this.RaiseAndSetIfChanged(ref errorCnt, result);
                return errorCnt;
            }
        }
    }
}