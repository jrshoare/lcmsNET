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
using System.Linq;
using static lcmsNET.Tests.TestUtils.MultiLocalizedUnicodeUtils;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class DictTest
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = DictUtils.CreateDict();

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = Dict.Create(expected);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = Dict.Create(null);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Duplicate_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = DictUtils.CreateDict();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Duplicate());
        }


        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var sut = DictUtils.CreateDict();

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void Add_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = DictUtils.CreateDict();
            sut.Dispose();

            using var mlu = CreateAsWide(Constants.Dict.DisplayName);

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Add(Constants.Dict.Name, Constants.Dict.Value, mlu, null));
        }

        [TestMethod()]
        public void Add_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            using var sut = DictUtils.CreateDict();
            using var mlu = CreateAsWide(Constants.Dict.DisplayName);

            // Act
            bool added = sut.Add(Constants.Dict.Name, Constants.Dict.Value, mlu, null);

            // Assert
            Assert.IsTrue(added);
        }

        [TestMethod()]
        public void GetEnumerator_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = DictUtils.CreateDict();
            sut.Add("first", null, null, null);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.GetEnumerator());
        }

        [TestMethod()]
        public void GetEnumerator_WhenInvoked_ShouldReturnAddedItems()
        {
            // Arrange
            using var sut = DictUtils.CreateDict();
            using var mlu = CreateAsASCII(Constants.Dict.DisplayName);

            sut.Add("first", null, null, null);
            sut.Add("second", "second-value", null, null);
            sut.Add("third", "third-value", mlu, null);
            int expected = 3;

            // Act
            var actual = sut.Count();   // use LINQ to enumerate count

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
