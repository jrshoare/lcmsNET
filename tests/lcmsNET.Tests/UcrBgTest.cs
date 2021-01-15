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

            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            using (var ucr = ToneCurve.BuildParametric(context, type, parameters))
            using (var bg = ToneCurve.BuildGamma(context, gamma))
            using (var desc = MultiLocalizedUnicode.Create(context, nItems))
            {
                desc.SetASCII(languageCode, countryCode, text);

                var expectedUcr = ucr.Handle;
                var expectedBg = bg.Handle;
                var expectedDesc = desc.Handle;

                // Act
                var target = new UcrBg(ucr, bg, desc);
                var actualUcr = target.Ucr;
                var actualBg = target.Bg;
                var actualDesc = target.Desc;

                // Assert
                Assert.AreEqual(expectedUcr, actualUcr);
                Assert.AreEqual(expectedBg, actualBg);
                Assert.AreEqual(expectedDesc, actualDesc);
            }
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            int type = 4;
            double[] parameters = new double[] { 2.4, 1.0 / 1.055, 0.055 / 1.055, 1.0 / 12.92, 0.04045 };
            double gamma = 2.2;
            uint nItems = 0;
            string languageCode = "en";
            string countryCode = "US";
            string expectedText = "from-handle";

            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            using (var ucr = ToneCurve.BuildParametric(context, type, parameters))
            using (var bg = ToneCurve.BuildGamma(context, gamma))
            using (var desc = MultiLocalizedUnicode.Create(context, nItems))
            {
                desc.SetASCII(languageCode, countryCode, expectedText);

                var notExpectedUcr = IntPtr.Zero;
                var notExpectedBg = IntPtr.Zero;
                var notExpectedDesc = IntPtr.Zero;

                var target = new UcrBg(ucr, bg, desc);

                using (var profile = Profile.CreatePlaceholder(null))
                {
                    profile.WriteTag(TagSignature.UcrBg, target);
                    IntPtr handle = profile.ReadTag(TagSignature.UcrBg);

                    // Act
                    var actual = UcrBg.FromHandle(handle);
                    var actualUcr = actual.Ucr;
                    var actualBg = actual.Bg;
                    var actualDesc = actual.Desc;

                    // Assert
                    Assert.AreNotEqual(notExpectedUcr, actualUcr);
                    Assert.AreNotEqual(notExpectedBg, actualBg);
                    Assert.AreNotEqual(notExpectedDesc, actualDesc);
                    var desc2 = MultiLocalizedUnicode.FromHandle(actualDesc);
                    var actualText = desc2.GetASCII(languageCode, countryCode);
                    Assert.AreEqual(expectedText, actualText);
                }
            }
        }
    }
}
