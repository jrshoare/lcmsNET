using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Represents an under color removal and black generation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct UcrBg
    {
        /// <summary>
        /// Pointer to under color removal tone curve.
        /// </summary>
        public IntPtr Ucr;
        /// <summary>
        /// Pointer to black generation tone curve.
        /// </summary>
        public IntPtr Bg;
        /// <summary>
        /// Pointer to multi-localized Unicode description.
        /// </summary>
        public IntPtr Desc;

        /// <summary>
        /// Creates an under color removal and black generation from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing under color removal and black generation.</param>
        /// <returns>A new <see cref="UcrBg"/> instance referencing an existing under color removal and black generation.</returns>
        public static UcrBg FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<UcrBg>(handle);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UcrBg"/> class.
        /// </summary>
        /// <param name="ucr">A tone curve for under color removal.</param>
        /// <param name="bg">A tone curve for black generation.</param>
        /// <param name="desc">A description for the under color removal and black generation.</param>
        public UcrBg(ToneCurve ucr, ToneCurve bg, MultiLocalizedUnicode desc)
        {
            Ucr = ucr.Handle;
            Bg = bg.Handle;
            Desc = desc.Handle;
        }
    }
}
