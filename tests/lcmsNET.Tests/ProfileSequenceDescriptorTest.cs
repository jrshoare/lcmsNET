using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ProfileSequenceDescriptorTest
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
            uint nItems = 3;

            // Act
            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                // Assert
                Assert.IsNotNull(psd);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            uint nItems = 1;

            // Act
            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var duplicate = psd.Duplicate())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void LengthTest()
        {
            // Arrange
            uint expected = 7;

            using (var psd = ProfileSequenceDescriptor.Create(null, expected))
            {
                // Act
                uint actual = psd.Length;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void IndexerTest()
        {
            // Arrange
            uint nItems = 4;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                // Act
                ProfileSequenceItem item = psd[2];

                // Assert
                Assert.IsNotNull(item);
            }
        }

        [TestMethod()]
        public void WriteTagTest()
        {
            // Arrange
            uint nItems = 3;

            using (var profile = Profile.CreatePlaceholder(null))
            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                var item = psd[0];
                item.Attributes = DeviceAttributes.Transparency | DeviceAttributes.Matte;
                item.Manufacturer = Create("Hello 0", "Hola 0");
                item.Model = Create("Hello 0", "Hola 0");

                item = psd[1];
                item.Attributes = DeviceAttributes.Reflective | DeviceAttributes.Matte;
                item.Manufacturer = Create("Hello 1", "Hola 1");
                item.Model = Create("Hello 1", "Hola 1");

                item = psd[2];
                item.Attributes = DeviceAttributes.Transparency | DeviceAttributes.Glossy;
                item.Manufacturer = Create("Hello 2", "Hola 2");
                item.Model = Create("Hello 2", "Hola 2");

                // Act
                bool written = profile.WriteTag(TagSignature.ProfileSequenceDesc, psd.Handle);

                // Assert
                Assert.IsTrue(written);
            }

            // local function to populate and return a multi-localized unicode instance
            MultiLocalizedUnicode Create(string enUS, string esES)
            {
                var mlu = MultiLocalizedUnicode.Create(null, 0);
                mlu.SetWide("en", "US", enUS);
                mlu.SetWide("es", "ES", esES);
                return mlu;
            }
        }
    }
}
