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
    public class ICCDataTest
    {
        [TestMethod()]
        public void Constructor_WhenStringParameter_ShouldSetAsASCII()
        {
            // Arrange
            uint expected = ICCData.ASCII;

            // Act
            var sut = new ICCData("set as ascii");
            var actual = sut.Flag;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Constructor_WhenStringParameterIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            string s = null;

            // Act & Assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new ICCData(s));
        }

        [TestMethod()]
        public void Constructor_WhenByeArrayParameter_ShouldSetAsBinary()
        {
            // Arrange
            uint expected = ICCData.Binary;

            // Act
            var target = new ICCData([12, 96, 14, 2]);

            // Assert
            var actual = target.Flag;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Constructor_WhenByteArrayParameterIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            byte[] bytes = null;

            // Act & Assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new ICCData(bytes));
        }

        [TestMethod()]
        public void ExplicitStringOperator_WhenASCII_ShouldConvertToString()
        {
            // Arrange
            string expected = "string operator";
            var sut = new ICCData(expected);

            // Act
            var actual = (string)sut;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ExplicitStringOperator_WhenNotASCII_ShouldThrowInvalidCastException()
        {
            // Arrange
            var sut = new ICCData([12, 96, 14, 2]);

            // Act & Assert
            var actual = Assert.ThrowsException<InvalidCastException>(() => (string)sut);
        }

        [TestMethod()]
        public void ExplicitByteArrayOperator_WhenBinary_ShouldConvertToByteArray()
        {
            // Arrange
            byte[] expected = [12, 96, 14, 2];
            var sut = new ICCData(expected);

            // Act
            byte[] actual = (byte[])sut;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ExplicitByteArrayOperator_WhenNotBinary_ShouldThrowInvalidCastException()
        {
            // Arrange
            var target = new ICCData("not binary");

            // Act & Assert
            var actual = Assert.ThrowsException<InvalidCastException>(() => (byte[])target);
        }
    }
}
