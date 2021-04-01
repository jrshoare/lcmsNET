using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Represents the laydown order that colorants will be printed on an n-colorant device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorantOrder
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        private readonly byte[] _;

        /// <summary>
        /// Initialises a new instance of the <see cref="ColorantOrder"/> class.
        /// </summary>
        /// <param name="bytes">An array of 16 values.</param>
        public ColorantOrder(byte[] bytes)
        {
            if (bytes?.Length != 16) throw new ArgumentException($"'{nameof(bytes)}' array size must equal 16.");
            _ = bytes;
        }

        /// <summary>
        /// Implicitly converts a <see cref="ColorantOrder"/> to a <see cref="byte"/> array.
        /// </summary>
        /// <param name="colorantOrder">The <see cref="ColorantOrder"/> to be converted.</param>
        public static implicit operator byte[](ColorantOrder colorantOrder) => colorantOrder._;

        /// <summary>
        /// Explicitly converts a <see cref="byte"/> array of 16 values to a <see cref="ColorantOrder"/>.
        /// </summary>
        /// <param name="bytes">The byte array to be converted.</param>
        public static explicit operator ColorantOrder(byte[] bytes) => new ColorantOrder(bytes);

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="ColorantOrder"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="ColorantOrder"/> instance.</returns>
        public static ColorantOrder FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<ColorantOrder>(handle);
        }
    }
}
