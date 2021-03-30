using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ColorantOrderTest
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
        public void ConstructorTest()
        {
            // Arrange
            byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Act
            var target = new ColorantOrder(expected);

            // Assert
            var actual = (byte[])target;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConstructorTestNullArrayArgument()
        {
            // Arrange
            byte[] expected = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new ColorantOrder(expected));
        }

        [TestMethod()]
        public void ConstructorTestInvalidArrayLength()
        {
            // Arrange
            const int invalidLength = 13;
            byte[] expected = new byte[invalidLength];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new ColorantOrder(expected));
        }

        [TestMethod()]
        public void ByteArrayOperatorTest()
        {
            // Arrange
            byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Act
            var target = new ColorantOrder(expected);

            // Assert
            byte[] actual = target;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ColorantOrderOperatorTest()
        {
            // Arrange
            byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // Act
            var target = (ColorantOrder)expected;

            // Assert
            byte[] actual = target;
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                var target = (ColorantOrder)expected;

                profile.WriteTag(TagSignature.ColorantOrder, target);
                var tag = profile.ReadTag(TagSignature.ColorantOrder);

                // Act
                byte[] actual = ColorantOrder.FromHandle(tag);

                // Assert
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void FromHandleTest2()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                byte[] expected = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

                profile.WriteTag(TagSignature.ColorantOrder, (ColorantOrder)expected);

                // Act
                // implicit call to FromHandle
                byte[] actual = profile.ReadTag<ColorantOrder>(TagSignature.ColorantOrder);

                // Assert
                CollectionAssert.AreEqual(expected, actual);
            }
        }
    }
}
