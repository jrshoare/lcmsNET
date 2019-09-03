using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCreateTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateTransform_Internal(
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateTransform(IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, uint intent, uint flags)
        {
            return CreateTransform_Internal(inputProfile, inputFormat, outputProfile, outputFormat, intent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateTransformTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateTransformTHR_Internal(
                IntPtr contextID,
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateTransform(IntPtr contextID, IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, uint intent, uint flags)
        {
            return CreateTransformTHR_Internal(contextID, inputProfile, inputFormat, outputProfile, outputFormat, intent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateProofingTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateProofingTransform_Internal(
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                IntPtr proofingProfile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint proofingIntent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateTransform(IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, IntPtr proofingProfile, uint intent, uint proofingIntent, uint flags)
        {
            return CreateProofingTransform_Internal(inputProfile, inputFormat, outputProfile, outputFormat,
                    proofingProfile, intent, proofingIntent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateProofingTransformTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateProofingTransformTHR_Internal(
                IntPtr contextID,
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                IntPtr proofingProfile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint proofingIntent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateTransform(IntPtr contextID, IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, IntPtr proofingProfile, uint intent, uint proofingIntent, uint flags)
        {
            return CreateProofingTransformTHR_Internal(contextID, inputProfile, inputFormat, outputProfile, outputFormat,
                    proofingProfile, intent, proofingIntent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateMultiprofileTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateMultiprofileTransform_Internal(
                IntPtr[] profiles,
                [MarshalAs(UnmanagedType.U4)] int nProfiles,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateMultiprofileTransform(IntPtr[] profiles, uint inputFormat,
                uint outputFormat, uint intent, uint flags)
        {
            return CreateMultiprofileTransform_Internal(profiles, profiles.Length, inputFormat, outputFormat, intent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateMultiprofileTransformTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateMultiprofileTransformTHR_Internal(
                IntPtr contextID,
                IntPtr[] profiles,
                [MarshalAs(UnmanagedType.U4)] int nProfiles,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateMultiprofileTransform(IntPtr contextID, IntPtr[] profiles, uint inputFormat,
                uint outputFormat, uint intent, uint flags)
        {
            return CreateMultiprofileTransformTHR_Internal(contextID, profiles, profiles.Length, inputFormat, outputFormat, intent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateExtendedTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateExtendedTransform_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] int nProfiles,
                IntPtr[] profiles,
                int[] BPC,
                uint[] intents,
                double[] adaptationStates,
                IntPtr gamutProfile,
                [MarshalAs(UnmanagedType.U4)] int gamutPcsPosition,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr CreateExtendedTransform(IntPtr contextID, IntPtr[] profiles, int[] bpc, uint[] intents,
                double[] adaptationStates, IntPtr gamutProfile, int gamutPcsPosition, uint inputFormat, uint outputFormat, uint flags)
        {
            return CreateExtendedTransform_Internal(contextID, profiles.Length, profiles, bpc, intents, adaptationStates,
                    gamutProfile, gamutPcsPosition, inputFormat, outputFormat, flags);
        }


        [DllImport(Liblcms, EntryPoint = "cmsDeleteTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern void DeleteTransform_Internal(IntPtr transform);

        internal static void DeleteTransform(IntPtr handle)
        {
            DeleteTransform_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDoTransform", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern void DoTransform_Internal(IntPtr transform,
                /*const*/ void* inputBuffer,
                void* outputBuffer,
                [MarshalAs(UnmanagedType.U4)] int size);

        internal unsafe static void DoTransform(IntPtr transform, byte[] inputBuffer, byte[] outputBuffer, int pixelCount)
        {
            fixed (void* pInBuffer = &inputBuffer[0], pOutBuffer = &outputBuffer[0])
            {
                DoTransform_Internal(transform, pInBuffer, pOutBuffer, pixelCount);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsDoTransformLineStride", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern void DoTransformLineStride_Internal(IntPtr transform,
                /*const*/ void* inputBuffer,
                void* outputBuffer,
                [MarshalAs(UnmanagedType.U4)] int pixelsPerLine,
                [MarshalAs(UnmanagedType.U4)] int lineCount,
                [MarshalAs(UnmanagedType.U4)] int bytesPerLineIn,
                [MarshalAs(UnmanagedType.U4)] int bytesPerLineOut,
                [MarshalAs(UnmanagedType.U4)] int bytesPerPlaneIn,
                [MarshalAs(UnmanagedType.U4)] int bytesPerPlaneOut
            );

        internal unsafe static void DoTransform(IntPtr transform, byte[] inputBuffer, byte[] outputBuffer,
                int pixelsPerLine, int lineCount, int bytesPerLineIn, int bytesPerLineOut, int bytesPerPlaneIn, int bytesPerPlaneOut)
        {
            fixed (void* pInBuffer = &inputBuffer[0], pOutBuffer = &outputBuffer[0])
            {
                DoTransformLineStride_Internal(transform, pInBuffer, pOutBuffer, pixelsPerLine, lineCount,
                        bytesPerLineIn, bytesPerLineOut, bytesPerPlaneIn, bytesPerPlaneOut);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetTransformInputFormat", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern uint GetTransformInputFormat_Internal(
                IntPtr transform);

        internal unsafe static uint GetTransformInputFormat(IntPtr transform)
        {
            return GetTransformInputFormat_Internal(transform);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetTransformOutputFormat", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern uint GetTransformOutputFormat_Internal(
                IntPtr transform);

        internal unsafe static uint GetTransformOutputFormat(IntPtr transform)
        {
            return GetTransformOutputFormat_Internal(transform);
        }
    }
}
