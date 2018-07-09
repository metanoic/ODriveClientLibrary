namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceCan : RemoteObject
    {
        public DeviceCan(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private byte nodeId;
        public byte NodeId
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(238);
                this.RaiseAndSetIfChanged(ref nodeId, result);
                return nodeId;
            }
        }

        private uint txMailboxCompleteCallbackCnt;
        public uint TxMailboxCompleteCallbackCnt
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(239);
                this.RaiseAndSetIfChanged(ref txMailboxCompleteCallbackCnt, result);
                return txMailboxCompleteCallbackCnt;
            }
        }

        private uint txMailboxAbortCallbackCnt;
        public uint TxMailboxAbortCallbackCnt
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(240);
                this.RaiseAndSetIfChanged(ref txMailboxAbortCallbackCnt, result);
                return txMailboxAbortCallbackCnt;
            }
        }

        private uint receivedMsgCnt;
        public uint ReceivedMsgCnt
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(241);
                this.RaiseAndSetIfChanged(ref receivedMsgCnt, result);
                return receivedMsgCnt;
            }
        }

        private uint receivedAck;
        public uint ReceivedAck
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(242);
                this.RaiseAndSetIfChanged(ref receivedAck, result);
                return receivedAck;
            }
        }

        private uint unexpectedErrors;
        public uint UnexpectedErrors
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(243);
                this.RaiseAndSetIfChanged(ref unexpectedErrors, result);
                return unexpectedErrors;
            }
        }

        private uint unhandledMessages;
        public uint UnhandledMessages
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(244);
                this.RaiseAndSetIfChanged(ref unhandledMessages, result);
                return unhandledMessages;
            }
        }
    }
}