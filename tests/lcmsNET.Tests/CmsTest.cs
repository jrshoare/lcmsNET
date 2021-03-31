using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

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
            try
            {
                // Arrange

                // Act
                int actual = Cms.EncodedCMMVersion; // >= 2.8

                // Assert
                Assert.AreNotEqual(0, actual);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
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

        [TestMethod()]
        public void ToColorSpaceSignatureTest()
        {
            // Arrange
            PixelType pixelType = PixelType.RGB;
            ColorSpaceSignature expected = ColorSpaceSignature.RgbData;

            // Act
            var actual = Cms.ToColorSpaceSignature(pixelType);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToPixelTypeTest()
        {
            // Arrange
            ColorSpaceSignature space = ColorSpaceSignature.YCbCrData;
            PixelType expected = PixelType.YCbCr;

            // Act
            var actual = Cms.ToPixelType(space);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ChannelsOfTest()
        {
            // Arrange
            ColorSpaceSignature space = ColorSpaceSignature._11colorData;
            uint expected = 11;

            // Act
            uint actual = Cms.ChannelsOf(space);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AlarmCodesTest()
        {
            // Arrange
            ushort[] alarmCodes = new ushort[16] { 10, 23, 46, 92, 1007, 2009, 6789, 7212, 8114, 9032, 10556, 11267, 12980, 13084, 14112, 15678 };

            // Act
            Cms.AlarmCodes = alarmCodes;
            var values = Cms.AlarmCodes;

            // Assert
            for (int i = 0; i < alarmCodes.Length; i++)
            {
                Assert.AreEqual(alarmCodes[i], values[i]);
            }
        }

        [TestMethod()]
        public void SetAdaptationStateTest()
        {
            // Arrange
            double expected = 0.7;

            // Act
            Cms.AdaptationState = expected;

            // Assert
            double actual = Cms.AdaptationState;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WhitePointFromTempTest()
        {
            // Arrange
            double tempK = 6504;

            // Act
            bool success = Cms.WhitePointFromTemp(out CIExyY xyY, tempK);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void TempFromWhitePointTest()
        {
            // Arrange
            double expected = 6504;
            Cms.WhitePointFromTemp(out CIExyY xyY, expected);

            // Act
            bool success = Cms.TempFromWhitePoint(out double actual, xyY);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void ICCMeasurementConditions_FromHandleTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new ICCMeasurementConditions
                {
                    Observer = Observer.CIE1931,
                    Backing = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 },
                    Geometry = MeasurementGeometry.ZeroDOrDZero,
                    Flare = 0.5,
                    IlluminantType = IlluminantType.D65
                };

                profile.WriteTag(TagSignature.Measurement, expected);
                var tag = profile.ReadTag(TagSignature.Measurement);

                // Act
                var actual = ICCMeasurementConditions.FromHandle(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ICCViewingConditions_FromHandleTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new ICCViewingConditions
                {
                    IlluminantXYZ = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 },
                    SurroundXYZ = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 },
                    IlluminantType = IlluminantType.E
                };

                profile.WriteTag(TagSignature.ViewingConditions, expected);
                var tag = profile.ReadTag(TagSignature.ViewingConditions);

                // Act
                var actual = ICCViewingConditions.FromHandle(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
