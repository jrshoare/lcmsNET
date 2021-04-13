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
        [DllImport(Liblcms, EntryPoint = "cmsOpenIOhandlerFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenIOhandlerFromFile_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);

        internal static IntPtr OpenIOHandler(IntPtr contextID, string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenIOhandlerFromFile_Internal(contextID, filepath, access);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenIOhandlerFromNULL", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenIOhandlerFromNull_Internal(
                IntPtr contextID);

        internal static IntPtr OpenIOHandler(IntPtr contextID)
        {
            return OpenIOhandlerFromNull_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCloseIOhandler", CallingConvention = CallingConvention.StdCall)]
        private static extern int CloseIOhandler_Internal(IntPtr handle);

        internal static int CloseIOHandler(IntPtr handle)
        {
            return CloseIOhandler_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileIOhandler", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetProfileIOHandler_Internal(IntPtr handle);

        internal static IntPtr GetProfileIOHandler(IntPtr handle)
        {
            return GetProfileIOHandler_Internal(handle);
        }
    }
}
