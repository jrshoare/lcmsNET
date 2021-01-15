using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UcrBg
    {
        public IntPtr Ucr;
        public IntPtr Bg;
        public IntPtr Desc;

        public static UcrBg FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<UcrBg>(handle);
        }

        public UcrBg(ToneCurve ucr, ToneCurve bg, MultiLocalizedUnicode desc)
        {
            Ucr = ucr.Handle;
            Bg = bg.Handle;
            Desc = desc.Handle;
        }
    }
}
