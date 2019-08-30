﻿using System;
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
