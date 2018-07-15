namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor1Config : RemoteObject
    {
        public Motor1Config(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private int controlMode;
        public int ControlMode
        {
            get
            {
                var result = FetchEndpointSync<int>(116);
                this.RaiseAndSetIfChanged(ref controlMode, result);
                return controlMode;
            }

            set
            {
                SetPropertySync<int>(116, value);
                ODriveDevice.RaiseAndSetIfChanged(ref controlMode, value);
            }
        }

        private float countsPerStep;
        public float CountsPerStep
        {
            get
            {
                var result = FetchEndpointSync<float>(117);
                this.RaiseAndSetIfChanged(ref countsPerStep, result);
                return countsPerStep;
            }

            set
            {
                SetPropertySync<float>(117, value);
                ODriveDevice.RaiseAndSetIfChanged(ref countsPerStep, value);
            }
        }

        private int polePairs;
        public int PolePairs
        {
            get
            {
                var result = FetchEndpointSync<int>(118);
                this.RaiseAndSetIfChanged(ref polePairs, result);
                return polePairs;
            }

            set
            {
                SetPropertySync<int>(118, value);
                ODriveDevice.RaiseAndSetIfChanged(ref polePairs, value);
            }
        }

        private float posGain;
        public float PosGain
        {
            get
            {
                var result = FetchEndpointSync<float>(119);
                this.RaiseAndSetIfChanged(ref posGain, result);
                return posGain;
            }

            set
            {
                SetPropertySync<float>(119, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posGain, value);
            }
        }

        private float velGain;
        public float VelGain
        {
            get
            {
                var result = FetchEndpointSync<float>(120);
                this.RaiseAndSetIfChanged(ref velGain, result);
                return velGain;
            }

            set
            {
                SetPropertySync<float>(120, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velGain, value);
            }
        }

        private float velIntegratorGain;
        public float VelIntegratorGain
        {
            get
            {
                var result = FetchEndpointSync<float>(121);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, result);
                return velIntegratorGain;
            }

            set
            {
                SetPropertySync<float>(121, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorGain, value);
            }
        }

        private float velLimit;
        public float VelLimit
        {
            get
            {
                var result = FetchEndpointSync<float>(122);
                this.RaiseAndSetIfChanged(ref velLimit, result);
                return velLimit;
            }

            set
            {
                SetPropertySync<float>(122, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velLimit, value);
            }
        }

        private float calibrationCurrent;
        public float CalibrationCurrent
        {
            get
            {
                var result = FetchEndpointSync<float>(123);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, result);
                return calibrationCurrent;
            }

            set
            {
                SetPropertySync<float>(123, value);
                ODriveDevice.RaiseAndSetIfChanged(ref calibrationCurrent, value);
            }
        }

        private float resistanceCalibMaxVoltage;
        public float ResistanceCalibMaxVoltage
        {
            get
            {
                var result = FetchEndpointSync<float>(124);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, result);
                return resistanceCalibMaxVoltage;
            }

            set
            {
                SetPropertySync<float>(124, value);
                ODriveDevice.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, value);
            }
        }

        private float phaseInductance;
        public float PhaseInductance
        {
            get
            {
                var result = FetchEndpointSync<float>(125);
                this.RaiseAndSetIfChanged(ref phaseInductance, result);
                return phaseInductance;
            }

            set
            {
                SetPropertySync<float>(125, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseInductance, value);
            }
        }

        private float phaseResistance;
        public float PhaseResistance
        {
            get
            {
                var result = FetchEndpointSync<float>(126);
                this.RaiseAndSetIfChanged(ref phaseResistance, result);
                return phaseResistance;
            }

            set
            {
                SetPropertySync<float>(126, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseResistance, value);
            }
        }

        private int motorType;
        public int MotorType
        {
            get
            {
                var result = FetchEndpointSync<int>(127);
                this.RaiseAndSetIfChanged(ref motorType, result);
                return motorType;
            }

            set
            {
                SetPropertySync<int>(127, value);
                ODriveDevice.RaiseAndSetIfChanged(ref motorType, value);
            }
        }

        private int rotorMode;
        public int RotorMode
        {
            get
            {
                var result = FetchEndpointSync<int>(128);
                this.RaiseAndSetIfChanged(ref rotorMode, result);
                return rotorMode;
            }

            set
            {
                SetPropertySync<int>(128, value);
                ODriveDevice.RaiseAndSetIfChanged(ref rotorMode, value);
            }
        }
    }
}