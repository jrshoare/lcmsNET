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

        [DllImport(Liblcms, EntryPoint = "cmsStageData", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageData_Internal(IntPtr handle);

        internal static IntPtr StageData(IntPtr handle)
        {
            return StageData_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageNext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageNext_Internal(IntPtr handle);

        internal static IntPtr StageNext(IntPtr handle)
        {
            return StageNext_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageSampleCLut16bit", CallingConvention = CallingConvention.StdCall)]
        private static extern int StageSampleCLut16bit_Internal(
                IntPtr mpe,
                Sampler16 sampler,
                IntPtr cargo,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static int StageSampleClut16Bit(IntPtr handle, Sampler16 sampler, IntPtr cargo, uint flags)
        {
            return StageSampleCLut16bit_Internal(handle, sampler, cargo, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsStageSampleCLutFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern int StageSampleCLutFloat_Internal(
                IntPtr mpe,
                SamplerFloat sampler,
                IntPtr cargo,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static int StageSampleClutFloat(IntPtr handle, SamplerFloat sampler, IntPtr cargo, uint flags)
        {
            return StageSampleCLutFloat_Internal(handle, sampler, cargo, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSliceSpace16", CallingConvention = CallingConvention.StdCall)]
        private static extern int SliceSpace16_Internal(
                [MarshalAs(UnmanagedType.U4)] uint nPoints,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] uint[] clutPoints,
                Sampler16 sampler,
                IntPtr cargo);

        internal static int SliceSpace16Bit(uint nPoints, uint[] clutPoints, Sampler16 sampler, IntPtr cargo)
        {
            return SliceSpace16_Internal(nPoints, clutPoints, sampler, cargo);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSliceSpaceFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern int SliceSpaceFloat_Internal(
                [MarshalAs(UnmanagedType.U4)] uint nPoints,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] uint[] clutPoints,
                SamplerFloat sampler,
                IntPtr cargo);

        internal static int SliceSpaceFloat(uint nPoints, uint[] clutPoints, SamplerFloat sampler, IntPtr cargo)
        {
            return SliceSpaceFloat_Internal(nPoints, clutPoints, sampler, cargo);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetStageContextID", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStageContextID_Internal(
                IntPtr mpe);

        internal static IntPtr GetStageContextID(IntPtr handle)
        {
            try
            {
                // Requires Little CMS version 2.13 or later.
                return GetStageContextID_Internal(handle);
            }
            catch (EntryPointNotFoundException)
            {
                return IntPtr.Zero;
            }
        }

        [DllImport(Liblcms, EntryPoint = "_cmsStageAllocPlaceholder", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr StageAllocPlaceholder_Internal(
                IntPtr ContextID,
                [MarshalAs(UnmanagedType.U4)] uint Type,
                [MarshalAs(UnmanagedType.U4)] uint InputChannels,
                [MarshalAs(UnmanagedType.U4)] uint OutputChannels,
                IntPtr EvalPtr,
                IntPtr DupElemPtr,
                IntPtr FreePtr,
                IntPtr Data);

        internal static IntPtr StageAllocPlaceholder(IntPtr contextId, uint type, uint inputChannels, uint outputChannels,
                StageEvalFn evalFn, StageDupElemFn dupElemFn, StageFreeElemFn freeElemFn, IntPtr data)
        {
            IntPtr evalPtr = (evalFn is null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(evalFn);
            IntPtr dupElemPtr = (dupElemFn is null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(dupElemFn);
            IntPtr freeElemPtr = (freeElemFn is null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(freeElemFn);

            return StageAllocPlaceholder_Internal(contextId, type, inputChannels, outputChannels, evalPtr, dupElemPtr, freeElemPtr, data);
        }
    }
}
