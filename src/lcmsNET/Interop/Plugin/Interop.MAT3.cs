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

using lcmsNET.Plugin;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "_cmsMAT3identity", CallingConvention = CallingConvention.StdCall)]
        private static extern void MAT3identity_Internal(
                ref MAT3 a);

        internal static void MAT3identity(ref MAT3 a)
        {
            MAT3identity_Internal(ref a);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsMAT3isIdentity", CallingConvention = CallingConvention.StdCall)]
        private static extern int MAT3isIdentity_Internal(
                in MAT3 a);

        internal static bool MAT3isIdentity(in MAT3 a)
        {
            return MAT3isIdentity_Internal(in a) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsMAT3per", CallingConvention = CallingConvention.StdCall)]
        private static extern void MAT3per_Internal(
                ref MAT3 r,
                in MAT3 a,
                in MAT3 b);

        internal static void MAT3multiply(ref MAT3 r, in MAT3 a, in MAT3 b)
        {
            MAT3per_Internal(ref r, in a, in b);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsMAT3inverse", CallingConvention = CallingConvention.StdCall)]
        private static extern int MAT3inverse_Internal(
                in MAT3 a,
                ref MAT3 b);

        internal static bool MAT3invert(in MAT3 a, ref MAT3 b)
        {
            return MAT3inverse_Internal(in a, ref b) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsMAT3solve", CallingConvention = CallingConvention.StdCall)]
        private static extern int MAT3solve_Internal(
                ref VEC3 x,
                in MAT3 a,
                in VEC3 b);

        internal static bool MAT3solve(ref VEC3 x, in MAT3 a, in VEC3 b)
        {
            return MAT3solve_Internal(ref x, in a, in b) != 0;
        }

        [DllImport(Liblcms, EntryPoint = "_cmsMAT3eval", CallingConvention = CallingConvention.StdCall)]
        private static extern int MAT3eval_Internal(
                ref VEC3 r,
                in MAT3 a,
                in VEC3 v);

        internal static bool MAT3eval(ref VEC3 r, in MAT3 a, in VEC3 v)
        {
            return MAT3eval_Internal(ref r, in a, in v) != 0;
        }
    }
}
