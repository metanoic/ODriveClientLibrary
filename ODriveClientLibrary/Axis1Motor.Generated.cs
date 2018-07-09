namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Motor : RemoteObject
    {
        public Axis1Motor(Device ODriveDevice): base(ODriveDevice)
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
                var result = ODriveDevice.FetchEndpointSync<ushort>(153);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(153, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private byte armedState;
        public byte ArmedState
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(154);
                this.RaiseAndSetIfChanged(ref armedState, result);
                return armedState;
            }
        }

        private bool isCalibrated;
        public bool IsCalibrated
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(155);
                this.RaiseAndSetIfChanged(ref isCalibrated, result);
                return isCalibrated;
            }
        }

        private float currentMeasPhB;
        public float CurrentMeasPhB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(156);
                this.RaiseAndSetIfChanged(ref currentMeasPhB, result);
                return currentMeasPhB;
            }
        }

        private float currentMeasPhC;
        public float CurrentMeasPhC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(157);
                this.RaiseAndSetIfChanged(ref currentMeasPhC, result);
                return currentMeasPhC;
            }
        }

        private float dCCalibPhB;
        public float DCCalibPhB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(158);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, result);
                return dCCalibPhB;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(158, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhB, value);
            }
        }

        private float dCCalibPhC;
        public float DCCalibPhC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(159);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, result);
                return dCCalibPhC;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(159, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhC, value);
            }
        }

        private float phaseCurrentRevGain;
        public float PhaseCurrentRevGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(160);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, result);
                return phaseCurrentRevGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(160, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseCurrentRevGain, value);
            }
        }
    }
}