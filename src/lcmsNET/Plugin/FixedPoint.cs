// Copyright(c) 2019-2022 John Stevenson-Hoare
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

namespace lcmsNET.Plugin
{
    /// <summary>
    /// Provides methods to convert to and from fixed point representations.
    /// </summary>
    public static class FixedPoint
    {
        /// <summary>
        /// Converts from an 8.8 fixed point representation to a double-precision floating point number.
        /// </summary>
        /// <param name="fixed8">An 8.8 encoded fixed point value.</param>
        /// <returns>The equivalent double-precision floating point number.</returns>
        public static double ToDouble(ushort fixed8)
        {
            return Interop.Fixed8Dot8ToDouble(fixed8);
        }

        /// <summary>
        /// Converts from a double-precision floating point number to an 8.8 fixed point representation.
        /// </summary>
        /// <param name="d">The value to be converted.</param>
        /// <returns>The equivalent, rounded 8.8 fixed point representation.</returns>
        public static ushort ToFixed8Dot8(double d)
        {
            return Interop.DoubleToFixed8Dot8(d);
        }

        /// <summary>
        /// Converts from a signed 15.16 fixed point representation to a double-precision floating point number.
        /// </summary>
        /// <param name="fixed32">A signed 15.16 encoded fixed point value.</param>
        /// <returns>The equivalent double-precision floating point number.</returns>
        public static double ToDouble(int fixed32)
        {
            return Interop.Fixed15Dot16ToDouble(fixed32);
        }

        /// <summary>
        /// Converts from a double-precision floating point number to a signed 15.16 fixed point representation.
        /// </summary>
        /// <param name="d">The value to be converted.</param>
        /// <returns>The equivalent, rounded signed 15.16 fixed point representation.</returns>
        public static int ToFixed15Dot16(double d)
        {
            return Interop.DoubleToFixed15Dot16(d);
        }
    }
}
