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

using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCIE2000DeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double CIE2000DeltaE_Internal(
            in CIELab lab1,
            in CIELab lab2,
            [MarshalAs(UnmanagedType.R8)] double kL,
            [MarshalAs(UnmanagedType.R8)] double kC,
            [MarshalAs(UnmanagedType.R8)] double kH
            );

        internal static double CIE2000DeltaE(in CIELab lab1, in CIELab lab2, double kL = 1.0, double kC = 1.0, double kH = 1.0)
        {
            return CIE2000DeltaE_Internal(lab1, lab2, kL, kC, kH);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double DeltaE_Internal(
            in CIELab lab1,
            in CIELab lab2);

        internal static double DeltaE(in CIELab lab1, in CIELab lab2)
        {
            return DeltaE_Internal(lab1, lab2);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCMCdeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double CMCdeltaE_Internal(
            in CIELab lab1,
            in CIELab lab2,
            [MarshalAs(UnmanagedType.R8)] double l,
            [MarshalAs(UnmanagedType.R8)] double c);

        internal static double CMCDeltaE(in CIELab lab1, in CIELab lab2, double l, double c)
        {
            return CMCdeltaE_Internal(lab1, lab2, l, c);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBFDdeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double BFDdeltaE_Internal(
            in CIELab lab1,
            in CIELab lab2);

        internal static double BFDDeltaE(in CIELab lab1, in CIELab lab2)
        {
            return BFDdeltaE_Internal(lab1, lab2);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIE94DeltaE", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.R8)]
        private static extern double CIE94DeltaE_Internal(
            in CIELab lab1,
            in CIELab lab2);

        internal static double CIE94DeltaE(in CIELab lab1, in CIELab lab2)
        {
            return CIE94DeltaE_Internal(lab1, lab2);
        }

    }
}
