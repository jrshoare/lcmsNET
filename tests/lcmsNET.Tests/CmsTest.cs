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
using System.Linq;

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

        [TestMethod()]
        public void EncodedCMMVersion_WhenGetting_ShouldNotBeZero()
        {
            try
            {
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
        public void Formats_WhenGetting_ShouldNotBeZero()
        {
            // Assert
            // TODO: Work out what these values should be and change assertion to AreEqual..
            Assert.AreNotEqual(0u, Cms.TYPE_ALab_8);
            Assert.AreNotEqual(0u, Cms.TYPE_ALabV2_8);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_16);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_8);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_ABGR_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_ARGB_16);
            Assert.AreNotEqual(0u, Cms.TYPE_ARGB_8);
            Assert.AreNotEqual(0u, Cms.TYPE_ARGB_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_ARGB_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_ARGB_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_16);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_8);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_DBL);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_BGR_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_BGRA_8);
            Assert.AreNotEqual(0u, Cms.TYPE_BGRA_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_BGRA_16);
            Assert.AreNotEqual(0u, Cms.TYPE_BGRA_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_BGRA_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_BGRA_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_CMY_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMY_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_CMY_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMY_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMY_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_16_REV);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_8_REV);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYKA_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK5_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK5_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK5_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK6_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK6_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK6_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK6_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK6_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK7_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK7_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK7_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK8_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK8_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK8_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK9_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK9_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK9_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK10_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK10_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK10_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK11_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK11_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK11_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK12_16);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK12_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK12_8);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_DBL);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_CMYK_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAYA_16);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAYA_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAYA_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAYA_8);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAYA_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_16);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_16_REV);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_8);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_8_REV);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_DBL);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_GRAY_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_HLS_16);
            Assert.AreNotEqual(0u, Cms.TYPE_HLS_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_HLS_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_HLS_8);
            Assert.AreNotEqual(0u, Cms.TYPE_HLS_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_HSV_16);
            Assert.AreNotEqual(0u, Cms.TYPE_HSV_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_HSV_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_HSV_8);
            Assert.AreNotEqual(0u, Cms.TYPE_HSV_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_KCMY_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KCMY_16_REV);
            Assert.AreNotEqual(0u, Cms.TYPE_KCMY_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KCMY_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KCMY_8_REV);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC5_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC5_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC5_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC7_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC7_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC7_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC8_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC8_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC8_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC9_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC9_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC9_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC10_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC10_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC10_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC11_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC11_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC11_8);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC12_16);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC12_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_KYMC12_8);
            Assert.AreNotEqual(0u, Cms.TYPE_Lab_16);
            Assert.AreNotEqual(0u, Cms.TYPE_LabV2_16);
            Assert.AreNotEqual(0u, Cms.TYPE_LabV2_8);
            Assert.AreNotEqual(0u, Cms.TYPE_Lab_8);
            Assert.AreNotEqual(0u, Cms.TYPE_Lab_DBL);
            Assert.AreNotEqual(0u, Cms.TYPE_Lab_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_LabA_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_NAMED_COLOR_INDEX);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_16);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_8);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_DBL);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_RGB_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_16);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_8);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_RGBA_HALF_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_XYZ_16);
            Assert.AreNotEqual(0u, Cms.TYPE_XYZ_DBL);
            Assert.AreNotEqual(0u, Cms.TYPE_XYZ_FLT);
            Assert.AreNotEqual(0u, Cms.TYPE_YCbCr_16);
            Assert.AreNotEqual(0u, Cms.TYPE_YCbCr_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_YCbCr_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_YCbCr_8);
            Assert.AreNotEqual(0u, Cms.TYPE_YCbCr_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_YUV_16);
            Assert.AreNotEqual(0u, Cms.TYPE_YUV_16_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_YUV_16_SE);
            Assert.AreNotEqual(0u, Cms.TYPE_YUV_8);
            Assert.AreNotEqual(0u, Cms.TYPE_YUV_8_PLANAR);
            Assert.AreNotEqual(0u, Cms.TYPE_YUVK_16);
            Assert.AreNotEqual(0u, Cms.TYPE_YUVK_8);
            Assert.AreNotEqual(0u, Cms.TYPE_Yxy_16);
        }

        [TestMethod()]
        public void SetErrorHandler_WhenError_ShouldInvokeHandler()
        {
            // Arrange
            bool handlerInvoked = false;

            // Act
            Cms.SetErrorHandler(HandleError);

            // force error to observe output in Test Explorer results window for this test
            try { Profile.Open(@"???", "r"); } catch { }

            // restore default error handler
            Cms.SetErrorHandler(null);

            // Assert
            Assert.IsTrue(handlerInvoked);

            void HandleError(IntPtr contextID, int errorCode, string errorText)
            {
                handlerInvoked = true;
                TestContext.WriteLine($"contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
            }
        }

        [TestMethod()]
        [DataRow(PixelType.Gray, ColorSpaceSignature.GrayData)]
        [DataRow(PixelType.RGB, ColorSpaceSignature.RgbData)]
        [DataRow(PixelType.CMY, ColorSpaceSignature.CmyData)]
        [DataRow(PixelType.CMYK, ColorSpaceSignature.CmykData)]
        [DataRow(PixelType.YCbCr, ColorSpaceSignature.YCbCrData)]
        [DataRow(PixelType.YUV, ColorSpaceSignature.LuvData)]
        [DataRow(PixelType.XYZ, ColorSpaceSignature.XYZData)]
        [DataRow(PixelType.Lab, ColorSpaceSignature.LabData)]
        [DataRow(PixelType.YUVK, ColorSpaceSignature.LuvKData)]
        [DataRow(PixelType.HSV, ColorSpaceSignature.HsvData)]
        [DataRow(PixelType.HLS, ColorSpaceSignature.HlsData)]
        [DataRow(PixelType.Yxy, ColorSpaceSignature.YxyData)]
        [DataRow(PixelType.MCH1, ColorSpaceSignature.MCH1Data)]
        [DataRow(PixelType.MCH2, ColorSpaceSignature.MCH2Data)]
        [DataRow(PixelType.MCH3, ColorSpaceSignature.MCH3Data)]
        [DataRow(PixelType.MCH4, ColorSpaceSignature.MCH4Data)]
        [DataRow(PixelType.MCH5, ColorSpaceSignature.MCH5Data)]
        [DataRow(PixelType.MCH6, ColorSpaceSignature.MCH6Data)]
        [DataRow(PixelType.MCH7, ColorSpaceSignature.MCH7Data)]
        [DataRow(PixelType.MCH8, ColorSpaceSignature.MCH8Data)]
        [DataRow(PixelType.MCH9, ColorSpaceSignature.MCH9Data)]
        [DataRow(PixelType.MCH10, ColorSpaceSignature.MCHAData)]
        [DataRow(PixelType.MCH11, ColorSpaceSignature.MCHBData)]
        [DataRow(PixelType.MCH12, ColorSpaceSignature.MCHCData)]
        [DataRow(PixelType.MCH13, ColorSpaceSignature.MCHDData)]
        [DataRow(PixelType.MCH14, ColorSpaceSignature.MCHEData)]
        [DataRow(PixelType.MCH15, ColorSpaceSignature.MCHFData)]
        [DataRow(PixelType.LabV2, ColorSpaceSignature.LabData)]
        public void ToColorSpaceSignature_WhenInvoked_ShouldConvertToColorSpaceSignature(PixelType from, ColorSpaceSignature to)
        {
            // Arrange
            ColorSpaceSignature expected = to;

            // Act
            var actual = Cms.ToColorSpaceSignature(from);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(ColorSpaceSignature.GrayData, PixelType.Gray)]
        [DataRow(ColorSpaceSignature.RgbData, PixelType.RGB)]
        [DataRow(ColorSpaceSignature.CmyData, PixelType.CMY)]
        [DataRow(ColorSpaceSignature.CmykData, PixelType.CMYK)]
        [DataRow(ColorSpaceSignature.YCbCrData, PixelType.YCbCr)]
        [DataRow(ColorSpaceSignature.LuvData, PixelType.YUV)]
        [DataRow(ColorSpaceSignature.XYZData, PixelType.XYZ)]
        [DataRow(ColorSpaceSignature.LabData, PixelType.Lab)]
        [DataRow(ColorSpaceSignature.LuvKData, PixelType.YUVK)]
        [DataRow(ColorSpaceSignature.HsvData, PixelType.HSV)]
        [DataRow(ColorSpaceSignature.HlsData, PixelType.HLS)]
        [DataRow(ColorSpaceSignature.YxyData, PixelType.Yxy)]
        [DataRow(ColorSpaceSignature.MCH1Data, PixelType.MCH1)]
        [DataRow(ColorSpaceSignature.MCH2Data, PixelType.MCH2)]
        [DataRow(ColorSpaceSignature.MCH3Data, PixelType.MCH3)]
        [DataRow(ColorSpaceSignature.MCH4Data, PixelType.MCH4)]
        [DataRow(ColorSpaceSignature.MCH5Data, PixelType.MCH5)]
        [DataRow(ColorSpaceSignature.MCH6Data, PixelType.MCH6)]
        [DataRow(ColorSpaceSignature.MCH7Data, PixelType.MCH7)]
        [DataRow(ColorSpaceSignature.MCH8Data, PixelType.MCH8)]
        [DataRow(ColorSpaceSignature.MCH9Data, PixelType.MCH9)]
        [DataRow(ColorSpaceSignature.MCHAData, PixelType.MCH10)]
        [DataRow(ColorSpaceSignature.MCHBData, PixelType.MCH11)]
        [DataRow(ColorSpaceSignature.MCHCData, PixelType.MCH12)]
        [DataRow(ColorSpaceSignature.MCHDData, PixelType.MCH13)]
        [DataRow(ColorSpaceSignature.MCHEData, PixelType.MCH14)]
        [DataRow(ColorSpaceSignature.MCHFData, PixelType.MCH15)]
        public void ToPixelType_WhenInvoked_ShouldConvertToPixelType(ColorSpaceSignature from, PixelType to)
        {
            // Arrange
            PixelType expected = to;

            // Act
            var actual = Cms.ToPixelType(from);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(ColorSpaceSignature.GrayData, 1u)]
        [DataRow(ColorSpaceSignature.RgbData, 3u)]
        [DataRow(ColorSpaceSignature.CmyData, 3u)]
        [DataRow(ColorSpaceSignature.CmykData, 4u)]
        [DataRow(ColorSpaceSignature.YCbCrData, 3u)]
        [DataRow(ColorSpaceSignature.LuvData, 3u)]
        [DataRow(ColorSpaceSignature.XYZData, 3u)]
        [DataRow(ColorSpaceSignature.LabData, 3u)]
        [DataRow(ColorSpaceSignature.LuvKData, 4u)]
        [DataRow(ColorSpaceSignature.HsvData, 3u)]
        [DataRow(ColorSpaceSignature.HlsData, 3u)]
        [DataRow(ColorSpaceSignature.YxyData, 3u)]
        [DataRow(ColorSpaceSignature.MCH1Data, 1u)]
        [DataRow(ColorSpaceSignature.MCH2Data, 2u)]
        [DataRow(ColorSpaceSignature.MCH3Data, 3u)]
        [DataRow(ColorSpaceSignature.MCH4Data, 4u)]
        [DataRow(ColorSpaceSignature.MCH5Data, 5u)]
        [DataRow(ColorSpaceSignature.MCH6Data, 6u)]
        [DataRow(ColorSpaceSignature.MCH7Data, 7u)]
        [DataRow(ColorSpaceSignature.MCH8Data, 8u)]
        [DataRow(ColorSpaceSignature.MCH9Data, 9u)]
        [DataRow(ColorSpaceSignature.MCHAData, 10u)]
        [DataRow(ColorSpaceSignature.MCHBData, 11u)]
        [DataRow(ColorSpaceSignature.MCHCData, 12u)]
        [DataRow(ColorSpaceSignature.MCHDData, 13u)]
        [DataRow(ColorSpaceSignature.MCHEData, 14u)]
        [DataRow(ColorSpaceSignature.MCHFData, 15u)]
        [DataRow(ColorSpaceSignature._1colorData, 1u)]
        [DataRow(ColorSpaceSignature._2colorData, 2u)]
        [DataRow(ColorSpaceSignature._3colorData, 3u)]
        [DataRow(ColorSpaceSignature._4colorData, 4u)]
        [DataRow(ColorSpaceSignature._5colorData, 5u)]
        [DataRow(ColorSpaceSignature._6colorData, 6u)]
        [DataRow(ColorSpaceSignature._7colorData, 7u)]
        [DataRow(ColorSpaceSignature._8colorData, 8u)]
        [DataRow(ColorSpaceSignature._9colorData, 9u)]
        [DataRow(ColorSpaceSignature._10colorData, 10u)]
        [DataRow(ColorSpaceSignature._11colorData, 11u)]
        [DataRow(ColorSpaceSignature._12colorData, 12u)]
        [DataRow(ColorSpaceSignature._13colorData, 13u)]
        [DataRow(ColorSpaceSignature._14colorData, 14u)]
        [DataRow(ColorSpaceSignature._15colorData, 15u)]
        public void ChannelsOf_WhenInvoked_ShouldReturnChannelCount(ColorSpaceSignature from, uint count)
        {
            // Arrange
            uint expected = count;

            // Act
            uint actual = Cms.ChannelsOf(from);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AlarmCodes_WhenGetting_ShouldReturnValuesSet()
        {
            // Arrange
            ushort[] expected = [10, 23, 46, 92, 1007, 2009, 6789, 7212, 8114, 9032, 10556, 11267, 12980, 13084, 14112, 15678];

            // Act
            Cms.AlarmCodes = expected;
            var actual = Cms.AlarmCodes;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AdaptationState_WhenGetting_ShouldReturnValueSet()
        {
            // Arrange
            double expected = 0.7;

            // Act
            Cms.AdaptationState = expected;
            double actual = Cms.AdaptationState;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WhitePointFromTemp_WhenConverting_ShouldSucceed()
        {
            // Arrange
            double tempK = 6504;

            // Act
            bool success = Cms.WhitePointFromTemp(out _, tempK);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void TempFromWhitePoint_WhenConverting_ShouldSucceed()
        {
            // Arrange
            double expected = 6504;
            Cms.WhitePointFromTemp(out CIExyY xyY, expected);

            // Act
            bool success = Cms.TempFromWhitePoint(out _, xyY);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void SupportedIntents_WhenGetting_ShouldReturnNonEmptyCollection()
        {
            // Act
            var supportedIntents = Cms.SupportedIntents;

            // Assert
            Assert.IsNotNull(supportedIntents);
            Assert.AreNotEqual(0, supportedIntents.Count());

            foreach (var (code, description) in supportedIntents)
            {
                TestContext.WriteLine($"code: {code}, description: {description}");
            }
        }
    }
}
