using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsEvalToneCurveFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern float EvalToneCurveFloat_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R4)] float v);

        internal static float EvaluateToneCurve(IntPtr contextID, float v)
        {
            return EvalToneCurveFloat_Internal(contextID, v);
        }

        [DllImport(Liblcms, EntryPoint = "cmsEvalToneCurve16", CallingConvention = CallingConvention.StdCall)]
        private static extern ushort EvalToneCurve16_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U2)] ushort v);

        internal static ushort EvaluateToneCurve(IntPtr contextID, ushort v)
        {
            return EvalToneCurve16_Internal(contextID, v);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBuildParametricToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildParametricToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int type,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] parameters);

        internal static IntPtr BuildParametricToneCurve(IntPtr contextID, int type, double[] parameters)
        {
            return BuildParametricToneCurve_Internal(contextID, type, parameters);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBuildGamma", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildGamma_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double gamma);

        internal static IntPtr BuildGammaToneCurve(IntPtr contextID, double gamma)
        {
            return BuildGamma_Internal(contextID, gamma);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBuildSegmentedToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildSegmentedToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nSegments,
                in CurveSegment[] segments);

        internal static IntPtr BuildSegmentedToneCurve(IntPtr contextID, CurveSegment[] segments)
        {
            return BuildSegmentedToneCurve_Internal(contextID, segments.Length, segments);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBuildTabulatedToneCurve16", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildTabulatedToneCurve16_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nEntries,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] ushort[] parameters);

        internal static IntPtr BuildTabulatedToneCurve(IntPtr contextID, ushort[] values)
        {
            return BuildTabulatedToneCurve16_Internal(contextID, values.Length, values);
        }

        [DllImport(Liblcms, EntryPoint = "cmsBuildTabulatedToneCurveFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr BuildTabulatedToneCurveFloat_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int nEntries,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] parameters);

        internal static IntPtr BuildTabulatedToneCurve(IntPtr contextID, float[] values)
        {
            return BuildTabulatedToneCurveFloat_Internal(contextID, values.Length, values);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDupToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DupToneCurve_Internal(
                IntPtr handle);

        internal static IntPtr DuplicateToneCurve(IntPtr handle)
        {
            return DupToneCurve_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsReverseToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ReverseToneCurve_Internal(
                IntPtr handle);

        internal static IntPtr ReverseToneCurve(IntPtr handle)
        {
            return ReverseToneCurve_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsReverseToneCurveEx", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ReverseToneCurveEx_Internal(
                [MarshalAs(UnmanagedType.I4)] int nResultSamples,
                IntPtr handle);

        internal static IntPtr ReverseToneCurve(IntPtr handle, int nResultSamples)
        {
            return ReverseToneCurveEx_Internal(nResultSamples, handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsJoinToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr JoinToneCurve_Internal(
                IntPtr contextID,
                IntPtr x,
                IntPtr y,
                [MarshalAs(UnmanagedType.U4)] int nPoints);

        internal static IntPtr JoinToneCurve(IntPtr contextID, IntPtr x, IntPtr y, int nPoints)
        {
            return JoinToneCurve_Internal(contextID, x, y, nPoints);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSmoothToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern int SmoothToneCurve_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double lambda);

        internal static int SmoothToneCurve(IntPtr handle, double lambda)
        {
            return SmoothToneCurve_Internal(handle, lambda);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveMultisegment", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveMultisegment_Internal(
                IntPtr handle);

        internal static int IsMultiSegmentToneCurve(IntPtr handle)
        {
            return IsToneCurveMultisegment_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveLinear", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveLinear_Internal(
                IntPtr handle);

        internal static int IsLinearToneCurve(IntPtr handle)
        {
            return IsToneCurveLinear_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveMonotonic", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveMonotonic_Internal(
                IntPtr handle);

        internal static int IsMonotonicToneCurve(IntPtr handle)
        {
            return IsToneCurveMonotonic_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsToneCurveDescending", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsToneCurveDescending_Internal(
                IntPtr handle);

        internal static int IsDescendingToneCurve(IntPtr handle)
        {
            return IsToneCurveDescending_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsEstimateGamma", CallingConvention = CallingConvention.StdCall)]
        private static extern double EstimateGamma_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double precision);

        internal static double EstimateGamma(IntPtr handle, double precision)
        {
            return EstimateGamma_Internal(handle, precision);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetToneCurveEstimatedTableEntries", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetToneCurveEstimatedTableEntries_Internal(
                IntPtr handle);

        internal static uint GetEstimatedTableEntries(IntPtr handle)
        {
            return GetToneCurveEstimatedTableEntries_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetToneCurveEstimatedTable", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetToneCurveEstimatedTable_Internal(
                IntPtr handle);

        internal static IntPtr GetEstimatedTable(IntPtr handle)
        {
            return GetToneCurveEstimatedTable_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsFreeToneCurve", CallingConvention = CallingConvention.StdCall)]
        private static extern void FreeToneCurve_Internal(
                IntPtr handle);

        internal static void FreeToneCurve(IntPtr handle)
        {
            FreeToneCurve_Internal(handle);
        }
    }
}
