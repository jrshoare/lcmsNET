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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        internal const string Liblcms = "lcms2";

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetEncodedCMMversion")]
        private static partial int GetEncodedCMMVersion_Internal();
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetEncodedCMMversion", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetEncodedCMMVersion_Internal();
#endif

        internal static int GetEncodedCMMVersion()
        {
            return GetEncodedCMMVersion_Internal();
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetLogErrorHandler")]
        private static partial int SetLogErrorHandler_Internal(
                IntPtr fn);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetLogErrorHandler", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetLogErrorHandler_Internal(
                IntPtr fn);
#endif

        internal static void SetErrorHandler(ErrorHandler handler)
        {
            IntPtr fn = (handler is null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(handler);
            SetLogErrorHandler_Internal(fn);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "_cmsLCMScolorSpace")]
        private static partial int LCMSColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] uint iccColorSpaceSignature);
#else
        [DllImport(Liblcms, EntryPoint = "_cmsLCMScolorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int LCMSColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] uint iccColorSpaceSignature);
#endif

        internal static int GetLCMSColorSpace(uint iccColorSpaceSignature)
        {
            return LCMSColorSpace_Internal(iccColorSpaceSignature);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "_cmsICCcolorSpace")]
        private static partial int ICCColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] uint lcmsColorSpaceSignature);
#else
        [DllImport(Liblcms, EntryPoint = "_cmsICCcolorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int ICCColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] uint lcmsColorSpaceSignature);
#endif

        internal static int GetICCColorSpace(uint lcmsColorSpaceSignature)
        {
            return ICCColorSpace_Internal(lcmsColorSpaceSignature);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsChannelsOf")]
        private static partial uint ChannelsOf_Internal(
                [MarshalAs(UnmanagedType.U4)] uint colorSpace);
#else
        [DllImport(Liblcms, EntryPoint = "cmsChannelsOf", CallingConvention = CallingConvention.StdCall)]
        private static extern uint ChannelsOf_Internal(
                [MarshalAs(UnmanagedType.U4)] uint colorSpace);
#endif

        internal static uint ChannelsOf(uint colorSpace)
        {
            return ChannelsOf_Internal(colorSpace);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetAlarmCodes")]
        private static partial void GetAlarmCodes_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetAlarmCodes", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetAlarmCodes_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);
#endif

        internal static void GetAlarmCodes(ushort[] alarmCodes)
        {
            GetAlarmCodes_Internal(alarmCodes);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetAlarmCodes")]
        private static partial void SetAlarmCodes_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetAlarmCodes", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetAlarmCodes_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);
#endif

        internal static void SetAlarmCodes(ushort[] alarmCodes)
        {
            SetAlarmCodes_Internal(alarmCodes);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetAdaptationState")]
        private static partial double SetAdaptationState_Internal(
                [MarshalAs(UnmanagedType.R8)] double adaptationState);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetAdaptationState", CallingConvention = CallingConvention.StdCall)]
        private static extern double SetAdaptationState_Internal(
                [MarshalAs(UnmanagedType.R8)] double adaptationState);
#endif

        internal static double SetAdaptationState(double adaptationState)
        {
            return SetAdaptationState_Internal(adaptationState);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsWhitePointFromTemp")]
        private static partial double WhitePointFromTemp_Internal(
                out CIExyY xyY,
                [MarshalAs(UnmanagedType.R8)] double tempK);
#else
        [DllImport(Liblcms, EntryPoint = "cmsWhitePointFromTemp", CallingConvention = CallingConvention.StdCall)]
        private static extern double WhitePointFromTemp_Internal(
                out CIExyY xyY,
                [MarshalAs(UnmanagedType.R8)] double tempK);
#endif

        internal static double WhitePointFromTemp(out CIExyY xyY, double tempK)
        {
            return WhitePointFromTemp_Internal(out xyY, tempK);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsTempFromWhitePoint")]
        private static partial double TempFromWhitePoint_Internal(
                [MarshalAs(UnmanagedType.R8)] out double tempK,
                in CIExyY xyY);
#else
        [DllImport(Liblcms, EntryPoint = "cmsTempFromWhitePoint", CallingConvention = CallingConvention.StdCall)]
        private static extern double TempFromWhitePoint_Internal(
                [MarshalAs(UnmanagedType.R8)] out double tempK,
                in CIExyY xyY);
#endif

        internal static double TempFromWhitePoint(out double tempK, in CIExyY xyY)
        {
            return TempFromWhitePoint_Internal(out tempK, xyY);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetSupportedIntents")]
        private static partial uint GetSupportedIntents_Internal(
                [MarshalAs(UnmanagedType.U4)] uint nMax,
                IntPtr Codes,
                IntPtr Descriptions);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetSupportedIntents", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetSupportedIntents_Internal(
                [MarshalAs(UnmanagedType.U4)] uint nMax,
                IntPtr Codes,
                IntPtr Descriptions);
#endif

        internal static IEnumerable<(uint code, string description)> GetSupportedIntents()
        {
            // get intent count first
            uint count = GetSupportedIntents_Internal(0, IntPtr.Zero, IntPtr.Zero);

            List<(uint code, string description)> result = new List<(uint code, string description)>();

            unsafe
            {
                IntPtr codesPtr = Marshal.AllocHGlobal((int)(count * sizeof(uint)));
                IntPtr descriptionsPtr = Marshal.AllocHGlobal((int)(count * sizeof(IntPtr)));

                count = GetSupportedIntents_Internal(count, codesPtr, descriptionsPtr);

                uint* codes = (uint*)codesPtr.ToPointer();
                IntPtr* descriptions = (IntPtr*)descriptionsPtr.ToPointer();
                for (var i = 0; i < count; i++)
                {
                    uint code = codes[i];
                    string description = Marshal.PtrToStringAnsi(descriptions[i]);
                    result.Add((code, description));
                }

                Marshal.FreeHGlobal(descriptionsPtr);
                Marshal.FreeHGlobal(codesPtr);
            }

            return result;
        }
    }
}
