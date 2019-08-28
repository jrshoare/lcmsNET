﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class CmsTest
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
        public void EncodedCMMVersionTest()
        {
            // Arrange

            // Act
            int actual = Cms.EncodedCMMVersion;

            // Assert
            Assert.AreNotEqual(0, actual);
        }

        [TestMethod]
        public void ColorspacesTest()
        {
            // Arrange

            // Act

            // Assert
            // TODO: Work out what these values should be and change assertion to AreEqual..
            Assert.AreNotEqual(0, Cms.TYPE_ALab_8);
            Assert.AreNotEqual(0, Cms.TYPE_ALabV2_8);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_16);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_8);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_ABGR_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_ARGB_16);
            Assert.AreNotEqual(0, Cms.TYPE_ARGB_8);
            Assert.AreNotEqual(0, Cms.TYPE_ARGB_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_ARGB_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_ARGB_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_16);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_8);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_DBL);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_BGR_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_BGRA_8);
            Assert.AreNotEqual(0, Cms.TYPE_BGRA_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_BGRA_16);
            Assert.AreNotEqual(0, Cms.TYPE_BGRA_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_BGRA_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_BGRA_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_CMY_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMY_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_CMY_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMY_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMY_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_16_REV);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_8_REV);
            Assert.AreNotEqual(0, Cms.TYPE_CMYKA_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK5_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK5_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK5_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK6_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK6_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK6_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK6_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK6_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK7_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK7_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK7_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK8_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK8_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK8_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK9_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK9_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK9_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK10_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK10_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK10_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK11_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK11_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK11_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK12_16);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK12_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK12_8);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_DBL);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_CMYK_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_GRAYA_16);
            Assert.AreNotEqual(0, Cms.TYPE_GRAYA_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_GRAYA_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_GRAYA_8);
            Assert.AreNotEqual(0, Cms.TYPE_GRAYA_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_16);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_16_REV);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_8);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_8_REV);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_DBL);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_GRAY_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_HLS_16);
            Assert.AreNotEqual(0, Cms.TYPE_HLS_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_HLS_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_HLS_8);
            Assert.AreNotEqual(0, Cms.TYPE_HLS_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_HSV_16);
            Assert.AreNotEqual(0, Cms.TYPE_HSV_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_HSV_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_HSV_8);
            Assert.AreNotEqual(0, Cms.TYPE_HSV_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_KCMY_16);
            Assert.AreNotEqual(0, Cms.TYPE_KCMY_16_REV);
            Assert.AreNotEqual(0, Cms.TYPE_KCMY_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KCMY_8);
            Assert.AreNotEqual(0, Cms.TYPE_KCMY_8_REV);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC5_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC5_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC5_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC7_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC7_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC7_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC8_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC8_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC8_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC9_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC9_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC9_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC10_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC10_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC10_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC11_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC11_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC11_8);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC12_16);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC12_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_KYMC12_8);
            Assert.AreNotEqual(0, Cms.TYPE_Lab_16);
            Assert.AreNotEqual(0, Cms.TYPE_LabV2_16);
            Assert.AreNotEqual(0, Cms.TYPE_LabV2_8);
            Assert.AreNotEqual(0, Cms.TYPE_Lab_8);
            Assert.AreNotEqual(0, Cms.TYPE_Lab_DBL);
            Assert.AreNotEqual(0, Cms.TYPE_Lab_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_LabA_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_NAMED_COLOR_INDEX);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_16);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_8);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_DBL);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_RGB_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_16);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_8);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_RGBA_HALF_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_XYZ_16);
            Assert.AreNotEqual(0, Cms.TYPE_XYZ_DBL);
            Assert.AreNotEqual(0, Cms.TYPE_XYZ_FLT);
            Assert.AreNotEqual(0, Cms.TYPE_YCbCr_16);
            Assert.AreNotEqual(0, Cms.TYPE_YCbCr_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_YCbCr_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_YCbCr_8);
            Assert.AreNotEqual(0, Cms.TYPE_YCbCr_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_YUV_16);
            Assert.AreNotEqual(0, Cms.TYPE_YUV_16_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_YUV_16_SE);
            Assert.AreNotEqual(0, Cms.TYPE_YUV_8);
            Assert.AreNotEqual(0, Cms.TYPE_YUV_8_PLANAR);
            Assert.AreNotEqual(0, Cms.TYPE_YUVK_16);
            Assert.AreNotEqual(0, Cms.TYPE_YUVK_8);
            Assert.AreNotEqual(0, Cms.TYPE_Yxy_16);
        }

        [TestMethod()]
        public void SetErrorHandlerTest()
        {
            // Arrange

            // Act
            Cms.SetErrorHandler(HandleError);

            // force error to observe output in Test Explorer results window for this test
            try { Profile.Open(@"???", "r"); } catch { }

            // restore default error handler
            Cms.SetErrorHandler(null);

            // Assert
            void HandleError(IntPtr contextID, int errorCode, string errorText)
            {
                TestContext.WriteLine($"contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
            }
        }
    }
}
