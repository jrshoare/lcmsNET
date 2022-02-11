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

using lcmsNET.Impl;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Defines the flags to control stage sampling.
    /// </summary>
    [Flags]
    public enum StageSamplingFlags : uint
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// CLUT is to be accessed as read-only.
        /// </summary>
        Inspect = 0x01000000
    }

    /// <summary>
    /// Defines a delegate that can used to populate CLUT stages in a way that is
    /// independent of the number of nodes.
    /// </summary>
    /// <param name="input">The node coordinates.</param>
    /// <param name="output">The contents of the CLUT on the node.</param>
    /// <param name="cargo">User supplied content.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Sampler16(
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = Stage.MAX_INPUT_DIMENSIONS + 1), In] ushort[] input,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = Stage.MAX_STAGE_CHANNELS)] ushort[] output,
        IntPtr cargo);

    /// <summary>
    /// Defines a delegate that can used to populate CLUT stages in a way that is
    /// independent of the number of nodes.
    /// </summary>
    /// <param name="input">The node coordinates.</param>
    /// <param name="output">The contents of the CLUT on the node.</param>
    /// <param name="cargo">User supplied content.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SamplerFloat(
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = Stage.MAX_INPUT_DIMENSIONS + 1), In] float[] input,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeConst = Stage.MAX_STAGE_CHANNELS)] float[] output,
        IntPtr cargo);

    /// <summary>
    /// Represents a stage in a pipeline.
    /// </summary>
    public sealed class Stage : CmsHandle<Stage>
    {
        internal Stage(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        internal static Stage CopyRef(IntPtr handle, Context context = null)
        {
            return new Stage(handle, context, isOwner: false);
        }

        /// <summary>
        /// Creates new instance of the <see cref="Stage"/> class for an empty stage that performs no operation.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nChannels">The number of channels.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, uint nChannels)
        {
            return new Stage(Interop.StageAllocIdentity(context?.Handle ?? IntPtr.Zero, nChannels), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Stage"/> class that contains tone curves.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nChannels">The number of tone curves.</param>
        /// <param name="curves">An array of <paramref name="nChannels"/> tone curves, or null to use identity curves.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, uint nChannels, ToneCurve[] curves)
        {
            return new Stage(Interop.StageAllocToneCurves(context?.Handle ?? IntPtr.Zero, nChannels,
                    curves?.Select(_ => _.Handle).ToArray()), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Stage"/> class that contains a matrix and optional offset.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="matrix">A matrix of [rows, columns].</param>
        /// <param name="offset">A vector of [columns] offsets, or null if no offset is to be applied.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, double[,] matrix, double[] offset)
        {
            if (!(offset is null) && offset.Length != (matrix?.GetUpperBound(1) + 1))
            {
                throw new ArgumentException($"'{nameof(offset)}' array size must equal size of '{nameof(matrix)}' second dimension.");
            }

            return new Stage(Interop.StageAllocMatrix(context?.Handle ?? IntPtr.Zero, matrix, offset), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Stage"/> class that contains a 16 bit multi-dimensional lookup table (CLUT)
        /// where each dimension has the same size.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nGridPoints">The number of nodes (same for each component).</param>
        /// <param name="inputChannels">The number of input channels.</param>
        /// <param name="outputChannels">The number of output channels.</param>
        /// <param name="table">An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, uint nGridPoints, uint inputChannels, uint outputChannels, ushort[] table)
        {
            return new Stage(Interop.StageAllocCLut16bit(context?.Handle ?? IntPtr.Zero, nGridPoints, inputChannels, outputChannels, table), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Stage"/> class that contains a floating point multi-dimensional lookup table (CLUT)
        /// where each dimension has the same size.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nGridPoints">The number of nodes (same for each component).</param>
        /// <param name="inputChannels">The number of input channels.</param>
        /// <param name="outputChannels">The number of output channels.</param>
        /// <param name="table">An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, uint nGridPoints, uint inputChannels, uint outputChannels, float[] table)
        {
            return new Stage(Interop.StageAllocCLutFloat(context?.Handle ?? IntPtr.Zero, nGridPoints, inputChannels, outputChannels, table), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Stage"/> class that contains a 16 bit multi-dimensional lookup table (CLUT)
        /// where each dimension can have a different size.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="clutPoints">An array of [input channels] values containing the number of nodes for each component.</param>
        /// <param name="outputChannels">The number of output channels.</param>
        /// <param name="table">An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, uint[] clutPoints, uint outputChannels, ushort[] table)
        {
            if (!(clutPoints?.Length > 0)) throw new ArgumentException($"'{nameof(clutPoints)}' array size must be greater than 0.");

            return new Stage(Interop.StageAllocCLut16bitGranular(context?.Handle ?? IntPtr.Zero, clutPoints, outputChannels, table), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Stage"/> class that contains a floating point multi-dimensional lookup table (CLUT)
        /// where each dimension can have a different size.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="clutPoints">An array of [inputChannels] values containing the number of nodes for each component.</param>
        /// <param name="outputChannels">The number of output channels.</param>
        /// <param name="table">An array of initial values for the nodes, or null if the CLUT is to be initialised to zero.</param>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Stage Create(Context context, uint[] clutPoints, uint outputChannels, float[] table)
        {
            if (!(clutPoints?.Length > 0)) throw new ArgumentException($"'{nameof(clutPoints)}' array size must be greater than 0.");

            return new Stage(Interop.StageAllocCLutFloatGranular(context?.Handle ?? IntPtr.Zero, clutPoints, outputChannels, table), context);
        }

        /// <summary>
        /// Duplicates a stage.
        /// </summary>
        /// <returns>A new <see cref="Stage"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The Stage has already been disposed.
        /// </exception>
        public Stage Duplicate()
        {
            return new Stage(Interop.StageDup(handle), Context);
        }

        /// <summary>
        /// Iterates on all nodes of the stage calling a 16-bit sampler on each node.
        /// </summary>
        /// <param name="sampler">The callback to be executed on each node.</param>
        /// <param name="cargo">A user-supplied value to be passed to the callback.</param>
        /// <param name="flags">Flags to control stage sampling.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Stage has already been disposed.
        /// </exception>
        public bool SampleCLUT(Sampler16 sampler, IntPtr cargo, StageSamplingFlags flags)
        {
            EnsureNotDisposed();

            return Interop.StageSampleClut16Bit(handle, sampler, cargo, Convert.ToUInt32(flags)) != 0;
        }

        /// <summary>
        /// Iterates on all nodes of the stage calling a floating point sampler on each node.
        /// </summary>
        /// <param name="sampler">The callback to be executed on each node.</param>
        /// <param name="cargo">A user-supplied value to be passed to the callback.</param>
        /// <param name="flags">Flags to control stage sampling.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Stage has already been disposed.
        /// </exception>
        public bool SampleCLUT(SamplerFloat sampler, IntPtr cargo, StageSamplingFlags flags)
        {
            EnsureNotDisposed();

            return Interop.StageSampleClutFloat(handle, sampler, cargo, Convert.ToUInt32(flags)) != 0;
        }

        /// <summary>
        /// Slices the target space calling a 16-bit sampler on each node.
        /// </summary>
        /// <param name="clutPoints">An array holding the division slices for each component.</param>
        /// <param name="sampler">The callback to be executed on each node.</param>
        /// <param name="cargo">A user-supplied value to be passed to the callback.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="clutPoints"/> is null or array size is zero.
        /// </exception>
        /// <remarks>
        /// When invoked, the <see cref="Sampler16"/> 'output' parameter is set to null.
        /// </remarks>
        public static bool SliceSpace(uint[] clutPoints, Sampler16 sampler, IntPtr cargo)
        {
            if (!(clutPoints?.Length > 0)) throw new ArgumentException($"'{nameof(clutPoints)}' array size must be greater than 0.");

            return Interop.SliceSpace16Bit((uint)clutPoints.Length, clutPoints, sampler, cargo) != 0;
        }

        /// <summary>
        /// Slices the target space calling a 16-bit sampler on each node.
        /// </summary>
        /// <param name="clutPoints">An array holding the division slices for each component.</param>
        /// <param name="sampler">The callback to be executed on each node.</param>
        /// <param name="cargo">A user-supplied value to be passed to the callback.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="clutPoints"/> is null or array size is zero.
        /// </exception>
        /// <remarks>
        /// When invoked, the <see cref="SamplerFloat"/> 'output' parameter is set to null.
        /// </remarks>
        public static bool SliceSpace(uint[] clutPoints, SamplerFloat sampler, IntPtr cargo)
        {
            if (!(clutPoints?.Length > 0)) throw new ArgumentException($"'{nameof(clutPoints)}' array size must be greater than 0.");

            return Interop.SliceSpaceFloat((uint)clutPoints.Length, clutPoints, sampler, cargo) != 0;
        }

        /// <summary>
        /// Gets the number of input channels in the stage.
        /// </summary>
        public uint InputChannels => Interop.StageInputChannels(handle);

        /// <summary>
        /// Gets the number of output channels in the stage.
        /// </summary>
        public uint OutputChannels => Interop.StageOutputChannels(handle);

        /// <summary>
        /// Gets the stage type.
        /// </summary>
        public StageSignature StageType => (StageSignature)Interop.StageType(handle);

        /// <summary>
        /// Frees the stage handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.StageFree(handle);
            return true;
        }

        // Constants from lcms_plugin.h and lcms_internal.h
        internal const int MAX_INPUT_DIMENSIONS = 8;
        internal const int MAX_STAGE_CHANNELS = 128;
    }
}
