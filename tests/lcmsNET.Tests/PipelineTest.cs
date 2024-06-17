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
using System.Linq;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class PipelineTest
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = Pipeline.Create(context, inputChannels: 3, outputChannels: 3);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = Pipeline.Create(expected, inputChannels: 3, outputChannels: 3);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Act
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Duplicate_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Duplicate());
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 4);

            // Act
            using var duplicate = sut.Duplicate();

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void Append_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            using var duplicate = sut.Duplicate();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Append(duplicate));
        }

        [TestMethod()]
        public void Append_WhenValid_ShouldSucceed()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 4);
            using var duplicate = sut.Duplicate();

            // Act
            bool result = sut.Append(duplicate);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Evaluate_WhenFloatArrayIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);

            float[] values = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Evaluate(values));
        }

        [TestMethod()]
        public void Evaluate_WhenFloatArrayLengthInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            uint inputChannels = 3;
            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels: 3);

            float[] values = new float[inputChannels - 1];  // invalid when != no. of input channels

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Evaluate(values));
        }

        [TestMethod()]
        public void Evaluate_WhenValidFloatArray_ShouldEvaluatePipeline()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);
            sut.Insert(stage, StageLoc.At_End);    // stage is not usable after insertion

            float[] values = [10 / 100.0f, 10 / 100.0f, 0];

            // Act
            float[] result = sut.Evaluate(values);

            // Assert
            Assert.AreEqual((int)outputChannels, result.Length);
        }

        [TestMethod()]
        public void Evaluate_WhenUShortArrayIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            ushort[] values = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Evaluate(values));
        }

        [TestMethod()]
        public void Evaluate_WhenUShortArrayLengthInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            uint inputChannels = 3;
            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels: 3);

            ushort[] values = new ushort[inputChannels - 1];  // invalid when != no. of input channels

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Evaluate(values));
        }

        [TestMethod()]
        public void Evaluate_WhenValidUShortArray_ShouldEvaluatePipeline()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);
            sut.Insert(stage, StageLoc.At_End);    // stage is not usable after insertion

            ushort[] values = [0x1234, 0x5678, 0x9ABC];

            // Act
            ushort[] result = sut.Evaluate(values);

            // Assert
            Assert.AreEqual((int)outputChannels, result.Length);
        }

        [TestMethod()]
        public void EvaluateReverse_WhenArrayIsNull_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            float[] values = null;
            float[] hint = [0.1f, 0.1f, 0.1f];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.EvaluateReverse(values, hint, out bool success));
        }

        [TestMethod()]
        public void EvaluateReverse_WhenArrayLengthInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            uint outputChannels = 3;
            using var sut = Pipeline.Create(context: null, inputChannels: 4, outputChannels);

            float[] values = new float[outputChannels - 1];  // invalid when != no. of output channels
            float[] hint = [0.1f, 0.1f, 0.1f];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.EvaluateReverse(values, hint, out bool success));
        }

        [TestMethod()]
        public void EvaluateReverse_WhenValidArray_ShouldEvaluatePipelineInReverse()
        {
            // Arrange
            uint inputChannels = 4;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table2);
            sut.Insert(stage, StageLoc.At_Begin);    // stage is not usable after insertion

            float[] values = [0, 0, 0];
            float[] hint = [0.1f, 0.1f, 0.1f];

            // Act
            float[] result = sut.EvaluateReverse(values, hint, out bool success);

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual((int)inputChannels, result.Length);
        }

        [TestMethod()]
        public void Insert_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            sut.Dispose();

            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Insert(stage, StageLoc.At_End));
        }

        [TestMethod()]
        public void Insert_WhenValid_ShouldSucceedAndCloseStage()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);

            // Act
            bool result = sut.Insert(stage, StageLoc.At_End);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(stage.IsClosed);    // stage is not usable after insertion
        }

        [TestMethod()]
        public void InputChannels_WhenInvoked_ShouldGetNumberOfInputChannels()
        {
            // Arrange
            uint expected = 3;
            uint outputChannels = 4;

            using var sut = Pipeline.Create(context: null, expected, outputChannels);

            // Act
            uint actual = sut.InputChannels;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void OutputChannels_WhenInvoked_ShouldGetNumberOfOutputChannels()
        {
            // Arrange
            uint inputChannels = 3;
            uint expected = 4;

            using var sut = Pipeline.Create(context: null, inputChannels, expected);

            // Act
            uint actual = sut.OutputChannels;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void StageCount_WhenInvoked_ShouldGetNumberOfStages()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;
            uint expectedBefore = 0, expectedAfter = 1;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);

            // Act
            uint actualBefore = sut.StageCount;
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);
            sut.Insert(stage, StageLoc.At_End);
            uint actualAfter = sut.StageCount;

            // Assert
            Assert.AreEqual(expectedBefore, actualBefore);
            Assert.AreEqual(expectedAfter, actualAfter);
        }

        [TestMethod()]
        public void Unlink_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Unlink(StageLoc.At_Begin));
        }

        [TestMethod()]
        public void Unlink_WhenPipelineHasStages_ShouldRemoveAndReturnStage()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);
            sut.Insert(stage, StageLoc.At_End);

            // Act
            using Stage unlinkedStage = sut.Unlink(StageLoc.At_Begin);

            // Assert
            Assert.IsNotNull(unlinkedStage);
        }

        [TestMethod()]
        public void Unlink_WhenPipelineHasNoStage_ShouldReturnNull()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);

            // Act
            using Stage unlinkedStage = sut.Unlink(StageLoc.At_Begin);

            // Assert
            Assert.IsNull(unlinkedStage);
        }

        [TestMethod()]
        public void UnlinkAndDispose_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.UnlinkAndDispose(StageLoc.At_Begin));
        }

        [TestMethod()]
        public void UnlinkAndDispose_WhenPipelineHasStages_ShouldRemoveAndDisposeStage()
        {
            // Arrange
            uint inputChannels = 3;
            uint outputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels);
            var stage = StageUtils.CreateStage(inputChannels, outputChannels, Constants.Stage.Table1);
            sut.Insert(stage, StageLoc.At_End);
            uint expected = 0;

            // Act
            sut.UnlinkAndDispose(StageLoc.At_Begin);

            // Assert
            var actual = sut.StageCount;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetEnumerator_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.GetEnumerator());
        }

        [TestMethod()]
        public void GetEnumerator_WhenInvoked_ShouldReturnAddedStages()
        {
            // Arrange
            uint inputChannels = 3;

            using var sut = Pipeline.Create(context: null, inputChannels, outputChannels: 3);
            sut.Insert(Stage.Create(context: null, inputChannels), StageLoc.At_Begin);
            sut.Insert(Stage.Create(context: null, inputChannels), StageLoc.At_Begin);
            int expected = 2;

            // Act
            var actual = sut.Count();   // use LINQ to enumerate count

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SetAs8BitsFlag_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.SetAs8BitsFlag(true));
        }

        [TestMethod()]
        public void SetAs8BitsFlag_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            using var sut = Pipeline.Create(context: null, inputChannels: 3, outputChannels: 3);

            // Act
            // api document states return is TRUE on success, FALSE on error
            // but code at v2.9 returns previous state of flag
            bool previous = sut.SetAs8BitsFlag(true);

            // Assert
            Assert.IsFalse(previous);
        }
    }
}
