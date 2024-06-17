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
using System.Linq;
using System.Runtime.InteropServices;
using static lcmsNET.Tests.TestUtils.MultiLocalizedUnicodeUtils;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ProfileTest
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

        [StructLayout(LayoutKind.Sequential)]
        private struct TestCIEXYZ
        {
            [MarshalAs(UnmanagedType.R8)]
            public double X;
            [MarshalAs(UnmanagedType.R8)]
            public double Y;
            [MarshalAs(UnmanagedType.R8)]
            public double Z;

            // Must NOT contain an 'internal static TestCIEXYZ FromHandle(IntPtr)' method
        }

        [TestMethod()]
        public void CreatePlaceholder_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreatePlaceholder(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateRGB_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            CIExyY whitePoint = Colorimetric.D50_xyY;
            CIExyYTRIPLE primaries = new()
            {
                Red = new() { x = 0.64, y = 0.33, Y = 1 },
                Green = new() { x = 0.21, y = 0.71, Y = 1 },
                Blue = new() { x = 0.15, y = 0.06, Y = 1 }
            };

            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, 2.19921875);
            ToneCurve[] transferFunction = [toneCurve, toneCurve, toneCurve];

            // Act
            using var sut = Profile.CreateRGB(whitePoint, primaries, transferFunction);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateRGB_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            CIExyY whitePoint = Colorimetric.D50_xyY;
            CIExyYTRIPLE primaries = new()
            {
                Red = new() { x = 0.64, y = 0.33, Y = 1 },
                Green = new() { x = 0.21, y = 0.71, Y = 1 },
                Blue = new() { x = 0.15, y = 0.06, Y = 1 }
            };

            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, 2.19921875);
            ToneCurve[] transferFunction = [toneCurve, toneCurve, toneCurve];

            // Act
            using var sut = Profile.CreateRGB(context, whitePoint, primaries, transferFunction);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateGray_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            CIExyY whitePoint = Colorimetric.D50_xyY;

            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, 2.19921875);

            // Act
            using var sut = Profile.CreateGray(whitePoint, toneCurve);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateGray_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            CIExyY whitePoint = Colorimetric.D50_xyY;

            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, 2.19921875);

            // Act
            using var sut = Profile.CreateGray(context, whitePoint, toneCurve);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateLinearizationDeviceLink_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            ColorSpaceSignature space = ColorSpaceSignature.CmykData;

            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, 3.0);
            ToneCurve[] transferFunction = [toneCurve, toneCurve, toneCurve, toneCurve];

            // Act
            using var sut = Profile.CreateLinearizationDeviceLink(space, transferFunction);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateLinearizationDeviceLink_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            ColorSpaceSignature space = ColorSpaceSignature.CmykData;

            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, 3.0);
            ToneCurve[] transferFunction = [toneCurve, toneCurve, toneCurve, toneCurve];

            // Act
            using var sut = Profile.CreateLinearizationDeviceLink(context, space, transferFunction);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateInkLimitingDeviceLink_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.CreateInkLimitingDeviceLink(ColorSpaceSignature.CmykData, 150.0);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateInkLimitingDeviceLink_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreateInkLimitingDeviceLink(context, ColorSpaceSignature.CmykData, 150.0);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        // At the time of writing these tests are excluded because an i/o exception occurs at runtime
        // due to Little CMS (2.16) leaving the file open, so do not re-enable until this is fixed!
#if false
        [TestMethod()]
        public void CreateDeviceLinkFromCubeFile_WhenInstantiated_ShouldHaveValidHandle()
        {
            try
            {
                // Arrange
                ResourceUtils.SaveTemporarily(".Resources.Aqua.cube", "Aqua.cube", (cubePath) =>
                {
                    // Act
                    using var sut = Profile.CreateDeviceLinkFromCubeFile(cubePath);

                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                });
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }

        [TestMethod()]
        public void CreateDeviceLinkFromCubeFile_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            try
            {
                // Arrange
                ResourceUtils.SaveTemporarily(".Resources.Aqua.cube", "Aqua.cube", (cubePath) =>
                {
                    using var context = ContextUtils.CreateContext();

                    // Act
                    using var sut = Profile.CreateDeviceLinkFromCubeFile(context, cubePath);

                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                });
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }
#endif

        [TestMethod()]
        public void CreateDeviceLink_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
                ResourceUtils.SaveTemporarily(".Resources.Lab.icc", "lab.icc", (labPath) =>
                {
                    using var srgb = Profile.Open(srgbPath, "r");
                    using var lab = Profile.Open(labPath, "r");
                    using var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                                Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None);

                    // Act
                    using var sut = Profile.CreateDeviceLink(transform, version: 3.4, CmsFlags.None);

                    // Assert
                    Assert.IsFalse(sut.IsInvalid);
                })
            );
        }

        [TestMethod()]
        public void CreateLab2_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.CreateLab2(Colorimetric.D50_xyY);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateLab2_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreateLab2(context, Colorimetric.D50_xyY);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateLab4_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.CreateLab4(Colorimetric.D50_xyY);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateLab4_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreateLab2(context, Colorimetric.D50_xyY);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateXYZ_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.CreateXYZ();

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateXYZ_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreateXYZ(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreatesRGB_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.Create_sRGB();

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreatesRGB_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.Create_sRGB(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateNull_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.CreateNull();

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateNull_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreateNull(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateBCHSWabstract_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Profile.CreateBCHSWabstract(nLutPoints: 17, bright: 0.0, contrast: 1.2,
                    hue: 0.0, saturation: 3.0, tempSrc: 5000, tempDest: 5000);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateBCHSWabstract_WhenInstantiatedWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.CreateBCHSWabstract(context, nLutPoints: 17, bright: 0.0, contrast: 1.2,
                    hue: 0.0, saturation: 3.0, tempSrc: 5000, tempDest: 5000);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void CreateOkLab_WhenInstantiated_ShouldHaveValidHandle()
        {
            try
            {
                // Arrange
                using var context = ContextUtils.CreateContext();

                // Act
                using var sut = Profile.Create_OkLab(context);

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }

        [TestMethod()]
        public void Open_WhenFromFile_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                // Act
                using var sut = Profile.Open(srgbPath, "r");

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            });
        }

        [TestMethod()]
        public void Open_WhenFromFileWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var context = ContextUtils.CreateContext();

                // Act
                using var sut = Profile.Open(context, srgbPath, "r");

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            });
        }

        [TestMethod()]
        public void Open_WhenFromMemory_ShouldHaveValidHandle()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");

            // Act
            using var sut = Profile.Open(ms.GetBuffer());

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Open_WhenFromMemoryWithContext_ShouldHaveValidHandle()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Profile.Open(context, ms.GetBuffer());

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Open_WhenUsingIoHandlerReadOnly_ShouldHaveValidHandle()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var context = ContextUtils.CreateContext();

                using var iohandler = IOHandler.Open(context, srgbPath, "r");
                using var sut = Profile.Open(context, iohandler);

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            });
        }

        [TestMethod()]
        public void Open_WhenUsingIoHandlerWritable_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var iohandler = IOHandler.Open(context);

            // Act
            using var sut = Profile.Open(context, iohandler, true);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Save_WhenToFile_ShouldCreateFile()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                var savePath = Path.Combine(Path.GetDirectoryName(srgbPath), "saved.icc");
                bool result = sut.Save(savePath);

                // Assert
                Assert.IsTrue(result);
                Assert.IsTrue(File.Exists(savePath));
            });
        }

        [TestMethod()]
        public void Save_WhenToMemory_ShouldWriteToMemory()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");
            byte[] source = ms.GetBuffer();
            using var sut = Profile.Open(source);
            uint expected = (uint)(source.Length + 1); // add 1 for '\0' termination
            byte[] destination = new byte[expected];

            // Act
            var result = sut.Save(destination, out uint actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Save_WhenToNull_ShouldReturnProfileSize()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");
            byte[] memory = ms.GetBuffer();
            using var sut = Profile.Open(memory);

            // oddly the method returns the size of the profile ignoring
            // the fact that it always terminates with `\0`
            uint expected = (uint)memory.Length;

            // Act
            var result = sut.Save(null, out uint actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Save_WhenUsingIoHandler_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");
                using var iohandler = IOHandler.Open(context: null);
                var bytesToWrite = sut.Save(iohandler: null);
                var bytesWritten = sut.Save(iohandler);

                // Assert
                Assert.AreNotEqual(0u, bytesWritten);
                Assert.AreEqual(bytesToWrite, bytesWritten);
            });
        }

        [TestMethod()]
        public void Open_WhenFromFileWithContext_ShouldHaveNonNullContext()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var expected = ContextUtils.CreateContext();
                using var sut = Profile.Open(expected, srgbPath, "r");
                var actual = sut.Context;

                // Assert
                Assert.AreSame(expected, actual);
            });
        }

        [TestMethod()]
        public void ColorSpace_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            var expected = ColorSpaceSignature.CmykData;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            sut.ColorSpace = expected;
            var actual = sut.ColorSpace;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PCS_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            var expected = ColorSpaceSignature.XYZData;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            sut.PCS = expected;
            var actual = sut.PCS;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(InfoType.Description, "sRGB IEC61966-2.1")]
        [DataRow(InfoType.Manufacturer, "IEC http://www.iec.ch")]
        [DataRow(InfoType.Model, "IEC 61966-2.1 Default RGB colour space - sRGB")]
        [DataRow(InfoType.Copyright, "Copyright (c) 1998 Hewlett-Packard Company")]
        public void GetProfileInfo_WhenInvoked_ShouldGetInfoTypeRequested(InfoType infoType, string expected)
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");
            byte[] memory = ms.GetBuffer();
            using var sut = Profile.Open(memory);

            // Act
            var actual = sut.GetProfileInfo(infoType, "en", "US");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(InfoType.Description, "sRGB IEC61966-2.1")]
        [DataRow(InfoType.Manufacturer, "IEC http://www.iec.ch")]
        [DataRow(InfoType.Model, "IEC 61966-2.1 Default RGB colour space - sRGB")]
        [DataRow(InfoType.Copyright, "Copyright (c) 1998 Hewlett-Packard Company")]
        public void GetProfileInfoASCII_WhenInvoked_ShouldGetInfoTypeRequested(InfoType infoType, string expected)
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");
            byte[] memory = ms.GetBuffer();
            using var sut = Profile.Open(memory);

            // Act
            var actual = sut.GetProfileInfoASCII(infoType, "en", "US");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DetectBlackPoint_WhenValid_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                bool detected = sut.DetectBlackPoint(out CIEXYZ blackPoint, Intent.RelativeColorimetric);

                // Assert
                Assert.IsTrue(detected);
            });
        }

        [TestMethod()]
        public void DetectDestinationBlackPoint_WhenValid_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.D50_XYZ.icc", "D50_XYZ.icc", (xyzPath) =>
            {
                using var sut = Profile.Open(xyzPath, "r");

                // Act
                bool detected = sut.DetectDestinationBlackPoint(out CIEXYZ blackPoint, Intent.RelativeColorimetric);

                // Assert
                Assert.IsTrue(detected);
            });
        }

        [TestMethod()]
        public void TotalAreaCoverage_WhenValid_ShouldSucceed()
        {
            // Arrange
            double expected = 0.0;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.DeviceClass = ProfileClassSignature.Output;

            // Act
            double actual = sut.TotalAreaCoverage;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeviceClass_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            var expected = ProfileClassSignature.Abstract;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.DeviceClass = expected;

            // Act
            var actual = sut.DeviceClass;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHeaderCreationDateTime_WhenInvoked_ShouldReturnCreationDateTime()
        {
            // Arrange
            var notExpected = DateTime.MinValue;
            DateTime now = DateTime.UtcNow - TimeSpan.FromMinutes(10);

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            bool obtained = sut.GetHeaderCreationDateTime(out DateTime actual);

            // Assert
            Assert.IsTrue(obtained);
            Assert.AreNotEqual(notExpected, actual);
            Assert.IsTrue(actual >= now);
        }

        [TestMethod()]
        public void HeaderFlags_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x2;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.HeaderFlags = expected;

            // Act
            var actual = sut.HeaderFlags;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HeaderManufacturer_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x54657374; // 'Test'

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.HeaderManufacturer = expected;

            // Act
            var actual = sut.HeaderManufacturer;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HeaderModel_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x6D6F646C; // 'modl'

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.HeaderModel = expected;

            // Act
            var actual = sut.HeaderModel;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HeaderAttributes_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            DeviceAttributes expected = DeviceAttributes.Reflective | DeviceAttributes.Matte;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.HeaderAttributes = expected;

            // Act
            var actual = sut.HeaderAttributes;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Version_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            double expected = 4.3;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.Version = expected;

            // Act
            var actual = sut.Version;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EncodedICCVersion_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            uint expected = 0x4000000;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);
            sut.EncodedICCVersion = expected;

            // Act
            var actual = sut.EncodedICCVersion;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsMatrixShaper_WhenMatrixShaperNotPresent_ShouldReturnFalse()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            bool isMatrixShaper = sut.IsMatrixShaper;

            // Assert
            Assert.IsFalse(isMatrixShaper);
        }

        [TestMethod()]
        public void IsCLUT_WhenCLUTNotPresent_ShouldReturnFalse()
        {
            // Arrange
            Intent intent = Intent.RelativeColorimetric;
            UsedDirection usedDirection = UsedDirection.AsInput;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            bool isCLUT = sut.IsCLUT(intent, usedDirection);

            // Assert
            Assert.IsFalse(isCLUT);
        }

        [TestMethod()]
        public void TagCount_WhenNoTags_ShouldReturnZero()
        {
            // Arrange
            int expected = 0;

            using var context = ContextUtils.CreateContext();
            using var profile = Profile.CreatePlaceholder(context);

            // Act
            int actual = profile.TagCount;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTag_WhenInvoked_ShouldReturnTagAtIndex()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                for (int i = 0; i < sut.TagCount; i++)
                {
                    TagSignature tag = sut.GetTag(Convert.ToUInt32(i));
                    TestContext.WriteLine($"tag: 0x{tag:X}");
                }
            });
        }

        [TestMethod()]
        public void HasTag_WhenTagIsPresent_ShouldReturnTrue()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                bool hasTag = sut.HasTag(TagSignature.MediaWhitePoint);

                // Assert
                Assert.IsTrue(hasTag);
            });
        }

        [TestMethod()]
        public void ReadTag_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                IntPtr notExpected = IntPtr.Zero;
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                IntPtr actual = sut.ReadTag(TagSignature.MediaWhitePoint);

                // Assert
                Assert.AreNotEqual(notExpected, actual);
            });
        }

        [TestMethod()]
        public void ReadTagT_WhenInvoked_ShouldSucceed()
        {
            using var sut = Profile.CreatePlaceholder(context: null);
            CIEXYZTRIPLE expected = new() 
            {
                Red = new() { X = 0.8322, Y = 1.0, Z = 0.7765 },
                Green = new() { X = 0.9642, Y = 1.0, Z = 0.8249 },
                Blue = new() { X = 0.7352, Y = 1.0, Z = 0.6115 }
            };

            sut.WriteTag(TagSignature.ChromaticAdaptation, expected);

            // Act
            var actual = sut.ReadTag<CIEXYZTRIPLE>(TagSignature.ChromaticAdaptation);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTagT_WhenFromHandleMethodMissing_ShouldThrowMissingMethodException()
        {
            using var sut = Profile.CreatePlaceholder(context: null);
            CIEXYZ xyz = new()  { X = 0.8322, Y = 1.0, Z = 0.7765 };

            sut.WriteTag(TagSignature.BlueColorant, xyz);

            // Act & Assert
            var actual = Assert.ThrowsException<MissingMethodException>(() =>
                sut.ReadTag<TestCIEXYZ>(TagSignature.BlueColorant));
        }

        [TestMethod()]
        public void ReadTagTTagNotFound()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder();

            // Act & Assert
            var actual = Assert.ThrowsException<LcmsNETException>(
                    () => sut.ReadTag<Dict>(TagSignature.Meta));
        }

        [TestMethod()]
        public void ReadTag_WhenICCDataIsASCII_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(context: null);
            var expected = "ascii data";
            var iccData = new ICCData(expected);

            // do not use TagSignature.Data as this is not supported
            sut.WriteTag(TagSignature.Ps2CRD0, iccData);

            // Act
            // implicit call to FromHandle
            var iccData2 = sut.ReadTag<ICCData>(TagSignature.Ps2CRD0);
            var actual = (string)iccData2;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenICCDataIsBinary_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(context: null);
            byte[] expected = [17, 99, 0, 253, 122, 19];
            var iccData = new ICCData(expected);

            // do not use TagSignature.Data as this is not supported
            sut.WriteTag(TagSignature.Ps2CRD2, iccData);

            // Act
            // implicit call to FromHandle
            var iccData2 = sut.ReadTag<ICCData>(TagSignature.Ps2CRD2);
            var actual = (byte[])iccData2;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenICCMeasurementConditions_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(context: null);
            ICCMeasurementConditions expected = new()
            {
                Observer = Observer.CIE1931,
                Backing = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 },
                Geometry = MeasurementGeometry.ZeroDOrDZero,
                Flare = 0.5,
                IlluminantType = IlluminantType.D65
            };

            sut.WriteTag(TagSignature.Measurement, expected);

            // Act
            // implicit call to FromHandle
            var actual = sut.ReadTag<ICCMeasurementConditions>(TagSignature.Measurement);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenICCViewingConditions_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(context: null);
            var expected = new ICCViewingConditions
            {
                IlluminantXYZ = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 },
                SurroundXYZ = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 },
                IlluminantType = IlluminantType.E
            };

            sut.WriteTag(TagSignature.ViewingConditions, expected);

            // Act
            // implicit call to FromHandle
            var actual = sut.ReadTag<ICCViewingConditions>(TagSignature.ViewingConditions);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenMultiLocalizedUnicode_ShouldReturnValueWritten()
        {
            // Arrange
            string expected = "sRGB IEC61966-2.1";  // from Resources/sRGB.icc

            using MemoryStream ms = ResourceUtils.Save(".Resources.sRGB.icc");
            using var sut = Profile.Open(ms.GetBuffer());

            // Act
            // implicit call to FromHandle
            using var mlu = sut.ReadTag<MultiLocalizedUnicode>(TagSignature.ProfileDescription);
            string actual = mlu.GetASCII(MultiLocalizedUnicode.NoLanguage, MultiLocalizedUnicode.NoCountry);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenNamedColorList_ShouldReturnValueWritten()
        {
            // Arrange
            int expected = 23;

            using var sut = Profile.CreatePlaceholder(context: null);
            using (var ncl = NamedColorList.Create(null, 256, 3, "pre", "post"))
            {
                for (uint i = 0; i < 256; i++)
                {
                    ushort[] pcs = [(ushort)i, (ushort)i, (ushort)i];
                    ushort[] colorant = new ushort[16];
                    colorant[0] = colorant[1] = colorant[2] = (ushort)i;

                    bool added = ncl.Add($"#{i}", pcs, colorant);
                }

                sut.WriteTag(TagSignature.NamedColor2, ncl);
            }

            // Act
            // implicit call to FromHandle
            using var nc2 = sut.ReadTag<NamedColorList>(TagSignature.NamedColor2);

            // Assert
            int actual = nc2[$"#{expected}"];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenPipeline_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreateInkLimitingDeviceLink(ColorSpaceSignature.CmykData, 150.0);
            sut.LinkTag(TagSignature.AToB1, TagSignature.AToB0);

            // Act
            // implicit call to FromHandle
            using var pipeline = sut.ReadTag<Pipeline>(TagSignature.AToB1);

            // Assert
            Assert.IsNotNull(pipeline);
        }

        [TestMethod()]
        public void ReadTag_WhenProfileSequenceDescriptor_ShouldReturnValueWritten()
        {
            // Arrange
            uint expected = 1;

            using var sut = Profile.CreatePlaceholder(context: null);
            using (var psd = ProfileSequenceDescriptor.Create(context: null, expected))
            {
                var item = psd[0];
                item.Attributes = DeviceAttributes.Transparency | DeviceAttributes.Matte;
                item.Manufacturer = ProfileSequenceDescriptorUtils.Create(0);
                item.Model = ProfileSequenceDescriptorUtils.Create(0);

                sut.WriteTag(TagSignature.ProfileSequenceDesc, psd);
            }

            // Act
            // implicit call to FromHandle
            using var psd1 = sut.ReadTag<ProfileSequenceDescriptor>(TagSignature.ProfileSequenceDesc);

            // Assert
            uint actual = psd1.Length;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenScreening_ShouldReturnValueWritten()
        {
            var expected = ScreeningUtils.CreateScreening();

            using var sut = Profile.CreatePlaceholder(context: null);
            sut.WriteTag(TagSignature.Screening, expected);

            // Act
            // implicit call to FromHandle
            var screening = sut.ReadTag<Screening>(TagSignature.Screening);
            var flag = screening.Flag;
            var nChannels = screening.nChannels;
            var channels = screening.Channels;

            // Assert
            Assert.AreEqual(expected.Flag, flag);
            Assert.AreEqual(expected.nChannels, nChannels);
            CollectionAssert.AreEqual(expected.Channels, channels);
        }

        [TestMethod()]
        public void ReadTag_WhenSignature_ShouldReturnValueWritten()
        {
            // Arrange
            var expected = (Signature)0xF32794E2;

            using var sut = Profile.CreatePlaceholder(context: null);
            sut.WriteTag(TagSignature.Technology, expected);

            // Act
            // implicit call to FromHandle
            var actual = sut.ReadTag<Signature>(TagSignature.Technology);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenTm_ShouldReturnValueWritten()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            var expected = new DateTime(2021, 1, 8, 10, 4, 32);
            Tm tm = new(expected);

            sut.WriteTag(TagSignature.CalibrationDateTime, tm);

            // Act
            // implicit call to FromHandle
            var tm1 = sut.ReadTag<Tm>(TagSignature.CalibrationDateTime);
            DateTime actual = tm1;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenToneCurve_ShouldReturnValueWritten()
        {
            // Arrange
            double expected = 2.2;
            double precision = 0.01;

            using var sut = Profile.CreatePlaceholder(context: null);
            using (var toneCurve = ToneCurve.BuildGamma(context: null, expected))
            {
                sut.WriteTag(TagSignature.RedTRC, toneCurve);
            }

            // Act
            using var toneCurve1 = sut.ReadTag<ToneCurve>(TagSignature.RedTRC);

            // Assert
            var actual = toneCurve1.EstimateGamma(precision);
            Assert.AreEqual(expected, actual, precision);
        }

        [TestMethod()]
        public void ReadTag_WhenUcrBg_ShouldReturnValueWritten()
        {
            // Arrange
            double gammaUcr = 2.4, gammaBg = -2.2;
            DisplayName displayName = new("ucrbg");

            using var ucr = ToneCurve.BuildGamma(context: null, gammaUcr);
            using var bg = ToneCurve.BuildGamma(context: null, gammaBg);
            using var desc = CreateAsASCII(displayName);

            var ucrbg = new UcrBg(ucr, bg, desc);
            using var sut = Profile.CreatePlaceholder(context: null);
            sut.WriteTag(TagSignature.UcrBg, ucrbg);

            // Act
            // implicit call to FromHandle
            var ucrbg1 = sut.ReadTag<UcrBg>(TagSignature.UcrBg);
            var actualUcr = ucrbg1.Ucr;
            var actualBg = ucrbg1.Bg;
            var actualDesc = ucrbg1.Desc;

            // Assert
            Assert.IsNotNull(actualUcr);
            Assert.IsNotNull(actualBg);
            var actualText = actualDesc.GetASCII(displayName.LanguageCode, displayName.CountryCode);
            Assert.AreEqual(displayName.Value, actualText);
        }


        [TestMethod()]
        public void ReadTag_WhenVideoCardGamma_ShouldReturnValueWritten()
        {
            // Arrange
            int type = 5;
            double[] parameters = [0.45, Math.Pow(1.099, 1.0 / 0.45), 0.0, 4.5, 0.018, -0.099, 0.0];

            using var red = ToneCurve.BuildParametric(context: null, type, parameters);
            using var green = ToneCurve.BuildParametric(context: null, type, parameters);
            using var blue = ToneCurve.BuildParametric(context: null, type, parameters);
            var vcg = new VideoCardGamma(red, green, blue);

            using var sut = Profile.CreatePlaceholder(context: null);
            sut.WriteTag(TagSignature.Vcgt, vcg);

            // Act
            // implicit call to FromHandle
            var vcg1 = sut.ReadTag<VideoCardGamma>(TagSignature.Vcgt);
            var actualRed = vcg1.Red;
            var actualGreen = vcg1.Green;
            var actualBlue = vcg1.Blue;

            // Assert
            Assert.IsNotNull(actualRed);
            Assert.IsNotNull(actualGreen);
            Assert.IsNotNull(actualBlue);
        }

        [TestMethod()]
        public void ReadTag_WhenVideoSignalType_ShouldReturnValueWritten()
        {
            try
            {
                // Arrange
                using var sut = Profile.CreatePlaceholder(context: null);
                var expected = new VideoSignalType
                {
                    ColourPrimaries = 1,
                    TransferCharacteristics = 13,
                    MatrixCoefficients = 0,
                    VideoFullRangeFlag = 1
                };

                sut.WriteTag(TagSignature.Cicp, expected);

                // Act
                // implicit call to FromHandle
                var actual = sut.ReadTag<VideoSignalType>(TagSignature.Cicp);

                // Assert
                Assert.AreEqual(expected, actual);
            }
            catch (LcmsNETException)
            {
                Assert.Inconclusive("Possibly requires later version of Little CMS.");
            }
        }

        // https://www.argyllcms.com/doc/ArgyllCMS_arts_tag.html
        [TestMethod()]
        public void ReadTag_WhenCIEXYZTRIPLE_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(null);

            CIEXYZTRIPLE expected = new()   // Bradford matrix
            {
                Red = new() { X = 0.89509583, Y = 0.26640320, Z = -0.16140747 },
                Green = new() { X = -0.75019836, Y = 1.71350098, Z = 0.03669739 },
                Blue = new() { X = 0.03889465, Y = -0.06849670, Z = 1.02960205 }
            };

            // Act
            sut.WriteTag(TagSignature.ArgyllArts, expected);
            var actual = sut.ReadTag<CIEXYZTRIPLE>(TagSignature.ArgyllArts);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenColorantOrder_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(null);
            byte[] expected = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

            sut.WriteTag(TagSignature.ColorantOrder, (ColorantOrder)expected);

            // Act
            // implicit call to FromHandle
            byte[] actual = sut.ReadTag<ColorantOrder>(TagSignature.ColorantOrder);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenCIEXYZ_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(null);
            var expected = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

            sut.WriteTag(TagSignature.BlueColorant, expected);

            // Act
            // implicit call to FromHandle
            var actual = sut.ReadTag<CIEXYZ>(TagSignature.BlueColorant);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenCIExyYTRIPLE_ShouldReturnValueWritten()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(null);
            var expected = new CIExyYTRIPLE
            {
                Red = new() { x = 0.64, y = 0.33, Y = 1 },
                Green = new() { x = 0.21, y = 0.71, Y = 1 },
                Blue = new() { x = 0.15, y = 0.06, Y = 1 }
            };

            sut.WriteTag(TagSignature.Chromaticity, expected);

            // Act
            // implicit call to FromHandle
            var actual = sut.ReadTag<CIExyYTRIPLE>(TagSignature.Chromaticity);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTag_WhenDict_ShouldReturnValueWritten()
        {
            // Arrange
            int expected = 3;

            using var sut = Profile.CreatePlaceholder(null);

            using (var dict = DictUtils.CreateDict())
            using (var mlu = CreateAsASCII(Constants.Profile.DisplayName))
            {
                dict.Add("first", value: null, displayName: null, displayValue: null);
                dict.Add("second", "second-value", displayName: null, displayValue: null);
                dict.Add("third", "third-value", mlu, displayValue: null);

                sut.WriteTag(TagSignature.Meta, dict);
            }

            // Act
            // implicit call to FromHandle
            using var dict1 = sut.ReadTag<Dict>(TagSignature.Meta);

            // Assert
            int actual = dict1.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteTag_WhenCIEXYZ_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");
                var whitePoint = sut.ReadTag<CIEXYZ>(TagSignature.MediaWhitePoint);

                // Act
                bool written = sut.WriteTag(TagSignature.MediaWhitePoint, whitePoint);

                // Assert
                Assert.IsTrue(written);
            });
        }

        [TestMethod()]
        public void Write_WhenCIEXYZTRIPLE_ShouldSucceed()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(null);
            CIEXYZTRIPLE expected = new()
            {
                Red = new() { X = 0.8322, Y = 1.0, Z = 0.7765 },
                Green = new() { X = 0.9642, Y = 1.0, Z = 0.8249 },
                Blue = new() { X = 0.7352, Y = 1.0, Z = 0.6115 }
            };

            // Act
            bool written = sut.WriteTag(TagSignature.ChromaticAdaptation, expected);

            // Assert
            Assert.IsTrue(written);
        }

        [TestMethod()]
        public void Write_WhenICCData_ShouldSucceed()
        {
            // Arrange
            using var sut = Profile.CreatePlaceholder(null);
            byte[] expected = [17, 99, 0, 253, 122, 19];
            var iccData = new ICCData(expected);

            // Act
            // do not use TagSignature.Data as this is not supported
            bool written = sut.WriteTag(TagSignature.Ps2CRD0, iccData);

            // Assert
            Assert.IsTrue(written);
        }

        [TestMethod()]
        public void WriteTag_WhenProfileSequenceDescriptor_ShouldSucceed()
        {
            // Arrange
            uint nItems = 3;

            using var sut = Profile.CreatePlaceholder(context: null);
            using var psd = ProfileSequenceDescriptor.Create(context: null, nItems);
            var item = psd[0];
            item.Attributes = DeviceAttributes.Transparency | DeviceAttributes.Matte;
            item.Manufacturer = ProfileSequenceDescriptorUtils.Create(0);
            item.Model = ProfileSequenceDescriptorUtils.Create(0);

            item = psd[1];
            item.Attributes = DeviceAttributes.Reflective | DeviceAttributes.Matte;
            item.Manufacturer = ProfileSequenceDescriptorUtils.Create(1);
            item.Model = ProfileSequenceDescriptorUtils.Create(1);

            item = psd[2];
            item.Attributes = DeviceAttributes.Transparency | DeviceAttributes.Glossy;
            item.Manufacturer = ProfileSequenceDescriptorUtils.Create(2);
            item.Model = ProfileSequenceDescriptorUtils.Create(2);

            // Act
            bool written = sut.WriteTag(TagSignature.ProfileSequenceDesc, psd);

            // Assert
            Assert.IsTrue(written);
        }

        [TestMethod()]
        public void LinkTag_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreateInkLimitingDeviceLink(context, ColorSpaceSignature.CmykData, limit: 150.0);

            // Act
            bool linked = sut.LinkTag(TagSignature.AToB1, TagSignature.AToB0);

            // Assert
            Assert.IsTrue(linked);
        }

        [TestMethod()]
        public void TagLinkedTo_WhenTagIsLinked_ShouldReturnLinkedTag()
        {
            // Arrange
            TagSignature expected = TagSignature.AToB0;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreateInkLimitingDeviceLink(context, ColorSpaceSignature.CmykData, 150.0);
            TagSignature tag = TagSignature.AToB1;
            sut.LinkTag(tag, expected);

            // Act
            TagSignature actual = sut.TagLinkedTo(tag);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void HeaderRenderingIntent_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            Intent expected = Intent.Perceptual;

            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            Intent actual = sut.HeaderRenderingIntent;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsIntentSupported_WhenNotSupported_ShouldReturnFalse()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = Profile.CreatePlaceholder(context);

            // Act
            bool supported = sut.IsIntentSupported(Intent.Perceptual, UsedDirection.AsInput);

            // Assert
            Assert.IsFalse(supported);
        }

        [TestMethod()]
        public void ComputeMD5_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                bool computed = sut.ComputeMD5();

                // Assert
                Assert.IsTrue(computed);
            });
        }

        [TestMethod()]
        public void HeaderProfileID_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                byte[] expected = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 12, 13, 14, 15];

                // Act
                sut.HeaderProfileID = expected;
                byte[] actual = sut.HeaderProfileID;

                // Assert
                CollectionAssert.AreEqual(expected, actual);
            });
        }

        [TestMethod()]
        public void IOHandler_WhenInvoked_ShouldReturnIOHandlerUsed()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                IOHandler iohandler = sut.IOHandler;

                // Assert
                Assert.IsNotNull(iohandler);
            });
        }

        [TestMethod()]
        public void GetPostScriptColorResource_WhenInvoked_ShouldCreatePostScriptColorResource()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                uint notExpected = 0;

                using var context = ContextUtils.CreateContext();
                using var iohandler = IOHandler.Open(context);
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                uint actual = sut.GetPostScriptColorResource(context, PostScriptResourceType.ColorRenderingDictionary,
                        Intent.RelativeColorimetric, CmsFlags.None, iohandler);

                // Assert
                Assert.AreNotEqual(notExpected, actual);
            });
        }

        [TestMethod()]
        public void GetPostScriptColorSpaceArray_WhenInvoked_ShouldCreatePostScriptColorSpaceArray()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                int notExpected = 0;

                using var context = ContextUtils.CreateContext();
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                byte[] actual = sut.GetPostScriptColorSpaceArray(context, Intent.RelativeColorimetric, CmsFlags.None);

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreNotEqual(notExpected, actual.Length);
            });
        }

        [TestMethod()]
        public void GetPostScriptColorRenderingDictionary_WhenInvoked_ShouldCreatePostScriptColorRenderingDictionary()
        {
            // Arrange
            ResourceUtils.SaveTemporarily(".Resources.sRGB.icc", "srgb.icc", (srgbPath) =>
            {
                int notExpected = 0;

                using var context = ContextUtils.CreateContext();
                using var sut = Profile.Open(srgbPath, "r");

                // Act
                byte[] actual = sut.GetPostScriptColorRenderingDictionary(context, Intent.AbsoluteColorimetric, CmsFlags.None);

                // Assert
                Assert.IsNotNull(actual);
                Assert.AreNotEqual(notExpected, actual.Length);
            });
        }

        [TestMethod()]
        public void DetectRGBGamma_WhenInvoked_ShouldSucceed()
        {
            try
            {
                const double threshold = 0.01;
                double expected = 1.0;
                using var sut = ProfileUtils.CreateRGBGamma(expected);
                var actual = sut.DetectRGBGamma(threshold);

                Assert.IsTrue(Math.Abs(actual - expected) <= 0.1);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.13 or later.");
            }
        }
    }
}
