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

        [DllImport(Liblcms, EntryPoint = "cmsOpenIOhandlerFromMem", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenIOhandlerFromMem_Internal(
                IntPtr contextID,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint size,
                [MarshalAs(UnmanagedType.LPStr)] string access);

        internal static IntPtr OpenIOHandler(IntPtr contextID, IntPtr buffer, uint bufferSize, string access)
        {
            return OpenIOhandlerFromMem_Internal(contextID, buffer, bufferSize, access);
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

        [DllImport(Liblcms, EntryPoint = "_cmsReadUInt8Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadUint8Number_Internal(
                IntPtr handle,
                out byte n);

        internal static bool ReadUint8(IntPtr handle, out byte n)
        {
            return ReadUint8Number_Internal(handle, out n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadUInt16Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadUint16Number_Internal(
                IntPtr handle,
                out ushort n);

        internal static bool ReadUint16(IntPtr handle, out ushort n)
        {
            return ReadUint16Number_Internal(handle, out n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadUInt32Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadUint32Number_Internal(
                IntPtr handle,
                out uint n);

        internal static bool ReadUint32(IntPtr handle, out uint n)
        {
            return ReadUint32Number_Internal(handle, out n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadUInt64Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadUint64Number_Internal(
                IntPtr handle,
                out ulong n);

        internal static bool ReadUint64(IntPtr handle, out ulong n)
        {
            return ReadUint64Number_Internal(handle, out n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadFloat32Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadFloat32Number_Internal(
                IntPtr handle,
                out float f);

        internal static bool ReadFloat(IntPtr handle, out float f)
        {
            return ReadFloat32Number_Internal(handle, out f) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsRead15Fixed16Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int Read15Fixed16Number_Internal(
                IntPtr handle,
                out double d);

        internal static bool Read15Fixed16(IntPtr handle, out double d)
        {
            return Read15Fixed16Number_Internal(handle, out d) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadXYZNumber", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadXYZNumber_Internal(
                IntPtr handle,
                out CIEXYZ xyz);

        internal static bool ReadXYZ(IntPtr handle, out CIEXYZ xyz)
        {
            return ReadXYZNumber_Internal(handle, out xyz) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadUInt16Array", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int ReadUint16Array_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint n,
                void* array);

        internal unsafe static bool ReadUint16Array(IntPtr handle, uint n, out ushort[] array)
        {
            array = new ushort[n];
            fixed (void *ptr = &array[0])
            {
                return ReadUint16Array_Internal(handle, n, ptr) != 0;
            }
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteUInt8Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteUint8Number_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U1)] byte n);

        internal static bool WriteUint8(IntPtr handle, byte n)
        {
            return WriteUint8Number_Internal(handle, n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteUInt16Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteUint16Number_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U2)] ushort n);

        internal static bool WriteUint16(IntPtr handle, ushort n)
        {
            return WriteUint16Number_Internal(handle, n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteUInt32Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteUint32Number_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint n);

        internal static bool WriteUint32(IntPtr handle, uint n)
        {
            return WriteUint32Number_Internal(handle, n) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteUInt64Number", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int WriteUint64Number_Internal(
                IntPtr handle,
                /*const*/ ulong* n);

        internal unsafe static bool WriteUint64(IntPtr handle, ulong n)
        {
            unsafe
            {
                ulong* ptr = &n;
                return WriteUint64Number_Internal(handle, ptr) != 0;
            }
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteFloat32Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteFloat32Number_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R4)] float f);

        internal static bool WriteFloat(IntPtr handle, float f)
        {
            return WriteFloat32Number_Internal(handle, f) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWrite15Fixed16Number", CallingConvention = CallingConvention.StdCall)]
        private static extern int Write15Fixed16Number_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double d);

        internal static bool Write15Fixed16(IntPtr handle, double d)
        {
            return Write15Fixed16Number_Internal(handle, d) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteXYZNumber", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteXYZNumber_Internal(
                IntPtr handle,
                CIEXYZ xyz);

        internal static bool WriteXYZ(IntPtr handle, CIEXYZ xyz)
        {
            return WriteXYZNumber_Internal(handle, xyz) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteUInt16Array", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int WriteUint16Array_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint n,
                /*const*/ void* array);

        internal unsafe static bool WriteUint16Array(IntPtr handle, ushort[] array)
        {
            uint n = (uint)array.Length;
            fixed (void* ptr = &array[0])
            {
                return WriteUint16Array_Internal(handle, n, ptr) != 0;
            }
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadAlignment", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadAlignment_Internal(
                IntPtr handle);

        internal static bool ReadAlignment(IntPtr handle)
        {
            return ReadAlignment_Internal(handle) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteAlignment", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteAlignment_Internal(
                IntPtr handle);

        internal static bool WriteAlignment(IntPtr handle)
        {
            return WriteAlignment_Internal(handle) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsReadTypeBase", CallingConvention = CallingConvention.StdCall)]
        private static extern uint ReadTypeBase_Internal(
                IntPtr handle);

        internal static uint ReadTypeBase(IntPtr handle)
        {
            return ReadTypeBase_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsWriteTypeBase", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteTypeBase_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint sig);

        internal static bool WriteTypeBase(IntPtr handle, uint sig)
        {
            return WriteTypeBase_Internal(handle, sig) != 0;
        }
    }
}
