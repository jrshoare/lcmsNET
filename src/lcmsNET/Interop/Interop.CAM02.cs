using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Init", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CIECAM02Init_Internal(
                IntPtr contextID,
                ref ViewingConditions conditions);

        internal static IntPtr CIECAM02Init(IntPtr contextID, ViewingConditions conditions)
        {
            return CIECAM02Init_Internal(contextID, ref conditions);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Done", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Done_Internal(
                IntPtr model);

        internal static void CIECAM02Done(IntPtr model)
        {
            CIECAM02Done_Internal(model);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Forward", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Forward_Internal(
                IntPtr model,
                ref CIEXYZ xyz,
                ref JCh jch);

        internal static void CIECAM02Forward(IntPtr model, CIEXYZ xyz, ref JCh jch)
        {
            CIECAM02Forward_Internal(model, ref xyz, ref jch);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Reverse", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Reverse_Internal(
                IntPtr model,
                ref JCh jch,
                ref CIEXYZ xyz);

        internal static void CIECAM02Reverse(IntPtr model, JCh jch, ref CIEXYZ xyz)
        {
            CIECAM02Reverse_Internal(model, ref jch, ref xyz);
        }
    }
}
