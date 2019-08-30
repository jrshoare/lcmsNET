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
                ref CIExyY whitePoint,
                ref CIExyYTRIPLE primaries,
                IntPtr[] transferFunction);

        internal static IntPtr CreateRGB(CIExyY whitePoint, CIExyYTRIPLE primaries, IntPtr[] transferFunction)
        {
            return CreateRGBProfile_Internal(ref whitePoint, ref primaries, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateRGBProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateRGBProfileTHR_Internal(
                IntPtr contextID,
                ref CIExyY whitePoint,
                ref CIExyYTRIPLE primaries,
                IntPtr[] transferFunction);

        internal static IntPtr CreateRGB(IntPtr contextID, CIExyY whitePoint, CIExyYTRIPLE primaries, IntPtr[] transferFunction)
        {
            return CreateRGBProfileTHR_Internal(contextID, ref whitePoint, ref primaries, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateGrayProfile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateGrayProfile_Internal(
                ref CIExyY whitePoint,
                IntPtr transferFunction);

        internal static IntPtr CreateGray(CIExyY whitePoint, IntPtr transferFunction)
        {
            return CreateGrayProfile_Internal(ref whitePoint, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateGrayProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateGrayProfileTHR_Internal(
                IntPtr contextID,
                ref CIExyY whitePoint,
                IntPtr transferFunction);

        internal static IntPtr CreateGray(IntPtr contextID, CIExyY whitePoint, IntPtr transferFunction)
        {
            return CreateGrayProfileTHR_Internal(contextID, ref whitePoint, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLinearizationDeviceLink_Internal(
                [MarshalAs(UnmanagedType.I4)] int space,
                IntPtr[] transferFunction);

        internal static IntPtr CreateLinearizationDeviceLink(int space, IntPtr[] transferFunction)
        {
            return CreateLinearizationDeviceLink_Internal(space, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLinearizationDeviceLinkTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLinearizationDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.I4)] int space,
                IntPtr[] transferFunction);

        internal static IntPtr CreateLinearizationDeviceLink(IntPtr contextID, int space, IntPtr[] transferFunction)
        {
            return CreateLinearizationDeviceLinkTHR_Internal(contextID, space, transferFunction);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateInkLimitingDeviceLink_Internal(
                [MarshalAs(UnmanagedType.I4)] int colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);

        internal static IntPtr CreateInkLimitingDeviceLink(int colorSpaceSignature, double limit)
        {
            return CreateInkLimitingDeviceLink_Internal(colorSpaceSignature, limit);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateInkLimitingDeviceLinkTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateInkLimitingDeviceLinkTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.I4)] int colorSpaceSignature,
                [MarshalAs(UnmanagedType.R8)] double limit);

        internal static IntPtr CreateInkLimitingDeviceLink(IntPtr contextID, int colorSpaceSignature, double limit)
        {
            return CreateInkLimitingDeviceLinkTHR_Internal(contextID, colorSpaceSignature, limit);
        }

        [DllImport(Liblcms, EntryPoint = "cmsTransform2DeviceLink", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr Transform2DeviceLink_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.R8)] double version,
                [MarshalAs(UnmanagedType.U4)] int flags);

        internal static IntPtr Transform2DeviceLink(IntPtr contextID, double version, int flags)
        {
            return Transform2DeviceLink_Internal(contextID, version, flags);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab2Profile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab2Profile_Internal(
                ref CIExyY whitePoint);

        internal static IntPtr CreateLab2(CIExyY whitePoint)
        {
            return CreateLab2Profile_Internal(ref whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab2ProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab2ProfileTHR_Internal(
                IntPtr contextID,
                ref CIExyY whitePoint);

        internal static IntPtr CreateLab2(IntPtr contextID, CIExyY whitePoint)
        {
            return CreateLab2ProfileTHR_Internal(contextID, ref whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab4Profile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab4Profile_Internal(
                ref CIExyY whitePoint);

        internal static IntPtr CreateLab4(CIExyY whitePoint)
        {
            return CreateLab4Profile_Internal(ref whitePoint);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCreateLab4ProfileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateLab4ProfileTHR_Internal(
                IntPtr contextID,
                ref CIExyY whitePoint);

        internal static IntPtr CreateLab4(IntPtr contextID, CIExyY whitePoint)
        {
            return CreateLab4ProfileTHR_Internal(contextID, ref whitePoint);
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
                int* bytesNeeded);

        internal unsafe static int SaveProfile(IntPtr handle, byte[] memPtr, out int bytesNeeded)
        {
            int result = 0;
            int n = memPtr?.Length ?? 0;
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

        [DllImport(Liblcms, EntryPoint = "cmsGetColorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetColorSpace_Internal(
                IntPtr profile);

        internal static int GetColorSpaceSignature(IntPtr handle)
        {
            return GetColorSpace_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetPCS", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetPCS_Internal(
                IntPtr profile);

        internal static int GetPCSSignature(IntPtr handle)
        {
            return GetPCS_Internal(handle);
        }

        internal static int GetPCS(IntPtr handle)
        {
            return LCMSColorSpace_Internal(GetPCS_Internal(handle));
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileInfo", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetProfileInfo_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int info,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] int bufferSize);

        internal static string GetProfileInfo(IntPtr handle, int info, string languageCode, string countryCode)
        {
            byte[] language = Encoding.ASCII.GetBytes(languageCode);
            byte[] country = Encoding.ASCII.GetBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            int bytes = GetProfileInfo_Internal(handle, info, language, country, buffer, 0);
            buffer = Marshal.AllocHGlobal(bytes);
            try
            {
                GetProfileInfo_Internal(handle, info, language, country, buffer, bytes);
                return Marshal.PtrToStringAuto(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetProfileInfoASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetProfileInfoASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.I4)] int info,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] int bufferSize);

        internal static string GetProfileInfoASCII(IntPtr handle, int info, string languageCode, string countryCode)
        {
            byte[] language = Encoding.ASCII.GetBytes(languageCode);
            byte[] country = Encoding.ASCII.GetBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            int bytes = GetProfileInfoASCII_Internal(handle, info, language, country, buffer, 0);
            buffer = Marshal.AllocHGlobal(bytes);
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
    }
}
