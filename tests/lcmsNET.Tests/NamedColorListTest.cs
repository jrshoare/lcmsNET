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
    public class NamedColorListTest
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = NamedColorList.Create(context: null, n: 0, colorantCount: 4, "prefix", "suffix");

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = NamedColorList.Create(expected, n: 0, colorantCount: 4, "prefix", "suffix");
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = NamedColorList.Create(context: null, n: 0, colorantCount: 4, "prefix", "suffix");
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Create_WhenNullPrefix_ShouldHaveValidHandle()
        {
            // Act
            using var sut = NamedColorList.Create(context: null, n: 0, colorantCount: 4, prefix: null, "suffix");

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenNullSuffix_ShouldHaveValidHandle()
        {
            // Act
            using var sut = NamedColorList.Create(context: null, n: 0, colorantCount: 4, "prefix", suffix: null);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Duplicate_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Duplicate());
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void Add_WhenInvalidPcsLength_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            ushort[] pcs = [1]; // length not 3 throws
            ushort[] colorant = new ushort[16];
            colorant[0] = colorant[1] = colorant[2] = 1;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Add(name: "#1", pcs, colorant));
        }

        [TestMethod()]
        public void Add_WhenInvalidColorantLength_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            ushort[] pcs = [1, 1, 1];
            ushort[] colorant = new ushort[17]; // length not 16 throws
            colorant[0] = colorant[1] = colorant[2] = 1;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Add(name: "#1", pcs, colorant));
        }

        [TestMethod()]
        public void Add_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            sut.Dispose();

            ushort[] pcs = [1, 1, 1];
            ushort[] colorant = new ushort[16];
            colorant[0] = colorant[1] = colorant[2] = 1;

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Add(name: "#1", pcs, colorant));
        }

        [TestMethod()]
        public void Add_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");

            ushort[] pcs = [1, 1, 1];
            ushort[] colorant = new ushort[16];
            colorant[0] = colorant[1] = colorant[2] = 1;

            // Act
            bool result = sut.Add(name: "#1", pcs, colorant);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Count_WhenInvoked_ShouldGetNumberOfSpotColors()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            uint expected = 256;

            for (uint i = 0; i < expected; i++)
            {
                ushort[] pcs = [(ushort)i, (ushort)i, (ushort)i];
                ushort[] colorant = new ushort[16];
                colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                bool added = sut.Add($"#{i}", pcs, colorant);
            }

            // Act
            uint actual = sut.Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Count_WhenEmpty_ShouldReturnZero()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: null, suffix: null);
            uint expected = 0;

            // Act
            var actual = sut.Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Indexer_WhenNamedSpotColorPresent_ShouldReturnIndex()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            int expected = 23;

            for (uint i = 0; i < 256; i++)
            {
                ushort[] pcs = [(ushort)i, (ushort)i, (ushort)i];
                ushort[] colorant = new ushort[16];
                colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                sut.Add($"#{i}", pcs, colorant);
            }

            // Act
            int actual = sut[$"#{expected}"];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Indexer_WhenNamedSpotColorNotPresent_ShouldReturnError()
        {
            // Arrange
            using var sut = NamedColorList.Create(context: null, n: 256, colorantCount: 3, prefix: "pre", suffix: "post");
            int expected = Constants.NamedColorList.IndexNotFound;

            // Act
            int actual = sut["not_found"];

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetInfo_WhenValid_ShouldGetSpotColorInfoForIndex()
        {
            // Arrange
            string expectedPrefix = "pre";
            string expectedSuffix = "post";
            uint expectedNColor = 42;
            string expectedName = $"#{expectedNColor}";

            using var sut = NamedColorList.Create(null, 256, 3, expectedPrefix, expectedSuffix);
            for (uint i = 0; i < 256; i++)
            {
                ushort[] pcs = [(ushort)i, (ushort)i, (ushort)i];
                ushort[] colorant = new ushort[16];
                colorant[0] = colorant[1] = colorant[2] = (ushort)i;
                sut.Add($"#{i}", pcs, colorant);
            }

            // Act
            bool getInfo = sut.GetInfo(expectedNColor, out string actualName, out string actualPrefix, out string actualSuffix,
                    out ushort[] actualPcs, out ushort[] actualColorant);

            // Assert
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedPrefix, actualPrefix);
            Assert.AreEqual(expectedSuffix, actualSuffix);
            for (ushort i = 0; i < 3; i++)
            {
                Assert.AreEqual(expectedNColor, actualPcs[i]);
                Assert.AreEqual(expectedNColor, actualColorant[i]);
            }
        }
    }
}
