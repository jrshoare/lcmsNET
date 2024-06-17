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
    public class SignatureTest
    {
        [TestMethod()]
        public void Signature_WhenInstantiated_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x237F9AB1;

            // Act
            var sut = new Signature(expected);

            // Assert
            uint actual = sut;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ImplicitUIntOperator_WhenInvokedShouldConvertToUInt()
        {
            // Arrange
            uint expected = 0x12345982;

            // Act
            var target = (Signature)expected;

            // Assert
            uint actual = target;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ExplicitSignatureOperator_WhenInvoked_ShouldConvertToSignature()
        {
            // Arrange
            uint expected = 0x75DF3108;

            // Act
            var target = (Signature)expected;
            uint actual = target;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
