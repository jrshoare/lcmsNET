using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ProfileSequenceItemTest
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
        public void DeviceMfgGetTest()
        {
            // Arrange
            uint nItems = 3;
            uint expected = 0x6A727368;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[1];
                item.DeviceMfg = expected;

                // Act
                uint actual = item.DeviceMfg;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DeviceMfgSetTest()
        {
            // Arrange
            uint nItems = 3;
            uint expected = 0x6A727368;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[0];

                // Act
                item.DeviceMfg = expected;

                // Assert
                uint actual = item.DeviceMfg;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DeviceModelGetTest()
        {
            // Arrange
            uint nItems = 3;
            uint expected = 0x6D6F646C;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[1];
                item.DeviceModel = expected;

                // Act
                uint actual = item.DeviceModel;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DeviceModelSetTest()
        {
            // Arrange
            uint nItems = 3;
            uint expected = 0x6D6F646C;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[2];

                // Act
                item.DeviceModel = expected;

                // Assert
                uint actual = item.DeviceModel;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void AttributesGetTest()
        {
            // Arrange
            uint nItems = 3;
            DeviceAttributes expected = DeviceAttributes.Transparency | DeviceAttributes.Matte;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[2];
                item.Attributes = expected;

                // Act
                DeviceAttributes actual = item.Attributes;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void AttributesSetTest()
        {
            // Arrange
            uint nItems = 3;
            DeviceAttributes expected = DeviceAttributes.Reflective | DeviceAttributes.Matte;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[0];

                // Act
                item.Attributes = expected;

                // Assert
                DeviceAttributes actual = item.Attributes;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void TechnologyGetTest()
        {
            // Arrange
            uint nItems = 3;
            TechnologySignature expected = TechnologySignature.ThermalWaxPrinter;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[2];
                item.Technology = expected;

                // Act
                TechnologySignature actual = item.Technology;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void TechnologySetTest()
        {
            // Arrange
            uint nItems = 3;
            TechnologySignature expected = TechnologySignature.ProjectionTelevision;

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[1];

                // Act
                item.Technology = expected;

                // Assert
                TechnologySignature actual = item.Technology;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ProfileIDGetTest()
        {
            // Arrange
            uint nItems = 3;

            using (var profile = Profile.CreatePlaceholder(null))
            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[2];
                profile.ComputeMD5();
                byte[] expected = profile.HeaderProfileID;
                item.ProfileID = expected;

                // Act
                byte[] actual = item.ProfileID;

                // Assert
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ProfileIDSetTest()
        {
            // Arrange
            uint nItems = 3;

            using (var profile = Profile.CreatePlaceholder(null))
            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            {
                ProfileSequenceItem item = psd[2];
                profile.ComputeMD5();
                byte[] expected = profile.HeaderProfileID;

                // Act
                item.ProfileID = expected;

                // Assert
                byte[] actual = item.ProfileID;
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ManufacturerGetTest()
        {
            // Arrange
            uint nItems = 3;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "Manufacturer";

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var mlu = MultiLocalizedUnicode.Create(null, 1))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                ProfileSequenceItem item = psd[1];
                item.Manufacturer = mlu;

                // Act
                string actual = item.Manufacturer.GetASCII(languageCode, countryCode);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ManufacturerSetTest()
        {
            // Arrange
            uint nItems = 3;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "Manufacturer";

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var mlu = MultiLocalizedUnicode.Create(null, 1))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                ProfileSequenceItem item = psd[0];

                // Act
                item.Manufacturer = mlu;

                // Assert
                string actual = item.Manufacturer.GetASCII(languageCode, countryCode);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ModelGetTest()
        {
            // Arrange
            uint nItems = 3;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "Model";

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var mlu = MultiLocalizedUnicode.Create(null, 1))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                ProfileSequenceItem item = psd[2];
                item.Model = mlu;

                // Act
                string actual = item.Model.GetASCII(languageCode, countryCode);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ModelSetTest()
        {
            // Arrange
            uint nItems = 3;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "Model";

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var mlu = MultiLocalizedUnicode.Create(null, 1))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                ProfileSequenceItem item = psd[1];

                // Act
                item.Model = mlu;

                // Assert
                string actual = item.Model.GetASCII(languageCode, countryCode);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DescriptionGetTest()
        {
            // Arrange
            uint nItems = 3;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "Description";

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var mlu = MultiLocalizedUnicode.Create(null, 1))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                ProfileSequenceItem item = psd[0];
                item.Description = mlu;

                // Act
                string actual = item.Description.GetASCII(languageCode, countryCode);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DescriptionSetTest()
        {
            // Arrange
            uint nItems = 3;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "Description";

            using (var psd = ProfileSequenceDescriptor.Create(null, nItems))
            using (var mlu = MultiLocalizedUnicode.Create(null, 1))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                ProfileSequenceItem item = psd[2];

                // Act
                item.Description = mlu;

                // Assert
                string actual = item.Description.GetASCII(languageCode, countryCode);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
