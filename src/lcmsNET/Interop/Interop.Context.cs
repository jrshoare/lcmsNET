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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsCreateContext")]
        private static partial IntPtr CreateContext_Internal(
                IntPtr plugin,
                IntPtr userData);
#else
        [DllImport(Liblcms, EntryPoint = "cmsCreateContext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateContext_Internal(
                IntPtr plugin,
                IntPtr userData);
#endif

        internal static IntPtr CreateContext(IntPtr plugin, IntPtr userData)
        {
            return CreateContext_Internal(plugin, userData);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDeleteContext")]
        private static partial void DeleteContext_Internal(
            IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDeleteContext", CallingConvention = CallingConvention.StdCall)]
        private static extern void DeleteContext_Internal(
            IntPtr handle);
#endif

        internal static void DeleteContext(IntPtr handle)
        {
            DeleteContext_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsDupContext")]
        private static partial IntPtr DuplicateContext_Internal(
            IntPtr handle,
            IntPtr userData);
#else
        [DllImport(Liblcms, EntryPoint = "cmsDupContext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DuplicateContext_Internal(
            IntPtr handle,
            IntPtr userData);
#endif

        internal static IntPtr DuplicateContext(IntPtr handle, IntPtr userData)
        {
            return DuplicateContext_Internal(handle, userData);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetContextUserData")]
        private static partial IntPtr GetContextUserData_Internal(
            IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetContextUserData", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetContextUserData_Internal(
            IntPtr handle);
#endif

        internal static IntPtr GetContextUserData(IntPtr handle)
        {
            return GetContextUserData_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsPluginTHR")]
        private static partial int PluginTHR_Internal(
            IntPtr handle,
            IntPtr plugin);
#else
        [DllImport(Liblcms, EntryPoint = "cmsPluginTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern int PluginTHR_Internal(
            IntPtr handle,
            IntPtr plugin);
#endif

        internal static int RegisterContextPlugins(IntPtr handle, IntPtr plugin)
        {
            return PluginTHR_Internal(handle, plugin);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsUnregisterPluginsTHR")]
        private static partial void UnregisterPluginsTHR_Internal(
            IntPtr handle);
#else
        [DllImport(Liblcms, EntryPoint = "cmsUnregisterPluginsTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern void UnregisterPluginsTHR_Internal(
            IntPtr handle);
#endif

        internal static void UnregisterContextPlugins(IntPtr handle)
        {
            UnregisterPluginsTHR_Internal(handle);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetLogErrorHandlerTHR")]
        private static partial void SetLogErrorHandlerTHR_Internal(
                IntPtr handle,
                IntPtr fn);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetLogErrorHandlerTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetLogErrorHandlerTHR_Internal(
                IntPtr handle,
                IntPtr fn);
#endif

        internal static void SetContextErrorHandler(IntPtr handle, ErrorHandler handler)
        {
            IntPtr fn = (handler is null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(handler);
            SetLogErrorHandlerTHR_Internal(handle, fn);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetAlarmCodesTHR")]
        private static partial void GetAlarmCodesTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] [Out] ushort[] alarmCodes);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetAlarmCodesTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetAlarmCodesTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] [Out] ushort[] alarmCodes);
#endif

        internal static void GetAlarmCodesTHR(IntPtr handle, ushort[] alarmCodes)
        {
            GetAlarmCodesTHR_Internal(handle, alarmCodes);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetAlarmCodesTHR")]
        private static partial void SetAlarmCodesTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] [In] ushort[] alarmCodes);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetAlarmCodesTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetAlarmCodesTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] [In] ushort[] alarmCodes);
#endif

        internal static void SetAlarmCodesTHR(IntPtr handle, ushort[] alarmCodes)
        {
            SetAlarmCodesTHR_Internal(handle, alarmCodes);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsSetAdaptationStateTHR")]
        private static partial double SetAdaptationStateTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double adaptationState);
#else
        [DllImport(Liblcms, EntryPoint = "cmsSetAdaptationStateTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern double SetAdaptationStateTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double adaptationState);
#endif

        internal static double SetAdaptationStateTHR(IntPtr handle, double adaptationState)
        {
            return SetAdaptationStateTHR_Internal(handle, adaptationState);
        }

#if NET7_0_OR_GREATER
        [LibraryImport(Liblcms, EntryPoint = "cmsGetSupportedIntentsTHR")]
        private static partial uint GetSupportedIntents_InternalTHR(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nMax,
                IntPtr Codes,
                IntPtr Descriptions);
#else
        [DllImport(Liblcms, EntryPoint = "cmsGetSupportedIntentsTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetSupportedIntents_InternalTHR(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nMax,
                IntPtr Codes,
                IntPtr Descriptions);
#endif

        internal static IEnumerable<(uint code, string description)> GetSupportedIntentsTHR(IntPtr handle)
        {
            // get intent count first
            uint count = GetSupportedIntents_InternalTHR(handle, 0, IntPtr.Zero, IntPtr.Zero);

            List<(uint code, string description)> result = [];

            unsafe
            {
                IntPtr codesPtr = Marshal.AllocHGlobal((int)(count * sizeof(uint)));
                IntPtr descriptionsPtr = Marshal.AllocHGlobal((int)(count * sizeof(IntPtr)));

                count = GetSupportedIntents_InternalTHR(handle, count, codesPtr, descriptionsPtr);

                uint* codes = (uint*)codesPtr.ToPointer();
                IntPtr* descriptions = (IntPtr*)descriptionsPtr.ToPointer();
                for (var i = 0; i < count; i++)
                {
                    uint code = codes[i];
                    string description = Marshal.PtrToStringAnsi(descriptions[i]);
                    result.Add((code, description));
                }

                Marshal.FreeHGlobal(descriptionsPtr);
                Marshal.FreeHGlobal(codesPtr);
            }

            return result;
        }
    }
}
