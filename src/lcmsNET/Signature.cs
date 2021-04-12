using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Represents a signature.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Signature
    {
        [MarshalAs(UnmanagedType.U4)]
        private readonly uint _;

        /// <summary>
        /// Initialises a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="u">The signature value.</param>
        public Signature(uint u)
        {
            _ = u;
        }

        /// <summary>
        /// Implicitly converts a <see cref="Signature"/> to an unsigned integer.
        /// </summary>
        /// <param name="signature">The <see cref="Signature"/> to be converted.</param>
        public static implicit operator uint(Signature signature) => signature._;

        /// <summary>
        /// Explicitly converts an unsigned integer to a <see cref="Signature"/>.
        /// </summary>
        /// <param name="u">The unsigned integer to be converted.</param>
        public static explicit operator Signature(uint u) => new Signature(u);

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="Signature"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="Signature"/> instance.</returns>
        public static Signature FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<Signature>(handle);
        }
    }
}
