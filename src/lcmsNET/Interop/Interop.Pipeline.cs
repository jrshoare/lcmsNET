using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsPipelineAlloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr PipelineAlloc_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint inputChannels,
                [MarshalAs(UnmanagedType.U4)] uint outputChannels);

        internal static IntPtr PipelineAlloc(IntPtr contextID, uint inputChannels, uint outputChannels)
        {
            return PipelineAlloc_Internal(contextID, inputChannels, outputChannels);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineFree", CallingConvention = CallingConvention.StdCall)]
        private static extern void PipelineFree_Internal(IntPtr handle);

        internal static void PipelineFree(IntPtr handle)
        {
            PipelineFree_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineDup", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr PipelineDup_Internal(
                IntPtr handle);

        internal static IntPtr PipelineDup(IntPtr handle)
        {
            return PipelineDup_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineCat", CallingConvention = CallingConvention.StdCall)]
        private static extern int PipelineCat_Internal(
                IntPtr handle,
                IntPtr other);

        internal static int PipelineCat(IntPtr handle, IntPtr other)
        {
            return PipelineCat_Internal(handle, other);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineEvalFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern void PipelineEvalFloat_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] vIn,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] vOut,
                IntPtr handle);

        internal static void PipelineEvalFloat(IntPtr handle, float[] vIn, float[] vOut)
        {
            PipelineEvalFloat_Internal(vIn, vOut, handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineEvalReverseFloat", CallingConvention = CallingConvention.StdCall)]
        private static extern int PipelineEvalReverseFloat_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] vIn,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] vOut,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4)] float[] hint,
                IntPtr handle);

        internal static int PipelineEvalReverseFloat(IntPtr handle, float[] vIn, float[] vOut, float[] hint)
        {
            return PipelineEvalReverseFloat_Internal(vIn, vOut, hint, handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineEval16", CallingConvention = CallingConvention.StdCall)]
        private static extern void PipelineEval16_Internal(
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] ushort[] vIn,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2)] ushort[] vOut,
                IntPtr handle);

        internal static void PipelineEval16(IntPtr handle, ushort[] vIn, ushort[] vOut)
        {
            PipelineEval16_Internal(vIn, vOut, handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineInsertStage", CallingConvention = CallingConvention.StdCall)]
        private static extern int PipelineInsertStage_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int location,
                IntPtr stage);

        internal static int PipelineInsertStage(IntPtr handle, IntPtr stage, int location)
        {
            return PipelineInsertStage_Internal(handle, location, stage);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineInputChannels", CallingConvention = CallingConvention.StdCall)]
        private static extern uint PipelineInputChannels_Internal(IntPtr handle);

        internal static uint PipelineInputChannels(IntPtr handle)
        {
            return PipelineInputChannels_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineOutputChannels", CallingConvention = CallingConvention.StdCall)]
        private static extern uint PipelineOutputChannels_Internal(IntPtr handle);

        internal static uint PipelineOutputChannels(IntPtr handle)
        {
            return PipelineOutputChannels_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineStageCount", CallingConvention = CallingConvention.StdCall)]
        private static extern uint PipelineStageCount_Internal(IntPtr handle);

        internal static uint PipelineStageCount(IntPtr handle)
        {
            return PipelineStageCount_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineUnlinkStage", CallingConvention = CallingConvention.StdCall)]
        private static extern void PipelineUnlinkStage_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int location,
                ref IntPtr stage);

        internal static void PipelineUnlinkStage(IntPtr handle, int location)
        {
            IntPtr stage = IntPtr.Zero;
            PipelineUnlinkStage_Internal(handle, location, ref stage);
        }

        internal static void PipelineUnlinkStage(IntPtr handle, int location, ref IntPtr stage)
        {
            PipelineUnlinkStage_Internal(handle, location, ref stage);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineGetPtrToFirstStage", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr PipelineGetPtrToFirstStage_Internal(IntPtr handle);

        internal static IntPtr PipelineGetPtrToFirstStage(IntPtr handle)
        {
            return PipelineGetPtrToFirstStage_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineGetPtrToLastStage", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr PipelineGetPtrToLastStage_Internal(IntPtr handle);

        internal static IntPtr PipelineGetPtrToLastStage(IntPtr handle)
        {
            return PipelineGetPtrToLastStage_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPipelineSetSaveAs8bitsFlag", CallingConvention = CallingConvention.StdCall)]
        private static extern int PipelineSetSaveAs8bitsFlag_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int on);

        internal static int PipelineSetSaveAs8BitsFlag(IntPtr handle, int on)
        {
            return PipelineSetSaveAs8bitsFlag_Internal(handle, on);
        }
    }
}
