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

    public sealed class Colorimetric
    {
        public static CIELab XYZ2Lab(CIEXYZ whitePoint, CIEXYZ xyz)
        {
            CIELab lab = new CIELab { };
            Interop.XYZ2Lab(whitePoint, ref lab, xyz);
            return lab;
        }

        public static CIEXYZ Lab2XYZ(CIEXYZ whitePoint, CIELab lab)
        {
            CIEXYZ xyz = new CIEXYZ { };
            Interop.Lab2XYZ(whitePoint, ref xyz, lab);
            return xyz;
        }

        public static CIELCh Lab2LCh(CIELab lab)
        {
            CIELCh lch = new CIELCh { };
            Interop.Lab2LCh(ref lch, lab);
            return lch;
        }

        public static CIELab LCh2Lab(CIELCh lch)
        {
            CIELab lab = new CIELab { };
            Interop.LCh2Lab(ref lab, lch);
            return lab;
        }

        public static CIELab LabEncoded2Float(ushort[] wLab)
        {
            if (wLab?.Length != 3) throw new ArgumentException($"'{nameof(wLab)}' array size must equal 3.");

            CIELab lab = new CIELab { };
            Interop.LabEncoded2Float(ref lab, wLab);
            return lab;
        }

        public static ushort[] Float2LabEncoded(CIELab lab)
        {
            ushort[] wLab = new ushort[3];
            Interop.Float2LabEncoded(lab, ref wLab);
            return wLab;
        }

        public static CIELab LabEncoded2FloatV2(ushort[] wLab)
        {
            if (wLab?.Length != 3) throw new ArgumentException($"'{nameof(wLab)}' array size must equal 3.");

            CIELab lab = new CIELab { };
            Interop.LabEncoded2FloatV2(ref lab, wLab);
            return lab;
        }

        public static ushort[] Float2LabEncodedV2(CIELab lab)
        {
            ushort[] wLab = new ushort[3];
            Interop.Float2LabEncodedV2(lab, ref wLab);
            return wLab;
        }

        public static CIEXYZ XYZEncoded2Float(ushort[] xyz)
        {
            if (xyz?.Length != 3) throw new ArgumentException($"'{nameof(xyz)}' array size must equal 3.");

            CIEXYZ fxyz = new CIEXYZ { };
            Interop.XYZEncoded2Float(ref fxyz, xyz);
            return fxyz;
        }

        public static ushort[] Float2XYZEncoded(CIEXYZ fxyz)
        {
            ushort[] xyz = new ushort[3];
            Interop.Float2XYZEncoded(fxyz, ref xyz);
            return xyz;
        }

        public static CIEXYZ D50_XYZ => Interop.GetD50_XYZ();

        public static CIExyY D50_xyY => Interop.GetD50_xyY();
    }
}
