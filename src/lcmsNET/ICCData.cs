using lcmsNET.Impl;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace lcmsNET
{
    public class ICCData
    {
        public const uint ASCII = 0;
        public const uint Binary = 1;

        public uint Flag { get; }

        public byte[] Data { get; }

        public ICCData(string s)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));

            Flag = ASCII;
            Data = Encoding.ASCII.GetBytes(s);
        }

        public ICCData(byte[] bytes)
        {
            if (bytes is null) throw new ArgumentNullException(nameof(bytes));

            Flag = Binary;
            Data = bytes;
        }

        public static explicit operator string(ICCData iccData)
        {
            if (iccData.Flag != ASCII) throw new InvalidCastException("Data is not ASCII.");
            return Helper.ToString(iccData.Data);
        }

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
