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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class UcrBgTest
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
            int type = 4;
            double[] parameters = new double[] { 2.4, 1.0 / 1.055, 0.055 / 1.055, 1.0 / 12.92, 0.04045 };
            double gamma = 2.2;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";
            string text = "constructor";

            using (var expectedUcr = ToneCurve.BuildParametric(null, type, parameters))
            using (var expectedBg = ToneCurve.BuildGamma(null, gamma))
            using (var expectedDesc = MultiLocalizedUnicode.Create(null, nItems))
            {
                expectedDesc.SetASCII(languageCode, countryCode, text);

                // Act
                var target = new UcrBg(expectedUcr, expectedBg, expectedDesc);
                var actualUcr = target.Ucr;
                var actualBg = target.Bg;
                var actualDesc = target.Desc;

                // Assert
                Assert.AreSame(expectedUcr, actualUcr);
                Assert.AreSame(expectedBg, actualBg);
                Assert.AreSame(expectedDesc, actualDesc);
            }
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            double gammaUcr = 2.4, gammaBg = -2.2;
            string languageCode = "en";
            string countryCode = "US";
            string expectedText = "from-handle";

            using (var ucr = ToneCurve.BuildGamma(null, gammaUcr))
            using (var bg = ToneCurve.BuildGamma(null, gammaBg))
            using (var desc = MultiLocalizedUnicode.Create(null))
            {
                desc.SetASCII(languageCode, countryCode, expectedText);

                var target = new UcrBg(ucr, bg, desc);
                using (var profile = Profile.CreatePlaceholder(null))
                {
                    profile.WriteTag(TagSignature.UcrBg, target);

                    // Act
                    // implicit call to FromHandle
                    var actual = profile.ReadTag<UcrBg>(TagSignature.UcrBg);
                    var actualUcr = actual.Ucr;
                    var actualBg = actual.Bg;
                    var actualDesc = actual.Desc;

                    // Assert
                    Assert.IsNotNull(actualUcr);
                    Assert.IsNotNull(actualBg);
                    Assert.IsNotNull(actualDesc);
                    var actualText = actualDesc.GetASCII(languageCode, countryCode);
                    Assert.AreEqual(expectedText, actualText);
                }
            }
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            double gammaUcr = 2.4, gammaBg = -2.2;
            string languageCode = "en";
            string countryCode = "US";
            string expectedText = "read-tag";

            using (var ucr = ToneCurve.BuildGamma(null, gammaUcr))
            using (var bg = ToneCurve.BuildGamma(null, gammaBg))
            using (var desc = MultiLocalizedUnicode.Create(null))
            {
                desc.SetASCII(languageCode, countryCode, expectedText);

                var target = new UcrBg(ucr, bg, desc);
                using (var profile = Profile.CreatePlaceholder(null))
                {
                    profile.WriteTag(TagSignature.UcrBg, target);

                    // Act
                    var actual = profile.ReadTag<UcrBg>(TagSignature.UcrBg);
                    var actualUcr = actual.Ucr;
                    var actualBg = actual.Bg;
                    var actualDesc = actual.Desc;

                    // Assert
                    Assert.IsNotNull(actualUcr);
                    Assert.IsNotNull(actualBg);
                    Assert.IsNotNull(actualDesc);
                    var actualText = actualDesc.GetASCII(languageCode, countryCode);
                    Assert.AreEqual(expectedText, actualText);
                }
            }
        }
    }
}
