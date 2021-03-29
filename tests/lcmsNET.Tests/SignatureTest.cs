using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class SignatureTest
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
            uint expected = 0x237F9AB1;

            // Act
            var target = new Signature(expected);

            // Assert
            uint actual = target;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UintOperatorTest()
        {
            // Arrange
            uint expected = 0x12345982;

            // Act
            var target = (Signature)expected;

            // Assert
            uint actual = target;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SignatureOperatorTest()
        {
            // Arrange
            uint expected = 0x75DF3108;

            // Act
            var target = (Signature)expected;
            uint actual = target;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = (Signature)0xF32794E2;

                profile.WriteTag(TagSignature.Technology, expected);
                var tag = profile.ReadTag(TagSignature.Technology);

                // Act
                var actual = Signature.FromHandle(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void FromHandleTest2()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new Signature(0x962E1457);

                profile.WriteTag(TagSignature.Technology, expected);

                // Act
                // implicit call to FromHandle
                var actual = profile.ReadTag<Signature>(TagSignature.Technology);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
