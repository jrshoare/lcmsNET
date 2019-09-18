using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsDictAlloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DictAlloc_Internal(
                IntPtr contextID);

        internal static IntPtr DictAlloc(IntPtr contextID)
        {
            return DictAlloc_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDictFree", CallingConvention = CallingConvention.StdCall)]
        private static extern void DictFree_Internal(IntPtr handle);

        internal static void DictFree(IntPtr handle)
        {
            DictFree_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDictDup", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DictDup_Internal(
                IntPtr handle);

        internal static IntPtr DictDup(IntPtr handle)
        {
            return DictDup_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDictAddEntry", CallingConvention = CallingConvention.StdCall)]
        private static extern int DictAddEntry_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPWStr)] string name,
                [MarshalAs(UnmanagedType.LPWStr)] string value,
                IntPtr displayName,
                IntPtr displayValue);

        internal static int DictAddEntry(IntPtr handle, string name, string value, IntPtr displayName, IntPtr displayValue)
        {
            return DictAddEntry_Internal(handle, name, value, displayName, displayValue);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDictGetEntryList", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DictGetEntryList_Internal(
                IntPtr handle);

        internal static IntPtr DictGetEntryList(IntPtr handle)
        {
            return DictGetEntryList_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDictNextEntry", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DictNextEntry_Internal(
                IntPtr handle);

        internal static IntPtr DictNextEntry(IntPtr handle)
        {
            return DictNextEntry_Internal(handle);
        }
    }
}
