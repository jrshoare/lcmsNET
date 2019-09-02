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
                [MarshalAs(UnmanagedType.U4)] int iccColorSpaceSignature);

        internal static int GetLCMSColorSpace(int iccColorSpaceSignature)
        {
            return LCMSColorSpace_Internal(iccColorSpaceSignature);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsICCcolorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int ICCColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] int lcmsColorSpaceSignature);

        internal static int GetICCColorSpace(int lcmsColorSpaceSignature)
        {
            return ICCColorSpace_Internal(lcmsColorSpaceSignature);
        }

        [DllImport(Liblcms, EntryPoint = "cmsChannelsOf", CallingConvention = CallingConvention.StdCall)]
        private static extern uint ChannelsOf_Internal(
                [MarshalAs(UnmanagedType.U4)] int colorSpace);

        internal static uint ChannelsOf(int colorSpace)
        {
            return ChannelsOf_Internal(colorSpace);
        }
    }
}
