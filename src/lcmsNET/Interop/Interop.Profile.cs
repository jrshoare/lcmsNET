﻿// Copyright(c) 2019-2021 John Stevenson-Hoare
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
        [DllImport(Liblcms, EntryPoint = "cmsCreateProfilePlaceholder", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateProfilePlaceholder_Internal(
                IntPtr contextID);

        internal static IntPtr CreatePlaceholder(IntPtr contextID)
        {
            return CreateProfilePlaceholder_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateRGBProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateRGBProfile_Internal(
                in CIExyY whitePoint,
                in CIExyYTRIPLE primaries,
                IntPtr[] transferFunction);

        internal static IntPtr CreateRGB(in CIExyY whitePoint, in CIExyYTRIPLE primaries, IntPtr[] transferFunction)
        {
            return CreateRGBProfile_Internal(whitePoint, primaries, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateRGBProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateRGBProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint,
                in CIExyYTRIPLE primaries,
                IntPtr[] transferFunction);

        internal static IntPtr CreateRGB(IntPtr contextID, in CIExyY whitePoint, in CIExyYTRIPLE primaries, IntPtr[] transferFunction)
        {
            return CreateRGBProfileTHR_Internal(contextID, whitePoint, primaries, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateGrayProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateGrayProfile_Internal(
                in CIExyY whitePoint,
                IntPtr transferFunction);

        internal static IntPtr CreateGray(in CIExyY whitePoint, IntPtr transferFunction)
        {
            return CreateGrayProfile_Internal(whitePoint, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateGrayProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateGrayProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint,
                IntPtr transferFunction);

        internal static IntPtr CreateGray(IntPtr contextID, in CIExyY whitePoint, IntPtr transferFunction)
        {
            return CreateGrayProfileTHR_Internal(contextID, whitePoint, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLinearizationDeviceLink_Internal(
                [MarshalAs(UnmanagedType.U4)] uint space,
                IntPtr[] transferFunction);

        internal static IntPtr CreateLinearizationDeviceLink(uint space, IntPtr[] transferFunction)
        {
            return CreateLinearizationDeviceLink_Internal(space, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLinkTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLinearizationDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint space,
                IntPtr[] transferFunction);

        internal static IntPtr CreateLinearizationDeviceLink(IntPtr contextID, uint space, IntPtr[] transferFunction)
        {
            return CreateLinearizationDeviceLinkTHR_Internal(contextID, space, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateInkLimitingDeviceLink_Internal(
                [MarshalAs(UnmanagedType.U4)] uint colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);

        internal static IntPtr CreateInkLimitingDeviceLink(uint colorSpaceSignature, double limit)
        {
            return CreateInkLimitingDeviceLink_Internal(colorSpaceSignature, limit);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLinkTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateInkLimitingDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);

        internal static IntPtr CreateInkLimitingDeviceLink(IntPtr contextID, uint colorSpaceSignature, double limit)
        {
            return CreateInkLimitingDeviceLinkTHR_Internal(contextID, colorSpaceSignature, limit);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateDeviceLinkFromCubeFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateDeviceLinkFromCubeFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)] string filename);

        internal static IntPtr CreateDeviceLinkFromCubeFile(string filepath)
        {
            return CreateDeviceLinkFromCubeFile_Internal(filepath);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateDeviceLinkFromCubeFileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateDeviceLinkFromCubeFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename);

        internal static IntPtr CreateDeviceLinkFromCubeFile(IntPtr contextID, string filepath)
        {
            return CreateDeviceLinkFromCubeFileTHR_Internal(contextID, filepath);
        }

        [DllImport(Liblcms, EntryPoint = "cmsTransform2DeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Transform2DeviceLink_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.R8)] double version,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static IntPtr Transform2DeviceLink(IntPtr contextID, double version, uint flags)
        {
            return Transform2DeviceLink_Internal(contextID, version, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab2Profile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab2Profile_Internal(
                in CIExyY whitePoint);

        internal static IntPtr CreateLab2(in CIExyY whitePoint)
        {
            return CreateLab2Profile_Internal(whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab2ProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab2ProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint);

        internal static IntPtr CreateLab2(IntPtr contextID, in CIExyY whitePoint)
        {
            return CreateLab2ProfileTHR_Internal(contextID, whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab4Profile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab4Profile_Internal(
                in CIExyY whitePoint);

        internal static IntPtr CreateLab4(in CIExyY whitePoint)
        {
            return CreateLab4Profile_Internal(whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab4ProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab4ProfileTHR_Internal(
                IntPtr contextID,
                in CIExyY whitePoint);

        internal static IntPtr CreateLab4(IntPtr contextID, in CIExyY whitePoint)
        {
            return CreateLab4ProfileTHR_Internal(contextID, whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateXYZProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateXYZProfile_Internal();

        internal static IntPtr CreateXYZ()
        {
            return CreateXYZProfile_Internal();
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateXYZProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateXYZProfileTHR_Internal(
                IntPtr contextID);

        internal static IntPtr CreateXYZ(IntPtr contextID)
        {
            return CreateXYZProfileTHR_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreate_sRGBProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Create_sRGBProfile_Internal();

        internal static IntPtr Create_sRGB()
        {
            return Create_sRGBProfile_Internal();
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreate_sRGBProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Create_sRGBProfileTHR_Internal(
                IntPtr contextID);

        internal static IntPtr Create_sRGB(IntPtr contextID)
        {
            return Create_sRGBProfileTHR_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateNULLProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateNULLProfile_Internal();

        internal static IntPtr CreateNull()
        {
            return CreateNULLProfile_Internal();
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateNULLProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateNULLProfileTHR_Internal(
                IntPtr contextID);

        internal static IntPtr CreateNull(IntPtr contextID)
        {
            return CreateNULLProfileTHR_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateBCHSWabstractProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateBCHSWabstractProfile_Internal(
                [MarshalAs(UnmanagedType.I4)] int nLutPoints,
                [MarshalAs(UnmanagedType.R8)] double bright,
                [MarshalAs(UnmanagedType.R8)] double contrast,
                [MarshalAs(UnmanagedType.R8)] double hue,
                [MarshalAs(UnmanagedType.R8)] double saturation,
                [MarshalAs(UnmanagedType.I4)] int tempSrc,
                [MarshalAs(UnmanagedType.I4)] int tempDest);

        internal static IntPtr CreateBCHSWabstract(int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return CreateBCHSWabstractProfile_Internal(nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest);
        }

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

        internal static IntPtr CreateBCHSWabstract(IntPtr contextID, int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return CreateBCHSWabstractProfileTHR_Internal(contextID, nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreate_OkLabProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Create_OkLabProfile_Internal(
                IntPtr contextID);

        internal static IntPtr Create_OkLab(IntPtr contextID)
        {
            return Create_OkLabProfile_Internal(contextID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);

        internal static IntPtr OpenProfile(string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenProfileFromFile_Internal(filepath, access);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromFileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)] string filename,
                [MarshalAs(UnmanagedType.LPStr)] string access);

        internal static IntPtr OpenProfile(IntPtr contextID, string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenProfileFromFileTHR_Internal(contextID, filepath, access);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromMem", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr OpenProfileFromMem_Internal(
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] int memSize);

        internal unsafe static IntPtr OpenProfile(byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                return OpenProfileFromMem_Internal(memPtr, memory.Length);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromMemTHR", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern IntPtr OpenProfileFromMemTHR_Internal(
                IntPtr contextID,
                /*const*/ void* memPtr,
                [MarshalAs(UnmanagedType.U4)] int memSize);

        internal unsafe static IntPtr OpenProfile(IntPtr contextID, byte[] memory)
        {
            fixed (void* memPtr = &memory[0])
            {
                return OpenProfileFromMemTHR_Internal(contextID, memPtr, memory.Length);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromIOhandlerTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromIOhandlerTHR_Internal(
                IntPtr contextID,
                IntPtr io);

        internal static IntPtr OpenProfile(IntPtr contextID, IntPtr iohandler)
        {
            return OpenProfileFromIOhandlerTHR_Internal(contextID, iohandler);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromIOhandler2THR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromIOhandler2THR_Internal(
                IntPtr contextID,
                IntPtr io,
                int write);

        internal static IntPtr OpenProfile(IntPtr contextID, IntPtr iohandler, int writeable)
        {
            return OpenProfileFromIOhandler2THR_Internal(contextID, iohandler, writeable);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCloseProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern int CloseProfile_Internal(IntPtr handle);

        internal static int CloseProfile(IntPtr handle)
        {
            return CloseProfile_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSaveProfileToFile", CallingConvention = CallingConvention.StdCall)]
        private static extern int SaveProfileToFile_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPStr)] string filename);

        internal static int SaveProfile(IntPtr handle, string filepath)
        {
            Debug.Assert(filepath != null);

            return SaveProfileToFile_Internal(handle, filepath);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSaveProfileToMem", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern int SaveProfileToMem_Internal(
                IntPtr handle,
                void* memPtr,
                uint* bytesNeeded);

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

        [DllImport(Liblcms, EntryPoint = "cmsSaveProfileToIOhandler", CallingConvention = CallingConvention.StdCall)]
        private unsafe static extern uint SaveProfileToIOhandler_Internal(
                IntPtr handle,
                IntPtr io);

        internal unsafe static uint SaveProfile(IntPtr handle, IntPtr iohandler)
        {
            return SaveProfileToIOhandler_Internal(handle, iohandler);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetColorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetColorSpace_Internal(
                IntPtr profile);

        internal static uint GetColorSpace(IntPtr handle)
        {
            return GetColorSpace_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetColorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetColorSpace_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint sig);

        internal static void SetColorSpace(IntPtr handle, uint sig)
        {
            SetColorSpace_Internal(handle, sig);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetPCS", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPCS_Internal(
                IntPtr profile);

        internal static uint GetPCS(IntPtr handle)
        {
            return GetPCS_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetPCS", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetPCS_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint pcs);

        internal static void SetPCS(IntPtr handle, uint pcs)
        {
            SetPCS_Internal(handle, pcs);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileInfo", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetProfileInfo_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint info,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer, /* wchar_t */
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);

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
                GetProfileInfo_Internal(handle, info, language, country, buffer, bytes);

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

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileInfoASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetProfileInfoASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint info,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);

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
                GetProfileInfoASCII_Internal(handle, info, language, country, buffer, bytes);
                return Marshal.PtrToStringAnsi(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsDetectBlackPoint", CallingConvention = CallingConvention.StdCall)]
        private static extern int DetectBlackPoint_Internal(
                out CIEXYZ blackPoint,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static int DetectBlackPoint(IntPtr handle, out CIEXYZ blackPoint, uint intent, uint flags)
        {
            return DetectBlackPoint_Internal(out blackPoint, handle, intent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDetectDestinationBlackPoint", CallingConvention = CallingConvention.StdCall)]
        private static extern int DetectDestinationBlackPoint_Internal(
                out CIEXYZ blackPoint,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static int DetectDestinationBlackPoint(IntPtr handle, out CIEXYZ blackPoint, uint intent, uint flags)
        {
            return DetectDestinationBlackPoint_Internal(out blackPoint, handle, intent, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDetectTAC", CallingConvention = CallingConvention.StdCall)]
        private static extern double DetectTAC_Internal(
                IntPtr profile);

        internal static double DetectTAC(IntPtr handle)
        {
            return DetectTAC_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetDeviceClass", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetDeviceClass_Internal(
                IntPtr profile);

        internal static int GetDeviceClass(IntPtr handle)
        {
            return GetDeviceClass_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetDeviceClass", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetDeviceClass_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint sig);

        internal static void SetDeviceClass(IntPtr handle, uint sig)
        {
            SetDeviceClass_Internal(handle, sig);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderCreationDateTime", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetHeaderCreationDateTime_Internal(
                IntPtr profile,
                IntPtr tm);

        internal static int GetHeaderCreationDateTime(IntPtr handle, out DateTime dest)
        {
            int size = Marshal.SizeOf(typeof(Tm));
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

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderFlags", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetHeaderFlags_Internal(
                IntPtr profile);

        internal static uint GetHeaderFlags(IntPtr handle)
        {
            return GetHeaderFlags_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderFlags", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderFlags_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static void SetHeaderFlags(IntPtr handle, uint flags)
        {
            SetHeaderFlags_Internal(handle, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderManufacturer", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetHeaderManufacturer_Internal(
                IntPtr profile);

        internal static uint GetHeaderManufacturer(IntPtr handle)
        {
            return GetHeaderManufacturer_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderManufacturer", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderManufacturer_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static void SetHeaderManufacturer(IntPtr handle, uint flags)
        {
            SetHeaderManufacturer_Internal(handle, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderModel", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetHeaderModel_Internal(
                IntPtr profile);

        internal static uint GetHeaderModel(IntPtr handle)
        {
            return GetHeaderModel_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderModel", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderModel_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint flags);

        internal static void SetHeaderModel(IntPtr handle, uint flags)
        {
            SetHeaderModel_Internal(handle, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderAttributes", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetHeaderAttributes_Internal(
                IntPtr profile,
                out ulong flags);

        internal static ulong GetHeaderAttributes(IntPtr handle)
        {
            GetHeaderAttributes_Internal(handle, out ulong flags);
            return flags;
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderAttributes", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderAttributes_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U8)] ulong flags);

        internal static void SetHeaderAttributes(IntPtr handle, ulong flags)
        {
            SetHeaderAttributes_Internal(handle, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileVersion", CallingConvention = CallingConvention.StdCall)]
        private static extern double GetProfileVersion_Internal(
                IntPtr profile);

        internal static double GetProfileVersion(IntPtr handle)
        {
            return GetProfileVersion_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetProfileVersion", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetProfileVersion_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.R8)] double version);

        internal static void SetProfileVersion(IntPtr handle, double version)
        {
            SetProfileVersion_Internal(handle, version);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetEncodedICCversion", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetEncodedICCVersion_Internal(
                IntPtr profile);

        internal static uint GetEncodedICCVersion(IntPtr handle)
        {
            return GetEncodedICCVersion_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetEncodedICCversion", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetEncodedICCVersion_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint version);

        internal static void SetEncodedICCVersion(IntPtr handle, uint version)
        {
            SetEncodedICCVersion_Internal(handle, version);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsMatrixShaper", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsMatrixShaper_Internal(
                IntPtr profile);

        internal static int IsMatrixShaper(IntPtr handle)
        {
            return IsMatrixShaper_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsCLUT", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsCLUT_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint usedDirection);

        internal static int IsCLUT(IntPtr handle, uint intent, uint usedDirection)
        {
            return IsCLUT_Internal(handle, intent, usedDirection);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetTagCount", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetTagCount_Internal(
                IntPtr profile);

        internal static int GetTagCount(IntPtr handle)
        {
            return GetTagCount_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetTagSignature", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetTagSignature_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint n);

        internal static int GetTagSignature(IntPtr handle, uint n)
        {
            return GetTagSignature_Internal(handle, n);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsTag", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);

        internal static int IsTag(IntPtr handle, uint tag)
        {
            return IsTag_Internal(handle, tag);
        }

        [DllImport(Liblcms, EntryPoint = "cmsReadTag", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr ReadTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);

        internal static IntPtr ReadTag(IntPtr handle, uint tag)
        {
            return ReadTag_Internal(handle, tag);
        }

        [DllImport(Liblcms, EntryPoint = "cmsWriteTag", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag,
                IntPtr data);

        internal static int WriteTag(IntPtr handle, uint tag, IntPtr data)
        {
            return WriteTag_Internal(handle, tag, data);
        }

        [DllImport(Liblcms, EntryPoint = "cmsLinkTag", CallingConvention = CallingConvention.StdCall)]
        private static extern int LinkTag_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag,
                [MarshalAs(UnmanagedType.U4)] uint dest);

        internal static int LinkTag(IntPtr handle, uint tag, uint dest)
        {
            return LinkTag_Internal(handle, tag, dest);
        }

        [DllImport(Liblcms, EntryPoint = "cmsTagLinkedTo", CallingConvention = CallingConvention.StdCall)]
        private static extern int TagLinkedTo_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint tag);

        internal static int TagLinkedTo(IntPtr handle, uint tag)
        {
            return TagLinkedTo_Internal(handle, tag);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderRenderingIntent", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetHeaderRenderingIntent_Internal(
                IntPtr profile);

        internal static int GetHeaderRenderingIntent(IntPtr handle)
        {
            return GetHeaderRenderingIntent_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderRenderingIntent", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetHeaderRenderingIntent_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent);

        internal static int SetHeaderRenderingIntent(IntPtr handle, uint intent)
        {
            return SetHeaderRenderingIntent_Internal(handle, intent);
        }

        [DllImport(Liblcms, EntryPoint = "cmsIsIntentSupported", CallingConvention = CallingConvention.StdCall)]
        private static extern int IsIntentSupported_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)] uint intent,
                [MarshalAs(UnmanagedType.U4)] uint usedDirection);

        internal static int IsIntentSupported(IntPtr handle, uint intent, uint usedDirection)
        {
            return IsIntentSupported_Internal(handle, intent, usedDirection);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMD5computeID", CallingConvention = CallingConvention.StdCall)]
        private static extern int MD5computeID_Internal(
                IntPtr profile);

        internal static int MD5ComputeID(IntPtr handle)
        {
            return MD5computeID_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetHeaderProfileID", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetHeaderProfileID_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] byte[] profileID);

        internal static void GetHeaderProfileID(IntPtr handle, byte[] profileID)
        {
            GetHeaderProfileID_Internal(handle, profileID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetHeaderProfileID", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetHeaderProfileID_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] byte[] profileID);

        internal static void SetHeaderProfileID(IntPtr handle, byte[] profileID)
        {
            SetHeaderProfileID_Internal(handle, profileID);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetPostScriptColorResource", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPostScriptColorResource_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)]uint type,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)]uint intent,
                [MarshalAs(UnmanagedType.U4)]uint flags,
                IntPtr io);

        internal static uint GetPostScriptColorResource(IntPtr handle, IntPtr contextID, uint type, uint intent, uint flags, IntPtr iohandler)
        {
            return GetPostScriptColorResource_Internal(contextID, type, handle, intent, flags, iohandler);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetPostScriptCSA", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPostScriptCSA_Internal(
                IntPtr contextID,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)]uint intent,
                [MarshalAs(UnmanagedType.U4)]uint flags,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);

        internal static byte[] GetPostScriptCSA(IntPtr handle, IntPtr contextID, uint intent, uint flags)
        {
            IntPtr buffer = IntPtr.Zero;
            uint bytes = GetPostScriptCSA_Internal(contextID, handle, intent, flags, buffer, 0);
            if (bytes == 0) return null;

            int nbytes = Convert.ToInt32(bytes);
            buffer = Marshal.AllocHGlobal(nbytes);
            try
            {
                GetPostScriptCSA_Internal(contextID, handle, intent, flags, buffer, bytes);
                byte[] arr = new byte[nbytes];
                Marshal.Copy(buffer, arr, 0, nbytes);
                return arr;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetPostScriptCRD", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetPostScriptCRD_Internal(
                IntPtr contextID,
                IntPtr profile,
                [MarshalAs(UnmanagedType.U4)]uint intent,
                [MarshalAs(UnmanagedType.U4)]uint flags,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);

        internal static byte[] GetPostScriptCRD(IntPtr handle, IntPtr contextID, uint intent, uint flags)
        {
            IntPtr buffer = IntPtr.Zero;
            uint bytes = GetPostScriptCRD_Internal(contextID, handle, intent, flags, buffer, 0);
            if (bytes == 0) return null;

            int nbytes = Convert.ToInt32(bytes);
            buffer = Marshal.AllocHGlobal(nbytes);
            try
            {
                GetPostScriptCRD_Internal(contextID, handle, intent, flags, buffer, bytes);
                byte[] arr = new byte[nbytes];
                Marshal.Copy(buffer, arr, 0, nbytes);
                return arr;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsDetectRGBProfileGamma", CallingConvention = CallingConvention.StdCall)]
        private static extern double DetectRGBProfileGamma_Internal(
                IntPtr profile,
                [MarshalAs(UnmanagedType.R8)] double threshold);

        internal static double DetectRGBProfileGamma(IntPtr handle, double threshold)
        {
            return DetectRGBProfileGamma_Internal(handle, threshold);
        }
    }
}
