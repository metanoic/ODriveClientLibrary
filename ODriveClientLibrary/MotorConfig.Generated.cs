namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorConfig : RemoteObject
    {
        public MotorConfig(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private bool preCalibrated;
        public bool PreCalibrated
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<bool>(79);
                this.RaiseAndSetIfChanged(ref preCalibrated, result);
                return preCalibrated;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<bool>(79, value);
                ODriveDevice.RaiseAndSetIfChanged(ref preCalibrated, value);
            }
        }

        private int polePairs;
        public int PolePairs
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(80);
                this.RaiseAndSetIfChanged(ref polePairs, result);
                return polePairs;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(80, value);
                ODriveDevice.RaiseAndSetIfChanged(ref polePairs, value);
            }
        }

        private float calibrationCurrent;
        public float CalibrationCurrent
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(81);
                this.RaiseAndSetIfChanged(ref calibrationCurrent, result);
                return calibrationCurrent;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(81, value);
                ODriveDevice.RaiseAndSetIfChanged(ref calibrationCurrent, value);
            }
        }

        private float resistanceCalibMaxVoltage;
        public float ResistanceCalibMaxVoltage
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(82);
                this.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, result);
                return resistanceCalibMaxVoltage;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(82, value);
                ODriveDevice.RaiseAndSetIfChanged(ref resistanceCalibMaxVoltage, value);
            }
        }

        private float phaseInductance;
        public float PhaseInductance
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(83);
                this.RaiseAndSetIfChanged(ref phaseInductance, result);
                return phaseInductance;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(83, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseInductance, value);
            }
        }

        private float phaseResistance;
        public float PhaseResistance
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(84);
                this.RaiseAndSetIfChanged(ref phaseResistance, result);
                return phaseResistance;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(84, value);
                ODriveDevice.RaiseAndSetIfChanged(ref phaseResistance, value);
            }
        }

        private int direction;
        public int Direction
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<int>(85);
                this.RaiseAndSetIfChanged(ref direction, result);
                return direction;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<int>(85, value);
                ODriveDevice.RaiseAndSetIfChanged(ref direction, value);
            }
        }

        private byte motorType;
        public byte MotorType
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<byte>(86);
                this.RaiseAndSetIfChanged(ref motorType, result);
                return motorType;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<byte>(86, value);
                ODriveDevice.RaiseAndSetIfChanged(ref motorType, value);
            }
        }

        private float currentLim;
        public float CurrentLim
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(87);
                this.RaiseAndSetIfChanged(ref currentLim, result);
                return currentLim;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(87, value);
                ODriveDevice.RaiseAndSetIfChanged(ref currentLim, value);
            }
        }

        private float requestedCurrentRange;
        public float RequestedCurrentRange
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<float>(88);
                this.RaiseAndSetIfChanged(ref requestedCurrentRange, result);
                return requestedCurrentRange;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<float>(88, value);
                ODriveDevice.RaiseAndSetIfChanged(ref requestedCurrentRange, value);
            }
        }
    }
}