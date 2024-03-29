﻿// Copyright(c) 2019-2022 John Stevenson-Hoare
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

namespace lcmsNET.Plugin
{
    /// <summary>
    /// Defines the plug-in types.
    /// </summary>
    public enum PluginType : uint
    {
        /// <summary>
        /// 'memH'
        /// </summary>
        MemoryHandler = 0x6D656D48,
        /// <summary>
        /// 'inpH'
        /// </summary>
        Interpolation = 0x696E7048,
        /// <summary>
        /// 'parH'
        /// </summary>
        ParametricCurve = 0x70617248,
        /// <summary>
        /// 'frmH'
        /// </summary>
        Formatters = 0x66726D48,
        /// <summary>
        /// 'typH'
        /// </summary>
        TagType = 0x74797048,
        /// <summary>
        /// 'tagH'
        /// </summary>
        Tag = 0x74616748,
        /// <summary>
        /// 'intH'
        /// </summary>
        RenderingIntent = 0x696E7448,
        /// <summary>
        /// 'mpeH'
        /// </summary>
        MultiProcessElement = 0x6D706548,
        /// <summary>
        /// 'optH'
        /// </summary>
        Optimization = 0x6F707448,
        /// <summary>
        /// 'xfmH'
        /// </summary>
        Transform = 0x7A666D48,
        /// <summary>
        /// 'mtxH'
        /// </summary>
        Mutex = 0x6D747A48
    }

    #region Base structure
    /// <summary>
    /// Base structure for all plug-ins.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginBase
    {
        /// <summary>
        /// Identifies the structure as a Little CMS 2 plug-in.
        /// </summary>
        /// <remarks>
        /// This must contain the value <see cref="Cms.PluginMagicNumber"/>.
        /// </remarks>
        [MarshalAs(UnmanagedType.U4)]
        public uint Magic;

        /// <summary>
        /// The expected Little CMS version.
        /// </summary>
        /// <remarks>
        /// Little CMS core will accept plug-ins with an expected version less or
        /// equal than the core version. If a plug-in is marked for a version
        /// greater than the core the plug-in will be rejected. This means that
        /// downgrading the core engine will disable certain plug-ins.
        /// </remarks>
        [MarshalAs(UnmanagedType.U4)]
        public uint ExpectedVersion;

        /// <summary>
        /// Identifies the type of plug-in.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public PluginType Type;

        /// <summary>
        /// Points to the next plug-in header in multi plug-in packages.
        /// </summary>
        /// <remarks>
        /// Set to <see cref="IntPtr.Zero"/> to mark the end of the chain.
        /// </remarks>
        public IntPtr Next;
    }
    #endregion

    #region Tag plug-in
    /// <summary>
    /// Defines a delegate to select tag type based on the version of the ICC profile.
    /// </summary>
    /// <param name="iccVersion">The ICC profile version.</param>
    /// <param name="data">Pointer to tag contents.</param>
    /// <returns>The <see cref="TagTypeSignature"/> of the tag.</returns>
    public delegate TagTypeSignature DecideType([MarshalAs(UnmanagedType.R8)] double iccVersion, IntPtr data);

    /// <summary>
    /// Defines a descriptor for tag plug-ins.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TagDescriptor
    {
        /// <summary>
        /// If the tag needs an array defines the number of elements, minimum 1.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int ElemCount;

        /// <summary>
        /// Number of elements in <see cref="SupportedTypes"/> array, minimum 1.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int nSupportedTypes;

        /// <summary>
        /// Types supported for the tag.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = MAX_TYPES_IN_LCMS_PLUGIN)]
        public TagTypeSignature[] SupportedTypes;

        /// <summary>
        /// Pointer to delegate of type <see cref="DecideType"/> to select the tag type
        /// based on the version of the ICC profile.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Decider;

        /// <summary>
        /// Defines size of array to be allocated for <see cref="SupportedTypes"/>.
        /// </summary>
        public const int MAX_TYPES_IN_LCMS_PLUGIN = 20;
    }

    /// <summary>
    /// Defines the tag plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginTag
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;
        /// <summary>
        /// Identifies the tag signature.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public TagSignature Signature;
        /// <summary>
        /// The descriptor for the tag.
        /// </summary>
        public TagDescriptor Descriptor;
    }
    #endregion

    #region Tag type plug-in
    /// <summary>
    /// Defines a delegate to allocate and reads items.
    /// </summary>
    /// <param name="self">The tag type handler.</param>
    /// <param name="io">A pointer to the I/O handler to be used to perform the read.</param>
    /// <param name="nItems">Returns the number of items allocated.</param>
    /// <param name="tagSize">The size of the tag.</param>
    /// <returns>A pointer to the unmanaged memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr TagTypeRead(in TagTypeHandler self,
            IntPtr io,
            [MarshalAs(UnmanagedType.U4)] out uint nItems,
            [MarshalAs(UnmanagedType.U4)] uint tagSize);

    /// <summary>
    /// Defines a delegate to write n items.
    /// </summary>
    /// <param name="self">The tag type handler.</param>
    /// <param name="io">A pointer to an I/O handler to be used to perform the write.</param>
    /// <param name="ptr">A pointer to the data to be written.</param>
    /// <param name="nItems">The number of items.</param>
    /// <returns>Non-zero on success, otherwise zero.</returns>
    public delegate int TagTypeWrite(in TagTypeHandler self,
            IntPtr io,
            IntPtr ptr,
            [MarshalAs(UnmanagedType.U4)] uint nItems);

    /// <summary>
    /// Defines a delegate to duplicate an item or array of items.
    /// </summary>
    /// <param name="self">The tag type handler.</param>
    /// <param name="ptr">A pointer to the unmanaged memory to be duplicated.</param>
    /// <param name="n">The number of items.</param>
    /// <returns>A pointer to the unmanaged memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr TagTypeDuplicate(in TagTypeHandler self,
            IntPtr ptr,
            [MarshalAs(UnmanagedType.U4)] uint n);

    /// <summary>
    /// Defines a delegate to free all resources.
    /// </summary>
    /// <param name="self">The tag type handler.</param>
    /// <param name="ptr">A pointer to the unmanaged memory to be freed.</param>
    public delegate void TagTypeFree(in TagTypeHandler self,
            IntPtr ptr);

    /// <summary>
    /// Defines the tag type handler.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TagTypeHandler
    {
        /// <summary>
        /// Identifies the tag type signature.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public TagTypeSignature Signature;

        /// <summary>
        /// Pointer to delegate of type <see cref="TagTypeRead"/> to allocate and read items.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Read;

        /// <summary>
        /// Pointer to delegate of type <see cref="TagTypeWrite"/> to write n items.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Write;

        /// <summary>
        /// Pointer to delegate of type <see cref="TagTypeDuplicate"/> to duplicate an item
        /// or array of items.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Duplicate;

        /// <summary>
        /// Pointer to delegate of type <see cref="TagTypeFree"/> to free all resources.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Free;

        /// <summary>
        /// The calling thread context.
        /// </summary>
        public readonly IntPtr ContextID;
    }

    /// <summary>
    /// Defines the tag type plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginTagType
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// The tag type handler.
        /// </summary>
        public TagTypeHandler Handler;
    }
    #endregion

    #region Memory handler plug-in
    /// <summary>
    /// Defines a delegate to allocate memory.
    /// </summary>
    /// <param name="contextID">The calling thread context. Can be <see cref="IntPtr.Zero"/>.</param>
    /// <param name="size">The size of memory to be allocated in bytes.</param>
    /// <returns>A pointer to the memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr MemoryMalloc(IntPtr contextID, uint size);

    /// <summary>
    /// Defines a delegate to free memory.
    /// </summary>
    /// <param name="contextID">The calling thread context. Can be <see cref="IntPtr.Zero"/>.</param>
    /// <param name="ptr">A pointer to the memory to be freed.</param>
    public delegate void MemoryFree(IntPtr contextID, IntPtr ptr);

    /// <summary>
    /// Defines a delegate to re-allocate memory.
    /// </summary>
    /// <param name="contextID">The calling thread context. Can be <see cref="IntPtr.Zero"/>.</param>
    /// <param name="ptr">A pointer to the existing memory.</param>
    /// <param name="newSize">The size of memory to be allocated in bytes.</param>
    /// <returns>A pointer to the memory re-allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr MemoryRealloc(IntPtr contextID, IntPtr ptr, uint newSize);

    /// <summary>
    /// Defines a delegate to allocate memory filled with zeroes.
    /// </summary>
    /// <param name="contextID">The calling thread context. Can be <see cref="IntPtr.Zero"/>.</param>
    /// <param name="size">The size of memory to be allocated in bytes.</param>
    /// <returns>A pointer to the memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr MemoryMallocZero(IntPtr contextID, uint size);

    /// <summary>
    /// Defines a delegate to allocate a block of memory for an array of elements filled with zeroes.
    /// </summary>
    /// <param name="contextID">The calling thread context. Can be <see cref="IntPtr.Zero"/>.</param>
    /// <param name="count">The number of elements to allocate.</param>
    /// <param name="size">The size of each element to be allocated in bytes.</param>
    /// <returns>A pointer to the memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr MemoryCalloc(IntPtr contextID, uint count, uint size);

    /// <summary>
    /// Defines a delegate to duplicate memory into a newly allocated block of memory.
    /// </summary>
    /// <param name="contextID">The calling thread context. Can be <see cref="IntPtr.Zero"/>.</param>
    /// <param name="ptr">A pointer to the memory to be duplicated.</param>
    /// <param name="size">The size of the memory to be duplicated in bytes.</param>
    /// <returns>A pointer to the memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr MemoryDuplicate(IntPtr contextID, IntPtr ptr, uint size);

    /// <summary>
    /// Defines a delegate to allocate memory to create contexts.
    /// </summary>
    /// <param name="userData">A pointer to user-defined data.</param>
    /// <param name="size">The size of memory to be allocated in bytes.</param>
    /// <returns>A pointer to the memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
    public delegate IntPtr MemoryNonContextualMalloc(IntPtr userData, uint size);

    /// <summary>
    /// Defines a delegate to free memory used to create contexts.
    /// </summary>
    /// <param name="userData">A pointer to user-defined data.</param>
    /// <param name="ptr">A pointer to the memory to be freed.</param>
    public delegate void MemoryNonContextualFree(IntPtr userData, IntPtr ptr);

    /// <summary>
    /// Defines the memory handler plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginMemoryHandler
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryMalloc"/> to allocate memory.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Malloc;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryFree"/> to free memory.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Free;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryRealloc"/> to re-allocate memory.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Realloc;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryMallocZero"/> to allocate zeroed memory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Optional. Can be set to <see cref="IntPtr.Zero"/>.
        /// </para>
        /// <para>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </para>
        /// </remarks>
        public IntPtr MallocZero;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryCalloc"/> to allocate a block of memory
        /// for an array of elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Optional. Can be set to <see cref="IntPtr.Zero"/>.
        /// </para>
        /// <para>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </para>
        /// </remarks>
        public IntPtr Calloc;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryDuplicate"/> to duplicate memory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Optional. Can be set to <see cref="IntPtr.Zero"/>.
        /// </para>
        /// <para>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </para>
        /// </remarks>
        public IntPtr Duplicate;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryNonContextualMalloc"/> to allocate memory.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr NonContextualMalloc;

        /// <summary>
        /// Pointer to delegate of type <see cref="MemoryNonContextualFree"/> to free memory.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr NonContextualFree;
    }
    #endregion

    #region Interpolation plug-in
    /// <summary>
    /// Defines flags that are passed to the interpolators factory.
    /// </summary>
    [Flags]
    public enum LerpFlags : uint
    {
        /// <summary>
        /// Defines the base type as 16 bit.
        /// </summary>
        SixteenBits = 0x0000,
        /// <summary>
        /// Defines the base type as floating-point.
        /// </summary>
        FloatingPoint = 0x0001,
        /// <summary>
        /// Hint to use tri-linear interpolation.
        /// </summary>
        Trilinear = 0x0100
    }

    /// <summary>
    /// Defines pre-computed parameters for use by interpolators.
    /// </summary>
    /// <remarks>
    /// Use this variant of interpolation parameters for Little CMS 2.11 and before.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct InterpolationParamsV1
    {
        /// <summary>
        /// The calling thread context. Can be <see cref="IntPtr.Zero"/>.
        /// </summary>
        public IntPtr ContextID;
        /// <summary>
        /// A copy of the flags specified when requesting the interpolation.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public LerpFlags dwFlags;
        /// <summary>
        /// The number of input channels.
        /// </summary>
        public uint nInputs;
        /// <summary>
        /// The number of output channels.
        /// </summary>
        public uint nOutputs;
        /// <summary>
        /// The number of grid points in each input dimension.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 8)]
        public uint[] nSamples;
        /// <summary>
        /// The number of grid points minus one in each input dimension.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 8)]
        public uint[] Domain;
        /// <summary>
        /// The result of multiplying Domin[n]*Opta[n-1] (offset in the table in base type).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 8)]
        public uint[] opta;
        /// <summary>
        /// Pointer to a block of unmanaged memory holding the table of gridpoints.
        /// </summary>
        public IntPtr Table;
        /// <summary>
        /// Pointer to the interpolator itself.
        /// </summary>
        public IntPtr Interpolation;
    }

    /// <summary>
    /// Defines pre-computed parameters for use by interpolators.
    /// </summary>
    /// <remarks>
    /// Use this variant of interpolation parameters for Little CMS 2.12 and later.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct InterpolationParamsV2
    {
        /// <summary>
        /// The calling thread context. Can be <see cref="IntPtr.Zero"/>.
        /// </summary>
        public IntPtr ContextID;
        /// <summary>
        /// A copy of the flags specified when requesting the interpolation.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public LerpFlags dwFlags;
        /// <summary>
        /// The number of input channels.
        /// </summary>
        public uint nInputs;
        /// <summary>
        /// The number of output channels.
        /// </summary>
        public uint nOutputs;
        /// <summary>
        /// The number of grid points in each input dimension.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 15)]
        public uint[] nSamples;
        /// <summary>
        /// The number of grid points minus one in each input dimension.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 15)]
        public uint[] Domain;
        /// <summary>
        /// The result of multiplying Domin[n]*Opta[n-1] (offset in the table in base type).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = 15)]
        public uint[] opta;
        /// <summary>
        /// Pointer to a block of unmanaged memory holding the table of gridpoints.
        /// </summary>
        public IntPtr Table;
        /// <summary>
        /// Pointer to the interpolator itself.
        /// </summary>
        public IntPtr Interpolation;
    }

    /// <summary>
    /// Defines a delegate to perform 16 bits interpolation.
    /// </summary>
    /// <param name="input">The inputs.</param>
    /// <param name="output">The outputs.</param>
    /// <param name="p">
    /// A pointer to the pre-computed parameters of type <see cref="InterpolationParamsV1"/> or <see cref="InterpolationParamsV2"/>.
    /// </param>
    public delegate void InterpFn16(
        IntPtr input,   // 'const' ushort[]
        IntPtr output,  // ushort[]
        IntPtr p);

    /// <summary>
    /// Defines a delegate to perform floating point interpolation.
    /// </summary>
    /// <param name="input">The inputs.</param>
    /// <param name="output">The outputs.</param>
    /// <param name="p">
    /// A pointer to the pre-computed parameters of type <see cref="InterpolationParamsV1"/> or <see cref="InterpolationParamsV2"/>.
    /// </param>
    public delegate void InterpFnFloat(
        IntPtr input,   // 'const' float[]
        IntPtr output,  // float[]
        IntPtr p);

    /// <summary>
    /// Defines a structure that provides the interpolator delegate returned by the interpolators factory.
    /// </summary>
    /// <remarks>
    /// In Little CMS this is a union of pointers that in the end behaves a single pointer, see cmsInterpFunction.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct InterpolationFunction
    {
        /// <summary>
        /// Pointer to the interpolator delegate,
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Interpolator;
    }

    /// <summary>
    /// Defines a delegate to define the interpolators factory.
    /// </summary>
    /// <param name="nInputChannels">The number of input channels.</param>
    /// <param name="nOutputChannels">The number of output channels.</param>
    /// <param name="flags">Flags defining base-type and any hints.</param>
    /// <returns>A structure of type <see cref="InterpolationFunction"/>.</returns>
    public delegate InterpolationFunction InterpolatorsFactory(
        uint nInputChannels, uint nOutputChannels, [MarshalAs(UnmanagedType.U4)] LerpFlags flags);

    /// <summary>
    /// Defines the interpolation plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginInterpolation
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// Pointer to a delegate of type <see cref="InterpolatorsFactory"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Factory;
    }
    #endregion

    #region Parametric curves plug-in
    /// <summary>
    /// Defines a delegate for user-supplied parametric curves.
    /// </summary>
    /// <param name="Type">The curve type.</param>
    /// <param name="Params">The parameters for the curve type.</param>
    /// <param name="R"></param>
    /// <returns>The result of applying the curve.</returns>
    /// <remarks>
    /// <para>
    /// Each parametric curve plug-in may implement an arbitrary number of upto 20 curve types.
    /// </para>
    /// <para>
    /// A negative type means the same function but analytically inverted.
    /// </para>
    /// <para>
    /// The domain of R is effectively from minus infinity to plus infinity. However, the normal,
    /// in-range domain is 0..1, so you have to normalize your function to get values of
    /// R = [0..1.0] and deal with remaining cases if you want your function to be able to work
    /// in unbounded mode.
    /// </para>
    /// </remarks>
    public delegate double ParametricCurveEvaluator(
        [MarshalAs(UnmanagedType.I4)] int Type,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R8, SizeConst = 10)] double[] Params,
        [MarshalAs(UnmanagedType.R8)] double R);

    /// <summary>
    /// Defines the parametric curves plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginParametricCurves
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// The number of functions.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint nFunctions;

        /// <summary>
        /// Id's for each parametric curve described by the plug-in.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = MAX_TYPES_IN_LCMS_PLUGIN)]
        public uint[] FunctionTypes;

        /// <summary>
        /// Number of parameters for each parametric curve described by the plug-in.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U4, SizeConst = MAX_TYPES_IN_LCMS_PLUGIN)]
        public uint[] ParameterCount;

        /// <summary>
        /// Pointer to a delegate of type <see cref="ParametricCurveEvaluator"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Evaluator;

        /// <summary>
        /// Defines size of array to be allocated for <see cref="FunctionTypes"/> and <see cref="ParameterCount"/>.
        /// </summary>
        public const int MAX_TYPES_IN_LCMS_PLUGIN = 20;
    }
    #endregion

    #region Formatters plug-in
    /// <summary>
    /// Defines the possible formatter directions.
    /// </summary>
    public enum FormatterDirection
    {
        /// <summary>
        /// Input direction.
        /// </summary>
        Input = 0,
        /// <summary>
        /// Output direction.
        /// </summary>
        Output = 1
    }

    /// <summary>
    /// Defines a delegate for a 16 bit unsigned integer formatter.
    /// </summary>
    /// <param name="CMMCargo"></param>
    /// <param name="Values"></param>
    /// <param name="Buffer"></param>
    /// <param name="Stride"></param>
    /// <returns></returns>
    public delegate IntPtr Formatter16(
        IntPtr CMMCargo,    // struct _cmstransfom_struct *
        IntPtr Values,      // ushort[]
        IntPtr Buffer,      // byte *
        [MarshalAs(UnmanagedType.U4)] uint Stride);

    /// <summary>
    /// Defines a delegate for a 32 bit float formatter.
    /// </summary>
    /// <param name="CMMCargo"></param>
    /// <param name="Values"></param>
    /// <param name="Buffer"></param>
    /// <param name="Stride"></param>
    /// <returns></returns>
    public delegate IntPtr FormatterFloat(
        IntPtr CMMCargo,    // struct _cmstransfom_struct *
        IntPtr Values,      // float[]
        IntPtr Buffer,      // byte *
        [MarshalAs(UnmanagedType.I4)] uint Stride);

    /// <summary>
    /// Defines a pointer to a formatter that can be either 16 bit unsigned or 32 bit float.
    /// </summary>
    public struct Formatter
    {
        /// <summary>
        /// Pointer to a delegate of type <see cref="Formatter16"/> or <see cref="FormatterFloat"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Fmt;
    }

    /// <summary>
    /// Defines a delegate to implement a format factory.
    /// </summary>
    /// <param name="Type">The type to format.</param>
    /// <param name="Dir">The required direction for the format.</param>
    /// <param name="dwFlags">Flags to select the formatter.</param>
    /// <returns></returns>
    public delegate Formatter FormatterFactory(
        [MarshalAs(UnmanagedType.U4)] uint Type,
        [MarshalAs(UnmanagedType.I4)] FormatterDirection Dir,
        [MarshalAs(UnmanagedType.U4)] uint dwFlags);

    /// <summary>
    /// Defines the formatters plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginFormatters
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// Pointer to a delegate of type <see cref="FormatterFactory"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr FormattersFactory;

        /// <summary>
        /// Identifies formatter as 16 bit.
        /// </summary>
        public const uint PACK_FLAGS_16BIT = 0x0000;
        /// <summary>
        /// Identifies formatter as 32 bit float.
        /// </summary>
        public const uint PACK_FLAGS_FLOAT = 0x0001;
    }
    #endregion

    #region Intent plug-in
    /// <summary>
    /// Defines a delegate to accept a chain of profiles and return a pipeline
    /// implementing a new intent.
    /// </summary>
    /// <param name="contextID"></param>
    /// <param name="nProfiles"></param>
    /// <param name="intents"></param>
    /// <param name="profiles"></param>
    /// <param name="bpc"></param>
    /// <param name="adaptationStates"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    public delegate IntPtr IntentFn(IntPtr contextID,
            [MarshalAs(UnmanagedType.U4)] uint nProfiles,
            IntPtr intents,             // uint[]
            IntPtr profiles,            // IntPtr[]
            IntPtr bpc,                 // int[]
            IntPtr adaptationStates,    // double[]
            [MarshalAs(UnmanagedType.U4)] uint flags);

    /// <summary>
    /// Defines the intent plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginIntent
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// The intent number.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint Intent;

        /// <summary>
        /// Pointer to a delegate of type <see cref="IntentFn"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Link;

        /// <summary>
        /// A description for the intent.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 256)]
        public byte[] Description;
    }
    #endregion

    #region Stages plug-in
    /// <summary>
    /// Defines the stages plug-in structure.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="TagTypeHandler.Duplicate"/> and <see cref="TagTypeHandler.Free"/>
    /// fields of <see cref="Handler"/> are not used and should be set to <see cref="IntPtr.Zero"/>.
    /// </para>
    /// <para>
    /// The <see cref="TagTypeHandler.Read"/> delegate must invoke
    /// <see cref="Stage.AllocPlaceholder(Context, StageSignature, uint, uint, StageEvalFn, StageDupElemFn, StageFreeElemFn, IntPtr)"/>
    /// to create the stage.
    /// </para>
    /// <para>
    /// The <see cref="TagTypeHandler.Write"/> delegate can access the stage internals by using
    /// <see cref="Stage.InputChannels"/>, <see cref="Stage.OutputChannels"/>, <see cref="Stage.StageType"/>
    /// and <see cref="Stage.Data"/>.
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginMultiProcessElement
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// The tag type handler.
        /// </summary>
        public TagTypeHandler Handler;
    }
    #endregion

    #region Optimization plug-in
    /// <summary>
    /// Defines a delegate that defines the optimization algorithm callback.
    /// </summary>
    /// <param name="Lut"></param>
    /// <param name="Intent"></param>
    /// <param name="InputFormat"></param>
    /// <param name="OutputFormat"></param>
    /// <param name="dwFlags"></param>
    /// <returns></returns>
    public delegate int OptimizeFn(
            IntPtr Lut,
            [MarshalAs(UnmanagedType.U4)] uint Intent,
            IntPtr InputFormat,
            IntPtr OutputFormat,
            IntPtr dwFlags);

    /// <summary>
    /// Defines the optimization plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginOptimization
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// Pointer to a delegate of type <see cref="OptimizeFn"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Optimize;
    }
    #endregion

    #region Full transform plug-in
    /// <summary>
    /// Defines the stride of a line. 
    /// </summary>
    /// <remarks>
    /// Requires Little CMS version 2.8 or later.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Stride
    {
        /// <summary>
        /// The distance in bytes from one line to the next on the input.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint BytesPerLineIn;
        /// <summary>
        /// The distance in bytes from one line to the next in the output.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint BytesPerLineOut;
        /// <summary>
        /// The distance in bytes from one plane to the next inside a line on the input.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint BytesPerPlaneIn;
        /// <summary>
        /// The distance in bytes from one plane to the next inside a line on the output.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint BytesPerPlaneOut;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CMM"></param>
    /// <param name="InputBuffer"></param>
    /// <param name="OutputBuffer"></param>
    /// <param name="Size"></param>
    /// <param name="Stride"></param>
    /// <remarks>
    /// Requires Little CMS version 2.4 to 2.7 and is deprecated in later versions.
    /// </remarks>
    public delegate void TransformFn(
            IntPtr CMM,             // struct _cmstransform_struct *
            IntPtr InputBuffer,     // const void *
            IntPtr OutputBuffer,    // void *
            [MarshalAs(UnmanagedType.U4)] uint Size,
            [MarshalAs(UnmanagedType.U4)] uint Stride);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CMMCargo"></param>
    /// <param name="InputBuffer"></param>
    /// <param name="OutputBuffer"></param>
    /// <param name="PixelsPerLine"></param>
    /// <param name="LineCount"></param>
    /// <param name="Stride">Pointer to <see cref="Stride"/> structure.</param>
    /// <remarks>
    /// Requires Little CMS version 2.8 or later.
    /// </remarks>
    public delegate void Transform2Fn(
            IntPtr CMMCargo,        // struct _cmstransfom_struct *
            IntPtr InputBuffer,     // const void *
            IntPtr OutputBuffer,    // void*
            [MarshalAs(UnmanagedType.U4)] uint PixelsPerLine,
            [MarshalAs(UnmanagedType.U4)] uint LineCount,
            IntPtr Stride);         // const Stride *

    /// <summary>
    /// Defines a delegate that defines the transform factory.
    /// </summary>
    /// <param name="xformPtr"></param>
    /// <param name="UserData"></param>
    /// <param name="FreePrivateDataFn"></param>
    /// <param name="Lut"></param>
    /// <param name="InputFormat"></param>
    /// <param name="OutputFormat"></param>
    /// <param name="dwFlags"></param>
    /// <returns></returns>
    /// <remarks>
    /// Requires Little CMS version 2.4 or later.
    /// </remarks>
    public delegate int TransformFactory(
            IntPtr xformPtr,        // Transform2Fn *
            IntPtr UserData,        // void**
            IntPtr FreePrivateDataFn,
            IntPtr Lut,             // pipeline**
            IntPtr InputFormat,     // uint*
            IntPtr OutputFormat,    // uint*
            IntPtr dwFlags);        // uint*

    /// <summary>
    /// Defines the full transform plug-in structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginTransform
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// Pointer to a delegate of type <see cref="TransformFactory"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Factory;
    }
    #endregion

    #region Mutex plug-in
    /// <summary>
    /// Defines a delegate to create a mutex.
    /// </summary>
    /// <param name="ContextID">The calling thread context.</param>
    /// <returns>A pointer to the mutex created.</returns>
    public delegate IntPtr CreateMutexFn(IntPtr ContextID);

    /// <summary>
    /// Defines a delegate to destroy a mutex.
    /// </summary>
    /// <param name="ContextID">The calling thread context.</param>
    /// <param name="mutex">A pointer to the mutex to be destroyed.</param>
    public delegate void DestroyMutexFn(IntPtr ContextID, IntPtr mutex);

    /// <summary>
    /// Defines a delegate to lock a mutex.
    /// </summary>
    /// <param name="ContextID">The calling thread context.</param>
    /// <param name="mutex">A pointer to the mutex to be locked.</param>
    /// <returns>1 if successful, otherwise 0.</returns>
    public delegate int LockMutexFn(IntPtr ContextID, IntPtr mutex);

    /// <summary>
    /// Defines a delegate to unlock a mutex.
    /// </summary>
    /// <param name="ContextID">The calling thread context.</param>
    /// <param name="mutex">A pointer to the mutex to be unlocked.</param>
    public delegate void UnlockMutexFn(IntPtr ContextID, IntPtr mutex);

    /// <summary>
    /// Defines the mutex plug-in structure.
    /// </summary>
    /// <remarks>
    /// Requires Little CMS version 2.6 or later.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginMutex
    {
        /// <summary>
        /// Inherited <see cref="PluginBase"/> structure.
        /// </summary>
        public PluginBase Base;

        /// <summary>
        /// Pointer to a delegate of type <see cref="CreateMutexFn"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Create;


        /// <summary>
        /// Pointer to a delegate of type <see cref="DestroyMutexFn"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Destroy;

        /// <summary>
        /// Pointer to a delegate of type <see cref="LockMutexFn"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Lock;

        /// <summary>
        /// Pointer to a delegate of type <see cref="UnlockMutexFn"/>.
        /// </summary>
        /// <remarks>
        /// Invoke <see cref="Marshal.GetFunctionPointerForDelegate(Delegate)"/>
        /// to obtain the <see cref="IntPtr"/> to be assigned to this value.
        /// </remarks>
        public IntPtr Unlock;
    }
    #endregion
}
