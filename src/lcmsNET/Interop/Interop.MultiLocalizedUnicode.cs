﻿using lcmsNET.Impl;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsMLUalloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr MLUalloc_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nItems);

        internal static IntPtr MLUAlloc(IntPtr contextID, uint nItems)
        {
            return MLUalloc_Internal(contextID, nItems);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUfree", CallingConvention = CallingConvention.StdCall)]
        private static extern void MLUfree_Internal(IntPtr handle);

        internal static void MLUFree(IntPtr handle)
        {
            MLUfree_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUdup", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr MLUdup_Internal(
                IntPtr handle);

        internal static IntPtr MLUDup(IntPtr handle)
        {
            return MLUdup_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUsetASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUsetASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPStr)] string asciiString);

        internal static int MLUSetAscii(IntPtr handle, string languageCode, string countryCode, string value)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            return MLUsetASCII_Internal(handle, language, country, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUsetWide", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUsetWide_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPWStr)] string wideString);

        internal static int MLUSetWide(IntPtr handle, string languageCode, string countryCode, string value)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            return MLUsetWide_Internal(handle, language, country, value);
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUgetASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUgetASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);

        internal static string MLUGetASCII(IntPtr handle, string languageCode, string countryCode)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            uint bytes = MLUgetASCII_Internal(handle, language, country, buffer, 0);
            if (bytes == 0) return null;

            buffer = Marshal.AllocHGlobal(Convert.ToInt32(bytes));
            try
            {
                MLUgetASCII_Internal(handle, language, country, buffer, bytes);
                return Marshal.PtrToStringAnsi(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUgetWide", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUgetWide_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);

        internal static string MLUGetWide(IntPtr handle, string languageCode, string countryCode)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            uint bytes = MLUgetWide_Internal(handle, language, country, buffer, 0);
            if (bytes == 0) return null;

            buffer = Marshal.AllocHGlobal(Convert.ToInt32(bytes));
            try
            {
                MLUgetWide_Internal(handle, language, country, buffer, bytes);
                return Marshal.PtrToStringUni(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUgetTranslation", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUgetTranslation_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] obtainedLanguage,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] obtainedCountry);

        internal static int MLUGetTranslation(IntPtr handle, string languageCode, string countryCode,
                out string translationLanguage, out string translationCountry)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);
            byte[] obtainedLanguage = new byte[3] { 0, 0, 0 };
            byte[] obtainedCountry = new byte[3] { 0, 0, 0 };

            int result = MLUgetTranslation_Internal(handle, language, country, obtainedLanguage, obtainedCountry);
            if (result != 0)
            {
                // remove any single trailing null character
                translationLanguage = Regex.Replace(Helper.ToString(obtainedLanguage), "\0$", "");
                translationCountry = Regex.Replace(Helper.ToString(obtainedCountry), "\0$", "");
            }
            else
            {
                translationLanguage = translationCountry = null;
            }
            return result;
        }

        [DllImport(Liblcms, EntryPoint = "cmsMLUtranslationsCount", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUtranslationsCount_Internal(
                IntPtr handle);

        internal static uint MLUTranslationsCount(IntPtr handle)
        {
            return MLUtranslationsCount_Internal(handle);
        }


        [DllImport(Liblcms, EntryPoint = "cmsMLUtranslationsCodes", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUtranslationsCodes_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint index,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode);

        internal static int MLUTranslationsCodes(IntPtr handle, uint index, out string languageCode, out string countryCode)
        {
            byte[] language = new byte[3] { 0, 0, 0 };
            byte[] country = new byte[3] { 0, 0, 0 };

            int result = MLUtranslationsCodes_Internal(handle, index, language, country);
            if (result != 0)
            {
                // remove any single trailing null character
                languageCode = Regex.Replace(Helper.ToString(language), "\0$", "");
                countryCode = Regex.Replace(Helper.ToString(country), "\0$", "");
            }
            else
            {
                languageCode = countryCode = null;
            }
            return result;
        }
    }
}
