// Copyright(c) 2019-2022 John Stevenson-Hoare
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
        [DllImport(Liblcms, EntryPoint = "_cms8Fixed8toDouble", CallingConvention = CallingConvention.StdCall)]
        private static extern double Fixed8Dot8ToDouble_Internal(
                [MarshalAs(UnmanagedType.U2)] ushort fixed8);

        internal static double Fixed8Dot8ToDouble(ushort fixed8)
        {
            return Fixed8Dot8ToDouble_Internal(fixed8);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsDoubleTo8Fixed8", CallingConvention = CallingConvention.StdCall)]
        private static extern ushort DoubleToFixed8Dot8_Internal(
                [MarshalAs(UnmanagedType.R8)] double val);

        internal static ushort DoubleToFixed8Dot8(double val)
        {
            return DoubleToFixed8Dot8_Internal(val);
        }

        [DllImport(Liblcms, EntryPoint = "_cms15Fixed16toDouble", CallingConvention = CallingConvention.StdCall)]
        private static extern double Fixed15Dot16ToDouble_Internal(
                [MarshalAs(UnmanagedType.I4)] int fixed32);

        internal static double Fixed15Dot16ToDouble(int fixed32)
        {
            return Fixed15Dot16ToDouble_Internal(fixed32);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsDoubleTo15Fixed16", CallingConvention = CallingConvention.StdCall)]
        private static extern int DoubleToFixed15Dot16_Internal(
                [MarshalAs(UnmanagedType.R8)] double val);

        internal static int DoubleToFixed15Dot16(double val)
        {
            return DoubleToFixed15Dot16_Internal(val);
        }
    }
}
