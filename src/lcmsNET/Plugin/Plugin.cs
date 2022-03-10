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

    /// <summary>
    /// Defines a delegate to select tag type based on the version of the ICC profile.
    /// </summary>
    /// <param name="iccVersion">The ICC profile version.</param>
    /// <param name="data">Pointer to tag contents.</param>
    /// <returns>The <see cref="TagTypeSignature"/> of the tag.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
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
        /// Delegate to select the tag type based on the version of the ICC profile.
        /// </summary>
        public DecideType Decider;

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
}
