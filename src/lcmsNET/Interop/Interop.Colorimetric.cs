// Copyright(c) 2019-2021 John Stevenson-Hoare
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
        private unsafe static extern void LabEncoded2Float_Internal(
                out CIELab lab,
                /*const*/ ushort* wLab);

        internal static void LabEncoded2Float(out CIELab lab, ReadOnlySpan<ushort> wLab)
        {
            unsafe
            {
                fixed (ushort* ptr = wLab)
                {
                    LabEncoded2Float_Internal(out lab, ptr);
                }
            }
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
        private unsafe static extern void LabEncoded2FloatV2_Internal(
                out CIELab lab,
                /*const*/ ushort* wLab);

        internal static void LabEncoded2FloatV2(out CIELab lab, ReadOnlySpan<ushort> wLab)
        {
            unsafe
            {
                fixed (ushort* ptr = wLab)
                {
                    LabEncoded2FloatV2_Internal(out lab, ptr);
                }
            }
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
        private unsafe static extern void XYZEncoded2Float_Internal(
                out CIEXYZ fxyz,
                /*const*/ ushort* xyz);

        internal static void XYZEncoded2Float(out CIEXYZ fxyz, ReadOnlySpan<ushort> xyz)
        {
            unsafe
            {
                fixed (ushort* ptr = xyz)
                {
                    XYZEncoded2Float_Internal(out fxyz, ptr);
                }
            }
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

        [DllImport(Liblcms, EntryPoint = "cmsXYZ2xyY", CallingConvention = CallingConvention.StdCall)]
        private static extern void XYZ2xyY_Internal(out CIExyY xyY, in CIEXYZ xyz);

        internal static void XYZ2xyY(out CIExyY xyY, in CIEXYZ xyz)
        {
            XYZ2xyY_Internal(out xyY, xyz);
        }

        [DllImport(Liblcms, EntryPoint = "cmsxyY2XYZ", CallingConvention = CallingConvention.StdCall)]
        private static extern void xyY2XYZ_Internal(out CIEXYZ xyz, in CIExyY xyY);

        internal static void xyY2XYZ(out CIEXYZ xyz, in CIExyY xyY)
        {
            xyY2XYZ_Internal(out xyz, xyY);
        }
    }
}
