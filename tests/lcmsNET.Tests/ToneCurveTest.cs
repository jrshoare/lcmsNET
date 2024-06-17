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
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ToneCurveTest
    {
        [TestMethod()]
        public void Evaluate_WhenValid_ShouldEvaluateFloatingPointNumber()
        {
            // Arrange
            const float delta = 1 / 65535.0f;
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 1.0);

            for (ushort i = 0; i < ushort.MaxValue; i++)
            {
                float f = i / 65535.0f;

                // Act
                float actual = sut.Evaluate(f);

                // Assert
                Assert.AreEqual(f, actual, delta);
            }
        }

        [TestMethod()]
        public void Evaluate_WhenValid_ShouldEvaluate16BitNumber()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 1.0);

            for (ushort i = 0; i < ushort.MaxValue; i++)
            {
                // Act
                ushort actual = sut.Evaluate(i);

                // Assert
                Assert.AreEqual(i, actual);
            }
        }

        [TestMethod()]
        public void BuildParametric_WhenInvoked_ShouldReturnParametricCurve()
        {
            // Arrange
            double[] parameters = [2.4, 1.0 / 1.055, 0.055 / 1.055, 1.0 / 12.92, 0.04045];
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = ToneCurve.BuildParametric(context, type: 4, parameters);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod()]
        public void BuildGamma_WhenInvoked_ShouldReturnGammaCurve()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod()]
        public void BuildSegmented_WhenInvoked_ShouldReturnSegmentedCurve()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            float[] sampled = [0.0f, 1.0f];

            MemoryUtils.UsingPinnedMemory(sampled, (pSampled) =>
            {
                CurveSegment[] segments = ToneCurveUtils.CreateCurveSegments(pSampled);

                // Act
                using var sut = ToneCurve.BuildSegmented(context, segments);

                // Assert
                Assert.IsNotNull(sut);
            });
        }

        [TestMethod()]
        public void BuildTabulated_WhenInvoked_ShouldCreateFromSupplied16BitArray()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            ushort[] values = [0, 0, 0, 0, 0, 0x5555, 0x6666, 0x7777, 0x8888, 0x9999, 0xffff, 0xffff, 0xffff, 0xffff, 0xffff, 0xffff];

            // Act
            using var sut = ToneCurve.BuildTabulated(context, values);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod()]
        public void BuildTabulated_WhenInvoked_ShouldCreateFromSuppliedFloatingPointArray()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            const double gamma = 2.2;

            float[] values = new float[1025];
            for (int i = 0; i <= 1024; i++)
            {
                values[i] = (float)Math.Pow(i / 1024.0, gamma);
            }

            // Act
            using var sut = ToneCurve.BuildTabulated(context, values);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotEqual(sut, duplicate);
        }

        [TestMethod()]
        public void Reverse_WhenInvoked_ShouldReturnInverse()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            using var reversed = sut.Reverse();

            // Assert
            Assert.AreNotEqual(sut, reversed);
        }

        [TestMethod()]
        public void Reverse_WhenInvokedWithNumberOfSamples_ShouldReturnInverseOrTabulatedCurve()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            using var reversed = sut.Reverse(4096);

            // Assert
            Assert.AreNotEqual(sut, reversed);
        }

        [TestMethod()]
        public void Join_WhenInvoked_ShouldReturnComposite()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var toneCurve1 = ToneCurve.BuildGamma(context, gamma: 3.0);
            using var toneCurve2 = ToneCurve.BuildGamma(context, gamma: 3.0);
            using var sut = toneCurve1.Join(context, toneCurve2, 256);

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod()]
        public void Smooth_WhenInvoked_ShouldSmoothCurve()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            ushort[] values = [0, 0xffff];
            using var sut = ToneCurve.BuildTabulated(context, values);

            // Act
            var smoothed = sut.Smooth(1.0);

            // Assert
            Assert.IsTrue(smoothed);
        }

        [TestMethod()]
        public void IsMultiSegment_WhenSingleSegment_ShouldReturnFalse()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            var isMultiSegment = sut.IsMultisegment;

            // Assert
            Assert.IsFalse(isMultiSegment);
        }

        [TestMethod()]
        public void IsLinear_WhenNonLinear_ShouldReturnFalse()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            var isLinear = sut.IsLinear;

            // Assert
            Assert.IsFalse(isLinear);
        }

        [TestMethod()]
        public void IsMonotonic_WhenIsMonotonic_ShouldReturnTrue()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            var isMonotonic = sut.IsMonotonic;

            // Assert
            Assert.IsTrue(isMonotonic);
        }

        [TestMethod()]
        public void IsDescending_WhenNotDescending_ShouldReturnFalse()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);

            // Act
            var isDescending = sut.IsDescending;

            // Assert
            Assert.IsFalse(isDescending);
        }

        [TestMethod()]
        public void EstimateGamma_WhenInvoked_ShouldEstimateApparentGammaOfCurve()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            double expected = 2.2;
            using var sut = ToneCurve.BuildGamma(context, gamma: expected);
            double precision = 0.01;

            // Act
            var actual = sut.EstimateGamma(precision);

            // Assert
            Assert.AreEqual(expected, actual, precision);
        }

        [TestMethod()]
        public void EstimatedTableEntries_WhenInvokedForGammaCurve_ShouldBeNonZero()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);
            uint notExpected = 0;

            // Act
            var actual = sut.EstimatedTableEntries;

            // Assert
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod()]
        public void EstimatedTable_WhenInvokedForGamma_ShouldBeNonZeroPointer()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = ToneCurve.BuildGamma(context, gamma: 2.2);
            IntPtr notExpected = IntPtr.Zero;

            // Act
            IntPtr actual = sut.EstimatedTable;

            // Assert
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod()]
        public void GetCurveSegment_WhenInvoked_ShouldGetCurveSegmentAtIndex()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            float[] sampled = [0.0f, 1.0f];

            try
            {
                MemoryUtils.UsingPinnedMemory(sampled, (pSampled) =>
                {
                    CurveSegment[] segments = ToneCurveUtils.CreateCurveSegments(pSampled);
                    using var sut = ToneCurve.BuildSegmented(context, segments);

                    // Act
                    CurveSegment curveSegment = sut.GetCurveSegment(segmentIndex: 1);
                    var x0 = curveSegment.x0;
                    var x1 = curveSegment.x1;
                    var type = curveSegment.type;
                    var parameters = curveSegment.parameters;
                    var nGridPoints = curveSegment.nGridPoints;
                    var sampledPoints = curveSegment.sampledPoints;

                    // Assert
                    Assert.AreEqual(segments[1].x0, x0);
                    Assert.AreEqual(segments[1].x1, x1);
                    Assert.AreEqual(segments[1].type, type);
                    CollectionAssert.AreEqual(segments[1].parameters, parameters);
                    Assert.AreEqual(segments[1].nGridPoints, nGridPoints);
                    if (type == 0)
                    {
                        unsafe
                        {
                            float* points = (float*)sampledPoints.ToPointer();
                            for (var i = 0; i < nGridPoints; i++)
                            {
                                Assert.AreEqual(sampled[i], points[i]);
                            }
                        }
                    }
                });
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.16 or later.");
            }
        }
    }
}
