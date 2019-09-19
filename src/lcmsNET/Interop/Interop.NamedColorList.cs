using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsAllocNamedColorList", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr AllocNamedColorList_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint n,
                [MarshalAs(UnmanagedType.U4)] uint colorantCount,
                [MarshalAs(UnmanagedType.LPStr)] string prefix,
                [MarshalAs(UnmanagedType.LPStr)] string suffix);

        internal static IntPtr AllocNamedColorList(IntPtr contextID, uint n, uint colorantCount, string prefix, string suffix)
        {
            return AllocNamedColorList_Internal(contextID, n, colorantCount, prefix, suffix);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFreeNamedColorList", CallingConvention = CallingConvention.StdCall)]
        private static extern void FreeNamedColorList_Internal(IntPtr handle);

        internal static void FreeNamedColorList(IntPtr handle)
        {
            FreeNamedColorList_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDupNamedColorList", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DupNamedColorList_Internal(
                IntPtr handle);

        internal static IntPtr DupNamedColorList(IntPtr handle)
        {
            return DupNamedColorList_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetNamedColorList", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetNamedColorList_Internal(
                IntPtr handle);

        internal static IntPtr GetNamedColorList(IntPtr handle)
        {
            return GetNamedColorList_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsAppendNamedColor", CallingConvention = CallingConvention.StdCall)]
        private static extern int AppendNamedColor_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] pcs,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] colorant);

        internal static int AppendNamedColor(IntPtr handle, string name, ushort[] pcs, ushort[] colorant)
        {
            return AppendNamedColor_Internal(handle, name, pcs, colorant);
        }

        [DllImport(Liblcms, EntryPoint = "cmsNamedColorCount", CallingConvention = CallingConvention.StdCall)]
        private static extern uint NamedColorCount_Internal(
                IntPtr handle);

        internal static uint NamedColorCount(IntPtr handle)
        {
            return NamedColorCount_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsNamedColorIndex", CallingConvention = CallingConvention.StdCall)]
        private static extern int NamedColorIndex_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name);

        internal static int NamedColorIndex(IntPtr handle, string name)
        {
            return NamedColorIndex_Internal(handle, name);
        }

        [DllImport(Liblcms, EntryPoint = "cmsNamedColorInfo", CallingConvention = CallingConvention.StdCall)]
        private static extern int NamedColorInfo_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nColor,
                IntPtr name,
                IntPtr prefix,
                IntPtr suffix,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 3)] ushort[] pcs,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] colorant);

        internal static int NamedColorInfo(IntPtr handle, uint nColor, out string name, out string prefix,
                out string suffix, ushort[] pcs, ushort[] colorant)
        {
            IntPtr bufName = Marshal.AllocHGlobal(256);
            IntPtr bufPrefix = Marshal.AllocHGlobal(33);
            IntPtr bufSuffix = Marshal.AllocHGlobal(33);
            try
            {
                int result = NamedColorInfo_Internal(handle, nColor, bufName, bufPrefix, bufSuffix, pcs, colorant);
                if (result != 0)
                {
                    name = Marshal.PtrToStringAnsi(bufName);
                    prefix = Marshal.PtrToStringAnsi(bufPrefix);
                    suffix = Marshal.PtrToStringAnsi(bufSuffix);
                }
                else
                {
                    name = prefix = suffix = string.Empty;
                }
                return result;
            }
            finally
            {
                Marshal.FreeHGlobal(bufSuffix);
                Marshal.FreeHGlobal(bufPrefix);
                Marshal.FreeHGlobal(bufName);
            }
        }
    }
}
