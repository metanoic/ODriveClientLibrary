namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceMotor0 : RemoteObject
    {
        public DeviceMotor0(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new Motor0Config(ODriveDevice);
            CurrentControl = new Motor0CurrentControl(ODriveDevice);
            GateDriver = new Motor0GateDriver(ODriveDevice);
            Encoder = new Motor0Encoder(ODriveDevice);
            TimingLog = new Motor0TimingLog(ODriveDevice);
        }

        public Motor0Config Config
        {
            get;
            private set;
        }

        public Motor0CurrentControl CurrentControl
        {
            get;
            private set;
        }

        public Motor0GateDriver GateDriver
        {
            get;
            private set;
        }

        public Motor0Encoder Encoder
        {
            get;
            private set;
        }

        public Motor0TimingLog TimingLog
        {
            get;
            private set;
        }

        private int error;
        public int Error
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(30);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(30, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private float posSetpoint;
        public float PosSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(31);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(31, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(32);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(32, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(33);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(33, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(34);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(34, value);
                ODriveDevice.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        private float currentMeasPhB;
        public float CurrentMeasPhB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(35);
                this.RaiseAndSetIfChanged(ref currentMeasPhB, result);
                return currentMeasPhB;
            }
        }

        private float currentMeasPhC;
        public float CurrentMeasPhC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(36);
                this.RaiseAndSetIfChanged(ref currentMeasPhC, result);
                return currentMeasPhC;
            }
        }

        private float dCCalibPhB;
        public float DCCalibPhB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(37);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, result);
                return dCCalibPhB;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(37, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhB, value);
            }
        }

        private float dCCalibPhC;
        public float DCCalibPhC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(38);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, result);
                return dCCalibPhC;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(38, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhC, value);
            }
        }

        private float shuntConductance;
        public float ShuntConductance
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(39);
                this.RaiseAndSetIfChanged(ref shuntConductance, result);
                return shuntConductance;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(39, value);
                ODriveDevice.RaiseAndSetIfChanged(ref shuntConductance, value);
            }
        }

        private float phaseCurrentRevGain;
        public float PhaseCurrentRevGain
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(40);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, result);
                return phaseCurrentRevGain;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(40, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseCurrentRevGain, value);
            }
        }

        private bool threadReady;
        public bool ThreadReady
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(41);
                this.RaiseAndSetIfChanged(ref threadReady, result);
                return threadReady;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(41, value);
                ODriveDevice.RaiseAndSetIfChanged(ref threadReady, value);
            }
        }

        private ushort controlDeadline;
        public ushort ControlDeadline
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(42);
                this.RaiseAndSetIfChanged(ref controlDeadline, result);
                return controlDeadline;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(42, value);
                ODriveDevice.RaiseAndSetIfChanged(ref controlDeadline, value);
            }
        }

        private ushort lastCpuTime;
        public ushort LastCpuTime
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(43);
                this.RaiseAndSetIfChanged(ref lastCpuTime, result);
                return lastCpuTime;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(43, value);
                ODriveDevice.RaiseAndSetIfChanged(ref lastCpuTime, value);
            }
        }

        private uint loopCounter;
        public uint LoopCounter
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<uint>(44);
                this.RaiseAndSetIfChanged(ref loopCounter, result);
                return loopCounter;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<uint>(44, value);
                ODriveDevice.RaiseAndSetIfChanged(ref loopCounter, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            ODriveDevice.FetchEndpointSync<float>(83, pos_setpoint);
            ODriveDevice.FetchEndpointSync<float>(84, vel_feed_forward);
            ODriveDevice.FetchEndpointSync<float>(85, current_feed_forward);
            ODriveDevice.FetchEndpointSync<byte>(82);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            ODriveDevice.FetchEndpointSync<float>(88, vel_setpoint);
            ODriveDevice.FetchEndpointSync<float>(89, current_feed_forward);
            ODriveDevice.FetchEndpointSync<byte>(87);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            ODriveDevice.FetchEndpointSync<float>(92, current_setpoint);
            ODriveDevice.FetchEndpointSync<byte>(91);
        }
    }
}