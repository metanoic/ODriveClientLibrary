namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class DeviceMotor1 : RemoteObject
    {
        public DeviceMotor1(Device ODriveDevice): base(ODriveDevice)
        {
            Config = new Motor1Config(ODriveDevice);
            CurrentControl = new Motor1CurrentControl(ODriveDevice);
            GateDriver = new Motor1GateDriver(ODriveDevice);
            Encoder = new Motor1Encoder(ODriveDevice);
            TimingLog = new Motor1TimingLog(ODriveDevice);
        }

        public Motor1Config Config
        {
            get;
            private set;
        }

        public Motor1CurrentControl CurrentControl
        {
            get;
            private set;
        }

        public Motor1GateDriver GateDriver
        {
            get;
            private set;
        }

        public Motor1Encoder Encoder
        {
            get;
            private set;
        }

        public Motor1TimingLog TimingLog
        {
            get;
            private set;
        }

        private int error;
        public int Error
        {
            get
            {
                var result = FetchEndpointSync<int>(130);
                this.RaiseAndSetIfChanged(ref error, result);
                return error;
            }

            set
            {
                SetPropertySync<int>(130, value);
                ODriveDevice.RaiseAndSetIfChanged(ref error, value);
            }
        }

        private float posSetpoint;
        public float PosSetpoint
        {
            get
            {
                var result = FetchEndpointSync<float>(131);
                this.RaiseAndSetIfChanged(ref posSetpoint, result);
                return posSetpoint;
            }

            set
            {
                SetPropertySync<float>(131, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posSetpoint, value);
            }
        }

        private float velSetpoint;
        public float VelSetpoint
        {
            get
            {
                var result = FetchEndpointSync<float>(132);
                this.RaiseAndSetIfChanged(ref velSetpoint, result);
                return velSetpoint;
            }

            set
            {
                SetPropertySync<float>(132, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velSetpoint, value);
            }
        }

        private float velIntegratorCurrent;
        public float VelIntegratorCurrent
        {
            get
            {
                var result = FetchEndpointSync<float>(133);
                this.RaiseAndSetIfChanged(ref velIntegratorCurrent, result);
                return velIntegratorCurrent;
            }

            set
            {
                SetPropertySync<float>(133, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorCurrent, value);
            }
        }

        private float currentSetpoint;
        public float CurrentSetpoint
        {
            get
            {
                var result = FetchEndpointSync<float>(134);
                this.RaiseAndSetIfChanged(ref currentSetpoint, result);
                return currentSetpoint;
            }

            set
            {
                SetPropertySync<float>(134, value);
                ODriveDevice.RaiseAndSetIfChanged(ref currentSetpoint, value);
            }
        }

        private float currentMeasPhB;
        public float CurrentMeasPhB
        {
            get
            {
                var result = FetchEndpointSync<float>(135);
                this.RaiseAndSetIfChanged(ref currentMeasPhB, result);
                return currentMeasPhB;
            }
        }

        private float currentMeasPhC;
        public float CurrentMeasPhC
        {
            get
            {
                var result = FetchEndpointSync<float>(136);
                this.RaiseAndSetIfChanged(ref currentMeasPhC, result);
                return currentMeasPhC;
            }
        }

        private float dCCalibPhB;
        public float DCCalibPhB
        {
            get
            {
                var result = FetchEndpointSync<float>(137);
                this.RaiseAndSetIfChanged(ref dCCalibPhB, result);
                return dCCalibPhB;
            }

            set
            {
                SetPropertySync<float>(137, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhB, value);
            }
        }

        private float dCCalibPhC;
        public float DCCalibPhC
        {
            get
            {
                var result = FetchEndpointSync<float>(138);
                this.RaiseAndSetIfChanged(ref dCCalibPhC, result);
                return dCCalibPhC;
            }

            set
            {
                SetPropertySync<float>(138, value);
                ODriveDevice.RaiseAndSetIfChanged(ref dCCalibPhC, value);
            }
        }

        private float shuntConductance;
        public float ShuntConductance
        {
            get
            {
                var result = FetchEndpointSync<float>(139);
                this.RaiseAndSetIfChanged(ref shuntConductance, result);
                return shuntConductance;
            }

            set
            {
                SetPropertySync<float>(139, value);
                ODriveDevice.RaiseAndSetIfChanged(ref shuntConductance, value);
            }
        }

        private float phaseCurrentRevGain;
        public float PhaseCurrentRevGain
        {
            get
            {
                var result = FetchEndpointSync<float>(140);
                this.RaiseAndSetIfChanged(ref phaseCurrentRevGain, result);
                return phaseCurrentRevGain;
            }

            set
            {
                SetPropertySync<float>(140, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseCurrentRevGain, value);
            }
        }

        private bool threadReady;
        public bool ThreadReady
        {
            get
            {
                var result = FetchEndpointSync<bool>(141);
                this.RaiseAndSetIfChanged(ref threadReady, result);
                return threadReady;
            }

            set
            {
                SetPropertySync<bool>(141, value);
                ODriveDevice.RaiseAndSetIfChanged(ref threadReady, value);
            }
        }

        private ushort controlDeadline;
        public ushort ControlDeadline
        {
            get
            {
                var result = FetchEndpointSync<ushort>(142);
                this.RaiseAndSetIfChanged(ref controlDeadline, result);
                return controlDeadline;
            }

            set
            {
                SetPropertySync<ushort>(142, value);
                ODriveDevice.RaiseAndSetIfChanged(ref controlDeadline, value);
            }
        }

        private ushort lastCpuTime;
        public ushort LastCpuTime
        {
            get
            {
                var result = FetchEndpointSync<ushort>(143);
                this.RaiseAndSetIfChanged(ref lastCpuTime, result);
                return lastCpuTime;
            }

            set
            {
                SetPropertySync<ushort>(143, value);
                ODriveDevice.RaiseAndSetIfChanged(ref lastCpuTime, value);
            }
        }

        private uint loopCounter;
        public uint LoopCounter
        {
            get
            {
                var result = FetchEndpointSync<uint>(144);
                this.RaiseAndSetIfChanged(ref loopCounter, result);
                return loopCounter;
            }

            set
            {
                SetPropertySync<uint>(144, value);
                ODriveDevice.RaiseAndSetIfChanged(ref loopCounter, value);
            }
        }

        public void SetPosSetpoint(float pos_setpoint, float vel_feed_forward, float current_feed_forward)
        {
            FetchEndpointSync<float>(183, pos_setpoint);
            FetchEndpointSync<float>(184, vel_feed_forward);
            FetchEndpointSync<float>(185, current_feed_forward);
            FetchEndpointSync<byte>(182);
        }

        public void SetVelSetpoint(float vel_setpoint, float current_feed_forward)
        {
            FetchEndpointSync<float>(188, vel_setpoint);
            FetchEndpointSync<float>(189, current_feed_forward);
            FetchEndpointSync<byte>(187);
        }

        public void SetCurrentSetpoint(float current_setpoint)
        {
            FetchEndpointSync<float>(192, current_setpoint);
            FetchEndpointSync<byte>(191);
        }
    }
}