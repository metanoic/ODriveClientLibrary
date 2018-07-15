namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class Motor1TimingLog : RemoteObject
    {
        public Motor1TimingLog(Device ODriveDevice): base(ODriveDevice)
        {
        }

        private ushort tIMINGLOGGENERAL;
        public ushort TIMINGLOGGENERAL
        {
            get
            {
                var result = FetchEndpointSync<ushort>(195);
                this.RaiseAndSetIfChanged(ref tIMINGLOGGENERAL, result);
                return tIMINGLOGGENERAL;
            }

            set
            {
                SetPropertySync<ushort>(195, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGGENERAL, value);
            }
        }

        private ushort tIMINGLOGADCCBM0I;
        public ushort TIMINGLOGADCCBM0I
        {
            get
            {
                var result = FetchEndpointSync<ushort>(196);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0I, result);
                return tIMINGLOGADCCBM0I;
            }

            set
            {
                SetPropertySync<ushort>(196, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0I, value);
            }
        }

        private ushort tIMINGLOGADCCBM0DC;
        public ushort TIMINGLOGADCCBM0DC
        {
            get
            {
                var result = FetchEndpointSync<ushort>(197);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0DC, result);
                return tIMINGLOGADCCBM0DC;
            }

            set
            {
                SetPropertySync<ushort>(197, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM0DC, value);
            }
        }

        private ushort tIMINGLOGADCCBM1I;
        public ushort TIMINGLOGADCCBM1I
        {
            get
            {
                var result = FetchEndpointSync<ushort>(198);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1I, result);
                return tIMINGLOGADCCBM1I;
            }

            set
            {
                SetPropertySync<ushort>(198, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1I, value);
            }
        }

        private ushort tIMINGLOGADCCBM1DC;
        public ushort TIMINGLOGADCCBM1DC
        {
            get
            {
                var result = FetchEndpointSync<ushort>(199);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1DC, result);
                return tIMINGLOGADCCBM1DC;
            }

            set
            {
                SetPropertySync<ushort>(199, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGADCCBM1DC, value);
            }
        }

        private ushort tIMINGLOGMEASR;
        public ushort TIMINGLOGMEASR
        {
            get
            {
                var result = FetchEndpointSync<ushort>(200);
                this.RaiseAndSetIfChanged(ref tIMINGLOGMEASR, result);
                return tIMINGLOGMEASR;
            }

            set
            {
                SetPropertySync<ushort>(200, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGMEASR, value);
            }
        }

        private ushort tIMINGLOGMEASL;
        public ushort TIMINGLOGMEASL
        {
            get
            {
                var result = FetchEndpointSync<ushort>(201);
                this.RaiseAndSetIfChanged(ref tIMINGLOGMEASL, result);
                return tIMINGLOGMEASL;
            }

            set
            {
                SetPropertySync<ushort>(201, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGMEASL, value);
            }
        }

        private ushort tIMINGLOGENCCALIB;
        public ushort TIMINGLOGENCCALIB
        {
            get
            {
                var result = FetchEndpointSync<ushort>(202);
                this.RaiseAndSetIfChanged(ref tIMINGLOGENCCALIB, result);
                return tIMINGLOGENCCALIB;
            }

            set
            {
                SetPropertySync<ushort>(202, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGENCCALIB, value);
            }
        }

        private ushort tIMINGLOGIDXSEARCH;
        public ushort TIMINGLOGIDXSEARCH
        {
            get
            {
                var result = FetchEndpointSync<ushort>(203);
                this.RaiseAndSetIfChanged(ref tIMINGLOGIDXSEARCH, result);
                return tIMINGLOGIDXSEARCH;
            }

            set
            {
                SetPropertySync<ushort>(203, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGIDXSEARCH, value);
            }
        }

        private ushort tIMINGLOGFOCVOLTAGE;
        public ushort TIMINGLOGFOCVOLTAGE
        {
            get
            {
                var result = FetchEndpointSync<ushort>(204);
                this.RaiseAndSetIfChanged(ref tIMINGLOGFOCVOLTAGE, result);
                return tIMINGLOGFOCVOLTAGE;
            }

            set
            {
                SetPropertySync<ushort>(204, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGFOCVOLTAGE, value);
            }
        }

        private ushort tIMINGLOGFOCCURRENT;
        public ushort TIMINGLOGFOCCURRENT
        {
            get
            {
                var result = FetchEndpointSync<ushort>(205);
                this.RaiseAndSetIfChanged(ref tIMINGLOGFOCCURRENT, result);
                return tIMINGLOGFOCCURRENT;
            }

            set
            {
                SetPropertySync<ushort>(205, value);
                ODriveDevice.RaiseAndSetIfChanged(ref tIMINGLOGFOCCURRENT, value);
            }
        }
    }
}