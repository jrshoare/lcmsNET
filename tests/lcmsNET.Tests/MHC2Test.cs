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
using System.Security.Cryptography;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class MHC2Test
    {
        [TestMethod()]
        public void WriteTag_WhenMHC2_ShouldSucceed()
        {
            // Arrange
            MemoryUtils.UsingPinnedMemory(Constants.MHC2.Curve, (pCurve) =>
            {
                MemoryUtils.UsingPinnedMemory(Constants.MHC2.Matrix, (pMatrix) =>
                {
                    using var sut = Profile.CreatePlaceholder(context: null);
                    try
                    {
                        int encodedVersion = 2070;
                        // next call throws if less than 2.8
                        try { encodedVersion = Cms.EncodedCMMVersion; } catch { }
                        if (encodedVersion < 2160) throw new LcmsNETException();

                        var mhc2 = MHC2Utils.CreateMHC2(pCurve, Constants.MHC2.Curve.Length, pMatrix);

                        // Act
                        bool result = sut.WriteTag(TagSignature.MHC2, mhc2);

                        // Assert
                        Assert.IsTrue(result);
                    }
                    catch (LcmsNETException)
                    {
                        Assert.Inconclusive("Requires Little CMS 2.16 or later.");
                    }
                });
            });
        }

        [TestMethod()]
        public void ReadTag_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            MemoryUtils.UsingPinnedMemory(Constants.MHC2.Curve, (pCurve) =>
            {
                MemoryUtils.UsingPinnedMemory(Constants.MHC2.Matrix, (pMatrix) =>
                {
                    using var sut = Profile.CreatePlaceholder(context: null);
                    try
                    {
                        var mhc2 = MHC2Utils.CreateMHC2(pCurve, Constants.MHC2.Curve.Length, pMatrix);
                        sut.WriteTag(TagSignature.MHC2, mhc2);

                        // Act
                        var actual = sut.ReadTag<MHC2>(TagSignature.MHC2);

                        // Assert
                        var curve = Constants.MHC2.Curve;
                        Assert.AreEqual(curve.Length, actual.CurveEntries);
                        Assert.AreEqual(Constants.MHC2.MinLuminance, actual.MinLuminance, double.Epsilon);
                        Assert.AreEqual(Constants.MHC2.PeakLuminance, actual.PeakLuminance, double.Epsilon);

                        AssertEquality(curve, actual.RedCurve, actual.CurveEntries);
                        AssertEquality(curve, actual.GreenCurve, actual.CurveEntries);
                        AssertEquality(curve, actual.BlueCurve, actual.CurveEntries);

                        unsafe
                        {
                            double* xyz2xyz = (double*)actual.XYZ2XYXmatrix.ToPointer();
                            int n = 0;
                            var matrix = Constants.MHC2.Matrix;
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
                });
            });

            static void AssertEquality(double[] expected, IntPtr actual, int count)
            {
                unsafe
                {
                    double* curve = (double*)actual.ToPointer();
                    for (var i = 0; i < count; i++)
                    {
                        Assert.AreEqual(expected[i], curve[i], double.Epsilon);
                    }
                }
            }
        }
    }
}
