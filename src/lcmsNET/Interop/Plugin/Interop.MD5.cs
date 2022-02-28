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
        [DllImport(Liblcms, EntryPoint = "cmsMD5alloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr MD5alloc_Internal(
                IntPtr contextID);

        internal static IntPtr MD5Alloc(IntPtr contextID)
        {
            return MD5alloc_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMD5add", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern void MD5Add_Internal(
                IntPtr handle,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] int memSize);

        internal unsafe static void MD5Add(IntPtr md5, byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                MD5Add_Internal(md5, memPtr, memory.Length);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsMD5finish", CallingConvention = CallingConvention.StdCall)]
        private static extern void MD5Finish_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] byte[] profileID,
                IntPtr handle);

        internal static void MD5Finish(IntPtr md5, byte[] profileID)
        {
            MD5Finish_Internal(profileID, md5);
        }
    }
}
