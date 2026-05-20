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
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateTransform")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateTransform_Internal(
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateTransform_Internal(
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static IntPtr CreateTransform(IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, uint intent, uint flags)
        {
            return CreateTransform_Internal(inputProfile, inputFormat, outputProfile, outputFormat, intent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateTransformTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateTransformTHR_Internal(
                IntPtr contextID,
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateTransformTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateTransformTHR_Internal(
                IntPtr contextID,
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static IntPtr CreateTransform(IntPtr contextID, IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, uint intent, uint flags)
        {
            return CreateTransformTHR_Internal(contextID, inputProfile, inputFormat, outputProfile, outputFormat, intent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateProofingTransform")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateProofingTransform_Internal(
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                IntPtr proofingProfile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint proofingIntent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
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
#endif

        internal static IntPtr CreateTransform(IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, IntPtr proofingProfile, uint intent, uint proofingIntent, uint flags)
        {
            return CreateProofingTransform_Internal(inputProfile, inputFormat, outputProfile, outputFormat,
                    proofingProfile, intent, proofingIntent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateProofingTransformTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateProofingTransformTHR_Internal(
                IntPtr contextID,
                IntPtr inputProfile,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                IntPtr outputProfile,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                IntPtr proofingProfile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint proofingIntent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
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
#endif

        internal static IntPtr CreateTransform(IntPtr contextID, IntPtr inputProfile, uint inputFormat,
                IntPtr outputProfile, uint outputFormat, IntPtr proofingProfile, uint intent, uint proofingIntent, uint flags)
        {
            return CreateProofingTransformTHR_Internal(contextID, inputProfile, inputFormat, outputProfile, outputFormat,
                    proofingProfile, intent, proofingIntent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateMultiprofileTransform")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateMultiprofileTransform_Internal(
                [In] IntPtr[] profiles,
                [MarshalAs(UnmanagedType.U4)] uint nProfiles,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateMultiprofileTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateMultiprofileTransform_Internal(
                IntPtr[] profiles,
                [MarshalAs(UnmanagedType.U4)] uint nProfiles,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static IntPtr CreateMultiprofileTransform(IntPtr[] profiles, uint inputFormat,
                uint outputFormat, uint intent, uint flags)
        {
            return CreateMultiprofileTransform_Internal(profiles, (uint)profiles.Length, inputFormat, outputFormat, intent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateMultiprofileTransformTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateMultiprofileTransformTHR_Internal(
                IntPtr contextID,
                [In] IntPtr[] profiles,
                [MarshalAs(UnmanagedType.U4)] uint nProfiles,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateMultiprofileTransformTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateMultiprofileTransformTHR_Internal(
                IntPtr contextID,
                IntPtr[] profiles,
                [MarshalAs(UnmanagedType.U4)] uint nProfiles,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static IntPtr CreateMultiprofileTransform(IntPtr contextID, IntPtr[] profiles, uint inputFormat,
                uint outputFormat, uint intent, uint flags)
        {
            return CreateMultiprofileTransformTHR_Internal(contextID, profiles, (uint)profiles.Length, inputFormat, outputFormat, intent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateExtendedTransform")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateExtendedTransform_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nProfiles,
                [In] IntPtr[] profiles,
                [In] int[] BPC,
                [In] uint[] intents,
                [In] double[] adaptationStates,
                IntPtr gamutProfile,
                [MarshalAs(UnmanagedType.U4)] uint gamutPcsPosition,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateExtendedTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateExtendedTransform_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nProfiles,
                IntPtr[] profiles,
                int[] BPC,
                uint[] intents,
                double[] adaptationStates,
                IntPtr gamutProfile,
                [MarshalAs(UnmanagedType.U4)] uint gamutPcsPosition,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static IntPtr CreateExtendedTransform(IntPtr contextID, IntPtr[] profiles, int[] bpc, uint[] intents,
                double[] adaptationStates, IntPtr gamutProfile, int gamutPcsPosition, uint inputFormat, uint outputFormat, uint flags)
        {
            return CreateExtendedTransform_Internal(contextID, (uint)profiles.Length, profiles, bpc, intents, adaptationStates,
                    gamutProfile, (uint)gamutPcsPosition, inputFormat, outputFormat, flags);
        }


#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDeleteTransform")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void DeleteTransform_Internal(IntPtr transform);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDeleteTransform", CallingConvention = CallingConvention.StdCall)]
        private static extern void DeleteTransform_Internal(IntPtr transform);
#endif

        internal static void DeleteTransform(IntPtr handle)
        {
            DeleteTransform_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDoTransform")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial void DoTransform_Internal(IntPtr transform,
                /*const*/ void* inputBuffer,
                void* outputBuffer,
                [MarshalAs(UnmanagedType.U4)] uint size);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDoTransform", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern void DoTransform_Internal(IntPtr transform,
                /*const*/ void* inputBuffer,
                void* outputBuffer,
                [MarshalAs(UnmanagedType.U4)] uint size);
#endif

        internal unsafe static void DoTransform(IntPtr transform, byte[] inputBuffer, byte[] outputBuffer, int pixelCount)
        {
            fixed (void* pInBuffer = &inputBuffer[0], pOutBuffer = &outputBuffer[0])
            {
                DoTransform_Internal(transform, pInBuffer, pOutBuffer, (uint)pixelCount);
            }
        }

        internal unsafe static void DoTransform(IntPtr transform, ReadOnlySpan<byte> inputBuffer, Span<byte> outputBuffer, int pixelCount)
        {
            fixed (void* pInBuffer = inputBuffer, pOutBuffer = outputBuffer)
            {
                DoTransform_Internal(transform, pInBuffer, pOutBuffer, (uint)pixelCount);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDoTransformLineStride")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial void DoTransformLineStride_Internal(IntPtr transform,
                /*const*/ void* inputBuffer,
                void* outputBuffer,
                [MarshalAs(UnmanagedType.U4)] uint pixelsPerLine,
                [MarshalAs(UnmanagedType.U4)] uint lineCount,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerLineIn,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerLineOut,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerPlaneIn,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerPlaneOut
            );
#else
        [DllImport(Liblcms, EntryPoint = "cmsDoTransformLineStride", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern void DoTransformLineStride_Internal(IntPtr transform,
                /*const*/ void* inputBuffer,
                void* outputBuffer,
                [MarshalAs(UnmanagedType.U4)] uint pixelsPerLine,
                [MarshalAs(UnmanagedType.U4)] uint lineCount,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerLineIn,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerLineOut,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerPlaneIn,
                [MarshalAs(UnmanagedType.U4)] uint bytesPerPlaneOut
            );
#endif

        internal unsafe static void DoTransform(IntPtr transform, byte[] inputBuffer, byte[] outputBuffer,
                int pixelsPerLine, int lineCount, int bytesPerLineIn, int bytesPerLineOut, int bytesPerPlaneIn, int bytesPerPlaneOut)
        {
            fixed (void* pInBuffer = &inputBuffer[0], pOutBuffer = &outputBuffer[0])
            {
                DoTransformLineStride_Internal(transform, pInBuffer, pOutBuffer, (uint)pixelsPerLine, (uint)lineCount,
                        (uint)bytesPerLineIn, (uint)bytesPerLineOut, (uint)bytesPerPlaneIn, (uint)bytesPerPlaneOut);
            }
        }

        internal unsafe static void DoTransform(IntPtr transform, ReadOnlySpan<byte> inputBuffer, Span<byte> outputBuffer,
                int pixelsPerLine, int lineCount, int bytesPerLineIn, int bytesPerLineOut, int bytesPerPlaneIn, int bytesPerPlaneOut)
        {
            fixed (void* pInBuffer = inputBuffer, pOutBuffer = outputBuffer)
            {
                DoTransformLineStride_Internal(transform, pInBuffer, pOutBuffer, (uint)pixelsPerLine, (uint)lineCount,
                        (uint)bytesPerLineIn, (uint)bytesPerLineOut, (uint)bytesPerPlaneIn, (uint)bytesPerPlaneOut);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetTransformInputFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetTransformInputFormat_Internal(
                IntPtr transform);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetTransformInputFormat", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetTransformInputFormat_Internal(
                IntPtr transform);
#endif

        internal static uint GetTransformInputFormat(IntPtr transform)
        {
            return GetTransformInputFormat_Internal(transform);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetTransformOutputFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetTransformOutputFormat_Internal(
                IntPtr transform);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetTransformOutputFormat", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetTransformOutputFormat_Internal(
                IntPtr transform);
#endif

        internal static uint GetTransformOutputFormat(IntPtr transform)
        {
            return GetTransformOutputFormat_Internal(transform);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsChangeBuffersFormat")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int ChangeBuffersFormat_Internal(
                IntPtr transform,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat);
#else
        [DllImport(Liblcms, EntryPoint = "cmsChangeBuffersFormat", CallingConvention = CallingConvention.StdCall)]
        private static extern int ChangeBuffersFormat_Internal(
                IntPtr transform,
                [MarshalAs(UnmanagedType.U4)] uint inputFormat,
                [MarshalAs(UnmanagedType.U4)] uint outputFormat);
#endif

        internal static int ChangeBuffersFormat(IntPtr transform, uint inputFormat, uint outputFormat)
        {
            return ChangeBuffersFormat_Internal(transform, inputFormat, outputFormat);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "_cmsGetTransformUserData")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr GetTransformUserData_Internal(
                IntPtr transform);
#else
        [DllImport(Liblcms, EntryPoint = "_cmsGetTransformUserData", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetTransformUserData_Internal(
                IntPtr transform);
#endif

        internal static IntPtr GetTransformUserData(IntPtr transform)
        {
            return GetTransformUserData_Internal(transform);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "_cmsSetTransformUserData")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetTransformUserData_Internal(
                IntPtr transform,
                IntPtr userData,
                FreeUserData fn);
#else
        [DllImport(Liblcms, EntryPoint = "_cmsSetTransformUserData", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetTransformUserData_Internal(
                IntPtr transform,
                IntPtr userData,
                FreeUserData fn);
#endif

        internal static void SetTransformUserData(IntPtr transform, IntPtr userData, FreeUserData fn)
        {
            SetTransformUserData_Internal(transform, userData, fn);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "_cmsGetTransformFlags")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetTransformFlags_Internal(
                IntPtr transform);
#else
        [DllImport(Liblcms, EntryPoint = "_cmsGetTransformFlags", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetTransformFlags_Internal(
                IntPtr transform);
#endif

        internal static uint GetTransformFlags(IntPtr transform)
        {
            return GetTransformFlags_Internal(transform);
        }
    }
}
