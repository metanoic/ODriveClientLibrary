namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorConfig : RemoteObject
    {
        public MotorConfig(Device device): base(device)
        {
        }

        private bool preCalibrated;
        public bool PreCalibrated
        {
            get
            {
                var result = device.FetchEndpointSync<bool>(79);
                this.RaiseAndSetIfChanged(ref preCalibrated, result);
                return preCalibrated;
            }

            private set
            {
                device.FetchEndpointSync<bool>(79, value);
                this.RaiseAndSetIfChanged(ref preCalibrated, value);
            }
        }

        private int polePairs;
        public int PolePairs
        {
            get
            {
                var result = device.FetchEndpointSync<int>(80);
                this.RaiseAndSetIfChanged(ref polePairs, result);
                return polePairs;
            }

            private set
            {
                device.FetchEndpointSync<int>(80, value);
                this.RaiseAndSetIfChanged(ref polePairs, value);
            }
        }

        private float calibrationCurrent;
        public float CalibrationCurrent
        {
            get
            {
                var result = device.FetchEndpointSync<float>(81);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, result);
                return calibrationCurrent;
            }

            private set
            {
                device.FetchEndpointSync<float>(81, value);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, value);
            }
        }

        private float resistanceCalibMaxVoltage;
        public float ResistanceCalibMaxVoltage
        {
            get
            {
                var result = device.FetchEndpointSync<float>(82);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, result);
                return resistanceCalibMaxVoltage;
            }

            private set
            {
                device.FetchEndpointSync<float>(82, value);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, value);
            }
        }

        private float phaseInductance;
        public float PhaseInductance
        {
            get
            {
                var result = device.FetchEndpointSync<float>(83);
                this.RaiseAndSetIfChanged(ref phaseInductance, result);
                return phaseInductance;
            }

            private set
            {
                device.FetchEndpointSync<float>(83, value);
                this.RaiseAndSetIfChanged(ref phaseInductance, value);
            }
        }

        private float phaseResistance;
        public float PhaseResistance
        {
            get
            {
                var result = device.FetchEndpointSync<float>(84);
                this.RaiseAndSetIfChanged(ref phaseResistance, result);
                return phaseResistance;
            }

            private set
            {
                device.FetchEndpointSync<float>(84, value);
                this.RaiseAndSetIfChanged(ref phaseResistance, value);
            }
        }

        private int direction;
        public int Direction
        {
            get
            {
                var result = device.FetchEndpointSync<int>(85);
                this.RaiseAndSetIfChanged(ref direction, result);
                return direction;
            }

            private set
            {
                device.FetchEndpointSync<int>(85, value);
                this.RaiseAndSetIfChanged(ref direction, value);
            }
        }

        private byte motorType;
        public byte MotorType
        {
            get
            {
                var result = device.FetchEndpointSync<byte>(86);
                this.RaiseAndSetIfChanged(ref motorType, result);
                return motorType;
            }

            private set
            {
                device.FetchEndpointSync<byte>(86, value);
                this.RaiseAndSetIfChanged(ref motorType, value);
            }
        }

        private float currentLim;
        public float CurrentLim
        {
            get
            {
                var result = device.FetchEndpointSync<float>(87);
                this.RaiseAndSetIfChanged(ref currentLim, result);
                return currentLim;
            }

            private set
            {
                device.FetchEndpointSync<float>(87, value);
                this.RaiseAndSetIfChanged(ref currentLim, value);
            }
        }

        private float requestedCurrentRange;
        public float RequestedCurrentRange
        {
            get
            {
                var result = device.FetchEndpointSync<float>(88);
                this.RaiseAndSetIfChanged(ref requestedCurrentRange, result);
                return requestedCurrentRange;
            }

            private set
            {
                device.FetchEndpointSync<float>(88, value);
                this.RaiseAndSetIfChanged(ref requestedCurrentRange, value);
            }
        }
    }
}