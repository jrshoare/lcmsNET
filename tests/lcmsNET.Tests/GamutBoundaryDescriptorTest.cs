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
    public class GamutBoundaryDescriptorTest
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = GamutBoundaryDescriptor.Create(expected);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = GamutBoundaryDescriptor.Create(null);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void AddPoint_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();
            sut.Dispose();

            CIELab lab = new() { L = 99.3, a = 12.6, b = 14.2 };

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.AddPoint(lab));
        }

        [TestMethod()]
        public void AddPoint_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();
            CIELab lab = new() { L = 99.3, a = 12.6, b = 14.2 };

            // Act
            bool added = sut.AddPoint(lab);

            // Assert
            Assert.IsTrue(added);
        }

        [TestMethod()]
        public void Compute_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Compute());
        }

        [TestMethod()]
        public void Compute_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();

            // Act
            bool computed = sut.Compute();

            // Assert
            Assert.IsTrue(computed);
        }

        [TestMethod()]
        public void CheckPoint_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();
            sut.Dispose();

            CIELab check = new();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.CheckPoint(check));
        }

        [TestMethod()]
        public void CheckPoint_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            using var sut = GamutBoundaryDescriptorUtils.CreateGamutBoundaryDescriptor();
            GamutBoundaryDescriptorUtils.AddPoints(sut);
            sut.Compute();

            // Act
            CIELab check = new();
            for (int L = 10; L <= 90; L += 25)
                for (int a = -120; a <= 120; a += 25)
                    for (int b = -120; b <= 120; b += 25)
                    {
                        check.L = L;
                        check.a = a;
                        check.b = b;

                        // Assert
                        Assert.IsTrue(sut.CheckPoint(check));
                    }
        }
    }
}
