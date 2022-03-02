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
        [DllImport(Liblcms, EntryPoint = "_cmsVEC3init", CallingConvention = CallingConvention.StdCall)]
        private static extern void VEC3init_Internal(
                ref VEC3 v,
                [MarshalAs(UnmanagedType.R8)] double x,
                [MarshalAs(UnmanagedType.R8)] double y,
                [MarshalAs(UnmanagedType.R8)] double z);

        internal static void VEC3init(ref VEC3 v, double x, double y, double z)
        {
            VEC3init_Internal(ref v, x, y, z);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsVEC3minus", CallingConvention = CallingConvention.StdCall)]
        private static extern void VEC3minus_Internal(
                ref VEC3 r,
                in VEC3 a,
                in VEC3 b);

        internal static void VEC3minus(ref VEC3 v, in VEC3 a, in VEC3 b)
        {
            VEC3minus_Internal(ref v, a, b);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsVEC3cross", CallingConvention = CallingConvention.StdCall)]
        private static extern void VEC3cross_Internal(
                ref VEC3 r,
                in VEC3 a,
                in VEC3 b);

        internal static void VEC3cross(ref VEC3 v, in VEC3 a, in VEC3 b)
        {
            VEC3cross_Internal(ref v, a, b);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsVEC3dot", CallingConvention = CallingConvention.StdCall)]
        private static extern double VEC3dot_Internal(
                in VEC3 a,
                in VEC3 b);

        internal static double VEC3dot(in VEC3 a, in VEC3 b)
        {
            return VEC3dot_Internal(a, b);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsVEC3length", CallingConvention = CallingConvention.StdCall)]
        private static extern double VEC3length_Internal(
                in VEC3 a);

        internal static double VEC3length(in VEC3 a)
        {
            return VEC3length_Internal(a);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsVEC3distance", CallingConvention = CallingConvention.StdCall)]
        private static extern double VEC3distance_Internal(
                in VEC3 a,
                in VEC3 b);

        internal static double VEC3distance(in VEC3 a, in VEC3 b)
        {
            return VEC3distance_Internal(a, b);
        }
    }
}
