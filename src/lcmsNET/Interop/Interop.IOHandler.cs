using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsOpenIOhandlerFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenIOhandlerFromFile_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);

        internal static IntPtr OpenIOHandler(IntPtr contextID, string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenIOhandlerFromFile_Internal(contextID, filepath, access);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenIOhandlerFromNULL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenIOhandlerFromNull_Internal(
                IntPtr contextID);

        internal static IntPtr OpenIOHandler(IntPtr contextID)
        {
            return OpenIOhandlerFromNull_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCloseIOhandler", CallingConvention = CallingConvention.StdCall)]
        private static extern int CloseIOhandler_Internal(IntPtr handle);

        internal static int CloseIOHandler(IntPtr handle)
        {
            return CloseIOhandler_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileIOhandler", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetProfileIOHandler_Internal(IntPtr handle);

        internal static IntPtr GetProfileIOHandler(IntPtr handle)
        {
            return GetProfileIOHandler_Internal(handle);
        }
    }
}
