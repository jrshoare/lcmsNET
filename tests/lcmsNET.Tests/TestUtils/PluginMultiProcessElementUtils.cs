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
    public static class PluginMultiProcessElementUtils
    {
        public static PluginMultiProcessElement CreatePluginMultiProcessElement() =>
            new()
            {
                Base = Constants.PluginMultiProcessElement.Base,
                Handler = new TagTypeHandler
                {
                    Signature = Constants.PluginMultiProcessElement.SigNegateType,
                    Read = Marshal.GetFunctionPointerForDelegate(_read),
                    Write = Marshal.GetFunctionPointerForDelegate(_write),
                    Duplicate = IntPtr.Zero,
                    Free = IntPtr.Zero
                }
            };

        private static void EvaluateNegate(IntPtr In, IntPtr Out, IntPtr mpe)
        {
            unsafe
            {
                float* pOut = (float*)Out.ToPointer();
                float* pIn = (float*)In.ToPointer();

                pOut[0] = 1.0f - pIn[0];
                pOut[1] = 1.0f - pIn[1];
                pOut[2] = 1.0f - pIn[2];
            }
        }

        public static IntPtr StageAllocNegate(IntPtr contextID)
        {
            using var context = Context.FromHandle(contextID);
            return Stage.AllocPlaceholder(context, (StageSignature)Constants.PluginMultiProcessElement.SigNegateType, 3, 3,
                    EvaluateNegate, null, null, IntPtr.Zero).Release();
        }

        private static IntPtr NegateRead(in TagTypeHandler self, IntPtr io, out uint nItems, uint tagSize)
        {
            nItems = 1;

            using (var iohandler = IOHandler.FromHandle(io))
            {
                ushort chans = 0;
                if (!iohandler.Read(ref chans)) return IntPtr.Zero;
                if (chans != 3) return IntPtr.Zero;
            }

            return StageAllocNegate(self.ContextID);
        }

        private static int NegateWrite(in TagTypeHandler self, IntPtr io, IntPtr ptr, uint nItems)
        {
            using var iohandler = IOHandler.FromHandle(io);
            const ushort chans = 3;
            return iohandler.Write(chans) ? 1 : 0;
        }

        private static readonly TagTypeRead _read = new (NegateRead);
        private static readonly TagTypeWrite _write = new (NegateWrite);

    }
}
