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
    public class ToneCurveTest
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
        public void EvaluateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 1.0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                for (ushort i = 0; i < ushort.MaxValue; i++)
                {
                    float f = i / 65535.0f;
                    float actual = toneCurve.Evaluate(f);

                    // Assert
                    Assert.AreEqual(f, actual, 1 / 65535.0f);
                }
            }
        }

        [TestMethod()]
        public void EvaluateTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 1.0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                for (ushort i = 0; i < ushort.MaxValue; i++)
                {
                    ushort actual = toneCurve.Evaluate(i);

                    // Assert
                    Assert.AreEqual(i, actual);
                }
            }
        }

        [TestMethod()]
        public void BuildParametricTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            int type = 4;
            double[] parameters = new double[] { 2.4, 1.0 / 1.055, 0.055 / 1.055, 1.0 / 12.92, 0.04045 };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildParametric(context, type, parameters))
            {
                // Assert
                Assert.IsNotNull(toneCurve);
            }
        }

        [TestMethod()]
        public void BuildGammaTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                // Assert
                Assert.IsNotNull(toneCurve);
            }
        }

        [TestMethod()]
        public void BuildSegmentedTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            float[] sampled = new float[] { 0.0f, 1.0f };
            GCHandle hSampled = GCHandle.Alloc(sampled, GCHandleType.Pinned);
            IntPtr ptrSampled = hSampled.AddrOfPinnedObject();

            try
            {
                CurveSegment[] segments = new CurveSegment[]
                {
                    new CurveSegment
                    {
                        x0 = -1e22f,
                        x1 = 1.0f,
                        type = 6,
                        parameters = new double[10] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        nGridPoints = 0,
                        sampledPoints = IntPtr.Zero
                    },
                    new CurveSegment
                    {
                        x0 = 0.0f,
                        x1 = 1.0f,
                        type = 0,
                        parameters = new double[10],
                        nGridPoints = 2,
                        sampledPoints = ptrSampled
                    },
                    new CurveSegment
                    {
                        x0 = 1.0f,
                        x1 = -1e22f,
                        type = 6,
                        parameters = new double[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        nGridPoints = 0,
                        sampledPoints = IntPtr.Zero
                    }
                };

                // Act
                using (var context = Context.Create(plugin, userData))
                using (var toneCurve = ToneCurve.BuildSegmented(context, segments))
                {
                    // Assert
                    Assert.IsNotNull(toneCurve);
                }
            }
            finally
            {
                hSampled.Free();
            }
        }

        [TestMethod()]
        public void BuildTabulatedTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            ushort[] values = new ushort[] { 0, 0, 0, 0, 0, 0x5555, 0x6666, 0x7777, 0x8888, 0x9999, 0xffff, 0xffff, 0xffff, 0xffff, 0xffff, 0xffff };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildTabulated(context, values))
            {
                // Assert
                Assert.IsNotNull(toneCurve);
            }
        }

        [TestMethod()]
        public void BuildTabulatedTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            float[] values = new float[1025];
            const double gamma = 2.2;
            for (int i = 0; i <= 1024; i++)
            {
                double d = i / 1024.0;
                values[i] = (float)Math.Pow(d, gamma);
            }

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildTabulated(context, values))
            {
                // Assert
                Assert.IsNotNull(toneCurve);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            using (var duplicate = toneCurve.Duplicate())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void ReverseTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            using (var duplicate = toneCurve.Reverse())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void ReverseTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            using (var duplicate = toneCurve.Reverse(4096))
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void JoinTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 3.0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var forward = ToneCurve.BuildGamma(context, gamma))
            using (var reverse = ToneCurve.BuildGamma(context, gamma))
            using (var joined = forward.Join(context, reverse, 256))
            {
                // Assert
                Assert.IsNotNull(joined);
            }
        }

        [TestMethod()]
        public void SmoothTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            ushort[] values = new ushort[] { 0, 0, 0, 0, 0, 0x5555, 0x6666, 0x7777, 0x8888, 0x9999, 0xffff, 0xffff, 0xffff, 0xffff, 0xffff, 0xffff };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildTabulated(context, values))
            {
                var smoothed = toneCurve.Smooth(1.0);

                // Assert
            }
        }

        [TestMethod()]
        public void IsMultisegmentTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                var isMultiSegment = toneCurve.IsMultisegment;

                // Assert
                Assert.IsFalse(isMultiSegment);
            }
        }

        [TestMethod()]
        public void IsLinearTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                var isLinear = toneCurve.IsLinear;

                // Assert
                Assert.IsFalse(isLinear);
            }
        }

        [TestMethod()]
        public void IsMonotonicTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                var isMonotonic = toneCurve.IsMonotonic;

                // Assert
                Assert.IsTrue(isMonotonic);
            }
        }

        [TestMethod()]
        public void IsDescendingTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                var isDescending = toneCurve.IsDescending;

                // Assert
                Assert.IsFalse(isDescending);
            }
        }

        [TestMethod()]
        public void EstimateGammaTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double expected = 2.2;
            double precision = 0.01;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, expected))
            {
                var actual = toneCurve.EstimateGamma(precision);

                // Assert
                Assert.AreEqual(expected, actual, precision);
            }
        }

        [TestMethod()]
        public void EstimatedTableEntriesTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;
            uint notExpected = 0;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                var actual = toneCurve.EstimatedTableEntries;

                // Assert
                Assert.AreNotEqual(notExpected, actual);
            }
        }

        [TestMethod()]
        public void EstimatedTableTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double gamma = 2.2;
            IntPtr notExpected = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, gamma))
            {
                IntPtr actual = toneCurve.EstimatedTable;

                // Assert
                Assert.AreNotEqual(notExpected, actual);
            }
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            double expected = 2.2;
            double precision = 0.01;

            using (var profile = Profile.CreatePlaceholder(null))
            {
                using (var toneCurve = ToneCurve.BuildGamma(null, expected))
                {
                    profile.WriteTag(TagSignature.RedTRC, toneCurve);
                }

                // Act
                using (var roToneCurve = profile.ReadTag<ToneCurve>(TagSignature.RedTRC))
                {
                    // Assert
                    var actual = roToneCurve.EstimateGamma(precision);
                    Assert.AreEqual(expected, actual, precision);
                }
            }
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            double expected = 2.2;
            double precision = 0.01;

            using (var profile = Profile.CreatePlaceholder(null))
            {
                using (var toneCurve = ToneCurve.BuildGamma(null, expected))
                {
                    profile.WriteTag(TagSignature.RedTRC, toneCurve);
                }

                // Act
                using (var toneCurve2 = profile.ReadTag<ToneCurve>(TagSignature.RedTRC))
                {
                    // Assert
                    var actual = toneCurve2.EstimateGamma(precision);
                    Assert.AreEqual(expected, actual, precision);
                }
            }
        }
    }
}
