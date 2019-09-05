using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    internal static partial class Interop
    {
        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Init", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr CIECAM02Init_Internal(
                IntPtr contextID,
                in ViewingConditions conditions);

        internal static IntPtr CIECAM02Init(IntPtr contextID, in ViewingConditions conditions)
        {
            return CIECAM02Init_Internal(contextID, conditions);
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
                in CIEXYZ xyz,
                out JCh jch);

        internal static void CIECAM02Forward(IntPtr model, in CIEXYZ xyz, out JCh jch)
        {
            CIECAM02Forward_Internal(model, xyz, out jch);
        }

        [DllImport(Liblcms, EntryPoint = "cmsCIECAM02Reverse", CallingConvention = CallingConvention.StdCall)]
        private static extern void CIECAM02Reverse_Internal(
                IntPtr model,
                in JCh jch,
                out CIEXYZ xyz);

        internal static void CIECAM02Reverse(IntPtr model, in JCh jch, out CIEXYZ xyz)
        {
            CIECAM02Reverse_Internal(model, jch, out xyz);
        }
    }
}
