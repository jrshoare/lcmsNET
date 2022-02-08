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
    /// Represents an under color removal and black generation.
    /// </summary>
    public sealed class UcrBg
    {
        /// <summary>
        /// Creates an under color removal and black generation from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing under color removal and black generation.</param>
        /// <returns>A new <see cref="UcrBg"/> instance referencing an existing under color removal and black generation.</returns>
        internal static UcrBg FromHandle(IntPtr handle)
        {
            return new UcrBg(Marshal.PtrToStructure<_ucrBg>(handle));
        }

        private UcrBg(_ucrBg ucrBg)
        {
            this.ucrBg = ucrBg;
            Ucr = ToneCurve.FromHandle(ucrBg.ucr);
            Bg = ToneCurve.FromHandle(ucrBg.bg);
            Desc = MultiLocalizedUnicode.FromHandle(ucrBg.desc);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UcrBg"/> class.
        /// </summary>
        /// <param name="ucr">A tone curve for under color removal.</param>
        /// <param name="bg">A tone curve for black generation.</param>
        /// <param name="desc">A description of the method used for under color removal and black generation.</param>
        public UcrBg(ToneCurve ucr, ToneCurve bg, MultiLocalizedUnicode desc)
        {
            Ucr = ucr;
            ucrBg.ucr = ucr.Handle;
            Bg = bg;
            ucrBg.bg = bg.Handle;
            Desc = desc;
            ucrBg.desc = desc.Handle;
        }

        /// <summary>
        /// Gets the under color removal tone curve.
        /// </summary>
        public ToneCurve Ucr { get; private set; }

        /// <summary>
        /// Gets the black generation tone curve.
        /// </summary>
        public ToneCurve Bg { get; private set; }

        /// <summary>
        /// Gets the description of the method used for under color removal and black generation.
        /// </summary>
        public MultiLocalizedUnicode Desc { get; private set; }

        internal IntPtr ToHandle()
        {
            int size = Marshal.SizeOf<_ucrBg>();
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(ucrBg, ptr, false);
            return ptr;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct _ucrBg
        {
            public IntPtr ucr;
            public IntPtr bg;
            public IntPtr desc;
        }
        private _ucrBg ucrBg;
    }
}
