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
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ProfileSequenceDescriptorTest
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = ProfileSequenceDescriptor.Create(context: null, nItems: 3);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = ProfileSequenceDescriptor.Create(expected, nItems: 3);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = ProfileSequenceDescriptor.Create(context: null, nItems: 3);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Duplicate_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ProfileSequenceDescriptor.Create(context: null, nItems: 3);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Duplicate());
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var sut = ProfileSequenceDescriptor.Create(context: null, nItems: 1);

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void Length_WhenInvoked_ShouldGetNumberOfProfilesInSequence()
        {
            // Arrange
            uint expected = 7;
            using var sut = ProfileSequenceDescriptor.Create(context: null, expected);

            // Act
            uint actual = sut.Length;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Indexer_WhenInvoked_ShouldGetItemAtIndex()
        {
            // Arrange
            using var sut = ProfileSequenceDescriptor.Create(context: null, nItems: 4);

            // Act
            ProfileSequenceItem item = sut[2];

            // Assert
            Assert.IsNotNull(item);
        }
    }
}
