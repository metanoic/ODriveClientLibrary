namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Axis1Motor : RemoteObject
    {
        public Axis1Motor(Device device): base(device)
        {
            CurrentControl = new MotorCurrentControl(device);
            GateDriver = new MotorGateDriver(device);
            TimingLog = new MotorTimingLog(device);
            Config = new MotorConfig(device);
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
                var result = device.FetchEndpointSync<ushort>(153);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                device.FetchEndpointSync<ushort>(153, value);
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private byte armedState;
        public byte ArmedState
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(154);
                this.RaiseAndSetIfChanged(ref armedState, result);
                return armedState;
            }
        }

        private bool isCalibrated;
        public bool IsCalibrated
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(155);
                this.RaiseAndSetIfChanged(ref isCalibrated, result);
                return isCalibrated;
            }
        }

        private float currentMeasPhB;
        public float CurrentMeasPhB
        {
            get
            {
                var result = device.FetchEndpointSync<float>(156);
                this.RaiseAndSetIfChanged(ref currentMeasPhB, result);
                return currentMeasPhB;
            }
        }

        private float currentMeasPhC;
        public float CurrentMeasPhC
        {
            get
            {
                var result = device.FetchEndpointSync<float>(157);
                this.RaiseAndSetIfChanged(ref currentMeasPhC, result);
                return currentMeasPhC;
            }
        }

        private float dCCalibPhB;
        public float DCCalibPhB
        {
            get
            {
                var result = device.FetchEndpointSync<float>(158);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, result);
                return dCCalibPhB;
            }

            set
            {
                device.FetchEndpointSync<float>(158, value);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, value);
            }
        }

        private float dCCalibPhC;
        public float DCCalibPhC
        {
            get
            {
                var result = device.FetchEndpointSync<float>(159);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, result);
                return dCCalibPhC;
            }

            set
            {
                device.FetchEndpointSync<float>(159, value);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, value);
            }
        }

        private float phaseCurrentRevGain;
        public float PhaseCurrentRevGain
        {
            get
            {
                var result = device.FetchEndpointSync<float>(160);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, result);
                return phaseCurrentRevGain;
            }

            set
            {
                device.FetchEndpointSync<float>(160, value);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, value);
            }
        }
    }
}