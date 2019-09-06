using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsGBDAlloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GBDAlloc_Internal(
                IntPtr contextID);

        internal static IntPtr GBDAlloc(IntPtr contextID)
        {
            return GBDAlloc_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGBDFree", CallingConvention = CallingConvention.StdCall)]
        private static extern void GBDFree_Internal(
            IntPtr handle);

        internal static void GBDFree(IntPtr handle)
        {
            GBDFree_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGDBAddPoint", CallingConvention = CallingConvention.StdCall)]
        private static extern int GDBAddPoint_Internal(
            IntPtr handle,
            in CIELab lab);

        internal static int GBDAddPoint(IntPtr handle, in CIELab lab)
        {
            return GDBAddPoint_Internal(handle, lab);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGDBCompute", CallingConvention = CallingConvention.StdCall)]
        private static extern int GDBCompute_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static int GBDCompute(IntPtr handle, uint flags)
        {
            return GDBCompute_Internal(handle, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGDBCheckPoint", CallingConvention = CallingConvention.StdCall)]
        private static extern int GDBCheckPoint_Internal(
            IntPtr handle,
            in CIELab lab);

        internal static int GBDCheckPoint(IntPtr handle, in CIELab lab)
        {
            return GDBCheckPoint_Internal(handle, lab);
        }
    }
}
