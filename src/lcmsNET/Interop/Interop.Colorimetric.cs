using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsXYZ2Lab", CallingConvention = CallingConvention.StdCall)]
        private static extern void XYZ2Lab_Internal(ref CIEXYZ whitePoint, ref CIELab lab, ref CIEXYZ xyz);

        internal static void XYZ2Lab(CIEXYZ whitePoint, ref CIELab lab, CIEXYZ xyz)
        {
            XYZ2Lab_Internal(ref whitePoint, ref lab, ref xyz);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLab2XYZ", CallingConvention = CallingConvention.StdCall)]
        private static extern void Lab2XYZ_Internal(ref CIEXYZ whitePoint, ref CIEXYZ xyz, ref CIELab lab);

        internal static void Lab2XYZ(CIEXYZ whitePoint, ref CIEXYZ xyz, CIELab lab)
        {
            Lab2XYZ_Internal(ref whitePoint, ref xyz, ref lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLab2LCh", CallingConvention = CallingConvention.StdCall)]
        private static extern void Lab2LCh_Internal(ref CIELCh lch, ref CIELab lab);

        internal static void Lab2LCh(ref CIELCh lch, CIELab lab)
        {
            Lab2LCh_Internal(ref lch, ref lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLCh2Lab", CallingConvention = CallingConvention.StdCall)]
        private static extern void LCh2Lab_Internal(ref CIELab lab, ref CIELCh lch);

        internal static void LCh2Lab(ref CIELab lab, CIELCh lch)
        {
            LCh2Lab_Internal(ref lab, ref lch);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLabEncoded2Float", CallingConvention = CallingConvention.StdCall)]
        private static extern void LabEncoded2Float_Internal(
                ref CIELab lab,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab);

        internal static void LabEncoded2Float(ref CIELab lab, ushort[] wLab)
        {
            LabEncoded2Float_Internal(ref lab, wLab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFloat2LabEncoded", CallingConvention = CallingConvention.StdCall)]
        private static extern void Float2LabEncoded_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab,
                ref CIELab lab);

        internal static void Float2LabEncoded(CIELab lab, ref ushort[] wLab)
        {
            Float2LabEncoded_Internal(wLab, ref lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLabEncoded2FloatV2", CallingConvention = CallingConvention.StdCall)]
        private static extern void LabEncoded2FloatV2_Internal(
                ref CIELab lab,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab);

        internal static void LabEncoded2FloatV2(ref CIELab lab, ushort[] wLab)
        {
            LabEncoded2FloatV2_Internal(ref lab, wLab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFloat2LabEncodedV2", CallingConvention = CallingConvention.StdCall)]
        private static extern void Float2LabEncodedV2_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab,
                ref CIELab lab);

        internal static void Float2LabEncodedV2(CIELab lab, ref ushort[] wLab)
        {
            Float2LabEncodedV2_Internal(wLab, ref lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsXYZEncoded2Float", CallingConvention = CallingConvention.StdCall)]
        private static extern void XYZEncoded2Float_Internal(
                ref CIEXYZ fxyz,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] xyz);

        internal static void XYZEncoded2Float(ref CIEXYZ fxyz, ushort[] xyz)
        {
            XYZEncoded2Float_Internal(ref fxyz, xyz);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFloat2XYZEncoded", CallingConvention = CallingConvention.StdCall)]
        private static extern void Float2XYZEncoded_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] xyz,
                ref CIEXYZ fxyz);

        internal static void Float2XYZEncoded(CIEXYZ fxyz, ref ushort[] xyz)
        {
            Float2XYZEncoded_Internal(xyz, ref fxyz);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDesaturateLab", CallingConvention = CallingConvention.StdCall)]
        private static extern int DesaturateLab_Internal(
                ref CIELab lab,
                [MarshalAs(UnmanagedType.R8)] double aMax,
                [MarshalAs(UnmanagedType.R8)] double aMin,
                [MarshalAs(UnmanagedType.R8)] double bMax,
                [MarshalAs(UnmanagedType.R8)] double bMin);

        internal static int DesaturateLab(ref CIELab lab, double aMax, double aMin, double bMax, double bMin)
        {
            return DesaturateLab_Internal(ref lab, aMax, aMin, bMax, bMin);
        }

        [DllImport(Liblcms, EntryPoint = "cmsD50_XYZ", CallingConvention = CallingConvention.StdCall)]
        private static extern ref CIEXYZ D50_XYZ_Internal();

        internal static CIEXYZ GetD50_XYZ()
        {
            return D50_XYZ_Internal();
        }

        [DllImport(Liblcms, EntryPoint = "cmsD50_xyY", CallingConvention = CallingConvention.StdCall)]
        private static extern ref CIExyY D50_xyY_Internal();

        internal static CIExyY GetD50_xyY()
        {
            return D50_xyY_Internal();
        }
    }
}
