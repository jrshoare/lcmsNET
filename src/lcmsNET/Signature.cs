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
    /// Represents a signature.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Signature
    {
        [MarshalAs(UnmanagedType.U4)]
        private readonly uint _;

        /// <summary>
        /// Initialises a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="u">The signature value.</param>
        public Signature(uint u)
        {
            _ = u;
        }

        /// <summary>
        /// Implicitly converts a <see cref="Signature"/> to an unsigned integer.
        /// </summary>
        /// <param name="signature">The <see cref="Signature"/> to be converted.</param>
        public static implicit operator uint(Signature signature) => signature._;

        /// <summary>
        /// Explicitly converts an unsigned integer to a <see cref="Signature"/>.
        /// </summary>
        /// <param name="u">The unsigned integer to be converted.</param>
        public static explicit operator Signature(uint u) => new Signature(u);

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="Signature"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="Signature"/> instance.</returns>
        public static Signature FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<Signature>(handle);
        }
    }
}
