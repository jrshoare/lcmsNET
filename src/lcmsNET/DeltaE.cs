namespace lcmsNET
{
    public sealed class DeltaE
    {
        public static double CIEDE2000(CIELab lab1, CIELab lab2, double kL = 1.0, double kC = 1.0, double kH = 1.0)
        {
            return Interop.CIE2000DeltaE(lab1, lab2, kL, kC, kH);
        }
    }
}
