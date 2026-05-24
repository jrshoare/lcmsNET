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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace lcmsNET
{
    internal static partial class Interop
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateProfilePlaceholder")]
        private static partial IntPtr CreateProfilePlaceholder_Internal(
                IntPtr contextID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateProfilePlaceholder", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateProfilePlaceholder_Internal(
                IntPtr contextID);
#endif

        internal static IntPtr CreatePlaceholder(IntPtr contextID)
        {
            return CreateProfilePlaceholder_Internal(contextID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateRGBProfile")]
        private static partial IntPtr CreateRGBProfile_Internal(
                in CIExyY whitePoint,
                in CIExyYTRIPLE primaries,
                [In] IntPtr[] transferFunction);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateRGBProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateRGBProfile_Internal(
                in CIExyY whitePoint,
                in CIExyYTRIPLE primaries,
                IntPtr[] transferFunction);
#endif

        internal static IntPtr CreateRGB(in CIExyY whitePoint, in CIExyYTRIPLE primaries, IntPtr[] transferFunction)
        {
            return CreateRGBProfile_Internal(whitePoint, primaries, transferFunction);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateRGBProfileTHR")]
        private static partial IntPtr CreateRGBProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint,
                in CIExyYTRIPLE primaries,
                [In] IntPtr[] transferFunction);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateRGBProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateRGBProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint,
                in CIExyYTRIPLE primaries,
                IntPtr[] transferFunction);
#endif

        internal static IntPtr CreateRGB(IntPtr contextID, in CIExyY whitePoint, in CIExyYTRIPLE primaries, IntPtr[] transferFunction)
        {
            return CreateRGBProfileTHR_Internal(contextID, whitePoint, primaries, transferFunction);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateGrayProfile")]
        private static partial IntPtr CreateGrayProfile_Internal(
                in CIExyY whitePoint,
                IntPtr transferFunction);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateGrayProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateGrayProfile_Internal(
                in CIExyY whitePoint,
                IntPtr transferFunction);
#endif

        internal static IntPtr CreateGray(in CIExyY whitePoint, IntPtr transferFunction)
        {
            return CreateGrayProfile_Internal(whitePoint, transferFunction);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateGrayProfileTHR")]
        private static partial IntPtr CreateGrayProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint,
                IntPtr transferFunction);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateGrayProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateGrayProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint,
                IntPtr transferFunction);
#endif

        internal static IntPtr CreateGray(IntPtr contextID, in CIExyY whitePoint, IntPtr transferFunction)
        {
            return CreateGrayProfileTHR_Internal(contextID, whitePoint, transferFunction);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLink")]
        private static partial IntPtr CreateLinearizationDeviceLink_Internal(
                [MarshalAs(UnmanagedType.U4)] uint space,
                [In] IntPtr[] transferFunction);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLinearizationDeviceLink_Internal(
                [MarshalAs(UnmanagedType.U4)] uint space,
                IntPtr[] transferFunction);
#endif

        internal static IntPtr CreateLinearizationDeviceLink(uint space, IntPtr[] transferFunction)
        {
            return CreateLinearizationDeviceLink_Internal(space, transferFunction);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLinkTHR")]
        private static partial IntPtr CreateLinearizationDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint space,
                [In] IntPtr[] transferFunction);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLinkTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLinearizationDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint space,
                IntPtr[] transferFunction);
#endif

        internal static IntPtr CreateLinearizationDeviceLink(IntPtr contextID, uint space, IntPtr[] transferFunction)
        {
            return CreateLinearizationDeviceLinkTHR_Internal(contextID, space, transferFunction);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLink")]
        private static partial IntPtr CreateInkLimitingDeviceLink_Internal(
                [MarshalAs(UnmanagedType.U4)] uint colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateInkLimitingDeviceLink_Internal(
                [MarshalAs(UnmanagedType.U4)] uint colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);
#endif

        internal static IntPtr CreateInkLimitingDeviceLink(uint colorSpaceSignature, double limit)
        {
            return CreateInkLimitingDeviceLink_Internal(colorSpaceSignature, limit);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLinkTHR")]
        private static partial IntPtr CreateInkLimitingDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLinkTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateInkLimitingDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);
#endif

        internal static IntPtr CreateInkLimitingDeviceLink(IntPtr contextID, uint colorSpaceSignature, double limit)
        {
            return CreateInkLimitingDeviceLinkTHR_Internal(contextID, colorSpaceSignature, limit);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateDeviceLinkFromCubeFile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateDeviceLinkFromCubeFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateDeviceLinkFromCubeFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateDeviceLinkFromCubeFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#endif

        internal static IntPtr CreateDeviceLinkFromCubeFile(string filepath)
        {
            return CreateDeviceLinkFromCubeFile_Internal(filepath);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateDeviceLinkFromCubeFileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateDeviceLinkFromCubeFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateDeviceLinkFromCubeFileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateDeviceLinkFromCubeFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#endif

        internal static IntPtr CreateDeviceLinkFromCubeFile(IntPtr contextID, string filepath)
        {
            return CreateDeviceLinkFromCubeFileTHR_Internal(contextID, filepath);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsTransform2DeviceLink")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr Transform2DeviceLink_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.R8)] double version,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsTransform2DeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Transform2DeviceLink_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.R8)] double version,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static IntPtr Transform2DeviceLink(IntPtr contextID, double version, uint flags)
        {
            return Transform2DeviceLink_Internal(contextID, version, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateLab2Profile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateLab2Profile_Internal(
                in CIExyY whitePoint);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateLab2Profile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab2Profile_Internal(
                in CIExyY whitePoint);
#endif

        internal static IntPtr CreateLab2(in CIExyY whitePoint)
        {
            return CreateLab2Profile_Internal(whitePoint);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateLab2ProfileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateLab2ProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateLab2ProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab2ProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint);
#endif

        internal static IntPtr CreateLab2(IntPtr contextID, in CIExyY whitePoint)
        {
            return CreateLab2ProfileTHR_Internal(contextID, whitePoint);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateLab4Profile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateLab4Profile_Internal(
                in CIExyY whitePoint);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateLab4Profile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab4Profile_Internal(
                in CIExyY whitePoint);
#endif

        internal static IntPtr CreateLab4(in CIExyY whitePoint)
        {
            return CreateLab4Profile_Internal(whitePoint);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateLab4ProfileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateLab4ProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateLab4ProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab4ProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint);
#endif

        internal static IntPtr CreateLab4(IntPtr contextID, in CIExyY whitePoint)
        {
            return CreateLab4ProfileTHR_Internal(contextID, whitePoint);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateXYZProfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateXYZProfile_Internal();
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateXYZProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateXYZProfile_Internal();
#endif

        internal static IntPtr CreateXYZ()
        {
            return CreateXYZProfile_Internal();
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateXYZProfileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateXYZProfileTHR_Internal(
                IntPtr contextID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateXYZProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateXYZProfileTHR_Internal(
                IntPtr contextID);
#endif

        internal static IntPtr CreateXYZ(IntPtr contextID)
        {
            return CreateXYZProfileTHR_Internal(contextID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreate_sRGBProfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr Create_sRGBProfile_Internal();
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreate_sRGBProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Create_sRGBProfile_Internal();
#endif

        internal static IntPtr Create_sRGB()
        {
            return Create_sRGBProfile_Internal();
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreate_sRGBProfileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr Create_sRGBProfileTHR_Internal(
                IntPtr contextID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreate_sRGBProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Create_sRGBProfileTHR_Internal(
                IntPtr contextID);
#endif

        internal static IntPtr Create_sRGB(IntPtr contextID)
        {
            return Create_sRGBProfileTHR_Internal(contextID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateNULLProfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateNULLProfile_Internal();
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateNULLProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateNULLProfile_Internal();
#endif

        internal static IntPtr CreateNull()
        {
            return CreateNULLProfile_Internal();
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateNULLProfileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateNULLProfileTHR_Internal(
                IntPtr contextID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateNULLProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateNULLProfileTHR_Internal(
                IntPtr contextID);
#endif

        internal static IntPtr CreateNull(IntPtr contextID)
        {
            return CreateNULLProfileTHR_Internal(contextID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateBCHSWabstractProfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateBCHSWabstractProfile_Internal(
                [MarshalAs(UnmanagedType.I4)] int nLutPoints,
                [MarshalAs(UnmanagedType.R8)] double bright,
                [MarshalAs(UnmanagedType.R8)] double contrast,
                [MarshalAs(UnmanagedType.R8)] double hue,
                [MarshalAs(UnmanagedType.R8)] double saturation,
                [MarshalAs(UnmanagedType.I4)] int tempSrc,
                [MarshalAs(UnmanagedType.I4)] int tempDest);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateBCHSWabstractProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateBCHSWabstractProfile_Internal(
                [MarshalAs(UnmanagedType.I4)] int nLutPoints,
                [MarshalAs(UnmanagedType.R8)] double bright,
                [MarshalAs(UnmanagedType.R8)] double contrast,
                [MarshalAs(UnmanagedType.R8)] double hue,
                [MarshalAs(UnmanagedType.R8)] double saturation,
                [MarshalAs(UnmanagedType.I4)] int tempSrc,
                [MarshalAs(UnmanagedType.I4)] int tempDest);
#endif

        internal static IntPtr CreateBCHSWabstract(int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return CreateBCHSWabstractProfile_Internal(nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateBCHSWabstractProfileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr CreateBCHSWabstractProfileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.I4)] int nLutPoints,
                [MarshalAs(UnmanagedType.R8)] double bright,
                [MarshalAs(UnmanagedType.R8)] double contrast,
                [MarshalAs(UnmanagedType.R8)] double hue,
                [MarshalAs(UnmanagedType.R8)] double saturation,
                [MarshalAs(UnmanagedType.I4)] int tempSrc,
                [MarshalAs(UnmanagedType.I4)] int tempDest);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateBCHSWabstractProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateBCHSWabstractProfileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.I4)] int nLutPoints,
                [MarshalAs(UnmanagedType.R8)] double bright,
                [MarshalAs(UnmanagedType.R8)] double contrast,
                [MarshalAs(UnmanagedType.R8)] double hue,
                [MarshalAs(UnmanagedType.R8)] double saturation,
                [MarshalAs(UnmanagedType.I4)] int tempSrc,
                [MarshalAs(UnmanagedType.I4)] int tempDest);
#endif

        internal static IntPtr CreateBCHSWabstract(IntPtr contextID, int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return CreateBCHSWabstractProfileTHR_Internal(contextID, nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreate_OkLabProfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr Create_OkLabProfile_Internal(
                IntPtr contextID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreate_OkLabProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Create_OkLabProfile_Internal(
                IntPtr contextID);
#endif

        internal static IntPtr Create_OkLab(IntPtr contextID)
        {
            return Create_OkLabProfile_Internal(contextID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsOpenProfileFromFile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr OpenProfileFromFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);
#else
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);
#endif

        internal static IntPtr OpenProfile(string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenProfileFromFile_Internal(filepath, access);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsOpenProfileFromFileTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr OpenProfileFromFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);
#else
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromFileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);
#endif

        internal static IntPtr OpenProfile(IntPtr contextID, string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenProfileFromFileTHR_Internal(contextID, filepath, access);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsOpenProfileFromMem")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr OpenProfileFromMem_Internal(
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] uint memSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromMem", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr OpenProfileFromMem_Internal(
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] uint memSize);
#endif

        internal unsafe static IntPtr OpenProfile(byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                return OpenProfileFromMem_Internal(memPtr, (uint)memory.Length);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsOpenProfileFromMemTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial IntPtr OpenProfileFromMemTHR_Internal(
                IntPtr contextID,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] uint memSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromMemTHR", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr OpenProfileFromMemTHR_Internal(
                IntPtr contextID,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] uint memSize);
#endif

        internal unsafe static IntPtr OpenProfile(IntPtr contextID, byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                return OpenProfileFromMemTHR_Internal(contextID, memPtr, (uint)memory.Length);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsOpenProfileFromIOhandlerTHR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr OpenProfileFromIOhandlerTHR_Internal(
                IntPtr contextID,
                IntPtr io);
#else
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromIOhandlerTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromIOhandlerTHR_Internal(
                IntPtr contextID,
                IntPtr io);
#endif

        internal static IntPtr OpenProfile(IntPtr contextID, IntPtr iohandler)
        {
            return OpenProfileFromIOhandlerTHR_Internal(contextID, iohandler);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsOpenProfileFromIOhandler2THR")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr OpenProfileFromIOhandler2THR_Internal(
                IntPtr contextID,
                IntPtr io,
                int write);
#else
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromIOhandler2THR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromIOhandler2THR_Internal(
                IntPtr contextID,
                IntPtr io,
                int write);
#endif

        internal static IntPtr OpenProfile(IntPtr contextID, IntPtr iohandler, int writeable)
        {
            return OpenProfileFromIOhandler2THR_Internal(contextID, iohandler, writeable);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCloseProfile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int CloseProfile_Internal(IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCloseProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern int CloseProfile_Internal(IntPtr handle);
#endif

        internal static int CloseProfile(IntPtr handle)
        {
            return CloseProfile_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSaveProfileToFile")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int SaveProfileToFile_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSaveProfileToFile", CallingConvention = CallingConvention.StdCall)]
        private static extern int SaveProfileToFile_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string filename);
#endif

        internal static int SaveProfile(IntPtr handle, string filepath)
        {
            Debug.Assert(filepath != null);

            return SaveProfileToFile_Internal(handle, filepath);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSaveProfileToMem")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial int SaveProfileToMem_Internal(
                IntPtr handle,
                void* memPtr,
                uint* bytesNeeded);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSaveProfileToMem", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int SaveProfileToMem_Internal(
                IntPtr handle,
                void* memPtr,
                uint* bytesNeeded);
#endif

        internal unsafe static int SaveProfile(IntPtr handle, byte[] memPtr, out uint bytesNeeded)
        {
            int result = 0;
            uint n = (uint)(memPtr?.Length ?? 0);
            if (memPtr is null)
            {
                result = SaveProfileToMem_Internal(handle, null, &n);
            }
            else
            {
                fixed (void* pMemPtr = &memPtr[0])
                {
                    result = SaveProfileToMem_Internal(handle, pMemPtr, &n);
                }
            }
            bytesNeeded = n;
            return result;
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSaveProfileToIOhandler")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static unsafe partial uint SaveProfileToIOhandler_Internal(
                IntPtr handle,
                IntPtr io);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSaveProfileToIOhandler", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern uint SaveProfileToIOhandler_Internal(
                IntPtr handle,
                IntPtr io);
#endif

        internal unsafe static uint SaveProfile(IntPtr handle, IntPtr iohandler)
        {
            return SaveProfileToIOhandler_Internal(handle, iohandler);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetColorSpace")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetColorSpace_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetColorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetColorSpace_Internal(
                IntPtr profile);
#endif

        internal static uint GetColorSpace(IntPtr handle)
        {
            return GetColorSpace_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetColorSpace")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetColorSpace_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint sig);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetColorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetColorSpace_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint sig);
#endif

        internal static void SetColorSpace(IntPtr handle, uint sig)
        {
            SetColorSpace_Internal(handle, sig);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetPCS")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetPCS_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetPCS", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPCS_Internal(
                IntPtr profile);
#endif

        internal static uint GetPCS(IntPtr handle)
        {
            return GetPCS_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetPCS")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetPCS_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint pcs);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetPCS", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetPCS_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint pcs);
#endif

        internal static void SetPCS(IntPtr handle, uint pcs)
        {
            SetPCS_Internal(handle, pcs);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetProfileInfo")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetProfileInfo_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint info,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                IntPtr buffer, /* wchar_t */
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetProfileInfo", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetProfileInfo_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint info,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer, /* wchar_t */
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

        internal static string GetProfileInfo(IntPtr handle, uint info, string languageCode, string countryCode)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            uint bytes = GetProfileInfo_Internal(handle, info, language, country, buffer, 0);
            if (bytes == 0) return null;

            int nbytes = Convert.ToInt32(bytes);
            buffer = Marshal.AllocHGlobal(nbytes);
            try
            {
                _ = GetProfileInfo_Internal(handle, info, language, country, buffer, bytes);

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // On Windows wchar_t is 2 bytes so just use in-built marshaling
                    return Marshal.PtrToStringUni(buffer);
                }

                // On Linux and OSX wchar_t is 4 bytes so we must convert accordingly
                Encoding encoding = Encoding.UTF32;
                byte[] arr = new byte[nbytes];
                Marshal.Copy(buffer, arr, 0, nbytes);
                return encoding.GetString(arr).TrimEnd('\0'); // remove any trailing NULLs
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetProfileInfoASCII")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetProfileInfoASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint info,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetProfileInfoASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetProfileInfoASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint info,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

        internal static string GetProfileInfoASCII(IntPtr handle, uint info, string languageCode, string countryCode)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            uint bytes = GetProfileInfoASCII_Internal(handle, info, language, country, buffer, 0);
            if (bytes == 0) return null;

            buffer = Marshal.AllocHGlobal(Convert.ToInt32(bytes));
            try
            {
                _ = GetProfileInfoASCII_Internal(handle, info, language, country, buffer, bytes);
                return Marshal.PtrToStringAnsi(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDetectBlackPoint")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int DetectBlackPoint_Internal(
                out CIEXYZ blackPoint,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDetectBlackPoint", CallingConvention = CallingConvention.StdCall)]
        private static extern int DetectBlackPoint_Internal(
                out CIEXYZ blackPoint,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static int DetectBlackPoint(IntPtr handle, out CIEXYZ blackPoint, uint intent, uint flags)
        {
            return DetectBlackPoint_Internal(out blackPoint, handle, intent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDetectDestinationBlackPoint")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int DetectDestinationBlackPoint_Internal(
                out CIEXYZ blackPoint,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDetectDestinationBlackPoint", CallingConvention = CallingConvention.StdCall)]
        private static extern int DetectDestinationBlackPoint_Internal(
                out CIEXYZ blackPoint,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static int DetectDestinationBlackPoint(IntPtr handle, out CIEXYZ blackPoint, uint intent, uint flags)
        {
            return DetectDestinationBlackPoint_Internal(out blackPoint, handle, intent, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDetectTAC")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial double DetectTAC_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDetectTAC", CallingConvention = CallingConvention.StdCall)]
        private static extern double DetectTAC_Internal(
                IntPtr profile);
#endif

        internal static double DetectTAC(IntPtr handle)
        {
            return DetectTAC_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetDeviceClass")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int GetDeviceClass_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetDeviceClass", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetDeviceClass_Internal(
                IntPtr profile);
#endif

        internal static int GetDeviceClass(IntPtr handle)
        {
            return GetDeviceClass_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetDeviceClass")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetDeviceClass_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint sig);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetDeviceClass", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetDeviceClass_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint sig);
#endif

        internal static void SetDeviceClass(IntPtr handle, uint sig)
        {
            SetDeviceClass_Internal(handle, sig);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderCreationDateTime")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int GetHeaderCreationDateTime_Internal(
                IntPtr profile,
                IntPtr tm);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderCreationDateTime", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetHeaderCreationDateTime_Internal(
                IntPtr profile,
                IntPtr tm);
#endif

        internal static int GetHeaderCreationDateTime(IntPtr handle, out DateTime dest)
        {
            int size = Marshal.SizeOf<Tm>();
            IntPtr ptr = Marshal.AllocHGlobal(size);

            try
            {
                int result = GetHeaderCreationDateTime_Internal(handle, ptr);
                if (result != 0)
                {
                    dest = Tm.FromHandle(ptr);
                }
                else
                {
                    dest = DateTime.MinValue;
                }
                return result;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderFlags")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetHeaderFlags_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderFlags", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetHeaderFlags_Internal(
                IntPtr profile);
#endif

        internal static uint GetHeaderFlags(IntPtr handle)
        {
            return GetHeaderFlags_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetHeaderFlags")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetHeaderFlags_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderFlags", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderFlags_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static void SetHeaderFlags(IntPtr handle, uint flags)
        {
            SetHeaderFlags_Internal(handle, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderManufacturer")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetHeaderManufacturer_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderManufacturer", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetHeaderManufacturer_Internal(
                IntPtr profile);
#endif

        internal static uint GetHeaderManufacturer(IntPtr handle)
        {
            return GetHeaderManufacturer_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetHeaderManufacturer")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetHeaderManufacturer_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderManufacturer", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderManufacturer_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static void SetHeaderManufacturer(IntPtr handle, uint flags)
        {
            SetHeaderManufacturer_Internal(handle, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderModel")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetHeaderModel_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderModel", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetHeaderModel_Internal(
                IntPtr profile);
#endif

        internal static uint GetHeaderModel(IntPtr handle)
        {
            return GetHeaderModel_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetHeaderModel")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetHeaderModel_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderModel", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderModel_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);
#endif

        internal static void SetHeaderModel(IntPtr handle, uint flags)
        {
            SetHeaderModel_Internal(handle, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderAttributes")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void GetHeaderAttributes_Internal(
                IntPtr profile,
                out ulong flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderAttributes", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetHeaderAttributes_Internal(
                IntPtr profile,
                out ulong flags);
#endif

        internal static ulong GetHeaderAttributes(IntPtr handle)
        {
            GetHeaderAttributes_Internal(handle, out ulong flags);
            return flags;
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetHeaderAttributes")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetHeaderAttributes_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U8)] ulong flags);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderAttributes", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderAttributes_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U8)] ulong flags);
#endif

        internal static void SetHeaderAttributes(IntPtr handle, ulong flags)
        {
            SetHeaderAttributes_Internal(handle, flags);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetProfileVersion")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial double GetProfileVersion_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetProfileVersion", CallingConvention = CallingConvention.StdCall)]
        private static extern double GetProfileVersion_Internal(
                IntPtr profile);
#endif

        internal static double GetProfileVersion(IntPtr handle)
        {
            return GetProfileVersion_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetProfileVersion")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetProfileVersion_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.R8)] double version);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetProfileVersion", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetProfileVersion_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.R8)] double version);
#endif

        internal static void SetProfileVersion(IntPtr handle, double version)
        {
            SetProfileVersion_Internal(handle, version);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetEncodedICCversion")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetEncodedICCVersion_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetEncodedICCversion", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetEncodedICCVersion_Internal(
                IntPtr profile);
#endif

        internal static uint GetEncodedICCVersion(IntPtr handle)
        {
            return GetEncodedICCVersion_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetEncodedICCversion")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetEncodedICCVersion_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint version);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetEncodedICCversion", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetEncodedICCVersion_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint version);
#endif

        internal static void SetEncodedICCVersion(IntPtr handle, uint version)
        {
            SetEncodedICCVersion_Internal(handle, version);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsMatrixShaper")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsMatrixShaper_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsMatrixShaper", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsMatrixShaper_Internal(
                IntPtr profile);
#endif

        internal static int IsMatrixShaper(IntPtr handle)
        {
            return IsMatrixShaper_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsCLUT")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsCLUT_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint usedDirection);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsCLUT", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsCLUT_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint usedDirection);
#endif

        internal static int IsCLUT(IntPtr handle, uint intent, uint usedDirection)
        {
            return IsCLUT_Internal(handle, intent, usedDirection);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetTagCount")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int GetTagCount_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetTagCount", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetTagCount_Internal(
                IntPtr profile);
#endif

        internal static int GetTagCount(IntPtr handle)
        {
            return GetTagCount_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetTagSignature")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int GetTagSignature_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint n);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetTagSignature", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetTagSignature_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint n);
#endif

        internal static int GetTagSignature(IntPtr handle, uint n)
        {
            return GetTagSignature_Internal(handle, n);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsTag")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsTag", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);
#endif

        internal static int IsTag(IntPtr handle, uint tag)
        {
            return IsTag_Internal(handle, tag);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsReadTag")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr ReadTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);
#else
        [DllImport(Liblcms, EntryPoint = "cmsReadTag", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ReadTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);
#endif

        internal static IntPtr ReadTag(IntPtr handle, uint tag)
        {
            return ReadTag_Internal(handle, tag);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsWriteTag")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int WriteTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag,
                IntPtr data);
#else
        [DllImport(Liblcms, EntryPoint = "cmsWriteTag", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag,
                IntPtr data);
#endif

        internal static int WriteTag(IntPtr handle, uint tag, IntPtr data)
        {
            return WriteTag_Internal(handle, tag, data);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsLinkTag")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int LinkTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag,
                [MarshalAs(UnmanagedType.U4)] uint dest);
#else
        [DllImport(Liblcms, EntryPoint = "cmsLinkTag", CallingConvention = CallingConvention.StdCall)]
        private static extern int LinkTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag,
                [MarshalAs(UnmanagedType.U4)] uint dest);
#endif

        internal static int LinkTag(IntPtr handle, uint tag, uint dest)
        {
            return LinkTag_Internal(handle, tag, dest);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsTagLinkedTo")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int TagLinkedTo_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);
#else
        [DllImport(Liblcms, EntryPoint = "cmsTagLinkedTo", CallingConvention = CallingConvention.StdCall)]
        private static extern int TagLinkedTo_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);
#endif

        internal static int TagLinkedTo(IntPtr handle, uint tag)
        {
            return TagLinkedTo_Internal(handle, tag);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderRenderingIntent")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int GetHeaderRenderingIntent_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderRenderingIntent", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetHeaderRenderingIntent_Internal(
                IntPtr profile);
#endif

        internal static int GetHeaderRenderingIntent(IntPtr handle)
        {
            return GetHeaderRenderingIntent_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetHeaderRenderingIntent")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int SetHeaderRenderingIntent_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderRenderingIntent", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetHeaderRenderingIntent_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent);
#endif

        internal static int SetHeaderRenderingIntent(IntPtr handle, uint intent)
        {
            return SetHeaderRenderingIntent_Internal(handle, intent);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsIsIntentSupported")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int IsIntentSupported_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint usedDirection);
#else
        [DllImport(Liblcms, EntryPoint = "cmsIsIntentSupported", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsIntentSupported_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint usedDirection);
#endif

        internal static int IsIntentSupported(IntPtr handle, uint intent, uint usedDirection)
        {
            return IsIntentSupported_Internal(handle, intent, usedDirection);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMD5computeID")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int MD5computeID_Internal(
                IntPtr profile);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMD5computeID", CallingConvention = CallingConvention.StdCall)]
        private static extern int MD5computeID_Internal(
                IntPtr profile);
#endif

        internal static int MD5ComputeID(IntPtr handle)
        {
            return MD5computeID_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetHeaderProfileID")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void GetHeaderProfileID_Internal(
                IntPtr profile,
                [Out] byte[] profileID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderProfileID", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetHeaderProfileID_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] byte[] profileID);
#endif

        internal static void GetHeaderProfileID(IntPtr handle, byte[] profileID)
        {
            GetHeaderProfileID_Internal(handle, profileID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetHeaderProfileID")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void SetHeaderProfileID_Internal(
                IntPtr profile,
                [In] byte[] profileID);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderProfileID", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderProfileID_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] byte[] profileID);
#endif

        internal static void SetHeaderProfileID(IntPtr handle, byte[] profileID)
        {
            SetHeaderProfileID_Internal(handle, profileID);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetPostScriptColorResource")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetPostScriptColorResource_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint type,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags,
                IntPtr io);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetPostScriptColorResource", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPostScriptColorResource_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)]uint type,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)]uint intent,
                [MarshalAs(UnmanagedType.U4)]uint flags,
                IntPtr io);
#endif

        internal static uint GetPostScriptColorResource(IntPtr handle, IntPtr contextID, uint type, uint intent, uint flags, IntPtr iohandler)
        {
            return GetPostScriptColorResource_Internal(contextID, type, handle, intent, flags, iohandler);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetPostScriptCSA")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetPostScriptCSA_Internal(
                IntPtr contextID,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetPostScriptCSA", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPostScriptCSA_Internal(
                IntPtr contextID,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)]uint intent,
                [MarshalAs(UnmanagedType.U4)]uint flags,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

        internal static byte[] GetPostScriptCSA(IntPtr handle, IntPtr contextID, uint intent, uint flags)
        {
            IntPtr buffer = IntPtr.Zero;
            uint bytes = GetPostScriptCSA_Internal(contextID, handle, intent, flags, buffer, 0);
            if (bytes == 0) return null;

            int nbytes = Convert.ToInt32(bytes);
            buffer = Marshal.AllocHGlobal(nbytes);
            try
            {
                _ = GetPostScriptCSA_Internal(contextID, handle, intent, flags, buffer, bytes);
                byte[] arr = new byte[nbytes];
                Marshal.Copy(buffer, arr, 0, nbytes);
                return arr;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetPostScriptCRD")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint GetPostScriptCRD_Internal(
                IntPtr contextID,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetPostScriptCRD", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPostScriptCRD_Internal(
                IntPtr contextID,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)]uint intent,
                [MarshalAs(UnmanagedType.U4)]uint flags,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

        internal static byte[] GetPostScriptCRD(IntPtr handle, IntPtr contextID, uint intent, uint flags)
        {
            IntPtr buffer = IntPtr.Zero;
            uint bytes = GetPostScriptCRD_Internal(contextID, handle, intent, flags, buffer, 0);
            if (bytes == 0) return null;

            int nbytes = Convert.ToInt32(bytes);
            buffer = Marshal.AllocHGlobal(nbytes);
            try
            {
                _ = GetPostScriptCRD_Internal(contextID, handle, intent, flags, buffer, bytes);
                byte[] arr = new byte[nbytes];
                Marshal.Copy(buffer, arr, 0, nbytes);
                return arr;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDetectRGBProfileGamma")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial double DetectRGBProfileGamma_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.R8)] double threshold);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDetectRGBProfileGamma", CallingConvention = CallingConvention.StdCall)]
        private static extern double DetectRGBProfileGamma_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.R8)] double threshold);
#endif

        internal static double DetectRGBProfileGamma(IntPtr handle, double threshold)
        {
            return DetectRGBProfileGamma_Internal(handle, threshold);
        }
    }
}
