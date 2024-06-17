// Copyright(c) 2019-2024 John Stevenson-Hoare
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class CIExyYTest
    {
        [TestMethod()]
        public void D50_WhenGetting_ShouldHaveCorrectValue()
        {
            // Arrange

            // Act
            var d50 = CIExyY.D50;

            // Assert
            Assert.AreEqual(0.3457, d50.x, 0.0001);
            Assert.AreEqual(0.3585, d50.y, 0.0001);
            Assert.AreEqual(1.0, d50.Y, double.Epsilon);
        }

        [TestMethod()]
        public void ImplicitCIEXYZOperator_WhenInvoked_ShouldConvertToCIEXYZ()
        {
            // Arrange
            CIExyY xyY = Colorimetric.D50_xyY;
            CIEXYZ expected = Colorimetric.xyY2XYZ(xyY);

            // Act
            CIEXYZ actual = xyY;

            // Assert
            Assert.AreEqual(expected.X, actual.X, double.Epsilon);
            Assert.AreEqual(expected.Y, actual.Y, double.Epsilon);
            Assert.AreEqual(expected.Z, actual.Z, double.Epsilon);
        }
    }
}
