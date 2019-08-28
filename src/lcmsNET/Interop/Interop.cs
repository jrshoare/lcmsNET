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
    }
}
