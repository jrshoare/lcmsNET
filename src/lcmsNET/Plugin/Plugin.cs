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
        Transform = 0x7A666D48
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
}
