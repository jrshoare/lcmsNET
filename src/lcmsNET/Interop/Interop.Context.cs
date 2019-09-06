using System;
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
            ErrorHandler handler);

        internal static void SetContextErrorHandler(IntPtr handle, ErrorHandler handler)
        {
            SetLogErrorHandlerTHR_Internal(handle, handler);
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
    }
}
