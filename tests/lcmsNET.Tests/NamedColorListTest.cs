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
    public class NamedColorListTest
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

            // Act
            using (var ncl = NamedColorList.Create(null, 0, 4, "prefix", "suffix"))
            {
                // Assert
                Assert.IsNotNull(ncl);
            }
        }

        [TestMethod()]
        public void CreateTestNullPrefix()
        {
            // Arrange

            // Act
            using (var ncl = NamedColorList.Create(null, 0, 4, null, "suffix"))
            {
                // Assert
                Assert.IsNotNull(ncl);
            }
        }

        [TestMethod()]
        public void CreateTestNullSuffix()
        {
            // Arrange

            // Act
            using (var ncl = NamedColorList.Create(null, 0, 4, "prefix", null))
            {
                // Assert
                Assert.IsNotNull(ncl);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange

            // Act
            using (var ncl = NamedColorList.Create(null, 256, 3, "pre", "post"))
            using (var duplicate = ncl.Duplicate())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            using (var ncl = NamedColorList.Create(null, 256, 3, "pre", "post"))
            {
                ushort[] pcs = new ushort[3] { 1, 1, 1 };
                ushort[] colorant = new ushort[16];
                colorant[0] = colorant[1] = colorant[2] = 1;

                // Act
                bool added = ncl.Add("#1", pcs, colorant);

                // Assert
                Assert.IsTrue(added);
            }
        }

        [TestMethod()]
        public void CountTest()
        {
            // Arrange
            uint expected = 256;

            using (var ncl = NamedColorList.Create(null, 256, 3, "pre", "post"))
            {
                for (uint i = 0; i < expected; i++)
                {
                    ushort[] pcs = new ushort[3] { (ushort)i, (ushort)i, (ushort)i };
                    ushort[] colorant = new ushort[16];
                    colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                    bool added = ncl.Add($"#{i}", pcs, colorant);
                }

                // Act
                uint actual = ncl.Count;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void CountTestEmptyList()
        {
            // Arrange
            uint expected = 0;

            using (var ncl = NamedColorList.Create(null, 256, 3, null, null))
            {
                // Act
                var actual = ncl.Count;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void IndexerTest()
        {
            // Arrange
            int expected = 23;

            using (var ncl = NamedColorList.Create(null, 256, 3, "pre", "post"))
            {
                for (uint i = 0; i < 256; i++)
                {
                    ushort[] pcs = new ushort[3] { (ushort)i, (ushort)i, (ushort)i };
                    ushort[] colorant = new ushort[16];
                    colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                    bool added = ncl.Add($"#{i}", pcs, colorant);
                }

                // Act
                int actual = ncl[$"#{expected}"];

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void IndexerTestNotFound()
        {
            // Arrange
            int expected = -1;

            using (var ncl = NamedColorList.Create(null, 256, 3, null, null))
            {
                // Act
                int actual = ncl["notfound"];

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetInfoTest()
        {
            // Arrange
            string expectedPrefix = "pre";
            string expectedSuffix = "post";
            uint expectedNColor = 42;
            string expectedName = $"#{expectedNColor}";

            using (var ncl = NamedColorList.Create(null, 256, 3, expectedPrefix, expectedSuffix))
            {
                for (uint i = 0; i < 256; i++)
                {
                    ushort[] pcs = new ushort[3] { (ushort)i, (ushort)i, (ushort)i };
                    ushort[] colorant = new ushort[16];
                    colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                    // Act
                    bool added = ncl.Add($"#{i}", pcs, colorant);
                }

                // Act
                bool getInfo = ncl.GetInfo(expectedNColor, out string actualName, out string actualPrefix, out string actualSuffix,
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

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            int expected = 23;

            using (var profile = Profile.CreatePlaceholder(null))
            {
                using (var ncl = NamedColorList.Create(null, 256, 3, "pre", "post"))
                {
                    for (uint i = 0; i < 256; i++)
                    {
                        ushort[] pcs = new ushort[3] { (ushort)i, (ushort)i, (ushort)i };
                        ushort[] colorant = new ushort[16];
                        colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                        bool added = ncl.Add($"#{i}", pcs, colorant);
                    }

                    profile.WriteTag(TagSignature.NamedColor2, ncl.Handle);
                }

                // Act
                // implicit call to FromHandle
                using (var roNcl = profile.ReadTag<NamedColorList>(TagSignature.NamedColor2))
                {
                    // Assert
                    int actual = roNcl[$"#{expected}"];
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
