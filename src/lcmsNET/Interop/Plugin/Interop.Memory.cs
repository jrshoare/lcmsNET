// Copyright(c) 2019-2022 John Stevenson-Hoare
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
        [DllImport(Liblcms, EntryPoint = "_cmsMalloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Malloc_Internal(
                IntPtr context,
                [MarshalAs(UnmanagedType.U4)] uint size);

        internal static IntPtr Malloc(IntPtr context, uint size)
        {
            return Malloc_Internal(context, size);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsFree", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Free_Internal(
                IntPtr context,
                IntPtr ptr);

        internal static IntPtr Free(IntPtr context, IntPtr ptr)
        {
            return Free_Internal(context, ptr);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsMallocZero", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr MallocZero_Internal(
                IntPtr context,
                [MarshalAs(UnmanagedType.U4)] uint size);

        internal static IntPtr MallocZero(IntPtr context, uint size)
        {
            return MallocZero_Internal(context, size);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsCalloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Calloc_Internal(
                IntPtr context,
                [MarshalAs(UnmanagedType.U4)] uint num,
                [MarshalAs(UnmanagedType.U4)] uint size);

        internal static IntPtr Calloc(IntPtr context, uint num, uint size)
        {
            return Calloc_Internal(context, num, size);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsRealloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Realloc_Internal(
                IntPtr context,
                IntPtr ptr,
                [MarshalAs(UnmanagedType.U4)] uint size);

        internal static IntPtr Realloc(IntPtr context, IntPtr ptr, uint size)
        {
            return Realloc_Internal(context, ptr, size);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsDupMem", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DupMem_Internal(
                IntPtr context,
                IntPtr org,
                [MarshalAs(UnmanagedType.U4)] uint size);

        internal static IntPtr DupMem(IntPtr context, IntPtr org, uint size)
        {
            return DupMem_Internal(context, org, size);
        }
    }
}
