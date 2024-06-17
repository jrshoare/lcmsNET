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
    public static class PluginOptimizationUtils
    {
        public static PluginOptimization CreatePluginOptimization() =>
            new()
            {
                Base = Constants.PluginOptimization.Base,
                Optimize = Marshal.GetFunctionPointerForDelegate(_optimize)
            };

        private static void FastEvaluteCurves(IntPtr In, IntPtr Out, IntPtr Data)
        {
            unsafe
            {
                ushort* pIn = (ushort*)In.ToPointer();
                ushort* pOut = (ushort*)Out.ToPointer();

                pOut[0] = pIn[0];
            }
        }

#pragma warning disable CS0649
        private struct StageToneCurveData
        {
            public uint nCurves;
            public IntPtr TheCurves;    // cmsToneCurve**
        }
#pragma warning restore CS0649

        private static int MyOptimize(IntPtr lut, uint intent, IntPtr inputFormat, IntPtr outputFormat, IntPtr dwFlags)
        {
            unsafe
            {
                IntPtr* pLut = (IntPtr*)lut.ToPointer();
                using var pipeline = Pipeline.FromHandle(pLut[0]);
                foreach (var stage in pipeline)
                {
                    if (stage.StageType != StageSignature.CurveSetElemType) return 0;

                    StageToneCurveData* data = (StageToneCurveData*)stage.Data;
                    if (data->nCurves != 1) return 0;
                    IntPtr* theCurves = (IntPtr*)data->TheCurves;
                    using var toneCurve = ToneCurve.FromHandle(theCurves[0]);
                    if (toneCurve.EstimateGamma(0.1) > 1.0) return 0;
                }

                uint* flags = (uint*)dwFlags.ToPointer();
                *flags |= (uint)CmsFlags.NoCache;

                pipeline.SetOptimizationParameters(FastEvaluteCurves, IntPtr.Zero, null, null);
            }

            return 1;
        }

        private static readonly OptimizeFn _optimize = new(MyOptimize);
    }
}
