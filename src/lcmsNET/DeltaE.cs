namespace lcmsNET
{
    /// <summary>
    /// Defines static methods to compute the difference between two colors.
    /// </summary>
    public sealed class DeltaE
    {
        /// <summary>
        /// Computes the difference between two colors using Delta-E 1976 (CIE76).
        /// </summary>
        /// <param name="lab1">First <see cref="CIELab"/> color.</param>
        /// <param name="lab2">Second <see cref="CIELab"/> color.</param>
        /// <returns>The Delta-E 76 difference between the colors.</returns>
        public static double DE76(in CIELab lab1, in CIELab lab2)
        {
            return Interop.DeltaE(lab1, lab2);
        }

        /// <summary>
        /// Computes the difference between two colors using the CMC l:c method.
        /// </summary>
        /// <param name="lab1">First <see cref="CIELab"/> color.</param>
        /// <param name="lab2">Second <see cref="CIELab"/> color.</param>
        /// <param name="l">Lightness factor.</param>
        /// <param name="c">Chroma factor.</param>
        /// <returns>The CMC l:c difference between the colors.</returns>
        public static double CMC(in CIELab lab1, in CIELab lab2, double l, double c)
        {
            return Interop.CMCDeltaE(lab1, lab2, l, c);
        }

        /// <summary>
        /// Computes the difference between two colors using the BFD method.
        /// </summary>
        /// <param name="lab1">First <see cref="CIELab"/> color.</param>
        /// <param name="lab2">Second <see cref="CIELab"/> color.</param>
        /// <returns>The BFD difference between the colors.</returns>
        public static double BFD(in CIELab lab1, in CIELab lab2)
        {
            return Interop.BFDDeltaE(lab1, lab2);
        }

        /// <summary>
        /// Computes the difference between two colors using the CIE94 method.
        /// </summary>
        /// <param name="lab1">First <see cref="CIELab"/> color.</param>
        /// <param name="lab2">Second <see cref="CIELab"/> color.</param>
        /// <returns>The CIE94 difference between the colors.</returns>
        public static double CIE94(in CIELab lab1, in CIELab lab2)
        {
            return Interop.CIE94DeltaE(lab1, lab2);
        }

        /// <summary>
        /// Computes the difference between two colors using the CIEDE2000 method.
        /// </summary>
        /// <param name="lab1">First <see cref="CIELab"/> color.</param>
        /// <param name="lab2">Second <see cref="CIELab"/> color.</param>
        /// <param name="kL">kL.</param>
        /// <param name="kC">kC.</param>
        /// <param name="kH">kH.</param>
        /// <returns>The CIEDE2000 difference between the colors.</returns>
        public static double CIEDE2000(in CIELab lab1, in CIELab lab2, double kL = 1.0, double kC = 1.0, double kH = 1.0)
        {
            return Interop.CIE2000DeltaE(lab1, lab2, kL, kC, kH);
        }
    }
}
