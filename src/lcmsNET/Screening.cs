using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    public enum SpotShape : uint
    {
        Unknown = 0,
        PrinterDefault = 1,
        Round = 2,
        Diamond = 3,
        Ellipse = 4,
        Line = 5,
        Square = 6,
        Cross = 7
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ScreeningChannel
    {
        [MarshalAs(UnmanagedType.R8)]
        public double Frequency;
        [MarshalAs(UnmanagedType.R8)]
        public double ScreenAngle;
        [MarshalAs(UnmanagedType.U4)]
        public SpotShape SpotShape;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Screening
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint Flag;
        [MarshalAs(UnmanagedType.U4)]
        public uint nChannels;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = MAX_CHANNELS)]
        public ScreeningChannel[] Channels;

        public static Screening FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<Screening>(handle);
        }

        public Screening(uint flag, uint nchannels, ScreeningChannel[] channels)
        {
            if (channels?.Length != MAX_CHANNELS) throw new ArgumentException($"'{nameof(channels)}' array size must equal {MAX_CHANNELS}.");

            Flag = flag;
            nChannels = nchannels;
            Channels = channels;
        }

        private const int MAX_CHANNELS = 16;
    }
}
