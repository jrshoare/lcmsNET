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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class StageTest
    {
        [TestMethod()]
        public void Create_WhenForEmptyStage_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Stage.Create(context, nChannels: 3);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContainToneCurves_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var toneCurve = ToneCurve.BuildGamma(context, gamma: 2.2);
            ToneCurve[] curves = [toneCurve, toneCurve, toneCurve];

            // Act
            using var sut = Stage.Create(context, nChannels: 3, curves);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContainIdentityToneCurves_ShouldHaveValidHandle()
        {
            // Act
            using var sut = Stage.Create(context: null, nChannels: 3, curves: null);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContainMatrixAndOffset_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            double[,] matrix = new double[3, 3]
            {
                { 1.0, 0.0, 0.0 },
                { 0.0, 1.0, 0.0 },
                { 0.0, 0.0, 1.0 }
            };
            double[] offset = [0, 0, 0];

            // Act
            using var sut = Stage.Create(context, matrix, offset);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContain16BitLUT_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Stage.Create(context, nGridPoints: 2, inputChannels: 3, outputChannels: 3, Constants.Stage.Table1);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContainFloatingPointLUT_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Stage.Create(context, nGridPoints: 2, inputChannels: 3, outputChannels: 3, Constants.Stage.Table3);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContain16BitLUTWithDifferentDimensions_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            uint[] clutPoint = [7, 8, 9];

            // Act
            using var sut = Stage.Create(context, clutPoint, outputChannels: 3, (ushort[])null);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenToContainFloatingPointLUTWithDifferentDimensions_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            uint[] clutPoint = [7, 8, 9];

            // Act
            using var sut = Stage.Create(context, clutPoint, outputChannels: 3, (float[])null);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var sut = Stage.Create(context, nChannels: 3);

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void StageType_WhenEmpty_ShouldBeIdentityElemType()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();
            using var stage = Stage.Create(context, nChannels: 3);
            StageSignature expected = StageSignature.IdentityElemType;

            // Act
            StageSignature actual = stage.StageType;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void InputChannels_WhenGetting_ShouldBeValueUsedToCreate()
        {
            // Arrange
            uint expected = 3;

            using var context = ContextUtils.CreateContext();
            using var sut = Stage.Create(context, expected);

            // Act
            uint actual = sut.InputChannels;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void OutputChannels_WhenGetting_ShouldBeValueUsedToCreate()
        {
            // Arrange
            uint expected = 3;

            using var context = ContextUtils.CreateContext();
            using var sut = Stage.Create(context, expected);

            // Act
            uint actual = sut.OutputChannels;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SampleCLUT_WhenValidFor16BitSampler_ShouldSucceed()
        {
            // Arrange
            using var sut = Stage.Create(context: null, nGridPoints: 9, inputChannels: 3, outputChannels: 3, (ushort[])null);

            // Act
            var actual = sut.SampleCLUT((Sampler16)StageUtils.Sampler3D, cargo: nint.Zero, StageSamplingFlags.None);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void SampleCLUT_WhenValidForFloatingPointSampler_ShouldSucceed()
        {
            // Arrange
            using var sut = Stage.Create(context: null, nGridPoints: 9, inputChannels: 3, outputChannels: 3, (float[])null);

            // Act
            var actual = sut.SampleCLUT((SamplerFloat)StageUtils.Sampler3D, cargo: nint.Zero, StageSamplingFlags.None);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void SliceSpace_WhenInvoked_ShouldSliceTargetSpaceCalling16BitSamplerOnEachNode()
        {
            // Arrange
            uint[] gridPoints = [6, 74, 74];

            // Act
            var actual = Stage.SliceSpace(gridPoints, (Sampler16)StageUtils.EstimateTAC, cargo: IntPtr.Zero);

            // Assert
            // (in callback)
        }

        [TestMethod()]
        public void SliceSpace_WhenInvoked_ShouldSliceTargetSpaceCallingFloatingPointSamplerOnEachNode()
        {
            // Arrange
            uint[] gridPoints = [2, 16, 16];

            // Act
            var actual = Stage.SliceSpace(gridPoints, (SamplerFloat)StageUtils.EstimateTAC, cargo: IntPtr.Zero);

            // Assert
            // (in callback)
        }

        [TestMethod()]
        public void SampleCLUT_WhenInvoked_ShouldCall16BitSamplerOnEachNode()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.Lab.icc");
            using var profile = Profile.Open(ms.GetBuffer());
            using var pipeline = profile.ReadTag<Pipeline>(TagSignature.AToB0);

            foreach (var sut in pipeline)
            {
                if (sut.StageType == StageSignature.CLutElemType)
                {
                    // Act
                    sut.SampleCLUT((Sampler16)StageUtils.SamplerInspect, cargo: IntPtr.Zero, StageSamplingFlags.Inspect);
                }
            }
        }

        [TestMethod()]
        public void SampleCLUT_WhenInvoked_ShouldCallFloatingPointSamplerOnEachNode()
        {
            // Arrange
            using MemoryStream ms = ResourceUtils.Save(".Resources.Lab.icc");
            using var profile = Profile.Open(ms.GetBuffer());
            using var pipeline = profile.ReadTag<Pipeline>(TagSignature.AToB0);

            foreach (var sut in pipeline)
            {
                if (sut.StageType == StageSignature.CLutElemType)
                {
                    // Act
                    sut.SampleCLUT((SamplerFloat)StageUtils.SamplerInspect, cargo: IntPtr.Zero, StageSamplingFlags.Inspect);
                }
            }
        }
    }
}
