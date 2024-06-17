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
    public class ColorantOrderTest
    {
        [TestMethod()]
        public void Constructor_WhenInstantiated_ShouldHaveValueOfSuppliedByteArray()
        {
            // Arrange
            byte[] expected = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

            // Act
            var target = new ColorantOrder(expected);

            // Assert
            var actual = (byte[])target;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Constructor_WhenNullByteArray_ShouldThrowArgumentException()
        {
            // Arrange
            byte[] expected = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new ColorantOrder(expected));
        }

        [TestMethod()]
        public void Constructor_WhenIncorrectByteArrayLength_ShouldThrowArgumentException()
        {
            // Arrange
            const int invalidLength = 13;
            byte[] expected = new byte[invalidLength];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new ColorantOrder(expected));
        }

        [TestMethod()]
        public void ImplicitByteArrayOperator_WhenInvoked_ShouldReturnByteArrayUsedToInstantiate()
        {
            // Arrange
            byte[] expected = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

            // Act
            var target = new ColorantOrder(expected);

            // Assert
            byte[] actual = target;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ExplicitColorantOrderOperator_WhenInvoked_ShouldInstantiateFromByteArray()
        {
            // Arrange
            byte[] expected = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

            // Act
            var target = (ColorantOrder)expected;

            // Assert
            byte[] actual = target;
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
