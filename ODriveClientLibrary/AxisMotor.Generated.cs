namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class AxisMotor : RemoteObject
    {
        public AxisMotor(Connection connection): base(connection)
        {
            CurrentControl = new MotorCurrentControl(connection);
            GateDriver = new MotorGateDriver(connection);
            TimingLog = new MotorTimingLog(connection);
            Config = new MotorConfig(connection);
        }

        public MotorCurrentControl CurrentControl
        {
            get;
            private set;
        }

        public MotorGateDriver GateDriver
        {
            get;
            private set;
        }

        public MotorTimingLog TimingLog
        {
            get;
            private set;
        }

        public MotorConfig Config
        {
            get;
            private set;
        }

        private ushort error;
        public ushort Error
        {
            get
            {
                var result = FetchEndpointSync<ushort>(51);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            private set
            {
                FetchEndpointSync<ushort>(51, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private byte armedState;
        public byte ArmedState
        {
            get
            {
                var result = FetchEndpointSync<byte>(52);
                this.RaiseAndSetIfChanged(ref armedState, result);
                return armedState;
            }
        }

        private bool isCalibrated;
        public bool IsCalibrated
        {
            get
            {
                var result = FetchEndpointSync<bool>(53);
                this.RaiseAndSetIfChanged(ref isCalibrated, result);
                return isCalibrated;
            }
        }

        private float currentMeasPhB;
        public float CurrentMeasPhB
        {
            get
            {
                var result = FetchEndpointSync<float>(54);
                this.RaiseAndSetIfChanged(ref currentMeasPhB, result);
                return currentMeasPhB;
            }
        }

        private float currentMeasPhC;
        public float CurrentMeasPhC
        {
            get
            {
                var result = FetchEndpointSync<float>(55);
                this.RaiseAndSetIfChanged(ref currentMeasPhC, result);
                return currentMeasPhC;
            }
        }

        private float dCCalibPhB;
        public float DCCalibPhB
        {
            get
            {
                var result = FetchEndpointSync<float>(56);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, result);
                return dCCalibPhB;
            }

            private set
            {
                FetchEndpointSync<float>(56, value);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, value);
            }
        }

        private float dCCalibPhC;
        public float DCCalibPhC
        {
            get
            {
                var result = FetchEndpointSync<float>(57);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, result);
                return dCCalibPhC;
            }

            private set
            {
                FetchEndpointSync<float>(57, value);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, value);
            }
        }

        private float phaseCurrentRevGain;
        public float PhaseCurrentRevGain
        {
            get
            {
                var result = FetchEndpointSync<float>(58);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, result);
                return phaseCurrentRevGain;
            }

            private set
            {
                FetchEndpointSync<float>(58, value);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, value);
            }
        }
    }
}