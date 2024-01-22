// Copyright(c) 2019-2024 John Stevenson-Hoare
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
    /// Represents a private tag used by Microsoft to describe GPU hardware
    /// pipelines for displays.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MHC2
    {
        /// <summary>
        /// Number of elements in each 1D LUT.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int CurveEntries;
        /// <summary>
        /// The red 1D LUT.
        /// </summary>
        public IntPtr RedCurve;
        /// <summary>
        /// The blue 1D LUT.
        /// </summary>
        public IntPtr GreenCurve;
        /// <summary>
        /// The green 1D LUT.
        /// </summary>
        public IntPtr BlueCurve;
        /// <summary>
        /// The ST.2086 min luminance in nits.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double MinLuminance;
        /// <summary>
        /// The ST.2086 peak luminance in nits.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double PeakLuminance;
        /// <summary>
        /// The 3x4 XYZ to XYZ adjustment matrix.
        /// </summary>
        public IntPtr XYZ2XYXmatrix;

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="MHC2"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="MHC2"/> instance.</returns>
        internal static MHC2 FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<MHC2>(handle);
        }
    }
}
