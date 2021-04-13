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

using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        internal const string Liblcms = "lcms2";

        [DllImport(Liblcms, EntryPoint = "cmsGetEncodedCMMversion", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetEncodedCMMVersion_Internal();

        internal static int GetEncodedCMMVersion()
        {
            return GetEncodedCMMVersion_Internal();
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetLogErrorHandler", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetLogErrorHandler_Internal(
            ErrorHandler handler);

        internal static void SetErrorHandler(ErrorHandler handler)
        {
            SetLogErrorHandler_Internal(handler);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsLCMScolorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int LCMSColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] uint iccColorSpaceSignature);

        internal static int GetLCMSColorSpace(uint iccColorSpaceSignature)
        {
            return LCMSColorSpace_Internal(iccColorSpaceSignature);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsICCcolorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int ICCColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] uint lcmsColorSpaceSignature);

        internal static int GetICCColorSpace(uint lcmsColorSpaceSignature)
        {
            return ICCColorSpace_Internal(lcmsColorSpaceSignature);
        }

        [DllImport(Liblcms, EntryPoint = "cmsChannelsOf", CallingConvention = CallingConvention.StdCall)]
        private static extern uint ChannelsOf_Internal(
                [MarshalAs(UnmanagedType.U4)] uint colorSpace);

        internal static uint ChannelsOf(uint colorSpace)
        {
            return ChannelsOf_Internal(colorSpace);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetAlarmCodes", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetAlarmCodes_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);

        internal static void GetAlarmCodes(ushort[] alarmCodes)
        {
            GetAlarmCodes_Internal(alarmCodes);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetAlarmCodes", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetAlarmCodes_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);

        internal static void SetAlarmCodes(ushort[] alarmCodes)
        {
            SetAlarmCodes_Internal(alarmCodes);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetAdaptationState", CallingConvention = CallingConvention.StdCall)]
        private static extern double SetAdaptationState_Internal(
                [MarshalAs(UnmanagedType.R8)] double adaptationState);

        internal static double SetAdaptationState(double adaptationState)
        {
            return SetAdaptationState_Internal(adaptationState);
        }

        [DllImport(Liblcms, EntryPoint = "cmsWhitePointFromTemp", CallingConvention = CallingConvention.StdCall)]
        private static extern double WhitePointFromTemp_Internal(
                out CIExyY xyY,
                [MarshalAs(UnmanagedType.R8)] double tempK);

        internal static double WhitePointFromTemp(out CIExyY xyY, double tempK)
        {
            return WhitePointFromTemp_Internal(out xyY, tempK);
        }

        [DllImport(Liblcms, EntryPoint = "cmsTempFromWhitePoint", CallingConvention = CallingConvention.StdCall)]
        private static extern double TempFromWhitePoint_Internal(
                [MarshalAs(UnmanagedType.R8)] out double tempK,
                in CIExyY xyY);

        internal static double TempFromWhitePoint(out double tempK, in CIExyY xyY)
        {
            return TempFromWhitePoint_Internal(out tempK, xyY);
        }
    }
}
