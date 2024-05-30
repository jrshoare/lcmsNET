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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class IOHandlerTest
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
        private static void Save(string resourceName, string path)
        {
            var thisExe = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(thisExe.FullName);
            using var s = thisExe.GetManifestResourceStream(assemblyName.Name + resourceName);
            using var fs = File.Create(path);
            s.CopyTo(fs);
        }

        [TestMethod()]
        public void OpenTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using var context = Context.Create(plugin, userData);
            using var iohandler = IOHandler.Open(context);

            // Assert
            Assert.IsNotNull(iohandler);
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
                using var context = Context.Create(plugin, userData);
                using var iohandler = IOHandler.Open(context, srgbpath, "r");

                // Assert
                Assert.IsNotNull(iohandler);
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod]
        public void OpenTest3()
        {
            // Arrange
            uint memorySize = 100;
            IntPtr hglobal = Marshal.AllocHGlobal((int)memorySize);

            try
            {
                // Act
                using var iohandler = IOHandler.Open(context: null, hglobal, memorySize, "r");

                // Assert
                Assert.IsNotNull(iohandler);
            }
            finally
            {
                Marshal.FreeHGlobal(hglobal);
            }
        }

        [TestMethod()]
        public void ReadByteTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            byte b = 0;
            bool actual = iohandler.Read(ref b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadUshortTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            ushort us = 0;
            bool actual = iohandler.Read(ref us);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadUintTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            uint n = 0;
            bool actual = iohandler.Read(ref n);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadUlongTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            ulong ul = 0;
            bool actual = iohandler.Read(ref ul);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadFloatTest()
        {
            uint memorySize = 100;
            IntPtr hglobal = Marshal.AllocHGlobal((int)memorySize);
            float expectedf = 1.0f;
            bool expected = true;

            try
            {
                using (var iohandler = IOHandler.Open(context: null, hglobal, memorySize, "w"))
                {
                    iohandler.Write(expectedf);
                }

                using (var iohandler = IOHandler.Open(context: null, hglobal, memorySize, "r"))
                {
                    // Act
                    float actualf = 0.0f;
                    bool actual = iohandler.Read(ref actualf);

                    // Assert
                    Assert.AreEqual(expected, actual);
                    Assert.AreEqual(expectedf, actualf);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(hglobal);
            }
        }

        [TestMethod()]
        public void ReadDoubleTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            double d = 0.0;
            bool actual = iohandler.Read(ref d);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadXYZTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            CIEXYZ xyz = new();
            bool actual = iohandler.Read(ref xyz);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadUshortArrayTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            bool expected = true;

            // Act
            ushort[] us = new ushort[17];
            bool actual = iohandler.Read(us);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteByteTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            byte n = 0x37;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(n);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteUshortTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            ushort n = 0x4c57;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(n);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteUintTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            uint n = 0xf6e21048;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(n);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteUlongTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            ulong n = 0xc2d41f6622386d1e;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(n);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteFloatTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            float f = 0.3897f;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(f);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteDoubleTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            double d = 0.2874502;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(d);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteXYZTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            CIEXYZ xyz = new() { X = 0.8322, Y = 1.0, Z = 0.7765 };
            bool expected = true;

            // Act
            bool actual = iohandler.Write(xyz);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteUshortArrayTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            ushort[] array = [0x2837, 0x0005, 0x1cdf, 0x4798, 0x2265];
            bool expected = true;

            // Act
            bool actual = iohandler.Write(array);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadAlignmentTest()
        {
            using var iohandler = IOHandler.Open(null);
            bool expected = true;
            byte b = 0;
            iohandler.Read(ref b);

            // Act
            bool actual = iohandler.ReadAlignment();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WriteAlignmentTest()
        {
            using var iohandler = IOHandler.Open(null);
            byte n = 0x37;
            bool expected = true;
            iohandler.Write(n);

            // Act
            bool actual = iohandler.WriteAlignment();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadTagTypeSignatureTest()
        {
            // Arrange
            uint memorySize = 100;
            IntPtr hglobal = Marshal.AllocHGlobal((int)memorySize);

            try
            {
                using (var iohandler = IOHandler.Open(context: null, hglobal, memorySize, "w"))
                {
                    TagTypeSignature sig = TagTypeSignature.MultiLocalizedUnicode;
                    iohandler.Write(sig);
                }

                using (var iohandler = IOHandler.Open(context: null, hglobal, memorySize, "r"))
                {
                    bool expected = true;

                    // Act
                    bool actual = iohandler.Read(out TagTypeSignature sig);

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(hglobal);
            }
        }

        [TestMethod()]
        public void WriteTagTypeSignatureTest()
        {
            // Arrange
            using var iohandler = IOHandler.Open(null);
            TagTypeSignature sig = TagTypeSignature.MultiLocalizedUnicode;
            bool expected = true;

            // Act
            bool actual = iohandler.Write(sig);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
