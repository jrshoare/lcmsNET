// Copyright(c) 2019-2021 John Stevenson-Hoare
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Defines spot shapes.
    /// </summary>
    public enum SpotShape : uint
    {
        /// <summary>
        /// Unknown spot shape.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Printer default spot shape.
        /// </summary>
        PrinterDefault = 1,
        /// <summary>
        /// Round spot shape.
        /// </summary>
        Round = 2,
        /// <summary>
        /// Diamond spot shape.
        /// </summary>
        Diamond = 3,
        /// <summary>
        /// Elliptical spot shape.
        /// </summary>
        Ellipse = 4,
        /// <summary>
        /// Line spot shape.
        /// </summary>
        Line = 5,
        /// <summary>
        /// Square spot shape.
        /// </summary>
        Square = 6,
        /// <summary>
        /// Cross spot shape.
        /// </summary>
        Cross = 7
    }

    /// <summary>
    /// Represents a screening channel.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ScreeningChannel
    {
        /// <summary>
        /// Frequency.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double Frequency;
        /// <summary>
        /// Screen angle.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double ScreenAngle;
        /// <summary>
        /// Spot shape.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public SpotShape SpotShape;
    }

    /// <summary>
    /// Defines screening flags.
    /// </summary>
    [Flags]
    public enum ScreeningFlags : uint
    {
        /// <summary>
        /// Frequency unit is lines per centimetre.
        /// </summary>
        FrequencyUnitLinesCm = 0x0000,
        /// <summary>
        /// Printer default screens.
        /// </summary>
        PrinterDefaultScreens = 0x0001,
        /// <summary>
        /// Frequency unit is lines per inch.
        /// </summary>
        FrequencyUnitLinesInch = 0x0002
    }

    /// <summary>
    /// Represents screening information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Screening
    {
        /// <summary>
        /// Screening flags.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public ScreeningFlags Flag;
        /// <summary>
        /// Number of screening channels defined for use.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public uint nChannels;
        /// <summary>
        /// Screening channels.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = MAX_CHANNELS)]
        public ScreeningChannel[] Channels;

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="Screening"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="Screening"/> instance.</returns>
        public static Screening FromHandle(IntPtr handle)
        {
            return Marshal.PtrToStructure<Screening>(handle);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Screening"/> class.
        /// </summary>
        /// <param name="flag">The screening flags.</param>
        /// <param name="nchannels">The number of screening channels defined for use.</param>
        /// <param name="channels">An array of 16 screening channels.</param>
        public Screening(ScreeningFlags flag, uint nchannels, ScreeningChannel[] channels)
        {
            if (channels?.Length != MAX_CHANNELS) throw new ArgumentException($"'{nameof(channels)}' array size must equal {MAX_CHANNELS}.");

            Flag = flag;
            nChannels = nchannels;
            Channels = channels;
        }

        private const int MAX_CHANNELS = 16;
    }
}
