namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceAxis0 : RemoteObject
    {
        public DeviceAxis0(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new Axis0Config(ODriveDevice);
            Motor = new Axis0Motor(ODriveDevice);
            Controller = new Axis0Controller(ODriveDevice);
            Encoder = new Axis0Encoder(ODriveDevice);
            SensorlessEstimator = new Axis0SensorlessEstimator(ODriveDevice);
        }

        public Axis0Config Config
        {
            get;
            private set;
        }

        public Axis0Motor Motor
        {
            get;
            private set;
        }

        public Axis0Controller Controller
        {
            get;
            private set;
        }

        public Axis0Encoder Encoder
        {
            get;
            private set;
        }

        public Axis0SensorlessEstimator SensorlessEstimator
        {
            get;
            private set;
        }

        private ushort error;
        public ushort Error
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(34);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(34, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(35);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(35, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private byte currentState;
        public byte CurrentState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(36);
                this.RaiseAndSetIfChanged(ref currentState, result);
                return currentState;
            }
        }

        private byte requestedState;
        public byte RequestedState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(37);
                this.RaiseAndSetIfChanged(ref requestedState, result);
                return requestedState;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(37, value);
                ODriveDevice.RaiseAndSetIfChanged(ref requestedState, value);
            }
        }

        private uint loopCounter;
        public uint LoopCounter
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(38);
                this.RaiseAndSetIfChanged(ref loopCounter, result);
                return loopCounter;
            }
        }
    }
}