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
    public class CIELabTest
    {
        [TestMethod()]
        public void ToXYZ_WhenInvoked_ShouldConvertToXYZ()
        {
            // Arrange
            CIEXYZ whitePoint = new() { X = 0.9642, Y = 1.0, Z = 0.8249 }; // D50 XYZ normalized to Y = 1.0
            CIELab lab = new() { L = 100.0, a = 0.0, b = 0.0 };

            // Act
            CIEXYZ xyz = lab.ToXYZ(whitePoint);

            // Assert
            Assert.AreEqual(0.9642, xyz.X);
            Assert.AreEqual(1.0, xyz.Y);
            Assert.AreEqual(0.8249, xyz.Z);
        }

        [TestMethod()]
        public void ImplicitCIELChOperator_WhenInvoked_ShouldConvertToCIELCh()
        {
            // Arrange
            CIELab lab = new() { L = 100.0, a = 0.0, b = 0.0 };

            // Act
            CIELCh lch = lab;

            // Assert
            Assert.AreEqual(100.0, lch.L);
            Assert.AreEqual(0.0, lch.C);
            Assert.AreEqual(0.0, lch.h);
        }
    }
}
