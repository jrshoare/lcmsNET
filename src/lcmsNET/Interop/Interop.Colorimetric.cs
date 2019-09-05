using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsXYZ2Lab", CallingConvention = CallingConvention.StdCall)]
        private static extern void XYZ2Lab_Internal(in CIEXYZ whitePoint, out CIELab lab, in CIEXYZ xyz);

        internal static void XYZ2Lab(in CIEXYZ whitePoint, out CIELab lab, in CIEXYZ xyz)
        {
            XYZ2Lab_Internal(whitePoint, out lab, xyz);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLab2XYZ", CallingConvention = CallingConvention.StdCall)]
        private static extern void Lab2XYZ_Internal(in CIEXYZ whitePoint, out CIEXYZ xyz, in CIELab lab);

        internal static void Lab2XYZ(in CIEXYZ whitePoint, out CIEXYZ xyz, in CIELab lab)
        {
            Lab2XYZ_Internal(whitePoint, out xyz, lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLab2LCh", CallingConvention = CallingConvention.StdCall)]
        private static extern void Lab2LCh_Internal(out CIELCh lch, in CIELab lab);

        internal static void Lab2LCh(out CIELCh lch, in CIELab lab)
        {
            Lab2LCh_Internal(out lch, lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLCh2Lab", CallingConvention = CallingConvention.StdCall)]
        private static extern void LCh2Lab_Internal(out CIELab lab, in CIELCh lch);

        internal static void LCh2Lab(out CIELab lab, in CIELCh lch)
        {
            LCh2Lab_Internal(out lab, lch);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLabEncoded2Float", CallingConvention = CallingConvention.StdCall)]
        private static extern void LabEncoded2Float_Internal(
                out CIELab lab,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab);

        internal static void LabEncoded2Float(out CIELab lab, ushort[] wLab)
        {
            LabEncoded2Float_Internal(out lab, wLab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFloat2LabEncoded", CallingConvention = CallingConvention.StdCall)]
        private static extern void Float2LabEncoded_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab,
                in CIELab lab);

        internal static void Float2LabEncoded(in CIELab lab, ushort[] wLab)
        {
            Float2LabEncoded_Internal(wLab, lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLabEncoded2FloatV2", CallingConvention = CallingConvention.StdCall)]
        private static extern void LabEncoded2FloatV2_Internal(
                out CIELab lab,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab);

        internal static void LabEncoded2FloatV2(out CIELab lab, ushort[] wLab)
        {
            LabEncoded2FloatV2_Internal(out lab, wLab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFloat2LabEncodedV2", CallingConvention = CallingConvention.StdCall)]
        private static extern void Float2LabEncodedV2_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] wLab,
                in CIELab lab);

        internal static void Float2LabEncodedV2(in CIELab lab, ushort[] wLab)
        {
            Float2LabEncodedV2_Internal(wLab, lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsXYZEncoded2Float", CallingConvention = CallingConvention.StdCall)]
        private static extern void XYZEncoded2Float_Internal(
                out CIEXYZ fxyz,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] xyz);

        internal static void XYZEncoded2Float(out CIEXYZ fxyz, ushort[] xyz)
        {
            XYZEncoded2Float_Internal(out fxyz, xyz);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFloat2XYZEncoded", CallingConvention = CallingConvention.StdCall)]
        private static extern void Float2XYZEncoded_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] xyz,
                in CIEXYZ fxyz);

        internal static void Float2XYZEncoded(in CIEXYZ fxyz, ushort[] xyz)
        {
            Float2XYZEncoded_Internal(xyz, fxyz);
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
