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

using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Represents an ICC date and time.
    /// </summary>
    /// <remarks>
    /// The data should be encoded as big-endian.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct DateTimeNumber
    {
        /// <summary>
        /// Number of the year (actual year, e.g. 1994)
        /// </summary>
        public ushort year;
        /// <summary>
        /// Number of the month (1 to 12)
        /// </summary>
        public ushort month;
        /// <summary>
        /// Number of the day of the month (1 to 31)
        /// </summary>
        public ushort day;
        /// <summary>
        /// Number of hours (0 to 23)
        /// </summary>
        public ushort hours;
        /// <summary>
        /// Number of minutes (0 to 59)
        /// </summary>
        public ushort minutes;
        /// <summary>
        /// Number of seconds (0 to 59)
        /// </summary>
        public ushort seconds;
    }
}
