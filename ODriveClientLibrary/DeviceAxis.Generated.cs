namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceAxis : RemoteObject
    {
        public DeviceAxis(Connection connection): base(connection)
        {
            Config = new AxisConfig(connection);
            Motor = new AxisMotor(connection);
            Controller = new AxisController(connection);
            Encoder = new AxisEncoder(connection);
            SensorlessEstimator = new AxisSensorlessEstimator(connection);
        }

        public AxisConfig Config
        {
            get;
            private set;
        }

        public AxisMotor Motor
        {
            get;
            private set;
        }

        public AxisController Controller
        {
            get;
            private set;
        }

        public AxisEncoder Encoder
        {
            get;
            private set;
        }

        public AxisSensorlessEstimator SensorlessEstimator
        {
            get;
            private set;
        }

        private ushort error;
        public ushort Error
        {
            get
            {
                var result = FetchEndpointSync<ushort>(34);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            private set
            {
                FetchEndpointSync<ushort>(34, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private bool enableStepDir;
        public bool EnableStepDir
        {
            get
            {
                var result = FetchEndpointSync<bool>(35);
                this.RaiseAndSetIfChanged(ref enableStepDir, result);
                return enableStepDir;
            }

            private set
            {
                FetchEndpointSync<bool>(35, value);
                this.RaiseAndSetIfChanged(ref enableStepDir, value);
            }
        }

        private byte currentState;
        public byte CurrentState
        {
            get
            {
                var result = FetchEndpointSync<byte>(36);
                this.RaiseAndSetIfChanged(ref currentState, result);
                return currentState;
            }
        }

        private byte requestedState;
        public byte RequestedState
        {
            get
            {
                var result = FetchEndpointSync<byte>(37);
                this.RaiseAndSetIfChanged(ref requestedState, result);
                return requestedState;
            }

            private set
            {
                FetchEndpointSync<byte>(37, value);
                this.RaiseAndSetIfChanged(ref requestedState, value);
            }
        }

        private uint loopCounter;
        public uint LoopCounter
        {
            get
            {
                var result = FetchEndpointSync<uint>(38);
                this.RaiseAndSetIfChanged(ref loopCounter, result);
                return loopCounter;
            }
        }
    }
}