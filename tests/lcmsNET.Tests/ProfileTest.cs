using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

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
                    var expected = memory.Length + 1; // add 1 for '\0' termination
                    byte[] destProfile = new byte[expected];

                    // Act
                    var result = srcProfile.Save(destProfile, out int actual);

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
                    var expected = memory.Length;

                    // Act
                    var result = profile.Save(null, out int actual);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
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
        public void ColorSpaceSignatureTest()
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
                    var actual = profile.ColorSpaceSignature;

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
        public void ColorSpaceTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            var expected = PixelType.RGB;

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
        public void PCSSignatureTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            var expected = ColorSpaceSignature.XYZData;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var profile = Profile.Open(srgbpath, "r"))
                {
                    var actual = profile.PCSSignature;

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
        public void PCSTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            var expected = PixelType.XYZ;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                using (var profile = Profile.Open(srgbpath, "r"))
                {
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
    }
}
