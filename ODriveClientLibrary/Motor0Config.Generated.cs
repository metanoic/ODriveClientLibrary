namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor0Config : RemoteObject
    {
        public Motor0Config(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private int controlMode;
        public int ControlMode
        {
            get
            {
                var result = FetchEndpointSync<int>(16);
                this.RaiseAndSetIfChanged(ref controlMode, result);
                return controlMode;
            }

            set
            {
                SetPropertySync<int>(16, value);
                ODriveDevice.RaiseAndSetIfChanged(ref controlMode, value);
            }
        }

        private float countsPerStep;
        public float CountsPerStep
        {
            get
            {
                var result = FetchEndpointSync<float>(17);
                this.RaiseAndSetIfChanged(ref countsPerStep, result);
                return countsPerStep;
            }

            set
            {
                SetPropertySync<float>(17, value);
                ODriveDevice.RaiseAndSetIfChanged(ref countsPerStep, value);
            }
        }

        private int polePairs;
        public int PolePairs
        {
            get
            {
                var result = FetchEndpointSync<int>(18);
                this.RaiseAndSetIfChanged(ref polePairs, result);
                return polePairs;
            }

            set
            {
                SetPropertySync<int>(18, value);
                ODriveDevice.RaiseAndSetIfChanged(ref polePairs, value);
            }
        }

        private float posGain;
        public float PosGain
        {
            get
            {
                var result = FetchEndpointSync<float>(19);
                this.RaiseAndSetIfChanged(ref posGain, result);
                return posGain;
            }

            set
            {
                SetPropertySync<float>(19, value);
                ODriveDevice.RaiseAndSetIfChanged(ref posGain, value);
            }
        }

        private float velGain;
        public float VelGain
        {
            get
            {
                var result = FetchEndpointSync<float>(20);
                this.RaiseAndSetIfChanged(ref velGain, result);
                return velGain;
            }

            set
            {
                SetPropertySync<float>(20, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velGain, value);
            }
        }

        private float velIntegratorGain;
        public float VelIntegratorGain
        {
            get
            {
                var result = FetchEndpointSync<float>(21);
                this.RaiseAndSetIfChanged(ref velIntegratorGain, result);
                return velIntegratorGain;
            }

            set
            {
                SetPropertySync<float>(21, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velIntegratorGain, value);
            }
        }

        private float velLimit;
        public float VelLimit
        {
            get
            {
                var result = FetchEndpointSync<float>(22);
                this.RaiseAndSetIfChanged(ref velLimit, result);
                return velLimit;
            }

            set
            {
                SetPropertySync<float>(22, value);
                ODriveDevice.RaiseAndSetIfChanged(ref velLimit, value);
            }
        }

        private float calibrationCurrent;
        public float CalibrationCurrent
        {
            get
            {
                var result = FetchEndpointSync<float>(23);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, result);
                return calibrationCurrent;
            }

            set
            {
                SetPropertySync<float>(23, value);
                ODriveDevice.RaiseAndSetIfChanged(ref calibrationCurrent, value);
            }
        }

        private float resistanceCalibMaxVoltage;
        public float ResistanceCalibMaxVoltage
        {
            get
            {
                var result = FetchEndpointSync<float>(24);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, result);
                return resistanceCalibMaxVoltage;
            }

            set
            {
                SetPropertySync<float>(24, value);
                ODriveDevice.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, value);
            }
        }

        private float phaseInductance;
        public float PhaseInductance
        {
            get
            {
                var result = FetchEndpointSync<float>(25);
                this.RaiseAndSetIfChanged(ref phaseInductance, result);
                return phaseInductance;
            }

            set
            {
                SetPropertySync<float>(25, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseInductance, value);
            }
        }

        private float phaseResistance;
        public float PhaseResistance
        {
            get
            {
                var result = FetchEndpointSync<float>(26);
                this.RaiseAndSetIfChanged(ref phaseResistance, result);
                return phaseResistance;
            }

            set
            {
                SetPropertySync<float>(26, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseResistance, value);
            }
        }

        private int motorType;
        public int MotorType
        {
            get
            {
                var result = FetchEndpointSync<int>(27);
                this.RaiseAndSetIfChanged(ref motorType, result);
                return motorType;
            }

            set
            {
                SetPropertySync<int>(27, value);
                ODriveDevice.RaiseAndSetIfChanged(ref motorType, value);
            }
        }

        private int rotorMode;
        public int RotorMode
        {
            get
            {
                var result = FetchEndpointSync<int>(28);
                this.RaiseAndSetIfChanged(ref rotorMode, result);
                return rotorMode;
            }

            set
            {
                SetPropertySync<int>(28, value);
                ODriveDevice.RaiseAndSetIfChanged(ref rotorMode, value);
            }
        }
    }
}