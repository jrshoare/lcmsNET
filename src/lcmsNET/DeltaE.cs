namespace lcmsNET
{
    public sealed class DeltaE
    {
        public static double DE76(CIELab lab1, CIELab lab2)
        {
            return Interop.DeltaE(lab1, lab2);
        }

        public static double CMC(CIELab lab1, CIELab lab2, double l, double c)
        {
            return Interop.CMCDeltaE(lab1, lab2, l, c);
        }

        public static double BFD(CIELab lab1, CIELab lab2)
        {
            return Interop.BFDDeltaE(lab1, lab2);
        }

        public static double CIE94(CIELab lab1, CIELab lab2)
        {
            return Interop.CIE94DeltaE(lab1, lab2);
        }

        public static double CIEDE2000(CIELab lab1, CIELab lab2, double kL = 1.0, double kC = 1.0, double kH = 1.0)
        {
            return Interop.CIE2000DeltaE(lab1, lab2, kL, kC, kH);
        }
    }
}
