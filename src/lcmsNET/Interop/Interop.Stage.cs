using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsStageAllocIdentity", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocIdentity_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nChannels);

        internal static IntPtr StageAllocIdentity(IntPtr contextID, uint nChannels)
        {
            return StageAllocIdentity_Internal(contextID, nChannels);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageAllocToneCurves", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocToneCurves_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nChannels,
                IntPtr[] curves);

        internal static IntPtr StageAllocToneCurves(IntPtr contextID, uint nChannels, IntPtr[] curves)
        {
            return StageAllocToneCurves_Internal(contextID, nChannels, curves);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageAllocMatrix", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocMatrix_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint rows,
                [MarshalAs(UnmanagedType.U4)] uint columns,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[,] matrix,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8)] double[] offset);

        internal static IntPtr StageAllocMatrix(IntPtr contextID, double[,] matrix, double[] offset)
        {
            return StageAllocMatrix_Internal(contextID, Convert.ToUInt32(matrix.GetUpperBound(0)+1),
                    Convert.ToUInt32(matrix.GetUpperBound(1)+1), matrix, offset);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageAllocCLut16bit", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocCLut16bit_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nGridPoints,
                [MarshalAs(UnmanagedType.U4)] uint inputChannels,
                [MarshalAs(UnmanagedType.U4)] uint outputChannels,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] ushort[] curves);

        internal static IntPtr StageAllocCLut16bit(IntPtr contextID, uint nGridPoints, uint inputChannels, uint outputChannels, ushort[] table)
        {
            return StageAllocCLut16bit_Internal(contextID, nGridPoints, inputChannels, outputChannels, table);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageAllocCLutFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocCLutFloat_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nGridPoints,
                [MarshalAs(UnmanagedType.U4)] uint inputChannels,
                [MarshalAs(UnmanagedType.U4)] uint outputChannels,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] curves);

        internal static IntPtr StageAllocCLutFloat(IntPtr contextID, uint nGridPoints, uint inputChannels, uint outputChannels, float[] table)
        {
            return StageAllocCLutFloat_Internal(contextID, nGridPoints, inputChannels, outputChannels, table);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageAllocCLut16bitGranular", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocCLut16bitGranular_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] uint[] clutPoints,
                [MarshalAs(UnmanagedType.U4)] uint inputChannels,
                [MarshalAs(UnmanagedType.U4)] uint outputChannels,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] ushort[] table);

        internal static IntPtr StageAllocCLut16bitGranular(IntPtr contextID, uint[] clutPoints, uint outputChannels, ushort[] table)
        {
            return StageAllocCLut16bitGranular_Internal(contextID, clutPoints, Convert.ToUInt32(clutPoints.Length), outputChannels, table);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageAllocCLutFloatGranular", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocCLutFloatGranular_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] uint[] clutPoints,
                [MarshalAs(UnmanagedType.U4)] uint inputChannels,
                [MarshalAs(UnmanagedType.U4)] uint outputChannels,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] table);

        internal static IntPtr StageAllocCLutFloatGranular(IntPtr contextID, uint[] clutPoints, uint outputChannels, float[] table)
        {
            return StageAllocCLutFloatGranular_Internal(contextID, clutPoints, Convert.ToUInt32(clutPoints.Length), outputChannels, table);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageDup", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageDup_Internal(
                IntPtr handle);

        internal static IntPtr StageDup(IntPtr handle)
        {
            return StageDup_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageFree", CallingConvention = CallingConvention.StdCall)]
        private static extern void StageFree_Internal(IntPtr handle);

        internal static void StageFree(IntPtr handle)
        {
            StageFree_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageInputChannels", CallingConvention = CallingConvention.StdCall)]
        private static extern uint StageInputChannels_Internal(IntPtr handle);

        internal static uint StageInputChannels(IntPtr handle)
        {
            return StageInputChannels_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageOutputChannels", CallingConvention = CallingConvention.StdCall)]
        private static extern uint StageOutputChannels_Internal(IntPtr handle);

        internal static uint StageOutputChannels(IntPtr handle)
        {
            return StageOutputChannels_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageType", CallingConvention = CallingConvention.StdCall)]
        private static extern uint StageType_Internal(IntPtr handle);

        internal static uint StageType(IntPtr handle)
        {
            return StageType_Internal(handle);
        }
    }
}
