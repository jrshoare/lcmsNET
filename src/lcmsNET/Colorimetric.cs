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
    /// <summary>
    /// Represents an XYZ tristimulus value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CIEXYZ
    {
        /// <summary>
        /// XYZ X.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double X;
        /// <summary>
        /// XYZ Y.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double Y;
        /// <summary>
        /// XYZ Z.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double Z;

        /// <summary>
        /// Converts the value to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="whitePoint">The white point to be used in the conversion.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public CIELab ToLab(in CIEXYZ whitePoint)
        {
            Interop.XYZ2Lab(whitePoint, out CIELab lab, this);
            return lab;
        }

        /// <summary>
        /// Gets the D50 white point in XYZ.
        /// </summary>
        public static CIEXYZ D50 => Interop.GetD50_XYZ();

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="CIEXYZ"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="CIEXYZ"/> instance.</returns>
        internal static CIEXYZ FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<CIEXYZ>(handle);
        }

        /// <summary>
        /// Implicitly converts a <see cref="CIEXYZ"/> value to <see cref="CIExyY"/>.
        /// </summary>
        /// <param name="xyz">The <see cref="CIEXYZ"/> value to be converted.</param>
        public static implicit operator CIExyY(in CIEXYZ xyz)
        {
            Interop.XYZ2xyY(out CIExyY xyY, xyz);
            return xyY;
        }
    }

    /// <summary>
    /// Represents an xyY tristimulus value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CIExyY
    {
        /// <summary>
        /// xyY x.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double x;
        /// <summary>
        /// xyY y.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double y;
        /// <summary>
        /// xyY Y.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double Y;

        /// <summary>
        /// Gets the D50 white point in xyY.
        /// </summary>
        public static CIExyY D50 => Interop.GetD50_xyY();

        /// <summary>
        /// Implicitly converts a <see cref="CIExyY"/> value to <see cref="CIEXYZ"/>.
        /// </summary>
        /// <param name="xyY">The <see cref="CIExyY"/> value to be converted.</param>
        public static implicit operator CIEXYZ(in CIExyY xyY)
        {
            Interop.xyY2XYZ(out CIEXYZ xyz, xyY);
            return xyz;
        }
    }

    /// <summary>
    /// Represents a CIELAB tristimulus value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CIELab
    {
        /// <summary>
        /// CIELAB L* lightness value.
        /// </summary>
        /// <remarks>
        /// Black is 0 and white is 100.
        /// </remarks>
        [MarshalAs(UnmanagedType.R8)]
        public double L;
        /// <summary>
        /// CIELAB a axis value representing the green-red opponent colors.
        /// </summary>
        /// <remarks>
        /// Negative values are towards green and positive values towards red.
        /// </remarks>
        [MarshalAs(UnmanagedType.R8)]
        public double a;
        /// <summary>
        /// CIELAB b axis value representing the blue-yellow opponent colors.
        /// </summary>
        /// <remarks>
        /// Negative values are towards blue and positive values towards yellow.
        /// </remarks>
        [MarshalAs(UnmanagedType.R8)]
        public double b;

        /// <summary>
        /// Converts the value to <see cref="CIEXYZ"/>.
        /// </summary>
        /// <param name="whitePoint">The white point to be used in the conversion.</param>
        /// <returns>The corresponding <see cref="CIEXYZ"/> value.</returns>
        public CIEXYZ ToXYZ(in CIEXYZ whitePoint)
        {
            Interop.Lab2XYZ(whitePoint, out CIEXYZ xyz, this);
            return xyz;
        }

        /// <summary>
        /// Implicitly converts a <see cref="CIELab"/> value to <see cref="CIELCh"/>.
        /// </summary>
        /// <param name="lab">The <see cref="CIELab"/> value to be converted.</param>
        public static implicit operator CIELCh(in CIELab lab)
        {
            Interop.Lab2LCh(out CIELCh lch, lab);
            return lch;
        }
    }

    /// <summary>
    /// Represents a CIELCh tristimulus value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CIELCh
    {
        /// <summary>
        /// CIELCh L lightness value.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double L;
        /// <summary>
        /// CIELCh C chroma value.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double C;
        /// <summary>
        /// CIELCh h hue value.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double h;

        /// <summary>
        /// Implicitly converts a <see cref="CIELCh"/> value to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="lch">The <see cref="CIELCh"/> value to be converted.</param>
        public static implicit operator CIELab(in CIELCh lch)
        {
            Interop.LCh2Lab(out CIELab lab, lch);
            return lab;
        }
    }

    /// <summary>
    /// Represents a CIE CAM02 JCh tristimulus value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JCh
    {
        /// <summary>
        /// CIE CAM02 J lightness value.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double J;
        /// <summary>
        /// CIE CAM02 C chroma value.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double C;
        /// <summary>
        /// CIE CAM02 h hue value.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double h;
    }

    /// <summary>
    /// Represents a triple of CIEXYZ values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CIEXYZTRIPLE
    {
        /// <summary>
        /// The CIEXYZ Red component.
        /// </summary>
        public CIEXYZ Red;
        /// <summary>
        /// The CIEXYZ Green component.
        /// </summary>
        public CIEXYZ Green;
        /// <summary>
        /// The CIEXYZ Blue component.
        /// </summary>
        public CIEXYZ Blue;

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="CIEXYZTRIPLE"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="CIEXYZTRIPLE"/> instance.</returns>
        internal static CIEXYZTRIPLE FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<CIEXYZTRIPLE>(handle);
        }
    }

    /// <summary>
    /// Represents a triple of CIExyY values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CIExyYTRIPLE
    {
        /// <summary>
        /// The CIExyY Red component.
        /// </summary>
        public CIExyY Red;
        /// <summary>
        /// The CIExyY Green component.
        /// </summary>
        public CIExyY Green;
        /// <summary>
        /// The CIExyY Blue component.
        /// </summary>
        public CIExyY Blue;

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="CIExyYTRIPLE"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="CIExyYTRIPLE"/> instance.</returns>
        internal static CIExyYTRIPLE FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<CIExyYTRIPLE>(handle);
        }
    }

    /// <summary>
    /// Provides static methods to convert between color spaces.
    /// </summary>
    public static class Colorimetric
    {
        /// <summary>
        /// Converts a <see cref="CIEXYZ"/> value to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="whitePoint">The white point to be used in the conversion.</param>
        /// <param name="xyz">The <see cref="CIEXYZ"/> value to be converted.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public static CIELab XYZ2Lab(in CIEXYZ whitePoint, in CIEXYZ xyz)
        {
            Interop.XYZ2Lab(whitePoint, out CIELab lab, xyz);
            return lab;
        }

        /// <summary>
        /// Converts a <see cref="CIELab"/> value to <see cref="CIEXYZ"/>.
        /// </summary>
        /// <param name="whitePoint">The white point to be used in the conversion.</param>
        /// <param name="lab">The <see cref="CIELab"/> value to be converted.</param>
        /// <returns>The corresponding <see cref="CIEXYZ"/> value.</returns>
        public static CIEXYZ Lab2XYZ(in CIEXYZ whitePoint, in CIELab lab)
        {
            Interop.Lab2XYZ(whitePoint, out CIEXYZ xyz, lab);
            return xyz;
        }

        /// <summary>
        /// Converts a <see cref="CIELab"/> value to <see cref="CIELCh"/>.
        /// </summary>
        /// <param name="lab">The <see cref="CIELab"/> value to be converted.</param>
        /// <returns>The corresponding <see cref="CIELCh"/> value.</returns>
        public static CIELCh Lab2LCh(in CIELab lab)
        {
            Interop.Lab2LCh(out CIELCh lch, lab);
            return lch;
        }

        /// <summary>
        /// Converts a <see cref="CIELCh"/> value to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="lch">The <see cref="CIELCh"/> value to be converted.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public static CIELab LCh2Lab(in CIELCh lch)
        {
            Interop.LCh2Lab(out CIELab lab, lch);
            return lab;
        }

        /// <summary>
        /// Converts an Lab value encoded using ICC v4 convention to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="wLab">An array in which the first 3 values encode an Lab value using ICC v4 convention.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public static CIELab LabEncoded2Float(ushort[] wLab)
        {
            return LabEncoded2Float(new ReadOnlySpan<ushort>(wLab, 0, 3));
        }

        /// <summary>
        /// Converts an Lab value encoded using ICC v4 convention to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="wLab">A read only span of 3 values encoding an Lab value using ICC v4 convention.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public static CIELab LabEncoded2Float(ReadOnlySpan<ushort> wLab)
        {
            if (wLab.Length != 3) throw new ArgumentException($"'{nameof(wLab)}' length must equal 3.");

            Interop.LabEncoded2Float(out CIELab lab, wLab);
            return lab;
        }

        /// <summary>
        /// Converts a <see cref="CIELab"/> value to an Lab value using ICC v4 convention.
        /// </summary>
        /// <param name="lab">The <see cref="CIELab"/> to be converted.</param>
        /// <returns>An array of 3 values encoding an Lab value using ICC v4 convention.</returns>
        public static ushort[] Float2LabEncoded(in CIELab lab)
        {
            ushort[] wLab = new ushort[3];
            Interop.Float2LabEncoded(lab, wLab);
            return wLab;
        }

        /// <summary>
        /// Converts an Lab value encoded using ICC v2 convention to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="wLab">An array in which the first 3 values encode an Lab value using ICC v2 convention.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public static CIELab LabEncoded2FloatV2(ushort[] wLab)
        {
            return LabEncoded2FloatV2(new ReadOnlySpan<ushort>(wLab, 0, 3));
        }

        /// <summary>
        /// Converts an Lab value encoded using ICC v2 convention to <see cref="CIELab"/>.
        /// </summary>
        /// <param name="wLab">A read only span of 3 values encoding an Lab value using ICC v2 convention.</param>
        /// <returns>The corresponding <see cref="CIELab"/> value.</returns>
        public static CIELab LabEncoded2FloatV2(ReadOnlySpan<ushort> wLab)
        {
            if (wLab.Length != 3) throw new ArgumentException($"'{nameof(wLab)}' length must equal 3.");

            Interop.LabEncoded2FloatV2(out CIELab lab, wLab);
            return lab;
        }

        /// <summary>
        /// Converts a <see cref="CIELab"/> value to an Lab value using ICC v2 convention.
        /// </summary>
        /// <param name="lab">The <see cref="CIELab"/> to be converted.</param>
        /// <returns>An array of 3 values encoding an Lab value using ICC v2 convention.</returns>
        public static ushort[] Float2LabEncodedV2(in CIELab lab)
        {
            ushort[] wLab = new ushort[3];
            Interop.Float2LabEncodedV2(lab, wLab);
            return wLab;
        }

        /// <summary>
        /// Converts an XYZ value encoded using ICC convention to <see cref="CIEXYZ"/>.
        /// </summary>
        /// <param name="xyz">An array of 3 values encoding an XYZ value using ICC convention.</param>
        /// <returns>The corresponding <see cref="CIEXYZ"/> value.</returns>
        public static CIEXYZ XYZEncoded2Float(ushort[] xyz)
        {
            return XYZEncoded2Float(new ReadOnlySpan<ushort>(xyz, 0, 3));
        }

        /// <summary>
        /// Converts an XYZ value encoded using ICC convention to <see cref="CIEXYZ"/>.
        /// </summary>
        /// <param name="xyz">A read only span of 3 values encoding an XYZ value using ICC convention.</param>
        /// <returns>The corresponding <see cref="CIEXYZ"/> value.</returns>
        public static CIEXYZ XYZEncoded2Float(ReadOnlySpan<ushort> xyz)
        {
            if (xyz.Length != 3) throw new ArgumentException($"'{nameof(xyz)}' length must equal 3.");

            Interop.XYZEncoded2Float(out CIEXYZ fxyz, xyz);
            return fxyz;
        }

        /// <summary>
        /// Converts a <see cref="CIEXYZ"/> value to an XYZ value using ICC convention.
        /// </summary>
        /// <param name="fxyz">The <see cref="CIEXYZ"/> to be converted.</param>
        /// <returns>An array of 3 values encoding an XYZ value using ICC convention.</returns>
        public static ushort[] Float2XYZEncoded(in CIEXYZ fxyz)
        {
            ushort[] xyz = new ushort[3];
            Interop.Float2XYZEncoded(fxyz, xyz);
            return xyz;
        }

        /// <summary>
        /// Performs gamut mapping on the supplied <see cref="CIELab"/> value.
        /// </summary>
        /// <param name="lab">The <see cref="CIELab"/> to be mapped.</param>
        /// <param name="aMax">The maximum a gamut boundary.</param>
        /// <param name="aMin">The minimum a gamut boundary.</param>
        /// <param name="bMax">The maximum b gamut boundary.</param>
        /// <param name="bMin">The minimum b gamut boundary.</param>
        /// <returns></returns>
        public static bool Desaturate(ref CIELab lab, double aMax, double aMin, double bMax, double bMin)
        {
            return Interop.DesaturateLab(ref lab, aMax, aMin, bMax, bMin) != 0;
        }

        /// <summary>
        /// Gets the D50 white point in XYZ.
        /// </summary>
        public static CIEXYZ D50_XYZ => CIEXYZ.D50;

        /// <summary>
        /// Gets the D50 white point in xyY.
        /// </summary>
        public static CIExyY D50_xyY => CIExyY.D50;

        /// <summary>
        /// Converts a <see cref="CIEXYZ"/> value to <see cref="CIExyY"/>.
        /// </summary>
        /// <param name="xyz">The <see cref="CIEXYZ"/> value to be converted.</param>
        /// <returns>The corresponding <see cref="CIExyY"/> value.</returns>
        public static CIExyY XYZ2xyY(in CIEXYZ xyz)
        {
            Interop.XYZ2xyY(out CIExyY xyY, xyz);
            return xyY;
        }

        /// <summary>
        /// Converts a <see cref="CIExyY"/> value to <see cref="CIEXYZ"/>.
        /// </summary>
        /// <param name="xyY">The <see cref="CIExyY"/> value to be converted.</param>
        /// <returns>The corresponding <see cref="CIEXYZ"/> value.</returns>
        public static CIEXYZ xyY2XYZ(in CIExyY xyY)
        {
            Interop.xyY2XYZ(out CIEXYZ xyz, xyY);
            return xyz;
        }
    }
}
