﻿// Copyright(c) 2019-2021 John Stevenson-Hoare
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
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Extracts the named resource and saves to the specified file path.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="path"></param>
        private void Save(string resourceName, string path)
        {
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using (var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName))
            {
                using (var fs = File.Create(path))
                {
                    s.CopyTo(fs);
                }
            }
        }

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

        [StructLayout(LayoutKind.Sequential)]
        private struct TestCIEXYZ
        {
            [MarshalAs(UnmanagedType.R8)]
            public double X;
            [MarshalAs(UnmanagedType.R8)]
            public double Y;
            [MarshalAs(UnmanagedType.R8)]
            public double Z;

            // Must be missing an 'internal static TestCIEXYZ FromHandle(IntPtr)' method!
        }

        [TestMethod()]
        public void CreatePlaceholderTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateRGBTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIExyY whitePoint = Colorimetric.D50_xyY;
            CIExyYTRIPLE primaries = new CIExyYTRIPLE
            {
                Red = new CIExyY { x = 0.64, y = 0.33, Y = 1 },
                Green = new CIExyY { x = 0.21, y = 0.71, Y = 1 },
                Blue = new CIExyY { x = 0.15, y = 0.06, Y = 1 }
            };

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 2.19921875))
            {
                ToneCurve[] transferFunction = new ToneCurve[]
                {
                    toneCurve, toneCurve, toneCurve
                };

                // Act
                using (var profile = Profile.CreateRGB(whitePoint, primaries, transferFunction))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void CreateRGBTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIExyY whitePoint = Colorimetric.D50_xyY;
            CIExyYTRIPLE primaries = new CIExyYTRIPLE
            {
                Red = new CIExyY { x = 0.64, y = 0.33, Y = 1 },
                Green = new CIExyY { x = 0.21, y = 0.71, Y = 1 },
                Blue = new CIExyY { x = 0.15, y = 0.06, Y = 1 }
            };

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 2.19921875))
            {
                ToneCurve[] transferFunction = new ToneCurve[]
                {
                    toneCurve, toneCurve, toneCurve
                };

                // Act
                using (var profile = Profile.CreateRGB(context, whitePoint, primaries, transferFunction))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void CreateGrayTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIExyY whitePoint = Colorimetric.D50_xyY;

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 2.19921875))
            {
                // Act
                using (var profile = Profile.CreateGray(whitePoint, toneCurve))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void CreateGrayTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIExyY whitePoint = Colorimetric.D50_xyY;

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 2.19921875))
            {
                // Act
                using (var profile = Profile.CreateGray(context, whitePoint, toneCurve))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void CreateLinearizationDeviceLinkTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIExyY whitePoint = Colorimetric.D50_xyY;
            ColorSpaceSignature space = ColorSpaceSignature.CmykData;

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 3.0))
            {
                ToneCurve[] transferFunction = new ToneCurve[]
                {
                    toneCurve, toneCurve, toneCurve, toneCurve
                };

                // Act
                using (var profile = Profile.CreateLinearizationDeviceLink(space, transferFunction))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void CreateLinearizationDeviceLinkTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            CIExyY whitePoint = Colorimetric.D50_xyY;
            ColorSpaceSignature space = ColorSpaceSignature.CmykData;

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 3.0))
            {
                ToneCurve[] transferFunction = new ToneCurve[]
                {
                    toneCurve, toneCurve, toneCurve, toneCurve
                };

                // Act
                using (var profile = Profile.CreateLinearizationDeviceLink(context, space, transferFunction))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void CreateInkLimitingDeviceLinkTest()
        {
            // Arrange

            // Act
            using (var profile = Profile.CreateInkLimitingDeviceLink(ColorSpaceSignature.CmykData, 150.0))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateInkLimitingDeviceLinkTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateInkLimitingDeviceLink(context, ColorSpaceSignature.CmykData, 150.0))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

// At the time of writing these tests are excluded because an i/o exception occurs at runtime
// due to Little CMS (2.16) leaving the file open, so do not re-enable until this is fixed!
#if false
        [TestMethod()]
        public void CreateDeviceLinkFromCubeFileTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var cubepath = Path.Combine(tempPath, "Aqua.cube");
                Save(".Resources.Aqua.cube", cubepath);

                // Act
                using (var profile = Profile.CreateDeviceLinkFromCubeFile(cubepath))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateDeviceLinkFromCubeFileTest2()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                IntPtr plugin = IntPtr.Zero;
                IntPtr userData = IntPtr.Zero;
                var cubepath = Path.Combine(tempPath, "Aqua.cube");
                Save(".Resources.Aqua.cube", cubepath);

                // Act
                using var context = Context.Create(plugin, userData);
                using var profile = Profile.CreateDeviceLinkFromCubeFile(context, cubepath);

                // Assert
                Assert.IsNotNull(profile);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }
#endif

        [TestMethod()]
        public void CreateDeviceLinkTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);

                // Act
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None))
                using (var profile = Profile.CreateDeviceLink(transform, 3.4, CmsFlags.None))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateLab2Test()
        {
            // Arrange

            // Act
            using (var profile = Profile.CreateLab2(Colorimetric.D50_xyY))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateLab2Test2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateLab2(context, Colorimetric.D50_xyY))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateLab4Test()
        {
            // Arrange

            // Act
            using (var profile = Profile.CreateLab4(Colorimetric.D50_xyY))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateLab4Test2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateLab4(context, Colorimetric.D50_xyY))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateXYZTest()
        {
            // Arrange

            // Act
            using (var profile = Profile.CreateXYZ())
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateXYZTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateXYZ(context))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void Create_sRGBTest()
        {
            // Arrange

            // Act
            using (var profile = Profile.Create_sRGB())
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void Create_sRGBTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.Create_sRGB(context))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateNullTest()
        {
            // Arrange

            // Act
            using (var profile = Profile.CreateNull())
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateNullTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateNull(context))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateBCHSWabstractTest()
        {
            // Arrange
            int nLutPoints = 17;
            double bright = 0.0, contrast = 1.2, hue = 0.0, saturation = 3.0;
            int tempSrc = 5000, tempDest = 5000;

            // Act
            using (var profile = Profile.CreateBCHSWabstract(nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void CreateBCHSWabstractTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            int nLutPoints = 17;
            double bright = 0.0, contrast = 1.2, hue = 0.0, saturation = 3.0;
            int tempSrc = 5000, tempDest = 5000;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateBCHSWabstract(context, nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void Create_OkLabTest()
        {
            try
            {
                // Arrange
                IntPtr plugin = IntPtr.Zero;
                IntPtr userData = IntPtr.Zero;

                // Act
                using var context = Context.Create(plugin, userData);
                using var profile = Profile.Create_OkLab(context);

                // Assert
                Assert.IsNotNull(profile);
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }

        [TestMethod()]
        public void OpenTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void OpenTest2()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var context = Context.Create(plugin, userData))
                using (var profile = Profile.Open(context, srgbpath, "r"))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void OpenTest3()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                // Act
                using (var profile = Profile.Open(ms.GetBuffer()))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void OpenTest4()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                // Act
                using (var context = Context.Create(plugin, userData))
                using (var profile = Profile.Open(context, ms.GetBuffer()))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
        }

        [TestMethod()]
        public void OpenTest5()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var context = Context.Create(plugin, userData))
                using (var iohandler = IOHandler.Open(context, srgbpath, "r"))
                using (var profile = Profile.Open(context, iohandler))
                {
                    // Assert
                    Assert.IsNotNull(profile);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void OpenTest6()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var iohandler = IOHandler.Open(context))
            using (var profile = Profile.Open(context, iohandler, true))
            {
                // Assert
                Assert.IsNotNull(profile);
            }
        }

        [TestMethod()]
        public void SaveTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    var savepath = Path.Combine(tempPath, "saved.icc");
                    bool saved = profile.Save(savepath);

                    // Assert
                    Assert.IsTrue(saved);
                    Assert.IsTrue(File.Exists(savepath));
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void SaveTest2()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                byte[] memory = ms.GetBuffer();
                using (var srcProfile = Profile.Open(memory))
                {
                    uint expected = (uint)(memory.Length + 1); // add 1 for '\0' termination
                    byte[] destProfile = new byte[expected];

                    // Act
                    var result = srcProfile.Save(destProfile, out uint actual);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void SaveTest3()
        {
            // Arrange
            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                byte[] memory = ms.GetBuffer();
                using (var profile = Profile.Open(memory))
                {
                    // oddly the method returns the size of the profile ignoring
                    // the fact that it always terminates with `\0`
                    uint expected = (uint)memory.Length;

                    // Act
                    var result = profile.Save(null, out uint actual);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void SaveTest4()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var profile = Profile.Open(srgbpath, "r"))
                using (var iohandler = IOHandler.Open(null))
                {
                    var bytesToWrite = profile.Save((IOHandler)null);
                    var bytesWritten = profile.Save(iohandler);

                    // Assert
                    Assert.AreEqual(bytesToWrite, bytesWritten);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void ContextTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var expected = Context.Create(plugin, userData))
                using (var profile = Profile.Open(expected, srgbpath, "r"))
                {
                    var actual = profile.Context;

                    // Assert
                    Assert.AreSame(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void ColorSpaceGetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            var expected = ColorSpaceSignature.RgbData;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    var actual = profile.ColorSpace;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void ColorSpaceSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            var expected = ColorSpaceSignature.CmykData;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.ColorSpace = expected;

                // Assert
                var actual = profile.ColorSpace;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void PCSGetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            var expected = ColorSpaceSignature.XYZData;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    var actual = profile.PCS;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void PCSSetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            var expected = ColorSpaceSignature.XYZData;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    profile.PCS = expected;

                    // Assert
                    var actual = profile.PCS;
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetProfileInfoTest()
        {
            // Arrange
            var expected = "sRGB IEC61966-2.1";

            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                byte[] memory = ms.GetBuffer();
                using (var profile = Profile.Open(memory))
                {
                    // Act
                    var actual = profile.GetProfileInfo(InfoType.Description, "en", "US");

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void GetProfileInfoASCIITest()
        {
            // Arrange
            var expected = "sRGB IEC61966-2.1";

            using (MemoryStream ms = Save(".Resources.sRGB.icc"))
            {
                byte[] memory = ms.GetBuffer();
                using (var profile = Profile.Open(memory))
                {
                    // Act
                    var actual = profile.GetProfileInfoASCII(InfoType.Description, "en", "US");

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod()]
        public void DetectBlackPointTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            Intent intent = Intent.RelativeColorimetric;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    bool detected = profile.DetectBlackPoint(out CIEXYZ blackPoint, intent);

                    // Assert
                    Assert.IsTrue(detected);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void DetectDestinationBlackPointTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            Intent intent = Intent.RelativeColorimetric;

            try
            {
                var srgbpath = Path.Combine(tempPath, "D50_XYZ.icc");
                Save(".Resources.D50_XYZ.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    bool detected = profile.DetectDestinationBlackPoint(out CIEXYZ blackPoint, intent);

                    // Assert
                    Assert.IsTrue(detected);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void TotalAreaCoverageTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double expected = 0.0;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.DeviceClass = ProfileClassSignature.Output;

                // Act
                double actual = profile.TotalAreaCoverage;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DeviceClassGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            var expected = ProfileClassSignature.Abstract;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.DeviceClass = expected;

                // Act
                var actual = profile.DeviceClass;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void DeviceClassSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            var expected = ProfileClassSignature.Display;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.DeviceClass = expected;

                // Assert
                var actual = profile.DeviceClass;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetProfileHeaderDateTimeTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            var notExpected = DateTime.MinValue;
            DateTime now = DateTime.UtcNow - TimeSpan.FromMinutes(1);

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                bool obtained = profile.GetHeaderCreationDateTime(out DateTime actual);

                // Assert
                Assert.IsTrue(obtained);
                Assert.AreNotEqual(notExpected, actual);
                Assert.IsTrue(actual >= now);
            }
        }

        [TestMethod()]
        public void HeaderFlagsGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x2;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.HeaderFlags = expected;

                // Act
                var actual = profile.HeaderFlags;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderFlagsSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x1;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.HeaderFlags = expected;

                // Assert
                var actual = profile.HeaderFlags;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderManufacturerGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x54657374; // 'Test'

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.HeaderManufacturer = expected;

                // Act
                var actual = profile.HeaderManufacturer;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderManufacturerSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x54657374; // 'Test'

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.HeaderManufacturer = expected;

                // Assert
                var actual = profile.HeaderManufacturer;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderModelGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x6D6F646C; // 'modl'

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.HeaderModel = expected;

                // Act
                var actual = profile.HeaderModel;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderModelSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x6D6F646C; // 'modl'

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.HeaderModel = expected;

                // Assert
                var actual = profile.HeaderModel;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderAttributesGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            DeviceAttributes expected = DeviceAttributes.Reflective | DeviceAttributes.Matte;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.HeaderAttributes = expected;

                // Act
                var actual = profile.HeaderAttributes;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderAttributesSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            DeviceAttributes expected = DeviceAttributes.Transparency;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.HeaderAttributes = expected;

                // Assert
                var actual = profile.HeaderAttributes;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void VersionGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double expected = 4.3;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.Version = expected;

                // Act
                var actual = profile.Version;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void VersionSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double expected = 4.3;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.Version = expected;

                // Assert
                var actual = profile.Version;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void EncodedICCVersionGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x4000000;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                profile.EncodedICCVersion = expected;

                // Act
                var actual = profile.EncodedICCVersion;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void EncodedICCVersionSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 0x4000000;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.EncodedICCVersion = expected;

                // Assert
                var actual = profile.EncodedICCVersion;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void IsMatrixShaperTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                bool isMatrixShaper = profile.IsMatrixShaper;

                // Assert
                Assert.IsFalse(isMatrixShaper);
            }
        }

        [TestMethod()]
        public void IsCLUTTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            Intent intent = Intent.RelativeColorimetric;
            UsedDirection usedDirection = UsedDirection.AsInput;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                bool isCLUT = profile.IsCLUT(intent, usedDirection);

                // Assert
                Assert.IsFalse(isCLUT);
            }
        }

        [TestMethod()]
        public void TagCountTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            int expected = 0;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                int actual = profile.TagCount;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetTagTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    for (int i = 0; i < profile.TagCount; i++)
                    {
                        TagSignature tag = profile.GetTag(Convert.ToUInt32(i));
                        TestContext.WriteLine($"tag: 0x{tag:X}");
                    }
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void HasTagTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    bool hasTag = profile.HasTag(TagSignature.MediaWhitePoint);
                    Assert.IsTrue(hasTag);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr notExpected = IntPtr.Zero;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    IntPtr actual = profile.ReadTag(TagSignature.MediaWhitePoint);

                    // Assert
                    Assert.AreNotEqual(notExpected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void ReadTagTTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new CIEXYZTRIPLE
                {
                    Red = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 },
                    Green = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 },
                    Blue = new CIEXYZ { X = 0.7352, Y = 1.0, Z = 0.6115 }
                };

                profile.WriteTag(TagSignature.ChromaticAdaptation, expected);

                // Act
                var actual = profile.ReadTag<CIEXYZTRIPLE>(TagSignature.ChromaticAdaptation);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ReadTagTMissingMethodTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

                profile.WriteTag(TagSignature.BlueColorant, expected);

                // Act & Assert
                var actual = Assert.ThrowsException<MissingMethodException>(
                        () => profile.ReadTag<TestCIEXYZ>(TagSignature.BlueColorant));
            }
        }

        [TestMethod()]
        public void ReadTagTTagNotFound()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder())
            {
                // Act & Assert
                var actual = Assert.ThrowsException<LcmsNETException>(
                        () => profile.ReadTag<Dict>(TagSignature.Meta));
            }
        }

        [TestMethod()]
        public void ReadTag_MarshalToStructTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            double notExpected = 0.0;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    IntPtr actual = profile.ReadTag(TagSignature.BlueColorant);
                    CIEXYZ bXYZ = Marshal.PtrToStructure<CIEXYZ>(actual);

                    // Assert
                    Assert.AreNotEqual(notExpected, bXYZ.X);
                    Assert.AreNotEqual(notExpected, bXYZ.Y);
                    Assert.AreNotEqual(notExpected, bXYZ.Z);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void WriteTagTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            IntPtr notExpected = IntPtr.Zero;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    var whitePoint = profile.ReadTag<CIEXYZ>(TagSignature.MediaWhitePoint);

                    // Act
                    bool written = profile.WriteTag(TagSignature.MediaWhitePoint, whitePoint);

                    // Assert
                    Assert.IsTrue(written);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void WriteTagTest2()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new CIEXYZTRIPLE
                {
                    Red = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 },
                    Green = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 },
                    Blue = new CIEXYZ { X = 0.7352, Y = 1.0, Z = 0.6115 }
                };

                profile.WriteTag(TagSignature.ChromaticAdaptation, expected);

                // Act
                // implicit call to FromHandle
                var actual = profile.ReadTag<CIEXYZTRIPLE>(TagSignature.ChromaticAdaptation);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void WriteTagTest3()
        {
            // Arrange
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new byte[] { 17, 99, 0, 253, 122, 19 };
                var iccData = new ICCData(expected);

                // do not use TagSignature.Data as this is not supported
                profile.WriteTag(TagSignature.Ps2CRD0, iccData);

                // Act
                var iccData2 = profile.ReadTag<ICCData>(TagSignature.Ps2CRD0);

                // Assert
                var actual = (byte[])iccData2;
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void LinkTagTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateInkLimitingDeviceLink(context, ColorSpaceSignature.CmykData, 150.0))
            {
                // Act
                bool linked = profile.LinkTag(TagSignature.AToB1, TagSignature.AToB0);

                // Assert
                Assert.IsTrue(linked);
            }
        }

        [TestMethod()]
        public void TagLinkedToTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            TagSignature expected = TagSignature.AToB0;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreateInkLimitingDeviceLink(context, ColorSpaceSignature.CmykData, 150.0))
            {
                TagSignature tag = TagSignature.AToB1;
                profile.LinkTag(tag, expected);

                // Act
                TagSignature actual = profile.TagLinkedTo(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderRenderingIntentGetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            Intent expected = Intent.Perceptual;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                Intent actual = profile.HeaderRenderingIntent;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void HeaderRenderingIntentSetTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            Intent expected = Intent.AbsoluteColorimetric;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                profile.HeaderRenderingIntent = expected;

                // Assert
                Intent actual = profile.HeaderRenderingIntent;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void IsIntentSupportedTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            using (var profile = Profile.CreatePlaceholder(context))
            {
                // Act
                bool supported = profile.IsIntentSupported(Intent.Perceptual, UsedDirection.AsInput);

                // Assert
                Assert.IsFalse(supported);
            }
        }

        [TestMethod()]
        public void ComputeMD5Test()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    bool computed = profile.ComputeMD5();

                    // Assert
                    Assert.IsTrue(computed);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void HeaderProfileIDGetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    profile.ComputeMD5();

                    // Act
                    byte[] profileID = profile.HeaderProfileID;

                    // Assert
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void HeaderProfileIDSetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    profile.ComputeMD5();
                    byte[] profileID = profile.HeaderProfileID;

                    // Act
                    profile.HeaderProfileID = profileID;

                    // Assert
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void IOHandlerGetTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    var iohandler = profile.IOHandler;

                    // Assert
                    Assert.IsNotNull(iohandler);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetPostScriptColorResourceTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            uint notExpected = 0;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
                using (var iohandler = IOHandler.Open(context))
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    uint actual = profile.GetPostScriptColorResource(context, PostScriptResourceType.ColorRenderingDictionary, Intent.RelativeColorimetric, CmsFlags.None, iohandler);

                    // Assert
                    Assert.AreNotEqual(notExpected, actual);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetPostScriptColorSpaceArrayTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            int notExpected = 0;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    byte[] actual = profile.GetPostScriptColorSpaceArray(context, Intent.RelativeColorimetric, CmsFlags.None);

                    // Assert
                    Assert.IsNotNull(actual);
                    Assert.AreNotEqual(notExpected, actual.Length);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void GetPostScriptColorRenderingDictionaryTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            int notExpected = 0;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    // Act
                    byte[] actual = profile.GetPostScriptColorRenderingDictionary(context, Intent.AbsoluteColorimetric, CmsFlags.None);

                    // Assert
                    Assert.IsNotNull(actual);
                    Assert.AreNotEqual(notExpected, actual.Length);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        private static Profile CreateRGBGamma(double gamma)
        {
            CIExyY D65 = new CIExyY { x = 0.3127, y = 0.3290, Y = 1.0 };
            CIExyYTRIPLE Rec709Primaries = new CIExyYTRIPLE
            {
                Red   = new CIExyY { x = 0.6400, y = 0.3300, Y = 1.0 },
                Green = new CIExyY { x = 0.3000, y = 0.6000, Y = 1.0 },
                Blue  = new CIExyY { x = 0.1500, y = 0.0600, Y = 1.0 }
            };

            using (var toneCurve = ToneCurve.BuildGamma(null, gamma))
            {
                return Profile.CreateRGB(D65, Rec709Primaries, new ToneCurve[] { toneCurve, toneCurve, toneCurve });
            }
        }

        [TestMethod()]
        public void DetectRGBGammaTest()
        {
            try
            {
                const double threshold = 0.01;
                double expected = 1.0;
                using (var profile = CreateRGBGamma(expected))
                {
                    var actual = profile.DetectRGBGamma(threshold);

                    Assert.IsTrue(Math.Abs(actual - expected) <= 0.1);
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.13 or later.");
            }
        }
    }
}
