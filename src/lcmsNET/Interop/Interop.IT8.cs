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

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetProperty", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr IT8GetProperty_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string propertyName);

        internal static string IT8GetProperty(IntPtr handle, string propertyName)
        {
            IntPtr ptr = IT8GetProperty_Internal(handle, propertyName);
            return Marshal.PtrToStringAnsi(ptr);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyStr", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetPropertyStr_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPStr)] string value);

        internal static int IT8SetProperty(IntPtr handle, string name, string value)
        {
            return IT8SetPropertyStr_Internal(handle, name, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetPropertyDbl", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern double IT8GetPropertyDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string propertyName);

        internal static double IT8GetPropertyDouble(IntPtr handle, string propertyName)
        {
            return IT8GetPropertyDbl_Internal(handle, propertyName);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyDbl", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetPropertyDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.R8)] double value);

        internal static int IT8SetPropertyDouble(IntPtr handle, string name, double value)
        {
            return IT8SetPropertyDbl_Internal(handle, name, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyHex", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetPropertyHex_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.U4)] uint value);

        internal static int IT8SetPropertyHex(IntPtr handle, string name, uint value)
        {
            return IT8SetPropertyHex_Internal(handle, name, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyUncooked", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetPropertyUncooked_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPStr)] string value);

        internal static int IT8SetPropertyUncooked(IntPtr handle, string name, string value)
        {
            return IT8SetPropertyUncooked_Internal(handle, name, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyMulti", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetPropertyMulti_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string key,
                [MarshalAs(UnmanagedType.LPStr)] string subkey,
                [MarshalAs(UnmanagedType.LPStr)] string value);

        internal static int IT8SetProperty(IntPtr handle, string key, string subkey, string value)
        {
            return IT8SetPropertyMulti_Internal(handle, key, subkey, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8EnumProperties", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern uint IT8EnumProperties_Internal(
                IntPtr handle,
                out IntPtr propertyNames);

        internal unsafe static string[] IT8EnumProperties(IntPtr handle)
        {
            uint count = IT8EnumProperties_Internal(handle, out IntPtr propertyNames);
            string[] properties = new string[count];
            char** names = (char**)propertyNames.ToPointer();
            for (uint i = 0; i < count; i++)
            {
                char* name = names[i];
                properties[i] = Marshal.PtrToStringAnsi(new IntPtr(name));
            }

            return properties;
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8EnumPropertyMulti", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern uint IT8EnumPropertyMulti_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string key,
                out IntPtr subPropertyNames);

        internal unsafe static string[] IT8EnumPropertyMulti(IntPtr handle, string key)
        {
            uint count = IT8EnumPropertyMulti_Internal(handle, key, out IntPtr subPropertyNames);
            string[] properties = new string[count];
            char** names = (char**)subPropertyNames.ToPointer();
            for (uint i = 0; i < count; i++)
            {
                char* name = names[i];
                properties[i] = Marshal.PtrToStringAnsi(new IntPtr(name));
            }

            return properties;
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetDataRowCol", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr IT8GetDataRowCol_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col);

        internal static string IT8GetDataRowCol(IntPtr handle, int row, int column)
        {
            IntPtr ptr = IT8GetDataRowCol_Internal(handle, row, column);
            return Marshal.PtrToStringAnsi(ptr);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetData", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr IT8GetData_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);

        internal static string IT8GetData(IntPtr handle, string patch, string sample)
        {
            IntPtr ptr = IT8GetData_Internal(handle, patch, sample);
            return Marshal.PtrToStringAnsi(ptr);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetDataRowColDbl", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern double IT8GetDataRowColDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col);

        internal static double IT8GetDataRowColDouble(IntPtr handle, int row, int column)
        {
            return IT8GetDataRowColDbl_Internal(handle, row, column);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetDataDbl", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern double IT8GetDataDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);

        internal static double IT8GetDataDbl(IntPtr handle, string patch, string sample)
        {
            return IT8GetDataDbl_Internal(handle, patch, sample);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetData", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetData_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample,
                [MarshalAs(UnmanagedType.LPStr)] string value);

        internal static int IT8SetData(IntPtr handle, string patch, string sample, string value)
        {
            return IT8SetData_Internal(handle, patch, sample, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataRowCol", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetDataRowCol_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col,
                [MarshalAs(UnmanagedType.LPStr)] string value);

        internal static int IT8SetDataRowCol(IntPtr handle, int row, int column, string value)
        {
            return IT8SetDataRowCol_Internal(handle, row, column, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataRowColDbl", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetDataRowColDbl_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.I4)] int row,
            [MarshalAs(UnmanagedType.I4)] int col,
            [MarshalAs(UnmanagedType.R8)] double value);

        internal static int IT8SetDataRowColDbl(IntPtr handle, int row, int column, double value)
        {
            return IT8SetDataRowColDbl_Internal(handle, row, column, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataDbl", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetDataDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample,
                [MarshalAs(UnmanagedType.R8)] double value);

        internal static int IT8SetDataDbl(IntPtr handle, string patch, string sample, double value)
        {
            return IT8SetDataDbl_Internal(handle, patch, sample, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8FindDataFormat", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8FindDataFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string sample);

        internal static int IT8FindDataFormat(IntPtr handle, string sample)
        {
            return IT8FindDataFormat_Internal(handle, sample);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataFormat", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8SetDataFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.I4)] int n,
            [MarshalAs(UnmanagedType.LPStr)] string sample);

        internal static int IT8SetDataFormat(IntPtr handle, int column, string sample)
        {
            return IT8SetDataFormat_Internal(handle, column, sample);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8EnumDataFormat", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int IT8EnumDataFormat_Internal(
                IntPtr handle,
                out IntPtr sampleNames);

        internal unsafe static string[] IT8EnumDataFormat(IntPtr handle)
        {
            int count = IT8EnumDataFormat_Internal(handle, out IntPtr sampleNames);
            string[] samples = new string[count];
            char** names = (char**)sampleNames.ToPointer();
            for (uint i = 0; i < count; i++)
            {
                char* name = names[i];
                samples[i] = Marshal.PtrToStringAnsi(new IntPtr(name));
            }

            return samples;
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8GetPatchName", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr IT8GetPatchName_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nPatch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);

        internal static string IT8GetPatchName(IntPtr handle, int nPatch)
        {
            IntPtr ptr = IT8GetPatchName_Internal(handle, nPatch, null);
            return Marshal.PtrToStringAnsi(ptr);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIT8DefineDblFormat", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern void IT8DefineDblFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string format);

        internal static void IT8DefineDblFormat(IntPtr handle, string format)
        {
            IT8DefineDblFormat_Internal(handle, format);
        }
    }
}
