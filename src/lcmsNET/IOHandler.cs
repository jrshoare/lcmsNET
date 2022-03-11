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

using lcmsNET.Impl;
using System;

namespace lcmsNET
{
    /// <summary>
    /// Represents an I/O handler.
    /// </summary>
    public sealed class IOHandler : CmsHandle<IOHandler>
    {
        internal IOHandler(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IOHandler"/> class from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing I/O handler.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        public static IOHandler FromHandle(IntPtr handle)
        {
            return new IOHandler(handle, isOwner: false);
        }

        #region Access functions
        /// <summary>
        /// Creates a void <see cref="IOHandler"/>. All read operations return 0 bytes and
        /// set the EOF flag. All write operations discard the given data.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Creates a <see cref="IOHandler"/> from a file.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="filepath">Full path to the file.</param>
        /// <param name="access">"r" for read access, or "w" for write access.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context, string filepath, string access)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero, filepath, access), context);
        }

        /// <summary>
        /// Creates a <see cref="IOHandler"/> from a memory block.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <param name="memorySize">The size of the block of memory in bytes.</param>
        /// <param name="access">"r" for read access, or "w" for write access.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context, IntPtr handle, uint memorySize, string access)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero, handle, memorySize, access), context);
        }
        #endregion

        #region Read functions
        /// <summary>
        /// Reads an 8-bit unsigned integer.
        /// </summary>
        /// <param name="n">Returns the 8-bit unsigned integer.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref byte n)
        {
            return Interop.ReadUint8(handle, ref n);
        }

        /// <summary>
        /// Reads a 16-bit unsigned integer.
        /// </summary>
        /// <param name="n">Returns the 16-bit unsigned integer.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref ushort n)
        {
            return Interop.ReadUint16(handle, ref n);
        }

        /// <summary>
        /// Reads a 32-bit unsigned integer.
        /// </summary>
        /// <param name="n">Returns the 32-bit unsigned integer.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref uint n)
        {
            return Interop.ReadUint32(handle, ref n);
        }

        /// <summary>
        /// Reads a 64-bit unsigned integer.
        /// </summary>
        /// <param name="n">Returns the 64-bit unsigned integer.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref ulong n)
        {
            return Interop.ReadUint64(handle, ref n);
        }

        /// <summary>
        /// Reads a 32-bit floating point number.
        /// </summary>
        /// <param name="f">Returns the 32-bit floating point number.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref float f)
        {
            return Interop.ReadFloat(handle, ref f);
        }

        /// <summary>
        /// Reads a double precision floating point point number.
        /// </summary>
        /// <param name="d">Returns the double precision floating point number.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref double d)
        {
            return Interop.Read15Fixed16(handle, ref d);
        }

        /// <summary>
        /// Reads an XYZ tristimulus value.
        /// </summary>
        /// <param name="xyz">Returns the XYZ tristimulus value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ref CIEXYZ xyz)
        {
            return Interop.ReadXYZ(handle, ref xyz);
        }

        /// <summary>
        /// Reads an array of 16-bit unsigned integers.
        /// </summary>
        /// <param name="array">The array of 16-bit unsigned integers.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(ushort[] array)
        {
            return Interop.ReadUint16Array(handle, array);
        }
        #endregion

        #region Write functions
        /// <summary>
        /// Writes an 8-bit unsigned integer.
        /// </summary>
        /// <param name="n">The 8-bit unsigned integer to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(byte n)
        {
            return Interop.WriteUint8(handle, n);
        }

        /// <summary>
        /// Writes a 16-bit unsigned integer.
        /// </summary>
        /// <param name="n">The 16-bit unsigned integer to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(ushort n)
        {
            return Interop.WriteUint16(handle, n);
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer.
        /// </summary>
        /// <param name="n">The 32-bit unsigned integer to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(uint n)
        {
            return Interop.WriteUint32(handle, n);
        }

        /// <summary>
        /// Writes a 64-bit unsigned integer.
        /// </summary>
        /// <param name="n">The 64-bit unsigned integer to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(ulong n)
        {
            return Interop.WriteUint64(handle, n);
        }

        /// <summary>
        /// Writes a 32-bit floating point number.
        /// </summary>
        /// <param name="f">The 32-bit floating point number to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(float f)
        {
            return Interop.WriteFloat(handle, f);
        }

        /// <summary>
        /// Writes a double precision floating point point number.
        /// </summary>
        /// <param name="d">The double precision floating point number to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(double d)
        {
            return Interop.Write15Fixed16(handle, d);
        }

        /// <summary>
        /// Writes an XYZ tristimulus value.
        /// </summary>
        /// <param name="xyz">The XYZ tristimulus value to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(CIEXYZ xyz)
        {
            return Interop.WriteXYZ(handle, xyz);
        }

        /// <summary>
        /// Writes an array of 16-bit unsigned integers.
        /// </summary>
        /// <param name="array">The array of 16-bit unsigned integers.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(ushort[] array)
        {
            return Interop.WriteUint16Array(handle, array);
        }
        #endregion

        #region Alignment functions
        /// <summary>
        /// Skips bytes on the I/O handler until a 32-bit aligned position.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public bool ReadAlignment()
        {
            return Interop.ReadAlignment(handle);
        }

        /// <summary>
        /// Writes zeroes on theI/O handler until a 32-bit aligned position.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public bool WriteAlignment()
        {
            return Interop.WriteAlignment(handle);
        }
        #endregion

        #region Type base helper functions
        /// <summary>
        /// Reads a <see cref="TagTypeSignature"/> from the I/O handler.
        /// </summary>
        /// <param name="sig">Returns the <see cref="TagTypeSignature"/>.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Read(out TagTypeSignature sig)
        {
            uint s = Interop.ReadTypeBase(handle);
            sig = (TagTypeSignature)s;
            return s != 0;
        }

        /// <summary>
        /// Writes a <see cref="TagTypeSignature"/> to the I/O handler.
        /// </summary>
        /// <param name="sig">The <see cref="TagTypeSignature"/> to be written.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool Write(TagTypeSignature sig)
        {
            return Interop.WriteTypeBase(handle, (uint)sig);
        }
        #endregion

        /// <summary>
        /// Frees the i/o handler handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.CloseIOHandler(handle);
            return true;
        }
    }
}
