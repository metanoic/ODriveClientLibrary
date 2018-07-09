namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceAxis1 : RemoteObject
    {
        public DeviceAxis1(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new Axis1Config(ODriveDevice);
            Motor = new Axis1Motor(ODriveDevice);
            Controller = new Axis1Controller(ODriveDevice);
            Encoder = new Axis1Encoder(ODriveDevice);
            SensorlessEstimator = new Axis1SensorlessEstimator(ODriveDevice);
        }

        public Axis1Config Config
        {
            get;
            private set;
        }

        public Axis1Motor Motor
        {
            get;
            private set;
        }

        public Axis1Controller Controller
        {
            get;
            private set;
        }

        public Axis1Encoder Encoder
        {
            get;
            private set;
        }

        public Axis1SensorlessEstimator SensorlessEstimator
        {
            get;
            private set;
        }

        private ushort error;
        public ushort Error
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(136);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(136, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(137);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(137, value);
                ODriveDevice.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private byte currentState;
        public byte CurrentState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(138);
                this.RaiseAndSetIfChanged(ref currentState, result);
                return currentState;
            }
        }

        private byte requestedState;
        public byte RequestedState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(139);
                this.RaiseAndSetIfChanged(ref requestedState, result);
                return requestedState;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(139, value);
                ODriveDevice.RaiseAndSetIfChanged(ref requestedState, value);
            }
        }

        private uint loopCounter;
        public uint LoopCounter
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(140);
                this.RaiseAndSetIfChanged(ref loopCounter, result);
                return loopCounter;
            }
        }
    }
}