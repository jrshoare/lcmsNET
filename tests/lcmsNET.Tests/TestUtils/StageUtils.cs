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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests.TestUtils
{
    public static class StageUtils
    {
        public static Stage CreateStage(uint inputChannels, uint outputChannels, ushort[] table) =>
            Stage.Create(context: null, nGridPoints: 2, inputChannels, outputChannels, table);

        public static ushort Fn8D1(ushort a1, ushort a2, ushort a3, ushort a4, ushort a5, ushort a6, ushort a7, ushort a8, uint m)
        {
            return (ushort)((a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8) / m);
        }

        public static ushort Fn8D2(ushort a1, ushort a2, ushort a3, ushort a4, ushort a5, ushort a6, ushort a7, ushort a8, uint m)
        {
            return (ushort)((a1 + 3 * a2 + 3 * a3 + a4 + a5 + a6 + a7 + a8) / (m + 4));
        }

        public static ushort Fn8D3(ushort a1, ushort a2, ushort a3, ushort a4, ushort a5, ushort a6, ushort a7, ushort a8, uint m)
        {
            return (ushort)((3 * a1 + 2 * a2 + 3 * a3 + a4 + a5 + a6 + a7 + a8) / (m + 5));
        }

        public static int Sampler3D(ushort[] input, ushort[] output, IntPtr cargo)
        {
            output[0] = Fn8D1(input[0], input[1], input[2], 0, 0, 0, 0, 0, 3);
            output[1] = Fn8D2(input[0], input[1], input[2], 0, 0, 0, 0, 0, 3);
            output[2] = Fn8D3(input[0], input[1], input[2], 0, 0, 0, 0, 0, 3);

            return 1; // 1 = true, 0 = false
        }

        public static int Sampler3D(float[] input, float[] output, IntPtr cargo)
        {
            return 1; // 1 = true, 0 = false
        }

        public static int EstimateTAC(ushort[] input, ushort[] output, IntPtr cargo)
        {
            Assert.IsNull(output);

            return 1; // 1 = true, 0 = false
        }

        public static int EstimateTAC(float[] input, float[] output, IntPtr cargo)
        {
            Assert.IsNull(output);

            return 1; // 1 = true, 0 = false
        }

        public static int SamplerInspect(ushort[] input, ushort[] output, IntPtr cargo)
        {
            return 1; // 1 = true, 0 = false
        }

        public static int SamplerInspect(float[] input, float[] output, IntPtr cargo)
        {
            return 1; // 1 = true, 0 = false
        }
    }
}
