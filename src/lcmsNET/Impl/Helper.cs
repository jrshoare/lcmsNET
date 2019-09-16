using System;
using System.Text;

namespace lcmsNET.Impl
{
    internal static class Helper
    {
        public static void CheckCreated<T>(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                throw new LcmsNETException($"Failed to create {typeof(T)}.");
            }
        }

        public static byte[] ToASCIIBytes(string code)
        {
            byte[] bytes = new byte[3] { 0, 0, 0 };
            Encoding.ASCII.GetBytes(code, 0, code.Length, bytes, 0);
            return bytes;
        }

        public static string ToString(byte[] asciiBytes)
        {
            Encoding ascii = Encoding.ASCII;
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            return new string(asciiChars);
        }
    }
}
