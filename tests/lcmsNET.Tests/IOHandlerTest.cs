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
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class IOHandlerTest
    {
        [TestMethod()]
        public void Open_WhenInstantiatedForVoid_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = IOHandler.Open(context);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Open_WhenInstantiatedFromFile_ShouldHaveValidHandle()
        {
            // Arrange
            var tempPath = Path.Combine(Path.GetTempPath(), "lcmsNET.Tests");
            Directory.CreateDirectory(tempPath);

            try
            {
                var srgbpath = Path.Combine(tempPath, "srgb.icc");
                ResourceUtils.Save(".Resources.sRGB.icc", srgbpath);
                using var context = ContextUtils.CreateContext();

                // Act
                using var sut = IOHandler.Open(context, srgbpath, "r");

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            }
            finally
            {
                Directory.Delete(tempPath, true);
            }
        }

        [TestMethod]
        public void Open_WhenInstantiatedFromMemory_ShouldHaveValidHandle()
        {
            // Arrange
            uint memorySize = 100;
            MemoryUtils.UsingMemory((int)memorySize, (hglobal) =>
            {
                // Act
                using var sut = IOHandler.Open(context: null, hglobal, memorySize, "r");

                // Assert
                Assert.IsFalse(sut.IsInvalid);
            });
        }

        [TestMethod()]
        public void Read_WhenByte_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            byte actual = 0;
            bool result = sut.Read(ref actual);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenUShort_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            ushort us = 0;
            bool result = sut.Read(ref us);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenUInt_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            uint n = 0;
            bool result = sut.Read(ref n);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenULong_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            ulong ul = 0;
            bool result = sut.Read(ref ul);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenFloat_ShouldSucceed()
        {
            uint memorySize = 100;
            MemoryUtils.UsingMemory((int)memorySize, (hglobal) =>
            {
                float expectedf = 1.0f;
                using (var sut = IOHandler.Open(context: null, hglobal, memorySize, "w"))
                {
                    sut.Write(expectedf);
                }

                using (var sut = IOHandler.Open(context: null, hglobal, memorySize, "r"))
                {
                    // Act
                    float actualf = 0.0f;
                    bool result = sut.Read(ref actualf);

                    // Assert
                    Assert.IsTrue(result);
                    Assert.AreEqual(expectedf, actualf);
                }
            });
        }

        [TestMethod()]
        public void Read_WhenDouble_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            double d = 0.0;
            bool result = sut.Read(ref d);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenCIEXYZ_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            CIEXYZ xyz = new();
            bool result = sut.Read(ref xyz);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenUShortArray_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);

            // Act
            ushort[] us = new ushort[17];
            bool result = sut.Read(us);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenByte_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            byte n = 0x37;

            // Act
            bool result = sut.Write(n);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenUShort_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            ushort n = 0x4c57;

            // Act
            bool result = sut.Write(n);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenUInt_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            uint n = 0xf6e21048;

            // Act
            bool result = sut.Write(n);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenULong_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            ulong n = 0xc2d41f6622386d1e;

            // Act
            bool result = sut.Write(n);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenFloat_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            float f = 0.3897f;

            // Act
            bool result = sut.Write(f);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenDouble_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            double d = 0.2874502;

            // Act
            bool result = sut.Write(d);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenCIEXYZ_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            CIEXYZ xyz = new() { X = 0.8322, Y = 1.0, Z = 0.7765 };

            // Act
            bool result = sut.Write(xyz);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Write_WhenUShortArray_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            ushort[] array = [0x2837, 0x0005, 0x1cdf, 0x4798, 0x2265];

            // Act
            bool result = sut.Write(array);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ReadAlignment_WhenInvoked_ShouldSucceed()
        {
            using var sut = IOHandler.Open(null);
            byte b = 0;
            sut.Read(ref b);

            // Act
            bool result = sut.ReadAlignment();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void WriteAlignment_WhenInvoked_ShouldSucceed()
        {
            using var sut = IOHandler.Open(null);
            byte n = 0x37;
            sut.Write(n);

            // Act
            bool result = sut.WriteAlignment();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Read_WhenTagTypeSignature_ShouldSucceed()
        {
            // Arrange
            uint memorySize = 100;
            MemoryUtils.UsingMemory((int)memorySize, (hglobal) =>
            {
                using (var sut = IOHandler.Open(context: null, hglobal, memorySize, "w"))
                {
                    TagTypeSignature sig = TagTypeSignature.MultiLocalizedUnicode;
                    sut.Write(sig);
                }

                using (var sut = IOHandler.Open(context: null, hglobal, memorySize, "r"))
                {
                    // Act
                    bool result = sut.Read(out TagTypeSignature sig);

                    // Assert
                    Assert.IsTrue(result);
                }
            });
        }

        [TestMethod()]
        public void Write_WhenTagTypeSignature_ShouldSucceed()
        {
            // Arrange
            using var sut = IOHandler.Open(null);
            TagTypeSignature sig = TagTypeSignature.MultiLocalizedUnicode;

            // Act
            bool result = sut.Write(sig);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
