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
    public class CIEXYZTest
    {
        [TestMethod()]
        public void D50_WhenGetting_ShouldHaveCorrectValue()
        {
            // Act
            var d50 = CIEXYZ.D50;

            // Assert
            Assert.AreEqual(0.9642, d50.X, double.Epsilon);
            Assert.AreEqual(1.0, d50.Y, double.Epsilon);
            Assert.AreEqual(0.8249, d50.Z, double.Epsilon);
        }

        [TestMethod()]
        public void ToLab_WhenInvoked_ShouldConvertToLab()
        {
            // Arrange
            CIEXYZ whitePoint = new() { X = 0.9642, Y = 1.0, Z = 0.8249 }; // D50 XYZ normalized to Y = 1.0
            CIEXYZ xyz = new() { X = 0.9642, Y = 1.0, Z = 0.8249 };

            // Act
            CIELab lab = xyz.ToLab(whitePoint);

            // Assert
            Assert.AreEqual(100.0, lab.L);
            Assert.AreEqual(0.0, lab.a);
            Assert.AreEqual(0.0, lab.b);
        }

        [TestMethod()]
        public void ImplicitCIExyYOperator_WhenInvoked_ShouldConvertToCIExyY()
        {
            // Arrange
            CIEXYZ xyz = new() { X = 0.9642, Y = 1.0, Z = 0.8249 };
            CIExyY expected = Colorimetric.XYZ2xyY(xyz);

            // Act
            CIExyY actual = xyz;

            // Assert
            Assert.AreEqual(expected.x, actual.x, double.Epsilon);
            Assert.AreEqual(expected.y, actual.y, double.Epsilon);
            Assert.AreEqual(expected.Y, actual.Y, double.Epsilon);
        }
    }
}
