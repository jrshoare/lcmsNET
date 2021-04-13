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
