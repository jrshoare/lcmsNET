using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ICCDataTest
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
        public void Constructor1Test()
        {
            // Arrange
            string expectedString = "constructor1test";
            uint expectedFlag = ICCData.ASCII;

            // Act
            var target = new ICCData(expectedString);

            // Assert
            var actualString = (string)target;
            var actualFlag = target.Flag;
            Assert.AreEqual(expectedString, actualString);
            Assert.AreEqual(expectedFlag, actualFlag);
        }

        [TestMethod()]
        public void Constructor1TestNullString()
        {
            // Arrange
            string s = null;

            // Act & Assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new ICCData(s));
        }

        [TestMethod()]
        public void Constructor2Test()
        {
            // Arrange
            var expectedData = new byte[] { 12, 96, 14, 2 };
            uint expectedFlag = ICCData.Binary;

            // Act
            var target = new ICCData(expectedData);

            // Assert
            var actualData = target.Data;
            var actualFlag = target.Flag;
            Assert.AreEqual(expectedData, actualData);
            Assert.AreEqual(expectedFlag, actualFlag);
        }

        [TestMethod()]
        public void Constructor2TestNullBytes()
        {
            // Arrange
            byte[] bytes = null;

            // Act & Assert
            var actual = Assert.ThrowsException<ArgumentNullException>(() => new ICCData(bytes));
        }

        [TestMethod()]
        public void StringOperatorTest()
        {
            // Arrange
            string expectedString = "stringoperatortest";

            // Act
            var target = new ICCData(expectedString);

            // Assert
            var actualString = (string)target;
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod()]
        public void StringOperatorTestNotAscii()
        {
            // Arrange
            var bytes = new byte[] { 12, 96, 14, 2 };

            // Act
            var target = new ICCData(bytes);

            // Assert
            var actual = Assert.ThrowsException<InvalidCastException>(() => (string)target);
        }

        [TestMethod()]
        public void FromHandleTestAscii()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = "fromhandletestascii";
                var iccData = new ICCData(expected);

                // do not use TagSignature.Data as this is not supported
                profile.WriteTag(TagSignature.Ps2CRD0, iccData);

                // Act
                var iccData2 = ICCData.FromHandle(profile.ReadTag(TagSignature.Ps2CRD0));

                // Assert
                var actual = (string)iccData2;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void FromHandleTestBinary()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new byte[] { 17, 99, 0, 253, 122, 19 };
                var iccData = new ICCData(expected);

                // do not use TagSignature.Data as this is not supported
                profile.WriteTag(TagSignature.Ps2CRD0, iccData);

                // Act
                var iccData2 = ICCData.FromHandle(profile.ReadTag(TagSignature.Ps2CRD0));

                // Assert
                var actual = iccData2.Data;
                CollectionAssert.AreEqual(expected, actual);
            }
        }
    }
}
