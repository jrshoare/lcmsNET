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
            var expected = 0x52474220;  // 'RGB '

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
            var expected = Cms.PixelType.RGB;

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
            var expected = 0x58595A20;  // 'XYZ '

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
            var expected = Cms.PixelType.XYZ;

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
    }
}
