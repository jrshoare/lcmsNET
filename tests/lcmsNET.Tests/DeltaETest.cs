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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class DeltaETest
    {
        [TestMethod()]
        public void DE76_WhenInvoked_ShouldComputeDifferenceUsingCIE76Method()
        {
            // Arrange
            CIELab lab1 = new() { L = 100.0, a = 0.0, b = 0.0 };
            CIELab lab2 = new() { L = 87.5, a = 3.21, b = -16.7 };

            // Act
            _ = DeltaE.DE76(lab1, lab2);
        }

        [TestMethod()]
        public void CMC_WhenInvoked_ShouldComputeDifferenceUsingCMCMethod()
        {
            // Arrange
            CIELab lab1 = new() { L = 100.0, a = 0.0, b = 0.0 };
            CIELab lab2 = new() { L = 87.5, a = 3.21, b = -16.7 };
            double l = 2.0;
            double c = 1.0;

            // Act
            _ = DeltaE.CMC(lab1, lab2, l, c);
        }

        [TestMethod()]
        public void CIEDE2000_WhenInvoked_ShouldComputeDifferenceUsingCIEDE2000Method()
        {
            // Arrange
            CIELab lab1 = new() { L = 100.0, a = 0.0, b = 0.0 };
            CIELab lab2 = new() { L = 87.5, a = 3.21, b = -16.7 };

            // Act
            _ = DeltaE.CIEDE2000(lab1, lab2);
        }

        [TestMethod()]
        public void BFD_WhenInvoked_ShouldComputeDifferenceUsingBFDMethod()
        {
            // Arrange
            CIELab lab1 = new() { L = 100.0, a = 0.0, b = 0.0 };
            CIELab lab2 = new() { L = 87.5, a = 3.21, b = -16.7 };

            // Act
            _ = DeltaE.BFD(lab1, lab2);
        }

        [TestMethod()]
        public void CIE94_WhenInvoked_ShouldComputeDifferenceUsingCIE94Method()
        {
            // Arrange
            CIELab lab1 = new() { L = 100.0, a = 0.0, b = 0.0 };
            CIELab lab2 = new() { L = 87.5, a = 3.21, b = -16.7 };

            // Act
            _ = DeltaE.CIE94(lab1, lab2);
        }
    }
}
