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
        [DllImport(Liblcms, EntryPoint = "_cmsEncodeDateTimeNumber", CallingConvention = CallingConvention.StdCall)]
        private static extern void EncodeDateTimeNumber_Internal(
                ref DateTimeNumber d,
                in Tm tm);

        internal static void EncodeDateTimeNumber(ref DateTimeNumber d, in Tm tm)
        {
            EncodeDateTimeNumber_Internal(ref d, in tm);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsDecodeDateTimeNumber", CallingConvention = CallingConvention.StdCall)]
        private static extern void DecodeDateTimeNumber_Internal(
                in DateTimeNumber d,
                ref Tm tm);

        internal static void DecodeDateTimeNumber(in DateTimeNumber d, ref Tm tm)
        {
            DecodeDateTimeNumber_Internal(in d, ref tm);
        }
    }
}
