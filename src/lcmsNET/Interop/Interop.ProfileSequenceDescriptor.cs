using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsAllocProfileSequenceDescription", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr AllocProfileSequenceDescription_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nItems);

        internal static IntPtr AllocProfileSequenceDescription(IntPtr contextID, uint nItems)
        {
            return AllocProfileSequenceDescription_Internal(contextID, nItems);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFreeProfileSequenceDescription", CallingConvention = CallingConvention.StdCall)]
        private static extern void FreeProfileSequenceDescription_Internal(IntPtr handle);

        internal static void FreeProfileSequenceDescription(IntPtr handle)
        {
            FreeProfileSequenceDescription_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDupProfileSequenceDescription", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DupProfileSequenceDescription_Internal(
                IntPtr handle);

        internal static IntPtr DupProfileSequenceDescription(IntPtr handle)
        {
            return DupProfileSequenceDescription_Internal(handle);
        }
    }
}
