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
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class MHC2Test
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
        public void WriteTest()
        {
            // Arrange
            double[] curve = [0, 0.5, 1.0];
            double[,] matrix = { { 0.5, 0.1, 0.1, 0.0 }, { 0.0, 1.0, 0.0, 0.0 }, { 0.4, 0.2, 0.4, 0.0 } };
            double minLuminance = 0.1;
            double peakLuminance = 100.0;
            int curveEntries = curve.Length;

            GCHandle hCurve = GCHandle.Alloc(curve, GCHandleType.Pinned);
            IntPtr pCurve = hCurve.AddrOfPinnedObject();
            GCHandle hMatrix = GCHandle.Alloc(matrix, GCHandleType.Pinned);
            IntPtr pMatrix = hMatrix.AddrOfPinnedObject();

            using var profile = Profile.CreatePlaceholder(null);
            try
            {
                int encodedVersion = 2070;
                // next call throws if less than 2.8
                try { encodedVersion = Cms.EncodedCMMVersion; } catch { }
                if (encodedVersion < 2160) throw new LcmsNETException();

                var target = new MHC2
                {
                    CurveEntries = curveEntries,
                    RedCurve = pCurve,
                    GreenCurve = pCurve,
                    BlueCurve = pCurve,
                    MinLuminance = minLuminance,
                    PeakLuminance = peakLuminance,
                    XYZ2XYXmatrix = pMatrix
                };

                // Act
                bool written = profile.WriteTag(TagSignature.MHC2, target);

                // Assert
                Assert.IsTrue(written);
            }
            catch (LcmsNETException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
            finally
            {
                hMatrix.Free();
                hCurve.Free();
            }
        }

        [TestMethod()]
        public void ReadTest()
        {
            // Arrange
            double[] curve = [0, 0.5, 1.0];
            double[,] matrix = { { 0.5, 0.1, 0.1, 0.0 }, { 0.0, 1.0, 0.0, 0.0 }, { 0.4, 0.2, 0.4, 0.0 } };
            double minLuminance = 0.1;
            double peakLuminance = 100.0;
            int curveEntries = curve.Length;

            GCHandle hCurve = GCHandle.Alloc(curve, GCHandleType.Pinned);
            IntPtr pCurve = hCurve.AddrOfPinnedObject();
            GCHandle hMatrix = GCHandle.Alloc(matrix, GCHandleType.Pinned);
            IntPtr pMatrix = hMatrix.AddrOfPinnedObject();

            using var profile = Profile.CreatePlaceholder(null);
            try
            {
                var target = new MHC2
                {
                    CurveEntries = curveEntries,
                    RedCurve = pCurve,
                    GreenCurve = pCurve,
                    BlueCurve = pCurve,
                    MinLuminance = minLuminance,
                    PeakLuminance = peakLuminance,
                    XYZ2XYXmatrix = pMatrix
                };

                profile.WriteTag(TagSignature.MHC2, target);

                // Act
                var actual = profile.ReadTag<MHC2>(TagSignature.MHC2);

                // Assert
                Assert.AreEqual(curveEntries, actual.CurveEntries);
                Assert.AreNotEqual(IntPtr.Zero, actual.RedCurve);
                Assert.AreNotEqual(IntPtr.Zero, actual.GreenCurve);
                Assert.AreNotEqual(IntPtr.Zero, actual.BlueCurve);
                Assert.AreEqual(minLuminance, actual.MinLuminance, double.Epsilon);
                Assert.AreEqual(peakLuminance, actual.PeakLuminance, double.Epsilon);
                Assert.AreNotEqual(IntPtr.Zero, actual.XYZ2XYXmatrix);

                unsafe
                {
                    double* redCurve = (double*)actual.RedCurve.ToPointer();
                    for (var i = 0; i < actual.CurveEntries; i++)
                    {
                        Assert.AreEqual(curve[i], redCurve[i], double.Epsilon);
                    }

                    double* greenCurve = (double*)actual.GreenCurve.ToPointer();
                    for (var i = 0; i < actual.CurveEntries; i++)
                    {
                        Assert.AreEqual(curve[i], redCurve[i], double.Epsilon);
                    }

                    double* blueCurve = (double*)actual.BlueCurve.ToPointer();
                    for (var i = 0; i < actual.CurveEntries; i++)
                    {
                        Assert.AreEqual(curve[i], redCurve[i], double.Epsilon);
                    }

                    double* xyz2xyz = (double*)actual.XYZ2XYXmatrix.ToPointer();
                    int n = 0;
                    for (var i = 0; i < matrix.GetLength(0); i++)
                        for (var j = 0; j < matrix.GetLength(1); j++)
                        {
                            Assert.AreEqual(matrix[i, j], xyz2xyz[n], double.Epsilon);
                            n++;
                        }
                }
            }
            catch (LcmsNETException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
            finally
            {
                hMatrix.Free();
                hCurve.Free();
            }
        }
    }
}
