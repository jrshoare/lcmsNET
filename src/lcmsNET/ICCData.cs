using lcmsNET.Impl;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace lcmsNET
{
    /// <summary>
    /// Represents a data structure that contains either 7-bit ASCII or binary data.
    /// </summary>
    public class ICCData
    {
        /// <summary>
        /// 7-bit ASCII data type.
        /// </summary>
        public const uint ASCII = 0;
        /// <summary>
        /// Binary (transparent 8-bit bytes) data type.
        /// </summary>
        public const uint Binary = 1;

        /// <summary>
        /// Gets the data type contained by this instance.
        /// </summary>
        public uint Flag { get; }

        /// <summary>
        /// Gets the raw data contained by this instance.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ICCData"/> class from the
        /// specified <see cref="string"/>.
        /// </summary>
        /// <param name="s">A string.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Sets the data type to <see cref="ASCII"/>.
        /// </para>
        /// <para>
        /// Non-ASCII characters are replaced with the value 63 which is the ASCII
        /// character code for '?'.
        /// </para>
        /// </remarks>
        public ICCData(string s)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));

            Flag = ASCII;
            Data = Encoding.ASCII.GetBytes(s);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ICCData"/> class from the
        /// specified byte array.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="bytes"/> is null.
        /// </exception>
        /// <remarks>
        /// Sets the data type to <see cref="Binary"/>.
        /// </remarks>
        public ICCData(byte[] bytes)
        {
            if (bytes is null) throw new ArgumentNullException(nameof(bytes));

            Flag = Binary;
            Data = bytes;
        }

        /// <summary>
        /// Explicitly converts an <see cref="ICCData"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="iccData">The <see cref="ICCData"/> to be converted.</param>
        /// <exception cref="InvalidCastException">
        /// <paramref name="iccData"/> does not have a data type of <see cref="ASCII"/>.
        /// </exception>
        public static explicit operator string(ICCData iccData)
        {
            if (iccData.Flag != ASCII) throw new InvalidCastException("Data is not ASCII.");
            return Helper.ToString(iccData.Data);
        }

        /// <summary>
        /// Marshals data from an unmanaged block of memory to a newly allocated <see cref="ICCData"/> object.
        /// </summary>
        /// <param name="handle">A handle to the unmanaged block of memory.</param>
        /// <returns>A new <see cref="ICCData"/> instance.</returns>
        public static ICCData FromHandle(IntPtr handle)
        {
            IntPtr ptr = handle;
            uint len = (uint)Marshal.ReadInt32(ptr); // len
            ptr += sizeof(uint);
            uint flag = (uint)Marshal.ReadInt32(ptr); // flag
            ptr += sizeof(uint);
            byte[] data = new byte[len]; // data[]
            int index = 0;
            while (len-- > 0)
            {
                data[index++] = Marshal.ReadByte(ptr);
                ptr += sizeof(byte);
            }

            if (flag == ASCII)
                return new ICCData(Helper.ToString(data).TrimEnd(new char[] { '\0' })); // remove 00h terminator byte
            else if (flag == Binary)
                return new ICCData(data);
            else
                throw new ArgumentException($"Value must be either {ASCII} or {Binary}'.", nameof(flag));
        }

        internal IntPtr ToHandle()
        {
            int cb = sizeof(uint) + sizeof(uint) + Data.Length;
            int nulLen = (Flag == ASCII) ? 1 : 0;
            IntPtr handle = Marshal.AllocHGlobal(cb + nulLen);

            IntPtr ptr = handle;
            int len = Data.Length;
            Marshal.WriteInt32(ptr, len + nulLen); // len
            ptr += sizeof(uint);
            Marshal.WriteInt32(ptr, (int)Flag); // flag
            ptr += sizeof(uint);
            int index = 0;
            while (len-- > 0)
            {
                Marshal.WriteByte(ptr, Data[index++]); // data[]
                ptr += sizeof(byte);
            }
            if (nulLen != 0) Marshal.WriteByte(ptr, 0); // 00h terminator byte

            return handle;
        }
    }
}
