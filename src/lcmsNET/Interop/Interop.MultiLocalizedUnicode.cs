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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace lcmsNET
{
    internal static partial class Interop
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUalloc")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr MLUalloc_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nItems);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUalloc", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr MLUalloc_Internal(
                IntPtr contextID,
                [MarshalAs(UnmanagedType.U4)] uint nItems);
#endif

        internal static IntPtr MLUAlloc(IntPtr contextID, uint nItems)
        {
            return MLUalloc_Internal(contextID, nItems);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUfree")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial void MLUfree_Internal(IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUfree", CallingConvention = CallingConvention.StdCall)]
        private static extern void MLUfree_Internal(IntPtr handle);
#endif

        internal static void MLUFree(IntPtr handle)
        {
            MLUfree_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUdup")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial IntPtr MLUdup_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUdup", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr MLUdup_Internal(
                IntPtr handle);
#endif

        internal static IntPtr MLUDup(IntPtr handle)
        {
            return MLUdup_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUsetASCII")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int MLUsetASCII_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPStr)] string asciiString);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUsetASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUsetASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPStr)] string asciiString);
#endif

        internal static int MLUSetAscii(IntPtr handle, string languageCode, string countryCode, string value)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            return MLUsetASCII_Internal(handle, language, country, value);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUsetWide")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int MLUsetWide_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPWStr)] string wideString);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUsetWide", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUsetWide_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPWStr)] string wideString);
#endif

        internal static int MLUSetWide(IntPtr handle, string languageCode, string countryCode, string value)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            return MLUsetWide_Internal(handle, language, country, value);
        }

#if NET5_0_OR_GREATER
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUsetUTF8")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int MLUsetUTF8_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8String);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUsetUTF8", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUsetUTF8_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8String);
#endif

        internal static int MLUSetUTF8(IntPtr handle, string languageCode, string countryCode, string value)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            return MLUsetUTF8_Internal(handle, language, country, value);
        }
#endif

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUgetASCII")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint MLUgetASCII_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUgetASCII", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUgetASCII_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

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
                _ = MLUgetASCII_Internal(handle, language, country, buffer, bytes);
                return Marshal.PtrToStringAnsi(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUgetWide")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint MLUgetWide_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUgetWide", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUgetWide_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

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
                _ = MLUgetWide_Internal(handle, language, country, buffer, bytes);
                return Marshal.PtrToStringUni(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

#if NET5_0_OR_GREATER
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUgetUTF8")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint MLUgetUTF8_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUgetUTF8", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUgetUTF8_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                IntPtr buffer,
                [MarshalAs(UnmanagedType.U4)] uint bufferSize);
#endif

        internal static string MLUGetUTF8(IntPtr handle, string languageCode, string countryCode)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);

            IntPtr buffer = IntPtr.Zero;
            uint bytes = MLUgetUTF8_Internal(handle, language, country, buffer, 0);
            if (bytes == 0) return null;

            buffer = Marshal.AllocHGlobal(Convert.ToInt32(bytes));
            try
            {
                _ = MLUgetUTF8_Internal(handle, language, country, buffer, bytes);
                return Marshal.PtrToStringUTF8(buffer);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
#endif

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUgetTranslation")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int MLUgetTranslation_Internal(
                IntPtr handle,
                [In] byte[] languageCode,
                [In] byte[] countryCode,
                [Out] byte[] obtainedLanguage,
                [Out] byte[] obtainedCountry);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUgetTranslation", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUgetTranslation_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] obtainedLanguage,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] obtainedCountry);
#endif

        internal static int MLUGetTranslation(IntPtr handle, string languageCode, string countryCode,
                out string translationLanguage, out string translationCountry)
        {
            byte[] language = Helper.ToASCIIBytes(languageCode);
            byte[] country = Helper.ToASCIIBytes(countryCode);
            byte[] obtainedLanguage = [0, 0, 0];
            byte[] obtainedCountry = [0, 0, 0];

            int result = MLUgetTranslation_Internal(handle, language, country, obtainedLanguage, obtainedCountry);
            if (result != 0)
            {
                // remove any single trailing null character
#if NET5_0_OR_GREATER
                translationLanguage = MatchSingleTrailingNull().Replace(Helper.ToString(obtainedLanguage), "");
                translationCountry = MatchSingleTrailingNull().Replace(Helper.ToString(obtainedCountry), "");
#else
                translationLanguage = Regex.Replace(Helper.ToString(obtainedLanguage), "\0$", "");
                translationCountry = Regex.Replace(Helper.ToString(obtainedCountry), "\0$", "");
#endif
            }
            else
            {
                translationLanguage = translationCountry = null;
            }
            return result;
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUtranslationsCount")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial uint MLUtranslationsCount_Internal(
                IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUtranslationsCount", CallingConvention = CallingConvention.StdCall)]
        private static extern uint MLUtranslationsCount_Internal(
                IntPtr handle);
#endif

        internal static uint MLUTranslationsCount(IntPtr handle)
        {
            return MLUtranslationsCount_Internal(handle);
        }


#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsMLUtranslationsCodes")]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int MLUtranslationsCodes_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint index,
                [Out] byte[] languageCode,
                [Out] byte[] countryCode);
#else
        [DllImport(Liblcms, EntryPoint = "cmsMLUtranslationsCodes", CallingConvention = CallingConvention.StdCall)]
        private static extern int MLUtranslationsCodes_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint index,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] languageCode,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeConst = 3)] byte[] countryCode);
#endif

        internal static int MLUTranslationsCodes(IntPtr handle, uint index, out string languageCode, out string countryCode)
        {
            byte[] language = [0, 0, 0];
            byte[] country = [0, 0, 0];

            int result = MLUtranslationsCodes_Internal(handle, index, language, country);
            if (result != 0)
            {
                // remove any single trailing null character
#if NET5_0_OR_GREATER
                languageCode = MatchSingleTrailingNull().Replace(Helper.ToString(language), "");
                countryCode = MatchSingleTrailingNull().Replace(Helper.ToString(country), "");
#else
                languageCode = Regex.Replace(Helper.ToString(language), "\0$", "");
                countryCode = Regex.Replace(Helper.ToString(country), "\0$", "");
#endif
            }
            else
            {
                languageCode = countryCode = null;
            }
            return result;
        }

#if NET5_0_OR_GREATER
        [GeneratedRegex("\0$")]
        private static partial Regex MatchSingleTrailingNull();
#endif
    }
}
