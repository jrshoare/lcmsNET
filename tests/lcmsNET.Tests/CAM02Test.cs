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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class CAM02Test
    {
        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();
            using var context = ContextUtils.CreateContext();

            // Act
            using var sut = CAM02.Create(context, conditions);

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNonNullContext_ShouldHaveNonNullContext()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();
            using var expected = ContextUtils.CreateContext();

            // Act
            using var sut = CAM02.Create(expected, conditions);
            var actual = sut.Context;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod()]
        public void Create_WhenInstantiatedWithNullContext_ShouldHaveNullContext()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();

            // Act
            using var sut = CAM02.Create(null, conditions);
            var actual = sut.Context;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void Forward_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();
            CIEXYZ xyz = new() { X = 0.8322, Y = 1.0, Z = 0.7765 };

            var sut = CAM02.Create(null, conditions);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Forward(xyz, out JCh jch));
        }

        [TestMethod()]
        public void Forward_WhenNotDisposed_ShouldEvaluateModelInForwardDirection()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();
            CIEXYZ xyz = new() { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using var context = ContextUtils.CreateContext();
            using var sut = CAM02.Create(context, conditions);

            // Act
            sut.Forward(xyz, out JCh jch);

            // Assert
        }

        [TestMethod()]
        public void Reverse_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();
            CIEXYZ xyz = new() { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using var sut = CAM02.Create(null, conditions);
            sut.Forward(xyz, out JCh jch);
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Reverse(jch, out CIEXYZ xyz2));
        }

        [TestMethod()]
        public void Reverse_WhenNotDisposed_ShouldEvaluateModelInReverseDirection()
        {
            // Arrange
            var conditions = ViewingConditionsUtils.CreateViewingConditions();
            CIEXYZ xyz = new() { X = 0.8322, Y = 1.0, Z = 0.7765 };

            using var context = ContextUtils.CreateContext();
            using var sut = CAM02.Create(context, conditions);
            sut.Forward(xyz, out JCh jch);

            // Act
            sut.Reverse(jch, out CIEXYZ xyz2);

            // Assert
        }
    }
}
