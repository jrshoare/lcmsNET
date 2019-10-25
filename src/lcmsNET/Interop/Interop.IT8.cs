using System;
using System.Diagnostics;
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

        [DllImport(Liblcms, EntryPoint = "cmsIT8TableCount", CallingConvention = CallingConvention.StdCall)]
        private static extern uint IT8TableCount_Internal(IntPtr handle);

        internal static uint IT8TableCount(IntPtr handle)
        {
            return IT8TableCount_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetTable", CallingConvention = CallingConvention.StdCall)]
        private static extern int IT8SetTable_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nTable);

        internal static int IT8SetTable(IntPtr handle, uint nTable)
        {
            return IT8SetTable_Internal(handle, nTable);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8LoadFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr IT8LoadFromFile_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename);

        internal static IntPtr IT8LoadFromFile(IntPtr contextID, string filepath)
        {
            Debug.Assert(filepath != null);

            return IT8LoadFromFile_Internal(contextID, filepath);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8LoadFromMem", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr IT8LoadFromMem_Internal(
                IntPtr contextID,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] int memSize);

        internal unsafe static IntPtr IT8LoadFromMem(IntPtr contextID, byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                return IT8LoadFromMem_Internal(contextID, memPtr, memory.Length);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SaveToFile", CallingConvention = CallingConvention.StdCall)]
        private static extern int IT8SaveToFile_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string filename);

        internal static int IT8SaveToFile(IntPtr handle, string filepath)
        {
            Debug.Assert(filepath != null);

            return IT8SaveToFile_Internal(handle, filepath);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SaveToMem", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SaveToMem_Internal(
                IntPtr handle,
                void* memPtr,
                int* bytesNeeded);

        internal unsafe static int IT8SaveToMem(IntPtr handle, byte[] memPtr, out int bytesNeeded)
        {
            int result = 0;
            int n = memPtr?.Length ?? 0;
            if (memPtr is null)
            {
                result = IT8SaveToMem_Internal(handle, null, &n);
            }
            else
            {
                fixed (void* pMemPtr = &memPtr[0])
                {
                    result = IT8SaveToMem_Internal(handle, pMemPtr, &n);
                }
            }
            bytesNeeded = n;
            return result;
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetSheetType", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr IT8GetSheetType_Internal(
                IntPtr handle);

        internal static string IT8GetSheetType(IntPtr handle)
        {
            IntPtr ptr = IT8GetSheetType_Internal(handle);
            return Marshal.PtrToStringAnsi(ptr);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetSheetType", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetSheetType_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string sheetType);

        internal static int IT8SetSheetType(IntPtr handle, string sheetType)
        {
            return IT8SetSheetType_Internal(handle, sheetType);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetComment", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetComment_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string comment);

        internal static int IT8SetComment(IntPtr handle, string comment)
        {
            return IT8SetComment_Internal(handle, comment);
        }
    }
}
