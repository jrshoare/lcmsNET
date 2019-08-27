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
    }
}
