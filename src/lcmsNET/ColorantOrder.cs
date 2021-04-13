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
    /// <summary>
    /// Represents the laydown order that colorants will be printed on an n-colorant device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorantOrder
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        private readonly byte[] _;

        /// <summary>
        /// Initialises a new instance of the <see cref="ColorantOrder"/> class.
        /// </summary>
        /// <param name="bytes">An array of 16 values.</param>
        public ColorantOrder(byte[] bytes)
        {
            if (bytes?.Length != 16) throw new ArgumentException($"'{nameof(bytes)}' array size must equal 16.");
            _ = bytes;
        }

        /// <summary>
        /// Implicitly converts a <see cref="ColorantOrder"/> to a <see cref="byte"/> array.
        /// </summary>
        /// <param name="colorantOrder">The <see cref="ColorantOrder"/> to be converted.</param>
        public static implicit operator byte[](ColorantOrder colorantOrder) => colorantOrder._;

        /// <summary>
        /// Explicitly converts a <see cref="byte"/> array of 16 values to a <see cref="ColorantOrder"/>.
        /// </summary>
        /// <param name="bytes">The byte array to be converted.</param>
        public static explicit operator ColorantOrder(byte[] bytes) => new ColorantOrder(bytes);

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="ColorantOrder"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="ColorantOrder"/> instance.</returns>
        public static ColorantOrder FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<ColorantOrder>(handle);
        }
    }
}
