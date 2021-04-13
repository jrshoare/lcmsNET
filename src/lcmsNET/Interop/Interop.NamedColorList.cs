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
