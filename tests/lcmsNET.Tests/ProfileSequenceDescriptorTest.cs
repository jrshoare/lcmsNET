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

        MultiLocalizedUnicode Create(string enUS, string esES)
        {
            var mlu = MultiLocalizedUnicode.Create(null, 0);
            mlu.SetWide("en", "US", enUS);
            mlu.SetWide("es", "ES", esES);
            return mlu;
        }

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
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            uint expected = 1;

            using (var profile = Profile.CreatePlaceholder(null))
            {
                using (var psd = ProfileSequenceDescriptor.Create(null, expected))
                {
                    var item = psd[0];
                    item.Attributes = DeviceAttributes.Transparency | DeviceAttributes.Matte;
                    item.Manufacturer = Create("Hello 0", "Hola 0");
                    item.Model = Create("Hello 0", "Hola 0");

                    profile.WriteTag(TagSignature.ProfileSequenceDesc, psd.Handle);
                }

                // Act
                // implicit call to FromHandle
                using (var roPsd = profile.ReadTag<ProfileSequenceDescriptor>(TagSignature.ProfileSequenceDesc))
                {
                    // Assert
                    uint actual = roPsd.Length;
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
