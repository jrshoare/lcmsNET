using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        internal const string Liblcms = "lcms2.dll";

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
