using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorantOrder
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        private readonly byte[] _;

        public ColorantOrder(byte[] bytes)
        {
            if (bytes?.Length != 16) throw new ArgumentException($"'{nameof(bytes)}' array size must equal 16.");
            _ = bytes;
        }

        public static implicit operator byte[](ColorantOrder colorantOrder) => colorantOrder._;

        public static explicit operator ColorantOrder(byte[] bytes) => new ColorantOrder(bytes);

        public static ColorantOrder FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<ColorantOrder>(handle);
        }
    }
}
