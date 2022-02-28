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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class MD5Test
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void CreateTest()
        {
            try
            {
                // Act
                using (var md5 = MD5.Create())
                {
                    // Assert
                    Assert.IsNotNull(md5);
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void AddTest()
        {
            try
            {
                // Arrange
                using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
                using (var md5 = MD5.Create(context))
                {
                    byte[] memory = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                    // Act
                    md5.Add(memory);

                    // Assert
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void FreezeTest()
        {
            try
            {
                // Arrange
                using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
                using (var md5 = MD5.Create(context))
                {
                    byte[] memory = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    md5.Add(memory);

                    // Act
                    md5.Freeze();

                    // Assert
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void DigestNotFrozenTest()
        {
            try
            {
                // Arrange
                using (var md5 = MD5.Create())
                {
                    // Act & Assert
                    Assert.ThrowsException<LcmsNETException>(() => _ = md5.Digest);
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }

        [TestMethod]
        public void DigestTest()
        {
            try
            {
                // Arrange
                using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
                using (var md5 = MD5.Create(context))
                {
                    byte[] memory = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    md5.Add(memory);

                    // Act
                    md5.Freeze();
                    var digest = md5.Digest;

                    // Assert
                    Assert.IsNotNull(digest);
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.10 or later.");
            }
        }
    }
}
