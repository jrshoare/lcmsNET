using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Impl
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct PSeqDesc
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint deviceMfg;
        [MarshalAs(UnmanagedType.U4)]
        public uint deviceModel;
        [MarshalAs(UnmanagedType.U8)]
        public ulong attributes;
        [MarshalAs(UnmanagedType.U4)]
        public TechnologySignature technology;
        public fixed byte ProfileID[16];
        public IntPtr Manufacturer; // cmsMLU*
        public IntPtr Model;        // cmsMLU*
        public IntPtr Description;  // cmsMLU*
    }
}
