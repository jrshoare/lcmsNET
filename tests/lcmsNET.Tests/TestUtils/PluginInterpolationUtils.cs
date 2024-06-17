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
    public static class PluginInterpolationUtils
    {
        public static PluginInterpolation CreatePluginInterpolation(InterpolatorsFactory factory) =>
            new()
            {
                Base = new PluginBase
                {
                    Magic = Cms.PluginMagicNumber,
                    ExpectedVersion = (uint)Cms.EncodedCMMVersion,  // >= 2.8
                    Type = PluginType.Interpolation,
                    Next = IntPtr.Zero
                },
                Factory = Marshal.GetFunctionPointerForDelegate(factory)
            };


        // this fake interpolation always takes the closest lower node in the interpolation table for 1D
        public static void Fake1DFloat(IntPtr input, IntPtr output, IntPtr p)
        {
            int encodedVersion = 2070;
            // next call throws if less than 2.8
            try { encodedVersion = Cms.EncodedCMMVersion; } catch { }

            unsafe
            {
                float* LutTable = null;
                uint[] Domain = null;

                if (encodedVersion >= 2120)
                {
                    var interpParams = Marshal.PtrToStructure<InterpolationParamsV2>(p);
                    LutTable = (float*)interpParams.Table.ToPointer();
                    Domain = interpParams.Domain;
                }
                else
                {
                    var interpParams = Marshal.PtrToStructure<InterpolationParamsV1>(p);
                    LutTable = (float*)interpParams.Table.ToPointer();
                    Domain = interpParams.Domain;
                }

                float* pInput = (float*)input.ToPointer();
                float* pOutput = (float*)output.ToPointer();

                if (pInput[0] >= 1.0)
                {
                    pOutput[0] = LutTable[Domain[0]];
                    return;
                }

                float val = Domain[0] * pInput[0];
                int cell = (int)Math.Floor(val);
                pOutput[0] = LutTable[cell];
            }
        }

        // this fake interpolation just uses scrambled negated indexes for output
        public static void Fake3D16(IntPtr input, IntPtr output, IntPtr p)
        {
            const ushort _ = 0xFFFF;
            unsafe
            {
                ushort* pInput = (ushort*)input.ToPointer();
                ushort* pOutput = (ushort*)output.ToPointer();

                pOutput[0] = (ushort)(_ - pInput[2]);
                pOutput[1] = (ushort)(_ - pInput[1]);
                pOutput[2] = (ushort)(_ - pInput[0]);
            }
        }

        public static InterpolationFunction Factory(uint nInputChannels, uint nOutputChannels, LerpFlags flags)
        {
            InterpolationFunction interpFn = new()
            {
                Interpolator = IntPtr.Zero
            };

            bool isFloat = (flags & LerpFlags.FloatingPoint) != 0;

            if (nInputChannels == 1 && nOutputChannels == 1 && isFloat)
            {
                interpFn.Interpolator = _fake1DfloatPtr;
            }
            else if (nInputChannels == 3 && nOutputChannels == 3 && !isFloat)
            {
                interpFn.Interpolator = _fake3D16Ptr;
            }

            return interpFn;
        }

        private static readonly InterpFnFloat _fake1Dfloat = new(Fake1DFloat);
        private static readonly IntPtr _fake1DfloatPtr = Marshal.GetFunctionPointerForDelegate(_fake1Dfloat);
        private static readonly InterpFn16 _fake3D16 = new(Fake3D16);
        private static readonly IntPtr _fake3D16Ptr = Marshal.GetFunctionPointerForDelegate(_fake3D16);
    }
}
