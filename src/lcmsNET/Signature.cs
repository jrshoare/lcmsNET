using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Signature
    {
        [MarshalAs(UnmanagedType.U4)]
        private readonly uint _;

        public Signature(uint u)
        {
            _ = u;
        }

        public static implicit operator uint(Signature signature) => signature._;

        public static explicit operator Signature(uint u) => new Signature(u);

        public static Signature FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<Signature>(handle);
        }
    }
}
