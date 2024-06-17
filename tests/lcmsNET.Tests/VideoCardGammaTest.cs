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
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class VideoCardGammaTest
    {
        [TestMethod()]
        public void VideoCardGamma_WhenInstantiated_ShouldSetProperties()
        {
            // Arrange
            int type = 5;
            double[] parameters = [0.45, Math.Pow(1.099, 1.0 / 0.45), 0.0, 4.5, 0.018, -0.099, 0.0];

            using var expectedRed = ToneCurve.BuildParametric(null, type, parameters);
            using var expectedGreen = ToneCurve.BuildParametric(null, type, parameters);
            using var expectedBlue = ToneCurve.BuildParametric(null, type, parameters);

            // Act
            var target = new VideoCardGamma(expectedRed, expectedGreen, expectedBlue);
            var actualRed = target.Red;
            var actualGreen = target.Green;
            var actualBlue = target.Blue;

            // Assert
            Assert.AreSame(expectedRed, actualRed);
            Assert.AreSame(expectedGreen, actualGreen);
            Assert.AreSame(expectedBlue, actualBlue);
        }
    }
}
