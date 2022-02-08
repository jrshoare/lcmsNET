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
using System.IO;
using System.Reflection;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class MultiLocalizedUnicodeTest
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

        private MemoryStream Save(string resourceName)
        {
            MemoryStream ms = new MemoryStream();
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using (var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName))
            {
                s.CopyTo(ms);
            }
            return ms;
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 3;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                // Assert
                Assert.IsNotNull(mlu);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                mlu.SetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, "Duplicate");

                using (var duplicate = mlu.Duplicate())
                {
                    // Assert
                    Assert.IsNotNull(duplicate);
                }
            }
        }

        [TestMethod()]
        public void SetASCIITest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                bool set = mlu.SetASCII(languageCode, countryCode, "SetASCII");

                // Assert
                Assert.IsTrue(set);
            }
        }

        [TestMethod()]
        public void SetWideTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                bool set = mlu.SetWide(languageCode, countryCode, "SetWide");

                // Assert
                Assert.IsTrue(set);
            }
        }

        [TestMethod()]
        public void GetASCIITest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "GetASCII";

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                mlu.SetASCII(languageCode, countryCode, expected);
                var actual = mlu.GetASCII(languageCode, countryCode);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetASCIITestNonExistent()
        {
            // Arrange
            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                // Act
                var actual = mlu.GetASCII("en", "US");

                // Assert
                Assert.IsNull(actual);
            }
        }

        [TestMethod()]
        public void GetWideTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";
            string expected = "GetWide";

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                mlu.SetWide(languageCode, countryCode, expected);
                var actual = mlu.GetWide(languageCode, countryCode);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetWideTestNonExistent()
        {
            // Arrange
            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                // Act
                var actual = mlu.GetWide("en", "US");

                // Assert
                Assert.IsNull(actual);
            }
        }

        [TestMethod()]
        public void GetTranslationTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string expectedLanguageCode = "en";
            string expectedCountryCode = "US";

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                mlu.SetASCII(expectedLanguageCode, expectedCountryCode, "GetTranslation");
                var actual = mlu.GetTranslation(expectedLanguageCode, expectedCountryCode,
                        out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void GetTranslationTestNonExistent()
        {
            // Arrange
            string expectedLanguageCode = null;
            string expectedCountryCode = null;

            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                // Act
                var actual = mlu.GetTranslation("en", "US",
                        out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.IsFalse(actual);
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void GetTranslationTestNoLanguage()
        {
            // Arrange
            var expectedLanguageCode = MultiLocalizedUnicode.NoLanguage;
            var expectedCountryCode = "US";

            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                mlu.SetASCII(expectedLanguageCode, expectedCountryCode, "GetTranslationNoLanguage");

                // Act
                var actual = mlu.GetTranslation(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry,
                        out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.IsTrue(actual);
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void GetTranslationTestLanguageOnlyMatch()
        {
            // Arrange
            var expectedLanguageCode = "en";
            var expectedCountryCode = "US";

            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                mlu.SetASCII("fr", "FR", "Pomme");
                mlu.SetASCII(expectedLanguageCode, expectedCountryCode, "Apple");

                // Act
                var actual = mlu.GetTranslation(expectedLanguageCode, "GB",
                        out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.IsTrue(actual);
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void GetTranslationTestNoMatch()
        {
            // Arrange
            var expectedLanguageCode = "en";
            var expectedCountryCode = "US";

            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                mlu.SetASCII(expectedLanguageCode, expectedCountryCode, "Apple");

                // Act
                var actual = mlu.GetTranslation("fr", "FR",
                        out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.IsTrue(actual);
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void TranslationsCountTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";
            uint notExpected = 0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                mlu.SetASCII(languageCode, countryCode, "TranslationsCount");
                var actual = mlu.TranslationsCount;

                // Assert
                Assert.AreNotEqual(notExpected, actual);
            }
        }

        [TestMethod()]
        public void TranslationsCodesTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nItems = 0;
            string expectedLanguageCode = "en";
            string expectedCountryCode = "US";
            uint index = 0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var mlu = MultiLocalizedUnicode.Create(context, nItems))
            {
                mlu.SetASCII(expectedLanguageCode, expectedCountryCode, "TranslationsCodes");
                var actual = mlu.TranslationsCodes(index, out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void TranslationsCodesTestNoLanguageNoCountry()
        {
            // Arrange
            var expectedLanguageCode = MultiLocalizedUnicode.NoLanguage;
            var expectedCountryCode = MultiLocalizedUnicode.NoCountry;
            uint index = 0;

            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                mlu.SetASCII(expectedLanguageCode, expectedCountryCode, "TranslationsCodes");

                // Act
                var actual = mlu.TranslationsCodes(index, out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void TranslationsCodesTestIndexOutOfRange()
        {
            // Arrange
            uint index = 3;
            string expectedLanguageCode = null;
            string expectedCountryCode = null;

            using (var mlu = MultiLocalizedUnicode.Create(null))
            {
                // Act
                var actual = mlu.TranslationsCodes(index, out string actualLanguageCode, out string actualCountryCode);

                // Assert
                Assert.IsFalse(actual);
                Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
                Assert.AreEqual(expectedCountryCode, actualCountryCode);
            }
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            string expected = "sRGB IEC61966-2.1";

            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                using (var profile = Profile.Open(ms.GetBuffer()))
                // Act
                // implicit call to FromHandle
                using (var mlu = profile.ReadTag<MultiLocalizedUnicode>(TagSignature.ProfileDescription))
                {
                    string actual = mlu.GetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                // Act
                using (var profile = Profile.Open(ms.GetBuffer()))
                using (var mlu = profile.ReadTag<MultiLocalizedUnicode>(TagSignature.ProfileDescription))
                {
                    // Assert
                    Assert.IsNotNull(mlu);
                }
            }
        }
    }
}
