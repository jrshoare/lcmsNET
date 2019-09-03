using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCIE2000DeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double CIE2000DeltaE_Internal(
            ref CIELab lab1,
            ref CIELab lab2,
            [MarshalAs(UnmanagedType.R8)] double kL,
            [MarshalAs(UnmanagedType.R8)] double kC,
            [MarshalAs(UnmanagedType.R8)] double kH
            );

        internal static double CIE2000DeltaE(CIELab lab1, CIELab lab2, double kL = 1.0, double kC = 1.0, double kH = 1.0)
        {
            return CIE2000DeltaE_Internal(ref lab1, ref lab2, kL, kC, kH);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double DeltaE_Internal(
            ref CIELab lab1,
            ref CIELab lab2);

        internal static double DeltaE(CIELab lab1, CIELab lab2)
        {
            return DeltaE_Internal(ref lab1, ref lab2);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCMCdeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double CMCdeltaE_Internal(
            ref CIELab lab1,
            ref CIELab lab2,
            [MarshalAs(UnmanagedType.R8)] double l,
            [MarshalAs(UnmanagedType.R8)] double c);

        internal static double CMCDeltaE(CIELab lab1, CIELab lab2, double l, double c)
        {
            return CMCdeltaE_Internal(ref lab1, ref lab2, l, c);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBFDdeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double BFDdeltaE_Internal(
            ref CIELab lab1,
            ref CIELab lab2);

        internal static double BFDDeltaE(CIELab lab1, CIELab lab2)
        {
            return BFDdeltaE_Internal(ref lab1, ref lab2);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIE94DeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double CIE94DeltaE_Internal(
            ref CIELab lab1,
            ref CIELab lab2);

        internal static double CIE94DeltaE(CIELab lab1, CIELab lab2)
        {
            return CIE94DeltaE_Internal(ref lab1, ref lab2);
        }

    }
}
