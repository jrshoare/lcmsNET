using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromFile", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromFile_Internal(
                [MarshalAs(UnmanagedType.LPStr)]string filename,
                [MarshalAs(UnmanagedType.LPStr)]string access);

        internal static IntPtr OpenProfile(string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenProfileFromFile_Internal(filepath, access);
        }

        [DllImport(Liblcms, EntryPoint = "cmsOpenProfileFromFileTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr OpenProfileFromFileTHR_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.LPStr)]string filename,
                [MarshalAs(UnmanagedType.LPStr)]string access);

        internal static IntPtr OpenProfile(IntPtr contextID, string filepath, string access)
        {
            Debug.Assert(filepath != null);
            Debug.Assert(access != null);

            return OpenProfileFromFileTHR_Internal(contextID, filepath, access);
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
                [MarshalAs(UnmanagedType.LPStr)]string filename);

        internal static int SaveProfile(IntPtr handle, string filepath)
        {
            Debug.Assert(filepath != null);

            return SaveProfileToFile_Internal(handle, filepath);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetColorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetColorSpace_Internal(
                IntPtr profile);

        internal static int GetColorSpaceSignature(IntPtr handle)
        {
            return GetColorSpace_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "_cmsLCMScolorSpace", CallingConvention = CallingConvention.StdCall)]
        private static extern int LCMSColorSpace_Internal(
                [MarshalAs(UnmanagedType.U4)] int colorSpaceSignature);

        internal static int GetColorSpace(IntPtr handle)
        {
            return LCMSColorSpace_Internal(GetColorSpace_Internal(handle));
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetPCS", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetPCSSignature_Internal(
                IntPtr profile);

        internal static int GetPCSSignature(IntPtr handle)
        {
            return GetPCSSignature_Internal(handle);
        }

        internal static int GetPCS(IntPtr handle)
        {
            return LCMSColorSpace_Internal(GetPCSSignature_Internal(handle));
        }
    }
}
