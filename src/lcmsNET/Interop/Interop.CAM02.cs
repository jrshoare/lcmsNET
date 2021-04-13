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

using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Init", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CIECAM02Init_Internal(
                IntPtr contextID,
                in ViewingConditions conditions);

        internal static IntPtr CIECAM02Init(IntPtr contextID, in ViewingConditions conditions)
        {
            return CIECAM02Init_Internal(contextID, conditions);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Done", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Done_Internal(
                IntPtr model);

        internal static void CIECAM02Done(IntPtr model)
        {
            CIECAM02Done_Internal(model);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Forward", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Forward_Internal(
                IntPtr model,
                in CIEXYZ xyz,
                out JCh jch);

        internal static void CIECAM02Forward(IntPtr model, in CIEXYZ xyz, out JCh jch)
        {
            CIECAM02Forward_Internal(model, xyz, out jch);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Reverse", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Reverse_Internal(
                IntPtr model,
                in JCh jch,
                out CIEXYZ xyz);

        internal static void CIECAM02Reverse(IntPtr model, in JCh jch, out CIEXYZ xyz)
        {
            CIECAM02Reverse_Internal(model, jch, out xyz);
        }
    }
}
