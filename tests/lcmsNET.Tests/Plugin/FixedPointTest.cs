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

using lcmsNET.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class FixedPointTest
    {
        [TestMethod]
        public void ToDoubleFrom8Dot8_WhenFixed8Dot8_ShouldSucceed()
        {
            // Arrange
            double expected = 1.0;
            ushort value = 0x0100;

            // Act
            double actual = FixedPoint.ToDouble(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToFixed8Dot8_WhenInRange_ShouldSucceed()
        {
            // Arrange
            ushort expected = 0xffff;
            double value = 255.0 + (255.0 / 256.0);

            // Act
            ushort actual = FixedPoint.ToFixed8Dot8(value);

            // Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToDouble_WhenSigned15Dot16_ShouldSucceed()
        {
            // Arrange
            double expected = 1.0;
            int value = 0x0001_0000;

            // Act
            double actual = FixedPoint.ToDouble(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToFixed15Dot16_WhenInRange_ShouldSucceed()
        {
            // Arrange
            int expected = 0x7fff_ffff;
            double value = 32767.0 + (65535.0 / 65536.0);

            // Act
            int actual = FixedPoint.ToFixed15Dot16(value);

            // Arrange
            Assert.AreEqual(expected, actual);
        }
    }
}
