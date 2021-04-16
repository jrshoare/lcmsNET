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
    /// Represents a video card gamma table.
    /// </summary>
    public sealed class VideoCardGamma
    {
        /// <summary>
        /// Creates a video card gamma table from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing video card gamma table.</param>
        /// <returns>A new <see cref="VideoCardGamma"/> instance referencing an existing video card gamma table.</returns>
        public static VideoCardGamma FromHandle(IntPtr handle)
        {
            return new VideoCardGamma(Marshal.PtrToStructure<_vcgt>(handle));
        }

        private VideoCardGamma(_vcgt vcgt)
        {
            this.vcgt = vcgt;
            Red = ToneCurve.FromHandle(vcgt.red);
            Green = ToneCurve.FromHandle(vcgt.green);
            Blue = ToneCurve.FromHandle(vcgt.blue);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="VideoCardGamma"/> class.
        /// </summary>
        /// <param name="red">A tone curve for the red component.</param>
        /// <param name="green">A tone curve for the red component.</param>
        /// <param name="blue">A tone curve for the red component.</param>
        public VideoCardGamma(ToneCurve red, ToneCurve green, ToneCurve blue)
        {
            Red = red;
            vcgt.red = red.Handle;
            Green = green;
            vcgt.green = green.Handle;
            Blue = blue;
            vcgt.blue = blue.Handle;
        }

        /// <summary>
        /// Gets the video card gamma table red tone curve.
        /// </summary>
        public ToneCurve Red { get; private set; }

        /// <summary>
        /// Gets the video card gamma table green tone curve.
        /// </summary>
        public ToneCurve Green { get; private set; }

        /// <summary>
        /// Gets the video card gamma table blue tone curve.
        /// </summary>
        public ToneCurve Blue { get; private set; }

        internal IntPtr ToHandle()
        {
            int size = Marshal.SizeOf<_vcgt>();
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(vcgt, ptr, false);
            return ptr;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct _vcgt
        {
            public IntPtr red;
            public IntPtr green;
            public IntPtr blue;
        }
        private _vcgt vcgt;
    }
}
