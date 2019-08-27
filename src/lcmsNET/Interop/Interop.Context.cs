using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCreateContext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateContext_Internal(
                IntPtr plugin,
                IntPtr userData);

        internal static IntPtr CreateContext(IntPtr plugin, IntPtr userData)
        {
            return CreateContext_Internal(plugin, userData);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDeleteContext", CallingConvention = CallingConvention.StdCall)]
        private static extern void DeleteContext_Internal(
            IntPtr handle);

        internal static void DeleteContext(IntPtr handle)
        {
            DeleteContext_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDupContext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DuplicateContext_Internal(
            IntPtr handle,
            IntPtr userData);

        internal static IntPtr DuplicateContext(IntPtr handle, IntPtr userData)
        {
            return DuplicateContext_Internal(handle, userData);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetContextUserData", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetContextUserData_Internal(
            IntPtr handle);

        internal static IntPtr GetContextUserData(IntPtr handle)
        {
            return GetContextUserData_Internal(handle);
        }
    }
}
