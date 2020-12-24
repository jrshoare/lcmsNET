using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class TransformTest
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

        [TestMethod()]
        public void CreateTest()
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
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateTest2()
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
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);

                // Act
                using (var context = Context.Create(plugin, userData))
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var transform = Transform.Create(context, srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None))
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateTest3()
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
                var proofingpath = Path.Combine(tempPath, "D50_XYZ.icc");
                Save(".Resources.D50_XYZ.icc", proofingpath);

                // Act
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var proofing = Profile.Open(proofingpath, "r"))
                using (var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, proofing, Intent.Perceptual, Intent.AbsoluteColorimetric, CmsFlags.None))
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateTest4()
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
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);
                var proofingpath = Path.Combine(tempPath, "D50_XYZ.icc");
                Save(".Resources.D50_XYZ.icc", proofingpath);

                // Act
                using (var context = Context.Create(plugin, userData))
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var proofing = Profile.Open(proofingpath, "r"))
                using (var transform = Transform.Create(context, srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, proofing, Intent.Perceptual, Intent.AbsoluteColorimetric, CmsFlags.None))
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateMultipleTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                Profile[] profiles = new Profile[1];
                using (profiles[0] = Profile.Open(srgbpath, "r"))
                using (var transform = Transform.Create(profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                            Intent.RelativeColorimetric, CmsFlags.None))
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateMultipleTest2()
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
                Profile[] profiles = new Profile[1];
                using (var context = Context.Create(plugin, userData))
                using (profiles[0] = Profile.Open(srgbpath, "r"))
                using (var transform = Transform.Create(context, profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                            Intent.RelativeColorimetric, CmsFlags.None))
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void CreateExtendedTest()
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
                Profile[] profiles = new Profile[1];
                bool[] bpc = new bool[] { true };
                Intent[] intents = new Intent[] { Intent.RelativeColorimetric };
                double[] adaptationStates = new double[] { 1.0 };
                Profile gamut = null;
                int gamutPCSPosition = 0;
                uint inputFormat = Cms.TYPE_RGB_8;
                uint outputFormat = Cms.TYPE_XYZ_16;
                CmsFlags flags = CmsFlags.None;
                using (var context = Context.Create(plugin, userData))
                using (profiles[0] = Profile.Open(srgbpath, "r"))
                using (var transform = Transform.Create(context, profiles, bpc, intents, adaptationStates,
                        gamut, gamutPCSPosition, inputFormat, outputFormat, flags))
                {
                    // Assert
                    Assert.IsNotNull(transform);
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void DoTransformTest()
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
                {
                    byte[] inputBuffer = new byte[] { 255, 147, 226 };
                    byte[] outputBuffer = new byte[3];
                    transform.DoTransform(inputBuffer, outputBuffer, 1);

                    // Assert
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void DoTransformTest2()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);
                var xyzpath = Path.Combine(tempPath, "xyz.icc");
                Save(".Resources.D50_XYZ.icc", xyzpath);

                // Act
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var xyz = Profile.Open(xyzpath, "r"))
                using (var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, xyz,
                            Cms.TYPE_RGB_8, Intent.Perceptual, CmsFlags.None))
                {
                    byte[] inputBuffer = new byte[] { 255, 255, 255 };
                    byte[] outputBuffer = new byte[3];
                    transform.DoTransform(inputBuffer, outputBuffer, 1);

                    // Assert
                }
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod()]
        public void DoTransformTest3()
        {
            try
            {
                // Arrange
                var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
                Directory.CreateDirectory(tempPath);
                const int pixelsPerLine = 3;
                const int lineCount = 1;
                const int bytesPerLineIn = pixelsPerLine * 3 + 4;
                const int bytesPerLineOut = pixelsPerLine * 3 + 2;
                const int bytesPerPlaneIn = 0; // ignored as not using planar formats
                const int bytesPerPlaneOut = 0; // ditto

                try
                {
                    var srgbpath = Path.Combine(tempPath, "srgb.icc");
                    Save(".Resources.sRGB.icc", srgbpath);
                    var xyzpath = Path.Combine(tempPath, "xyz.icc");
                    Save(".Resources.D50_XYZ.icc", xyzpath);

                    // Act
                    using (var srgb = Profile.Open(srgbpath, "r"))
                    using (var xyz = Profile.Open(xyzpath, "r"))
                    using (var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, xyz,
                                Cms.TYPE_RGB_8, Intent.Perceptual, CmsFlags.None))
                    {
                        byte[] inputBuffer = new byte[bytesPerLineIn * lineCount]; // uninitialised is ok
                        byte[] outputBuffer = new byte[bytesPerLineOut * lineCount];
                        transform.DoTransform(inputBuffer, outputBuffer, pixelsPerLine, lineCount,
                                bytesPerLineIn, bytesPerLineOut, bytesPerPlaneIn, bytesPerPlaneOut);    // >= 2.8

                        // Assert
                    }
                }
                finally
                {
                    Directory.Delete(tempPath, true);
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
        }

        [TestMethod()]
        public void DoTransformMultipleTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);

                // Act
                Profile[] profiles = new Profile[1];
                using (profiles[0] = Profile.Open(srgbpath, "r"))
                using (var transform = Transform.Create(profiles, Cms.TYPE_RGB_8, Cms.TYPE_XYZ_16,
                        Intent.RelativeColorimetric, CmsFlags.None))
                {
                    byte[] inputBuffer = new byte[] { 255, 255, 255 };
                    byte[] outputBuffer = new byte[6];
                    transform.DoTransform(inputBuffer, outputBuffer, 1);

                    // Assert
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
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);

                // Act
                using (var expected = Context.Create(plugin, userData))
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var transform = Transform.Create(expected, srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None))
                {
                    var actual = transform.Context;

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
        public void ContextMultipleTest()
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
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);

                // Act
                using (var expected = Context.Create(plugin, userData))
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var transform = Transform.Create(expected, srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None))
                {
                    var actual = transform.Context;

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
        public void ContextProofingTest()
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
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);
                var proofingpath = Path.Combine(tempPath, "D50_XYZ.icc");
                Save(".Resources.D50_XYZ.icc", proofingpath);

                // Act
                using (var expected = Context.Create(plugin, userData))
                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var proofing = Profile.Open(proofingpath, "r"))
                using (var transform = Transform.Create(expected, srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, proofing, Intent.Perceptual, Intent.AbsoluteColorimetric, CmsFlags.None))
                {
                    var actual = transform.Context;

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
        public void InputFormatTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            uint expected = Cms.TYPE_RGB_8;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);

                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None))
                {
                    // Act
                    uint actual = transform.InputFormat;

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
        public void OutputFormatTest()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);
            uint expected = Cms.TYPE_Lab_8;

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                Save(".Resources.sRGB.icc", srgbpath);
                var labpath = Path.Combine(tempPath, "lab.icc");
                Save(".Resources.Lab.icc", labpath);

                using (var srgb = Profile.Open(srgbpath, "r"))
                using (var lab = Profile.Open(labpath, "r"))
                using (var transform = Transform.Create(srgb, Cms.TYPE_RGB_8, lab,
                            Cms.TYPE_Lab_8, Intent.Perceptual, CmsFlags.None))
                {
                    // Act
                    uint actual = transform.OutputFormat;

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
        public void ChangeBuffersFormatTest()
        {
            // Arrange
            using (var profile = Profile.Create_sRGB())
            using (var transform = Transform.Create(profile, Cms.TYPE_RGB_16, profile, Cms.TYPE_RGB_16, Intent.Perceptual, CmsFlags.None))
            {
                profile.Dispose();

                // Act
                var changed = transform.ChangeBuffersFormat(Cms.TYPE_BGR_16, Cms.TYPE_RGB_16);

                // Assert
                Assert.IsTrue(changed);
            }
        }

        [TestMethod()]
        public void NamedColorListTest()
        {
            // TODO:
            // Need to understand how to create a transform where the cmsStage type
            // is cmsSigNamedColorElemType. See 'cmsGetNamedColorList' in cmsnamed.c.
        }
    }
}
