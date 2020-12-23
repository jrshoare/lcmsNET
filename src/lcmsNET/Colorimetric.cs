using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CIEXYZ
    {
        [MarshalAs(UnmanagedType.R8)]
        public double X;
        [MarshalAs(UnmanagedType.R8)]
        public double Y;
        [MarshalAs(UnmanagedType.R8)]
        public double Z;

        public static CIEXYZ D50 => Interop.GetD50_XYZ();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIExyY
    {
        [MarshalAs(UnmanagedType.R8)]
        public double x;
        [MarshalAs(UnmanagedType.R8)]
        public double y;
        [MarshalAs(UnmanagedType.R8)]
        public double Y;

        public static CIExyY D50 => Interop.GetD50_xyY();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIELab
    {
        [MarshalAs(UnmanagedType.R8)]
        public double L;
        [MarshalAs(UnmanagedType.R8)]
        public double a;
        [MarshalAs(UnmanagedType.R8)]
        public double b;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIELCh
    {
        [MarshalAs(UnmanagedType.R8)]
        public double L;
        [MarshalAs(UnmanagedType.R8)]
        public double C;
        [MarshalAs(UnmanagedType.R8)]
        public double h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JCh
    {
        [MarshalAs(UnmanagedType.R8)]
        public double J;
        [MarshalAs(UnmanagedType.R8)]
        public double C;
        [MarshalAs(UnmanagedType.R8)]
        public double h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIEXYZTRIPLE
    {
        public CIEXYZ Red;
        public CIEXYZ Green;
        public CIEXYZ Blue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CIExyYTRIPLE
    {
        public CIExyY Red;
        public CIExyY Green;
        public CIExyY Blue;
    }

    public static class Colorimetric
    {
        public static CIELab XYZ2Lab(in CIEXYZ whitePoint, in CIEXYZ xyz)
        {
            Interop.XYZ2Lab(whitePoint, out CIELab lab, xyz);
            return lab;
        }

        public static CIEXYZ Lab2XYZ(in CIEXYZ whitePoint, in CIELab lab)
        {
            Interop.Lab2XYZ(whitePoint, out CIEXYZ xyz, lab);
            return xyz;
        }

        public static CIELCh Lab2LCh(in CIELab lab)
        {
            Interop.Lab2LCh(out CIELCh lch, lab);
            return lch;
        }

        public static CIELab LCh2Lab(in CIELCh lch)
        {
            Interop.LCh2Lab(out CIELab lab, lch);
            return lab;
        }

        public static CIELab LabEncoded2Float(ushort[] wLab)
        {
            if (wLab?.Length != 3) throw new ArgumentException($"'{nameof(wLab)}' array size must equal 3.");

            Interop.LabEncoded2Float(out CIELab lab, wLab);
            return lab;
        }

        public static ushort[] Float2LabEncoded(in CIELab lab)
        {
            ushort[] wLab = new ushort[3];
            Interop.Float2LabEncoded(lab, wLab);
            return wLab;
        }

        public static CIELab LabEncoded2FloatV2(ushort[] wLab)
        {
            if (wLab?.Length != 3) throw new ArgumentException($"'{nameof(wLab)}' array size must equal 3.");

            Interop.LabEncoded2FloatV2(out CIELab lab, wLab);
            return lab;
        }

        public static ushort[] Float2LabEncodedV2(in CIELab lab)
        {
            ushort[] wLab = new ushort[3];
            Interop.Float2LabEncodedV2(lab, wLab);
            return wLab;
        }

        public static CIEXYZ XYZEncoded2Float(ushort[] xyz)
        {
            if (xyz?.Length != 3) throw new ArgumentException($"'{nameof(xyz)}' array size must equal 3.");

            Interop.XYZEncoded2Float(out CIEXYZ fxyz, xyz);
            return fxyz;
        }

        public static ushort[] Float2XYZEncoded(in CIEXYZ fxyz)
        {
            ushort[] xyz = new ushort[3];
            Interop.Float2XYZEncoded(fxyz, xyz);
            return xyz;
        }

        public static bool Desaturate(ref CIELab lab, double aMax, double aMin, double bMax, double bMin)
        {
            return Interop.DesaturateLab(ref lab, aMax, aMin, bMax, bMin) != 0;
        }

        public static CIEXYZ D50_XYZ => CIEXYZ.D50;

        public static CIExyY D50_xyY => CIExyY.D50;
    }
}
