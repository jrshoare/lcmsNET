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
    }
}
