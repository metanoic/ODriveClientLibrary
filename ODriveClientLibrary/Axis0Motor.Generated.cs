namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis0Motor : RemoteObject
    {
        public Axis0Motor(Device ODriveDevice): base(ODriveDevice)
        {
            CurrentControl = new MotorCurrentControl(ODriveDevice);
            GateDriver = new MotorGateDriver(ODriveDevice);
            TimingLog = new MotorTimingLog(ODriveDevice);
            Config = new MotorConfig(ODriveDevice);
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
                var result = ODriveDevice.FetchEndpointSync<ushort>(51);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(51, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private byte armedState;
        public byte ArmedState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(52);
                this.RaiseAndSetIfChanged(ref armedState, result);
                return armedState;
            }
        }

        private bool isCalibrated;
        public bool IsCalibrated
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(53);
                this.RaiseAndSetIfChanged(ref isCalibrated, result);
                return isCalibrated;
            }
        }

        private float currentMeasPhB;
        public float CurrentMeasPhB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(54);
                this.RaiseAndSetIfChanged(ref currentMeasPhB, result);
                return currentMeasPhB;
            }
        }

        private float currentMeasPhC;
        public float CurrentMeasPhC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(55);
                this.RaiseAndSetIfChanged(ref currentMeasPhC, result);
                return currentMeasPhC;
            }
        }

        private float dCCalibPhB;
        public float DCCalibPhB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(56);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, result);
                return dCCalibPhB;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(56, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhB, value);
            }
        }

        private float dCCalibPhC;
        public float DCCalibPhC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(57);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, result);
                return dCCalibPhC;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(57, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhC, value);
            }
        }

        private float phaseCurrentRevGain;
        public float PhaseCurrentRevGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(58);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, result);
                return phaseCurrentRevGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(58, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseCurrentRevGain, value);
            }
        }
    }
}