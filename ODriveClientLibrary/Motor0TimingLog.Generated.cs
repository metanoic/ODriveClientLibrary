namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor0TimingLog : RemoteObject
    {
        public Motor0TimingLog(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private ushort tIMINGLOGGENERAL;
        public ushort TIMINGLOGGENERAL
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(95);
                this.RaiseAndSetIfChanged(ref tIMINGLOGGENERAL, result);
                return tIMINGLOGGENERAL;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(95, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGGENERAL, value);
            }
        }

        private ushort tIMINGLOGADCCBM0I;
        public ushort TIMINGLOGADCCBM0I
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(96);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0I, result);
                return tIMINGLOGADCCBM0I;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(96, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0I, value);
            }
        }

        private ushort tIMINGLOGADCCBM0DC;
        public ushort TIMINGLOGADCCBM0DC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(97);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0DC, result);
                return tIMINGLOGADCCBM0DC;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(97, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0DC, value);
            }
        }

        private ushort tIMINGLOGADCCBM1I;
        public ushort TIMINGLOGADCCBM1I
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(98);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1I, result);
                return tIMINGLOGADCCBM1I;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(98, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1I, value);
            }
        }

        private ushort tIMINGLOGADCCBM1DC;
        public ushort TIMINGLOGADCCBM1DC
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(99);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1DC, result);
                return tIMINGLOGADCCBM1DC;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(99, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1DC, value);
            }
        }

        private ushort tIMINGLOGMEASR;
        public ushort TIMINGLOGMEASR
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(100);
                this.RaiseAndSetIfChanged(ref tIMINGLOGMEASR, result);
                return tIMINGLOGMEASR;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(100, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGMEASR, value);
            }
        }

        private ushort tIMINGLOGMEASL;
        public ushort TIMINGLOGMEASL
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(101);
                this.RaiseAndSetIfChanged(ref tIMINGLOGMEASL, result);
                return tIMINGLOGMEASL;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(101, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGMEASL, value);
            }
        }

        private ushort tIMINGLOGENCCALIB;
        public ushort TIMINGLOGENCCALIB
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(102);
                this.RaiseAndSetIfChanged(ref tIMINGLOGENCCALIB, result);
                return tIMINGLOGENCCALIB;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(102, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGENCCALIB, value);
            }
        }

        private ushort tIMINGLOGIDXSEARCH;
        public ushort TIMINGLOGIDXSEARCH
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(103);
                this.RaiseAndSetIfChanged(ref tIMINGLOGIDXSEARCH, result);
                return tIMINGLOGIDXSEARCH;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(103, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGIDXSEARCH, value);
            }
        }

        private ushort tIMINGLOGFOCVOLTAGE;
        public ushort TIMINGLOGFOCVOLTAGE
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(104);
                this.RaiseAndSetIfChanged(ref tIMINGLOGFOCVOLTAGE, result);
                return tIMINGLOGFOCVOLTAGE;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(104, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGFOCVOLTAGE, value);
            }
        }

        private ushort tIMINGLOGFOCCURRENT;
        public ushort TIMINGLOGFOCCURRENT
        {
            get
            {
                var result = ODriveDevice.FetchEndpointSync<ushort>(105);
                this.RaiseAndSetIfChanged(ref tIMINGLOGFOCCURRENT, result);
                return tIMINGLOGFOCCURRENT;
            }

            set
            {
                ODriveDevice.FetchEndpointSync<ushort>(105, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGFOCCURRENT, value);
            }
        }
    }
}