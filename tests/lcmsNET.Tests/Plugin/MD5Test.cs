// Copyright(c) 2019-2022 John Stevenson-Hoare
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

using lcmsNET.Plugin;
using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class MD5Test
    {
        [TestMethod]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            try
            {
                // Act
                using var sut = MD5.Create();

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void Add_WhenInvoked_ShouldAddToDigest()
        {
            try
            {
                // Arrange
                using var context = ContextUtils.CreateContext();
                using var sut = MD5.Create(context);
                byte[] memory = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

                // Act
                sut.Add(memory);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void Freeze_WhenInvoked_ShouldComputeDigestAndFreeze()
        {
            try
            {
                // Arrange
                using var context = Context.Create(IntPtr.Zero, IntPtr.Zero);
                using var sut = MD5.Create(context);
                byte[] memory = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
                sut.Add(memory);

                // Act
                sut.Freeze();
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void Digest_WhenNotFrozen_ShouldThrowLcmsNETException()
        {
            try
            {
                // Arrange
                using var sut = MD5.Create();

                // Act & Assert
                Assert.ThrowsException<LcmsNETException>(() => _ = sut.Digest);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void Digest_WhenFrozen_ShouldReturnDigest()
        {
            try
            {
                // Arrange
                using var context = ContextUtils.CreateContext();
                using var sut = MD5.Create(context);
                byte[] memory = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
                sut.Add(memory);
                sut.Freeze();

                // Act
                var digest = sut.Digest;

                // Assert
                Assert.IsNotNull(digest);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }
    }
}
