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
        [DllImport(Liblcms, EntryPoint = "cmsCreateContext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CreateContext_Internal(
                IntPtr plugin,
                IntPtr userData);

        internal static IntPtr CreateContext(IntPtr plugin, IntPtr userData)
        {
            return CreateContext_Internal(plugin, userData);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDeleteContext", CallingConvention = CallingConvention.StdCall)]
        private static extern void DeleteContext_Internal(
            IntPtr handle);

        internal static void DeleteContext(IntPtr handle)
        {
            DeleteContext_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsDupContext", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr DuplicateContext_Internal(
            IntPtr handle,
            IntPtr userData);

        internal static IntPtr DuplicateContext(IntPtr handle, IntPtr userData)
        {
            return DuplicateContext_Internal(handle, userData);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetContextUserData", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetContextUserData_Internal(
            IntPtr handle);

        internal static IntPtr GetContextUserData(IntPtr handle)
        {
            return GetContextUserData_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsPluginTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern int PluginTHR_Internal(
            IntPtr handle,
            IntPtr plugin);

        internal static int RegisterContextPlugins(IntPtr handle, IntPtr plugin)
        {
            return PluginTHR_Internal(handle, plugin);
        }

        [DllImport(Liblcms, EntryPoint = "cmsUnregisterPluginsTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern int UnregisterPluginsTHR_Internal(
            IntPtr handle);

        internal static void UnregisterContextPlugins(IntPtr handle)
        {
            UnregisterPluginsTHR_Internal(handle);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetLogErrorHandlerTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetLogErrorHandlerTHR_Internal(
                IntPtr handle,
                IntPtr fn);

        internal static void SetContextErrorHandler(IntPtr handle, ErrorHandler handler)
        {
            IntPtr fn = (handler is null) ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(handler);
            SetLogErrorHandlerTHR_Internal(handle, fn);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetAlarmCodesTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern void GetAlarmCodesTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);

        internal static void GetAlarmCodesTHR(IntPtr handle, ushort[] alarmCodes)
        {
            GetAlarmCodesTHR_Internal(handle, alarmCodes);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetAlarmCodesTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetAlarmCodesTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 16)] ushort[] alarmCodes);

        internal static void SetAlarmCodesTHR(IntPtr handle, ushort[] alarmCodes)
        {
            SetAlarmCodesTHR_Internal(handle, alarmCodes);
        }

        [DllImport(Liblcms, EntryPoint = "cmsSetAdaptationStateTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern double SetAdaptationStateTHR_Internal(
                IntPtr handle,
                [MarshalAs(UnmanagedType.R8)] double adaptationState);

        internal static double SetAdaptationStateTHR(IntPtr handle, double adaptationState)
        {
            return SetAdaptationStateTHR_Internal(handle, adaptationState);
        }

        [DllImport(Liblcms, EntryPoint = "cmsGetSupportedIntentsTHR", CallingConvention = CallingConvention.StdCall)]
        private static extern uint GetSupportedIntents_InternalTHR(
                IntPtr handle,
                [MarshalAs(UnmanagedType.U4)] uint nMax,
                IntPtr Codes,
                IntPtr Descriptions);

        internal static IEnumerable<(uint code, string description)> GetSupportedIntentsTHR(IntPtr handle)
        {
            // get intent count first
            uint count = GetSupportedIntents_InternalTHR(handle, 0, IntPtr.Zero, IntPtr.Zero);

            List<(uint code, string description)> result = new List<(uint code, string description)>();

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
