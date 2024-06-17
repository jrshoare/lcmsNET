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

using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class MultiLocalizedUnicodeTest
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = MultiLocalizedUnicode.Create(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = MultiLocalizedUnicode.Create(expected);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = MultiLocalizedUnicode.Create(context: null);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Duplicate_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            sut.SetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, "duplicate");
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Duplicate());
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            sut.SetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, "duplicate");

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void SetASCII_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() =>
                sut.SetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, "set_ascii_disposed"));
        }

        [TestMethod()]
        public void SetASCII_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);

            // Act
            bool result = sut.SetASCII(languageCode: "en", countryCode: "US", "set_ascii");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SetWide_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() =>
                sut.SetWide(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, "set_wide_disposed"));
        }

        [TestMethod()]
        public void SetWide_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);

            // Act
            bool result = sut.SetWide(languageCode: "en", countryCode: "US", "set_wide");

            // Assert
            Assert.IsTrue(result);
        }

#if NET5_0_OR_GREATER
        [TestMethod()]
        public void SetUTF8_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() =>
                sut.SetUTF8(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry, "set_utf8_disposed"));
        }

        [TestMethod()]
        public void SetUTF8_WhenValid_ShouldSucceed()
        {
            try
            {
                // Arrange
                using var context = ContextUtils.CreateContext();
                using var sut = MultiLocalizedUnicode.Create(context);

                // Act
                bool set = sut.SetUTF8(languageCode: "en", countryCode: "US", "set_utf8");

                // Assert
                Assert.IsTrue(set);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }
#endif

        [TestMethod()]
        public void GetASCII_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            string expected = "get_ascii_disposed";
            sut.SetASCII(languageCode: "en", countryCode: "US", expected);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.GetASCII(languageCode: "en", countryCode: "US"));
        }

        [TestMethod()]
        public void GetASCII_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);
            string expected = "get_ascii";
            sut.SetASCII(languageCode: "en", countryCode: "US", expected);

            // Act
            var actual = sut.GetASCII(languageCode: "en", countryCode: "US");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetASCII_WhenNonExistent_ShouldReturnNull()
        {
            // Arrange
            using var sut = MultiLocalizedUnicode.Create(context: null);

            // Act
            var actual = sut.GetASCII(languageCode: "en", countryCode: "US");

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetWide_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = MultiLocalizedUnicode.Create(context: null);
            string expected = "get_wide_disposed";
            sut.SetWide(languageCode: "en", countryCode: "US", expected);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.GetWide(languageCode: "en", countryCode: "US"));
        }

        [TestMethod()]
        public void GetWide_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            using var sut = MultiLocalizedUnicode.Create(context: null);
            string expected = "get_wide";
            sut.SetWide(languageCode: "en", countryCode: "US", expected);

            // Act
            var actual = sut.GetWide(languageCode: "en", countryCode: "US");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetWideWhenNonExistent_ShouldReturnNull()
        {
            // Arrange
            using var sut = MultiLocalizedUnicode.Create(context: null);

            // Act
            var actual = sut.GetWide(languageCode: "en", countryCode: "US");

            // Assert
            Assert.IsNull(actual);
        }

#if NET5_0_OR_GREATER
        [TestMethod()]
        public void GetUTF8_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            try
            {
                // Arrange
                string expected = "get_utf8";
                using var sut = MultiLocalizedUnicode.Create(context: null);
                sut.SetUTF8(languageCode: "en", countryCode: "US", expected);
                sut.Dispose();

                // Act & Assert
                Assert.ThrowsException<ObjectDisposedException>(() => sut.GetUTF8(languageCode: "en", countryCode: "US"));
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }

        [TestMethod()]
        public void GetUTF8_WhenRoundTripped_ShouldHaveValueSet()
        {
            try
            {
                // Arrange
                string expected = "get_utf8";
                using var sut = MultiLocalizedUnicode.Create(context: null);

                // Act
                sut.SetUTF8(languageCode: "en", countryCode: "US", expected);
                var actual = sut.GetUTF8(languageCode: "en", countryCode: "US");

                // Assert
                Assert.AreEqual(expected, actual);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }

        [TestMethod()]
        public void GetUTF8_WhenNonExistent_ShouldReturnNull()
        {
            try
            {
                // Arrange
                using var sut = MultiLocalizedUnicode.Create(null);

                // Act
                var actual = sut.GetUTF8(languageCode: "en", countryCode: "US");

                // Assert
                Assert.IsNull(actual);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }
#endif

        [TestMethod()]
        public void GetTranslation_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            string expectedLanguageCode = "en";
            string expectedCountryCode = "US";
            using var sut = MultiLocalizedUnicode.Create(context: null);
            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "get_translation_disposed");
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.GetTranslation(
                expectedLanguageCode, expectedCountryCode, out string actualLanguageCode, out string actualCountryCode));
        }

        [TestMethod()]
        public void GetTranslation_WhenValid_ShouldReturnTranslationRule()
        {
            // Arrange
            string expectedLanguageCode = "en";
            string expectedCountryCode = "US";
            using var sut = MultiLocalizedUnicode.Create(context: null);
            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "get_translation");

            // Act
            var result = sut.GetTranslation(expectedLanguageCode, expectedCountryCode,
                    out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void GetTranslation_WhenNonExistent_ShouldReturnFalse()
        {
            // Arrange
            string expectedLanguageCode = null;
            string expectedCountryCode = null;

            using var sut = MultiLocalizedUnicode.Create(context: null);

            // Act
            var result = sut.GetTranslation("en", "US",
                    out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void GetTranslation_WhenNoLanguageSet_ShouldReturnNoLanguage()
        {
            // Arrange
            var expectedLanguageCode = MultiLocalizedUnicode.NoLanguage;
            var expectedCountryCode = "US";

            using var sut = MultiLocalizedUnicode.Create(null);
            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "get_translation_no_language");

            // Act
            var result = sut.GetTranslation(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry,
                    out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void GetTranslation_WhenOnlyLanguageMatch_ShouldReturnTranslation()
        {
            // Arrange
            var expectedLanguageCode = "en";
            var expectedCountryCode = "US";

            using var sut = MultiLocalizedUnicode.Create(context: null);
            sut.SetASCII("fr", "FR", "Pomme");
            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "Apple");

            // Act
            var result = sut.GetTranslation(expectedLanguageCode, countryCode: "GB",
                    out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void GetTranslation_WhenNoMatch_ShouldReturnFirstTranslation()
        {
            // Arrange
            var expectedLanguageCode = "en";
            var expectedCountryCode = "US";

            using var sut = MultiLocalizedUnicode.Create(context: null);
            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "Apple");

            // Act
            var result = sut.GetTranslation("fr", "FR",
                    out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void TranslationsCount_WhenValid_ShouldReturnNumberOfTranslations()
        {
            // Arrange
            using var sut = MultiLocalizedUnicode.Create(context: null);
            sut.SetASCII(languageCode: "en", countryCode: "US", "translations_count");

            // Act
            var actual = sut.TranslationsCount;

            // Assert
            Assert.AreNotEqual(0u, actual);
        }

        [TestMethod()]
        public void TranslationsCodes_WhenValid_ShouldReturnLanguageAndCountryForIndex()
        {
            // Arrange
            string expectedLanguageCode = "en";
            string expectedCountryCode = "US";

            using var context = ContextUtils.CreateContext();
            using var sut = MultiLocalizedUnicode.Create(context);

            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "translation_codes");
            uint index = 0;

            // Act
            var actual = sut.TranslationsCodes(index, out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void TranslationsCodes_WhenNoTranslations_ShouldReturnNoLanguageNoCountry()
        {
            // Arrange
            var expectedLanguageCode = MultiLocalizedUnicode.NoLanguage;
            var expectedCountryCode = MultiLocalizedUnicode.NoCountry;

            using var sut = MultiLocalizedUnicode.Create(context: null);
            sut.SetASCII(expectedLanguageCode, expectedCountryCode, "translation_code_none");
            uint index = 0;

            // Act
            var result = sut.TranslationsCodes(index, out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }

        [TestMethod()]
        public void TranslationsCodes_WhenIndexOutOfRange_ShouldReturnFalse()
        {
            // Arrange
            string expectedLanguageCode = null;
            string expectedCountryCode = null;

            using var sut = MultiLocalizedUnicode.Create(context: null);
            uint index = 3;

            // Act
            var result = sut.TranslationsCodes(index, out string actualLanguageCode, out string actualCountryCode);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(expectedLanguageCode, actualLanguageCode);
            Assert.AreEqual(expectedCountryCode, actualCountryCode);
        }
    }
}
