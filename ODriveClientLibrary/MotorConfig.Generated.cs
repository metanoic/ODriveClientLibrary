namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorConfig : RemoteObject
    {
        public MotorConfig(Connection connection): base(connection)
        {
        }

        private bool preCalibrated;
        public bool PreCalibrated
        {
            get
            {
                var result = FetchEndpointSync<bool>(79);
                this.RaiseAndSetIfChanged(ref preCalibrated, result);
                return preCalibrated;
            }

            private set
            {
                FetchEndpointSync<bool>(79, value);
                this.RaiseAndSetIfChanged(ref preCalibrated, value);
            }
        }

        private int polePairs;
        public int PolePairs
        {
            get
            {
                var result = FetchEndpointSync<int>(80);
                this.RaiseAndSetIfChanged(ref polePairs, result);
                return polePairs;
            }

            private set
            {
                FetchEndpointSync<int>(80, value);
                this.RaiseAndSetIfChanged(ref polePairs, value);
            }
        }

        private float calibrationCurrent;
        public float CalibrationCurrent
        {
            get
            {
                var result = FetchEndpointSync<float>(81);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, result);
                return calibrationCurrent;
            }

            private set
            {
                FetchEndpointSync<float>(81, value);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, value);
            }
        }

        private float resistanceCalibMaxVoltage;
        public float ResistanceCalibMaxVoltage
        {
            get
            {
                var result = FetchEndpointSync<float>(82);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, result);
                return resistanceCalibMaxVoltage;
            }

            private set
            {
                FetchEndpointSync<float>(82, value);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, value);
            }
        }

        private float phaseInductance;
        public float PhaseInductance
        {
            get
            {
                var result = FetchEndpointSync<float>(83);
                this.RaiseAndSetIfChanged(ref phaseInductance, result);
                return phaseInductance;
            }

            private set
            {
                FetchEndpointSync<float>(83, value);
                this.RaiseAndSetIfChanged(ref phaseInductance, value);
            }
        }

        private float phaseResistance;
        public float PhaseResistance
        {
            get
            {
                var result = FetchEndpointSync<float>(84);
                this.RaiseAndSetIfChanged(ref phaseResistance, result);
                return phaseResistance;
            }

            private set
            {
                FetchEndpointSync<float>(84, value);
                this.RaiseAndSetIfChanged(ref phaseResistance, value);
            }
        }

        private int direction;
        public int Direction
        {
            get
            {
                var result = FetchEndpointSync<int>(85);
                this.RaiseAndSetIfChanged(ref direction, result);
                return direction;
            }

            private set
            {
                FetchEndpointSync<int>(85, value);
                this.RaiseAndSetIfChanged(ref direction, value);
            }
        }

        private byte motorType;
        public byte MotorType
        {
            get
            {
                var result = FetchEndpointSync<byte>(86);
                this.RaiseAndSetIfChanged(ref motorType, result);
                return motorType;
            }

            private set
            {
                FetchEndpointSync<byte>(86, value);
                this.RaiseAndSetIfChanged(ref motorType, value);
            }
        }

        private float currentLim;
        public float CurrentLim
        {
            get
            {
                var result = FetchEndpointSync<float>(87);
                this.RaiseAndSetIfChanged(ref currentLim, result);
                return currentLim;
            }

            private set
            {
                FetchEndpointSync<float>(87, value);
                this.RaiseAndSetIfChanged(ref currentLim, value);
            }
        }

        private float requestedCurrentRange;
        public float RequestedCurrentRange
        {
            get
            {
                var result = FetchEndpointSync<float>(88);
                this.RaiseAndSetIfChanged(ref requestedCurrentRange, result);
                return requestedCurrentRange;
            }

            private set
            {
                FetchEndpointSync<float>(88, value);
                this.RaiseAndSetIfChanged(ref requestedCurrentRange, value);
            }
        }
    }
}