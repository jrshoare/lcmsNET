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
    public class GamutBoundaryDescriptorTest
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

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var gbd = GamutBoundaryDescriptor.Create(context))
            {
                // Assert
                Assert.IsNotNull(gbd);
            }
        }

        [TestMethod()]
        public void AddPointTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIELab lab = new CIELab { L = 99.3, a = 12.6, b = 14.2 };

            using (var context = Context.Create(plugin, userData))
            using (var gbd = GamutBoundaryDescriptor.Create(context))
            {
                // Act
                bool added = gbd.AddPoint(lab);

                // Assert
                Assert.IsTrue(added);
            }
        }

        [TestMethod()]
        public void ComputeTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            using (var gbd = GamutBoundaryDescriptor.Create(context))
            {
                // Act
                bool computed = gbd.Compute();

                // Assert
                Assert.IsTrue(computed);
            }
        }

        [TestMethod()]
        public void CheckPointTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            using (var gbd = GamutBoundaryDescriptor.Create(context))
            {
                CIELab add = new CIELab { };
                for (int L = 0; L <= 100; L += 10)
                    for (int a = -128; a <= 128; a += 5)
                        for (int b = -128; b <= 128; b+= 5)
                        {
                            add.L = L;
                            add.a = a;
                            add.b = b;
                            gbd.AddPoint(add);
                        }

                gbd.Compute();

                // Act
                CIELab check = new CIELab { };
                for (int L = 10; L <= 90; L += 25)
                    for (int a = -120; a <= 120; a += 25)
                        for (int b = -120; b <= 120; b += 25)
                        {
                            check.L = L;
                            check.a = a;
                            check.b = b;

                            // Assert
                            Assert.IsTrue(gbd.CheckPoint(check));
                        }
            }
        }
    }
}
