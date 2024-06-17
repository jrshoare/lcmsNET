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

using lcmsNET.Plugin;
using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.TestUtils
{
    public static class PluginTransformUtils
    {
        public static PluginTransform CreatePluginTransform() =>
            new()
            {
                Base = Constants.PluginTransform.Base,
                Factory = Marshal.GetFunctionPointerForDelegate(_factory)
            };


        // only works for gray8 as output and always returns '42'
        private static void TranscendentalTransform2(IntPtr CMMCargo, IntPtr InputBuffer, IntPtr OutputBuffer, uint PixelsPerLine,
                uint LineCount, IntPtr Stride)
        {
            unsafe
            {
                byte* pOut = (byte*)OutputBuffer.ToPointer();
                Stride* stride = (Stride*)Stride.ToPointer();

                for (uint line = 0; line < LineCount; line++)
                {
                    for (uint pixel = 0; pixel < PixelsPerLine; pixel++)
                    {
                        *pOut++ = 42;
                    }
                    pOut += stride->BytesPerLineOut - PixelsPerLine;
                }
            }
        }

        private static int MyTransformFactory(IntPtr xformPtr, IntPtr UserData, IntPtr FreePrivateDataFn, IntPtr Lut,
                IntPtr InputFormat, IntPtr OutputFormat, IntPtr dwFlags)
        {
            unsafe
            {
                uint* outputFormat = (uint*)OutputFormat.ToPointer();
                if (*outputFormat == Cms.TYPE_GRAY_8)
                {
                    IntPtr* ptr = (IntPtr*)xformPtr.ToPointer();
                    *ptr = Marshal.GetFunctionPointerForDelegate(new Transform2Fn(TranscendentalTransform2));
                    return 1;
                }

                return 0;
            }
        }

        private static readonly TransformFactory _factory = new(MyTransformFactory);
    }
}
