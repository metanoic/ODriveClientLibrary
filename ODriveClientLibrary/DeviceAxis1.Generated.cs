namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceAxis1 : RemoteObject
    {
        public DeviceAxis1(Device device): base(device)
        {
            Config = new Axis1Config(device);
            Motor = new Axis1Motor(device);
            Controller = new Axis1Controller(device);
            Encoder = new Axis1Encoder(device);
            SensorlessEstimator = new Axis1SensorlessEstimator(device);
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
                var result = device.FetchEndpointSync<ushort>(136);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                device.FetchEndpointSync<ushort>(136, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(137);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            set
            {
                device.FetchEndpointSync<bool>(137, value);
                this.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private byte currentState;
        public byte CurrentState
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(138);
                this.RaiseAndSetIfChanged(ref currentState, result);
                return currentState;
            }
        }

        private byte requestedState;
        public byte RequestedState
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(139);
                this.RaiseAndSetIfChanged(ref requestedState, result);
                return requestedState;
            }

            set
            {
                device.FetchEndpointSync<byte>(139, value);
                this.RaiseAndSetIfChanged(ref requestedState, value);
            }
        }

        private uint loopCounter;
        public uint LoopCounter
        {
            get
            {
                var result = device.FetchEndpointSync<uint>(140);
                this.RaiseAndSetIfChanged(ref loopCounter, result);
                return loopCounter;
            }
        }
    }
}