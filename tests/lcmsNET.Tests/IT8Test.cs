using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class IT8Test
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
            using (var it8 = IT8.Create(null))
            {
                // Assert
                Assert.IsNotNull(it8);
            }
        }

        [TestMethod()]
        public void TableCountTest()
        {
            // Arrange
            uint expected = 1; // empty CGATS.17 object has a single table

            using (var it8 = IT8.Create(null))
            {
                // Act
                uint actual = it8.TableCount;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SetTableTest()
        {
            // Arrange
            uint expectedCount = 2; // empty CGATS.17 object has a single table and we add 1
            uint nTable = 1;

            using (var it8 = IT8.Create(null))
            {
                // Act
                int currentTable = it8.SetTable(nTable);

                // Assert
                Assert.AreEqual(nTable, Convert.ToUInt32(currentTable));
                uint actualCount = it8.TableCount;
                Assert.AreEqual(expectedCount, actualCount);
            }
        }

        [TestMethod()]
        public void SetTableOutOfRangeTest()
        {
            // Arrange
            uint nTable = 2; // empty CGATS.17 object has a single table so index two greater (zero-based)
            int expected = -1; // error returns -1

            using (var it8 = IT8.Create(null))
            {
                // Act
                int actual = it8.SetTable(nTable);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
