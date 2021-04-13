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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ColorimetricTest
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
        public void XYZ2LabTest()
        {
            // Arrange
            CIEXYZ whitePoint = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 }; // D50 XYZ normalized to Y = 1.0
            CIEXYZ xyz = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 };

            // Act
            CIELab lab = Colorimetric.XYZ2Lab(whitePoint, xyz);

            // Assert
            Assert.AreEqual(100.0, lab.L);
            Assert.AreEqual(0.0, lab.a);
            Assert.AreEqual(0.0, lab.b);
        }

        [TestMethod()]
        public void Lab2XYZTest()
        {
            // Arrange
            CIEXYZ whitePoint = new CIEXYZ { X = 0.9642, Y = 1.0, Z = 0.8249 }; // D50 XYZ normalized to Y = 1.0
            CIELab lab = new CIELab { L = 100.0, a = 0.0, b = 0.0 };

            // Act
            CIEXYZ xyz = Colorimetric.Lab2XYZ(whitePoint, lab);

            // Assert
            Assert.AreEqual(0.9642, xyz.X);
            Assert.AreEqual(1.0, xyz.Y);
            Assert.AreEqual(0.8249, xyz.Z);
        }

        [TestMethod()]
        public void Lab2LChTest()
        {
            // Arrange
            CIELab lab = new CIELab { L = 100.0, a = 0.0, b = 0.0 };

            // Act
            CIELCh lch = Colorimetric.Lab2LCh(lab);

            // Assert
            Assert.AreEqual(100.0, lch.L);
            Assert.AreEqual(0.0, lch.C);
            Assert.AreEqual(0.0, lch.h);
        }

        [TestMethod()]
        public void LCh2LabTest()
        {
            // Arrange
            CIELCh lch = new CIELCh { L = 100.0, C = 0.0, h = 0.0 };

            // Act
            CIELab lab = Colorimetric.LCh2Lab(lch);

            // Assert
            Assert.AreEqual(100.0, lab.L);
            Assert.AreEqual(0.0, lab.a);
            Assert.AreEqual(0.0, lab.b);
        }

        [TestMethod()]
        public void LabV4EncodingTest()
        {
            ushort[] inWLab = new ushort[3];
            for (ushort u = 0; u < ushort.MaxValue; u++)
            {
                inWLab[0] = inWLab[1] = inWLab[2] = u;

                CIELab lab = Colorimetric.LabEncoded2Float(inWLab);
                ushort[] outWLab = Colorimetric.Float2LabEncoded(lab);

                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(u, outWLab[i]);
                }
            }
        }

        [TestMethod()]
        public void LabV2EncodingTest()
        {
            ushort[] inWLab = new ushort[3];
            for (ushort u = 0; u < ushort.MaxValue; u++)
            {
                inWLab[0] = inWLab[1] = inWLab[2] = u;

                CIELab lab = Colorimetric.LabEncoded2FloatV2(inWLab);
                ushort[] outWLab = Colorimetric.Float2LabEncodedV2(lab);

                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(u, outWLab[i]);
                }
            }
        }

        [TestMethod()]
        public void XYZEncodingTest()
        {
            ushort[] inXyz = new ushort[3];
            for (ushort u = 0; u < ushort.MaxValue; u++)
            {
                inXyz[0] = inXyz[1] = inXyz[2] = u;

                CIEXYZ fxyz = Colorimetric.XYZEncoded2Float(inXyz);
                ushort[] outXyz = Colorimetric.Float2XYZEncoded(fxyz);

                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(u, outXyz[i]);
                }
            }
        }

        [TestMethod()]
        public void D50_XYZTest()
        {
            // Arrange

            // Act
            var d50 = Colorimetric.D50_XYZ;

            // Assert
            Assert.AreEqual(0.9642, d50.X, double.Epsilon);
            Assert.AreEqual(1.0, d50.Y, double.Epsilon);
            Assert.AreEqual(0.8249, d50.Z, double.Epsilon);
        }

        [TestMethod()]
        public void D50_xyYTest()
        {
            // Arrange

            // Act
            var d50 = Colorimetric.D50_xyY;

            // Assert
            Assert.AreEqual(0.3457, d50.x, 0.0001);
            Assert.AreEqual(0.3585, d50.y, 0.0001);
            Assert.AreEqual(1.0, d50.Y, double.Epsilon);
        }

        [TestMethod()]
        public void DesaturateTest()
        {
            // Arrange
            CIELab lab = new CIELab { L = 97.4, a = 62.3, b = 81.2 };
            double aMax = 55, aMin = -55, bMax = 75, bMin = -75;

            // Act
            bool desaturated = Colorimetric.Desaturate(ref lab, aMax, aMin, bMax, bMin);

            // Assert
            Assert.IsTrue(desaturated);
        }

        [TestMethod()]
        public void CIEXYZ_D50Test()
        {
            // Arrange

            // Act
            var d50 = CIEXYZ.D50;

            // Assert
            Assert.AreEqual(0.9642, d50.X, double.Epsilon);
            Assert.AreEqual(1.0, d50.Y, double.Epsilon);
            Assert.AreEqual(0.8249, d50.Z, double.Epsilon);
        }

        [TestMethod()]
        public void CIEXYZ_FromHandleTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new CIEXYZ { X = 0.8322, Y = 1.0, Z = 0.7765 };

                profile.WriteTag(TagSignature.BlueColorant, expected);
                var tag = profile.ReadTag(TagSignature.BlueColorant);

                // Act
                var actual = CIEXYZ.FromHandle(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void CIExyY_D50Test()
        {
            // Arrange

            // Act
            var d50 = CIExyY.D50;

            // Assert
            Assert.AreEqual(0.3457, d50.x, 0.0001);
            Assert.AreEqual(0.3585, d50.y, 0.0001);
            Assert.AreEqual(1.0, d50.Y, double.Epsilon);
        }

        [TestMethod()]
        public void CIExyYTRIPLE_FromHandleTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                var expected = new CIExyYTRIPLE
                {
                    Red = new CIExyY { x = 0.64, y = 0.33, Y = 1 },
                    Green = new CIExyY { x = 0.21, y = 0.71, Y = 1 },
                    Blue = new CIExyY { x = 0.15, y = 0.06, Y = 1 }
                };

                profile.WriteTag(TagSignature.Chromaticity, expected);
                var tag = profile.ReadTag(TagSignature.Chromaticity);

                // Act
                var actual = CIExyYTRIPLE.FromHandle(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void CIEXYZTRIPLE_FromHandleTest()
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
                var tag = profile.ReadTag(TagSignature.ChromaticAdaptation);

                // Act
                var actual = CIEXYZTRIPLE.FromHandle(tag);

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
