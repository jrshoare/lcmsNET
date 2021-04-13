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
