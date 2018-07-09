namespace ODrive
{
    using System;
    using ReactiveUI;

    public partial class MotorTimingLog : RemoteObject
    {
        public MotorTimingLog(Connection connection): base(connection)
        {
        }

        private ushort tIMINGLOGGENERAL;
        public ushort TIMINGLOGGENERAL
        {
            get
            {
                var result = FetchEndpointSync<ushort>(70);
                this.RaiseAndSetIfChanged(ref tIMINGLOGGENERAL, result);
                return tIMINGLOGGENERAL;
            }
        }

        private ushort tIMINGLOGADCCBI;
        public ushort TIMINGLOGADCCBI
        {
            get
            {
                var result = FetchEndpointSync<ushort>(71);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBI, result);
                return tIMINGLOGADCCBI;
            }
        }

        private ushort tIMINGLOGADCCBDC;
        public ushort TIMINGLOGADCCBDC
        {
            get
            {
                var result = FetchEndpointSync<ushort>(72);
                this.RaiseAndSetIfChanged(ref tIMINGLOGADCCBDC, result);
                return tIMINGLOGADCCBDC;
            }
        }

        private ushort tIMINGLOGMEASR;
        public ushort TIMINGLOGMEASR
        {
            get
            {
                var result = FetchEndpointSync<ushort>(73);
                this.RaiseAndSetIfChanged(ref tIMINGLOGMEASR, result);
                return tIMINGLOGMEASR;
            }
        }

        private ushort tIMINGLOGMEASL;
        public ushort TIMINGLOGMEASL
        {
            get
            {
                var result = FetchEndpointSync<ushort>(74);
                this.RaiseAndSetIfChanged(ref tIMINGLOGMEASL, result);
                return tIMINGLOGMEASL;
            }
        }

        private ushort tIMINGLOGENCCALIB;
        public ushort TIMINGLOGENCCALIB
        {
            get
            {
                var result = FetchEndpointSync<ushort>(75);
                this.RaiseAndSetIfChanged(ref tIMINGLOGENCCALIB, result);
                return tIMINGLOGENCCALIB;
            }
        }

        private ushort tIMINGLOGIDXSEARCH;
        public ushort TIMINGLOGIDXSEARCH
        {
            get
            {
                var result = FetchEndpointSync<ushort>(76);
                this.RaiseAndSetIfChanged(ref tIMINGLOGIDXSEARCH, result);
                return tIMINGLOGIDXSEARCH;
            }
        }

        private ushort tIMINGLOGFOCVOLTAGE;
        public ushort TIMINGLOGFOCVOLTAGE
        {
            get
            {
                var result = FetchEndpointSync<ushort>(77);
                this.RaiseAndSetIfChanged(ref tIMINGLOGFOCVOLTAGE, result);
                return tIMINGLOGFOCVOLTAGE;
            }
        }

        private ushort tIMINGLOGFOCCURRENT;
        public ushort TIMINGLOGFOCCURRENT
        {
            get
            {
                var result = FetchEndpointSync<ushort>(78);
                this.RaiseAndSetIfChanged(ref tIMINGLOGFOCCURRENT, result);
                return tIMINGLOGFOCCURRENT;
            }
        }
    }
}