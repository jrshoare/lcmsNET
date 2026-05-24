// Copyright(c) 2019-2021 John Stevenson-Hoare
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8Alloc")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr IT8Alloc_Internal(
            IntPtr contextID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8Alloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr IT8Alloc_Internal(
            IntPtr contextID);
#endif

        internal static IntPtr IT8Alloc(IntPtr contextID)
        {
            return IT8Alloc_Internal(contextID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8Free")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void IT8Free_Internal(IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8Free", CallingConvention = CallingConvention.StdCall)]
        private static extern void IT8Free_Internal(IntPtr handle);
#endif

        internal static void IT8Free(IntPtr handle)
        {
            IT8Free_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8TableCount")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint IT8TableCount_Internal(IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8TableCount", CallingConvention = CallingConvention.StdCall)]
        private static extern uint IT8TableCount_Internal(IntPtr handle);
#endif

        internal static uint IT8TableCount(IntPtr handle)
        {
            return IT8TableCount_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetTable")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IT8SetTable_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nTable);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetTable", CallingConvention = CallingConvention.StdCall)]
        private static extern int IT8SetTable_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nTable);
#endif

        internal static int IT8SetTable(IntPtr handle, uint nTable)
        {
            return IT8SetTable_Internal(handle, nTable);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8LoadFromFile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr IT8LoadFromFile_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8LoadFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr IT8LoadFromFile_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#endif

        internal static IntPtr IT8LoadFromFile(IntPtr contextID, string filepath)
        {
            Debug.Assert(filepath != null);

            return IT8LoadFromFile_Internal(contextID, filepath);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8LoadFromMem")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr IT8LoadFromMem_Internal(
                IntPtr contextID,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] uint memSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8LoadFromMem", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern IntPtr IT8LoadFromMem_Internal(
                IntPtr contextID,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] uint memSize);
#endif

        internal static unsafe IntPtr IT8LoadFromMem(IntPtr contextID, byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                return IT8LoadFromMem_Internal(contextID, memPtr, (uint)memory.Length);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SaveToFile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IT8SaveToFile_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SaveToFile", CallingConvention = CallingConvention.StdCall)]
        private static extern int IT8SaveToFile_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#endif

        internal static int IT8SaveToFile(IntPtr handle, string filepath)
        {
            Debug.Assert(filepath != null);

            return IT8SaveToFile_Internal(handle, filepath);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SaveToMem")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SaveToMem_Internal(
                IntPtr handle,
                void* memPtr,
                uint* bytesNeeded);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SaveToMem", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SaveToMem_Internal(
                IntPtr handle,
                void* memPtr,
                uint* bytesNeeded);
#endif

        internal static unsafe int IT8SaveToMem(IntPtr handle, byte[] memPtr, out uint bytesNeeded)
        {
            int result = 0;
            uint n = (uint)(memPtr?.Length ?? 0);
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

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetSheetType")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr IT8GetSheetType_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetSheetType", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern IntPtr IT8GetSheetType_Internal(
                IntPtr handle);
#endif

        internal static string IT8GetSheetType(IntPtr handle)
        {
            IntPtr ptr = IT8GetSheetType_Internal(handle);
            return Marshal.PtrToStringAnsi(ptr);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetSheetType")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetSheetType_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string sheetType);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetSheetType", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetSheetType_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string sheetType);
#endif

        internal static int IT8SetSheetType(IntPtr handle, string sheetType)
        {
            return IT8SetSheetType_Internal(handle, sheetType);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetComment")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetComment_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string comment);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetComment", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetComment_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string comment);
#endif

        internal static int IT8SetComment(IntPtr handle, string comment)
        {
            return IT8SetComment_Internal(handle, comment);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetProperty")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr IT8GetProperty_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string propertyName);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetProperty", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern IntPtr IT8GetProperty_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string propertyName);
#endif

        internal static string IT8GetProperty(IntPtr handle, string propertyName)
        {
            IntPtr ptr = IT8GetProperty_Internal(handle, propertyName);
            return Marshal.PtrToStringAnsi(ptr);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetPropertyStr")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetPropertyStr_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyStr", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetPropertyStr_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#endif

        internal static int IT8SetProperty(IntPtr handle, string name, string value)
        {
            return IT8SetPropertyStr_Internal(handle, name, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetPropertyDbl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial double IT8GetPropertyDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string propertyName);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetPropertyDbl", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern double IT8GetPropertyDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string propertyName);
#endif

        internal static double IT8GetPropertyDouble(IntPtr handle, string propertyName)
        {
            return IT8GetPropertyDbl_Internal(handle, propertyName);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetPropertyDbl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetPropertyDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.R8)] double value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyDbl", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetPropertyDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.R8)] double value);
#endif

        internal static int IT8SetPropertyDouble(IntPtr handle, string name, double value)
        {
            return IT8SetPropertyDbl_Internal(handle, name, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetPropertyHex")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetPropertyHex_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.U4)] uint value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyHex", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetPropertyHex_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.U4)] uint value);
#endif

        internal static int IT8SetPropertyHex(IntPtr handle, string name, uint value)
        {
            return IT8SetPropertyHex_Internal(handle, name, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetPropertyUncooked")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetPropertyUncooked_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyUncooked", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetPropertyUncooked_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string name,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#endif

        internal static int IT8SetPropertyUncooked(IntPtr handle, string name, string value)
        {
            return IT8SetPropertyUncooked_Internal(handle, name, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetPropertyMulti")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetPropertyMulti_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string key,
                [MarshalAs(UnmanagedType.LPStr)] string subkey,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetPropertyMulti", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetPropertyMulti_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string key,
                [MarshalAs(UnmanagedType.LPStr)] string subkey,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#endif

        internal static int IT8SetProperty(IntPtr handle, string key, string subkey, string value)
        {
            return IT8SetPropertyMulti_Internal(handle, key, subkey, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8EnumProperties")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial uint IT8EnumProperties_Internal(
                IntPtr handle,
                out IntPtr propertyNames);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8EnumProperties", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern uint IT8EnumProperties_Internal(
                IntPtr handle,
                out IntPtr propertyNames);
#endif

        internal static unsafe string[] IT8EnumProperties(IntPtr handle)
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

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8EnumPropertyMulti")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial uint IT8EnumPropertyMulti_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string key,
                out IntPtr subPropertyNames);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8EnumPropertyMulti", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern uint IT8EnumPropertyMulti_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string key,
                out IntPtr subPropertyNames);
#endif

        internal static unsafe string[] IT8EnumPropertyMulti(IntPtr handle, string key)
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

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetDataRowCol")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr IT8GetDataRowCol_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetDataRowCol", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern IntPtr IT8GetDataRowCol_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col);
#endif

        internal static string IT8GetDataRowCol(IntPtr handle, int row, int column)
        {
            IntPtr ptr = IT8GetDataRowCol_Internal(handle, row, column);
            return Marshal.PtrToStringAnsi(ptr);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetData")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr IT8GetData_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetData", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern IntPtr IT8GetData_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);
#endif

        internal static string IT8GetData(IntPtr handle, string patch, string sample)
        {
            IntPtr ptr = IT8GetData_Internal(handle, patch, sample);
            return Marshal.PtrToStringAnsi(ptr);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetDataRowColDbl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial double IT8GetDataRowColDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetDataRowColDbl", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern double IT8GetDataRowColDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col);
#endif

        internal static double IT8GetDataRowColDouble(IntPtr handle, int row, int column)
        {
            return IT8GetDataRowColDbl_Internal(handle, row, column);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetDataDbl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial double IT8GetDataDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetDataDbl", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern double IT8GetDataDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);
#endif

        internal static double IT8GetDataDbl(IntPtr handle, string patch, string sample)
        {
            return IT8GetDataDbl_Internal(handle, patch, sample);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetData")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetData_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetData", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetData_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#endif

        internal static int IT8SetData(IntPtr handle, string patch, string sample, string value)
        {
            return IT8SetData_Internal(handle, patch, sample, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetDataRowCol")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetDataRowCol_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataRowCol", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetDataRowCol_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int row,
                [MarshalAs(UnmanagedType.I4)] int col,
                [MarshalAs(UnmanagedType.LPStr)] string value);
#endif

        internal static int IT8SetDataRowCol(IntPtr handle, int row, int column, string value)
        {
            return IT8SetDataRowCol_Internal(handle, row, column, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetDataRowColDbl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetDataRowColDbl_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.I4)] int row,
            [MarshalAs(UnmanagedType.I4)] int col,
            [MarshalAs(UnmanagedType.R8)] double value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataRowColDbl", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetDataRowColDbl_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.I4)] int row,
            [MarshalAs(UnmanagedType.I4)] int col,
            [MarshalAs(UnmanagedType.R8)] double value);
#endif

        internal static int IT8SetDataRowColDbl(IntPtr handle, int row, int column, double value)
        {
            return IT8SetDataRowColDbl_Internal(handle, row, column, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetDataDbl")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetDataDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample,
                [MarshalAs(UnmanagedType.R8)] double value);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataDbl", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetDataDbl_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string patch,
                [MarshalAs(UnmanagedType.LPStr)] string sample,
                [MarshalAs(UnmanagedType.R8)] double value);
#endif

        internal static int IT8SetDataDbl(IntPtr handle, string patch, string sample, double value)
        {
            return IT8SetDataDbl_Internal(handle, patch, sample, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8FindDataFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8FindDataFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string sample);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8FindDataFormat", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8FindDataFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string sample);
#endif

        internal static int IT8FindDataFormat(IntPtr handle, string sample)
        {
            return IT8FindDataFormat_Internal(handle, sample);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8SetDataFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8SetDataFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.I4)] int n,
            [MarshalAs(UnmanagedType.LPStr)] string sample);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8SetDataFormat", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8SetDataFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.I4)] int n,
            [MarshalAs(UnmanagedType.LPStr)] string sample);
#endif

        internal static int IT8SetDataFormat(IntPtr handle, int column, string sample)
        {
            return IT8SetDataFormat_Internal(handle, column, sample);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8EnumDataFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int IT8EnumDataFormat_Internal(
                IntPtr handle,
                out IntPtr sampleNames);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8EnumDataFormat", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern int IT8EnumDataFormat_Internal(
                IntPtr handle,
                out IntPtr sampleNames);
#endif

        internal static unsafe string[] IT8EnumDataFormat(IntPtr handle)
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

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8GetPatchName")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr IT8GetPatchName_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nPatch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8GetPatchName", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern IntPtr IT8GetPatchName_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nPatch,
                [MarshalAs(UnmanagedType.LPStr)] string sample);
#endif

        internal static string IT8GetPatchName(IntPtr handle, int nPatch)
        {
            IntPtr ptr = IT8GetPatchName_Internal(handle, nPatch, null);
            return Marshal.PtrToStringAnsi(ptr);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIT8DefineDblFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial void IT8DefineDblFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string format);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIT8DefineDblFormat", CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern void IT8DefineDblFormat_Internal(
            IntPtr handle,
            [MarshalAs(UnmanagedType.LPStr)] string format);
#endif

        internal static void IT8DefineDblFormat(IntPtr handle, string format)
        {
            IT8DefineDblFormat_Internal(handle, format);
        }
    }
}
