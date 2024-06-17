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
using System.Runtime.InteropServices;
using System;

namespace lcmsNET.Tests.TestUtils
{
    public static class PluginFormattersUtils
    {
        public static PluginFormatters CreatePluginFormatters() =>
            new()
            {
                Base = Constants.PluginFormatters.Base
            };

        public static Formatter MyFormatterFactory(uint Type, FormatterDirection Dir, uint dwFlags)
        {
            Formatter result = new()
            {
                Fmt = IntPtr.Zero
            };

            if ((Type == Constants.PluginFormatters.TYPE_RGB_565) &&
                ((dwFlags & PluginFormatters.PACK_FLAGS_FLOAT) == 0) &&
                (Dir == FormatterDirection.Input))
            {
                result.Fmt = Marshal.GetFunctionPointerForDelegate(_myUnroll565);
            }

            return result;
        }

        public static Formatter MyFormatterFactory2(uint Type, FormatterDirection Dir, uint dwFlags)
        {
            Formatter result = new()
            {
                Fmt = IntPtr.Zero
            };

            if ((Type == Constants.PluginFormatters.TYPE_RGB_565) &&
                ((dwFlags & PluginFormatters.PACK_FLAGS_FLOAT) == 0) &&
                (Dir == FormatterDirection.Output))
            {
                result.Fmt = Marshal.GetFunctionPointerForDelegate(_myPack565);
            }

            return result;
        }

        public static IntPtr MyUnroll565(IntPtr CMMCargo, IntPtr Values, IntPtr Buffer, uint Stride)
        {
            unsafe
            {
                ushort pixel = *(ushort*)Buffer.ToPointer();

                double r = Math.Floor((pixel & 31) * 65535.0 / 31.0 + 0.5);
                double g = Math.Floor((((pixel >> 5) & 63) * 65535.0) / 63.0 + 0.5);
                double b = Math.Floor((((pixel >> 11) & 31) * 65535.0) / 31.0 + 0.5);

                ushort* wIn = (ushort*)Values.ToPointer();
                wIn[2] = (ushort)r;
                wIn[1] = (ushort)g;
                wIn[0] = (ushort)b;

                return Buffer + 2;
            }
        }

        public static IntPtr MyPack565(IntPtr CMMCargo, IntPtr Values, IntPtr Buffer, uint Stride)
        {
            unsafe
            {
                ushort* wOut = (ushort*)Values.ToPointer();

                int r = (int)Math.Floor((wOut[2] * 31) / 65535.0 + 0.5);
                int g = (int)Math.Floor((wOut[1] * 63) / 65535.0 + 0.5);
                int b = (int)Math.Floor((wOut[0] * 31) / 65535.0 + 0.5);

                ushort pixel = (ushort)((r & 31) | ((g & 63) << 5) | ((b & 31) << 11));

                ushort* output = (ushort*)Buffer.ToPointer();
                *output = pixel;

                return Buffer + 2;
            }
        }

        private static readonly Formatter16 _myUnroll565 = new(MyUnroll565);
        private static readonly Formatter16 _myPack565 = new(MyPack565);
    }
}
