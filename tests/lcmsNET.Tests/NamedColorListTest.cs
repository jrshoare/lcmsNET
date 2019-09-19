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

                    // Act
                    bool added = ncl.Add($"#{i}", pcs, colorant);
                }

                // Act
                uint actual = ncl.Count;

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

                    // Act
                    bool added = ncl.Add($"#{i}", pcs, colorant);
                }

                // Act
                int actual = ncl[$"#{expected}"];

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
    }
}
