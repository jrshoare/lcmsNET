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
using System.Linq;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsEvalToneCurveFloat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial float EvalToneCurveFloat_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R4)] float v);
#else
        [DllImport(Liblcms, EntryPoint = "cmsEvalToneCurveFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern float EvalToneCurveFloat_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R4)] float v);
#endif

        internal static float EvaluateToneCurve(IntPtr contextID, float v)
        {
            return EvalToneCurveFloat_Internal(contextID, v);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsEvalToneCurve16")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial ushort EvalToneCurve16_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U2)] ushort v);
#else
        [DllImport(Liblcms, EntryPoint = "cmsEvalToneCurve16", CallingConvention = CallingConvention.StdCall)]
        private static extern ushort EvalToneCurve16_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U2)] ushort v);
#endif

        internal static ushort EvaluateToneCurve(IntPtr contextID, ushort v)
        {
            return EvalToneCurve16_Internal(contextID, v);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsBuildParametricToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr BuildParametricToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int type,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] [In] double[] parameters);
#else
        [DllImport(Liblcms, EntryPoint = "cmsBuildParametricToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildParametricToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int type,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] parameters);
#endif

        internal static IntPtr BuildParametricToneCurve(IntPtr contextID, int type, double[] parameters)
        {
            return BuildParametricToneCurve_Internal(contextID, type, parameters);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsBuildGamma")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr BuildGamma_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double gamma);
#else
        [DllImport(Liblcms, EntryPoint = "cmsBuildGamma", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildGamma_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double gamma);
#endif

        internal static IntPtr BuildGammaToneCurve(IntPtr contextID, double gamma)
        {
            return BuildGamma_Internal(contextID, gamma);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsBuildSegmentedToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr BuildSegmentedToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nSegments,
                IntPtr segments);
#else
        [DllImport(Liblcms, EntryPoint = "cmsBuildSegmentedToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildSegmentedToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nSegments,
                IntPtr segments);
#endif

        internal static IntPtr BuildSegmentedToneCurve(IntPtr contextID, CurveSegment[] segments)
        {
            var totalSize = segments.Select(_ => Marshal.SizeOf(_)).Sum();
            IntPtr ptr = Marshal.AllocHGlobal(totalSize);
            try
            {
                byte[] temp = new byte[totalSize];
                int start = 0;
                foreach (var segment in segments)
                {
                    int segmentLength = Marshal.SizeOf(segment);
                    IntPtr segmentPtr = Marshal.AllocHGlobal(segmentLength);
                    Marshal.StructureToPtr(segment, segmentPtr, false);
                    Marshal.Copy(segmentPtr, temp, start, segmentLength);
                    start += segmentLength;
                    Marshal.DestroyStructure<CurveSegment>(segmentPtr);
                    Marshal.FreeHGlobal(segmentPtr);
                }
                Marshal.Copy(temp, 0, ptr, totalSize);

                return BuildSegmentedToneCurve_Internal(contextID, segments.Length, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsBuildTabulatedToneCurve16")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr BuildTabulatedToneCurve16_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nEntries,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] [In] ushort[] parameters);
#else
        [DllImport(Liblcms, EntryPoint = "cmsBuildTabulatedToneCurve16", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildTabulatedToneCurve16_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nEntries,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] ushort[] parameters);
#endif

        internal static IntPtr BuildTabulatedToneCurve(IntPtr contextID, ushort[] values)
        {
            return BuildTabulatedToneCurve16_Internal(contextID, values.Length, values);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsBuildTabulatedToneCurveFloat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr BuildTabulatedToneCurveFloat_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nEntries,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] [In] float[] parameters);
#else
        [DllImport(Liblcms, EntryPoint = "cmsBuildTabulatedToneCurveFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildTabulatedToneCurveFloat_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nEntries,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] parameters);
#endif

        internal static IntPtr BuildTabulatedToneCurve(IntPtr contextID, float[] values)
        {
            return BuildTabulatedToneCurveFloat_Internal(contextID, values.Length, values);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDupToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr DupToneCurve_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDupToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DupToneCurve_Internal(
                IntPtr handle);
#endif

        internal static IntPtr DuplicateToneCurve(IntPtr handle)
        {
            return DupToneCurve_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsReverseToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr ReverseToneCurve_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsReverseToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ReverseToneCurve_Internal(
                IntPtr handle);
#endif

        internal static IntPtr ReverseToneCurve(IntPtr handle)
        {
            return ReverseToneCurve_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsReverseToneCurveEx")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr ReverseToneCurveEx_Internal(
                [MarshalAs(UnmanagedType.I4)] int nResultSamples,
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsReverseToneCurveEx", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ReverseToneCurveEx_Internal(
                [MarshalAs(UnmanagedType.I4)] int nResultSamples,
                IntPtr handle);
#endif

        internal static IntPtr ReverseToneCurve(IntPtr handle, int nResultSamples)
        {
            return ReverseToneCurveEx_Internal(nResultSamples, handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsJoinToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr JoinToneCurve_Internal(
                IntPtr contextID,
                IntPtr x,
                IntPtr y,
                [MarshalAs(UnmanagedType.U4)] uint nPoints);
#else
        // Use classic DllImport but change parameter to uint to match UnmanagedType.U4.
        [DllImport(Liblcms, EntryPoint = "cmsJoinToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr JoinToneCurve_Internal(
                IntPtr contextID,
                IntPtr x,
                IntPtr y,
                [MarshalAs(UnmanagedType.U4)] uint nPoints);
#endif

        internal static IntPtr JoinToneCurve(IntPtr contextID, IntPtr x, IntPtr y, int nPoints)
        {
            return JoinToneCurve_Internal(contextID, x, y, (uint)nPoints);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSmoothToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int SmoothToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double lambda);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSmoothToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern int SmoothToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double lambda);
#endif

        internal static int SmoothToneCurve(IntPtr handle, double lambda)
        {
            return SmoothToneCurve_Internal(handle, lambda);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsToneCurveMultisegment")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsToneCurveMultisegment_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveMultisegment", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveMultisegment_Internal(
                IntPtr handle);
#endif

        internal static int IsMultiSegmentToneCurve(IntPtr handle)
        {
            return IsToneCurveMultisegment_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsToneCurveLinear")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsToneCurveLinear_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveLinear", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveLinear_Internal(
                IntPtr handle);
#endif

        internal static int IsLinearToneCurve(IntPtr handle)
        {
            return IsToneCurveLinear_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsToneCurveMonotonic")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsToneCurveMonotonic_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveMonotonic", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveMonotonic_Internal(
                IntPtr handle);
#endif

        internal static int IsMonotonicToneCurve(IntPtr handle)
        {
            return IsToneCurveMonotonic_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsToneCurveDescending")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsToneCurveDescending_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveDescending", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveDescending_Internal(
                IntPtr handle);
#endif

        internal static int IsDescendingToneCurve(IntPtr handle)
        {
            return IsToneCurveDescending_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsEstimateGamma")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial double EstimateGamma_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double precision);
#else
        [DllImport(Liblcms, EntryPoint = "cmsEstimateGamma", CallingConvention = CallingConvention.StdCall)]
        private static extern double EstimateGamma_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double precision);
#endif

        internal static double EstimateGamma(IntPtr handle, double precision)
        {
            return EstimateGamma_Internal(handle, precision);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetToneCurveEstimatedTableEntries")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetToneCurveEstimatedTableEntries_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetToneCurveEstimatedTableEntries", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetToneCurveEstimatedTableEntries_Internal(
                IntPtr handle);
#endif

        internal static uint GetEstimatedTableEntries(IntPtr handle)
        {
            return GetToneCurveEstimatedTableEntries_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetToneCurveEstimatedTable")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr GetToneCurveEstimatedTable_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetToneCurveEstimatedTable", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetToneCurveEstimatedTable_Internal(
                IntPtr handle);
#endif

        internal static IntPtr GetEstimatedTable(IntPtr handle)
        {
            return GetToneCurveEstimatedTable_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsFreeToneCurve")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void FreeToneCurve_Internal(IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsFreeToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern void FreeToneCurve_Internal(IntPtr handle);
#endif

        internal static void FreeToneCurve(IntPtr handle)
        {
            FreeToneCurve_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetToneCurveSegment")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr GetToneCurveSegment_Internal(
                [MarshalAs(UnmanagedType.I4)] int segment,
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetToneCurveSegment", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetToneCurveSegment_Internal(
                [MarshalAs(UnmanagedType.I4)] int segment,
                IntPtr handle);
#endif

        internal static IntPtr GetCurveSegment(IntPtr handle, int segment)
        {
            return GetToneCurveSegment_Internal(segment, handle);
        }
    }
}
