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

using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class UcrBgTest
    {
        [TestMethod()]
        public void UcrBg_WhenInstantiated_ShouldSetProperties()
        {
            // Arrange
            double[] parameters = [2.4, 1.0 / 1.055, 0.055 / 1.055, 1.0 / 12.92, 0.04045];
            MultiLocalizedUnicodeUtils.DisplayName displayName = new("ucrbg");

            using var expectedUcr = ToneCurve.BuildParametric(context: null, type: 4, parameters);
            using var expectedBg = ToneCurve.BuildGamma(context: null, gamma: 2.2);
            using var expectedDesc = MultiLocalizedUnicodeUtils.CreateAsASCII(displayName);

            // Act
            var sut = new UcrBg(expectedUcr, expectedBg, expectedDesc);
            var actualUcr = sut.Ucr;
            var actualBg = sut.Bg;
            var actualDesc = sut.Desc;

            // Assert
            Assert.AreSame(expectedUcr, actualUcr);
            Assert.AreSame(expectedBg, actualBg);
            Assert.AreSame(expectedDesc, actualDesc);
        }
    }
}
