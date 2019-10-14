using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsIT8Alloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr IT8Alloc_Internal(
            IntPtr contextID);

        internal static IntPtr IT8Alloc(IntPtr contextID)
        {
            return IT8Alloc_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8Free", CallingConvention = CallingConvention.StdCall)]
        private static extern void IT8Free_Internal(IntPtr handle);

        internal static void IT8Free(IntPtr handle)
        {
            IT8Free_Internal(handle);
        }
    }
}
