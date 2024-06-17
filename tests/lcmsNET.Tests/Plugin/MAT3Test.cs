// Copyright(c) 2019-2022 John Stevenson-Hoare
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

using lcmsNET.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class MAT3Test
    {
        [TestMethod]
        public void Zeroes_WhenInvoked_ShouldReturnZeroedMatrix()
        {
            // Arrange
            double expected = 0.0;

            // Act
            MAT3 sut = MAT3.Zeroes();

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    double actual = sut[row][column];

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void Identity_WhenInvoked_ShouldReturnIdentityMatrix()
        {
            // Arrange

            // Act
            MAT3 sut = MAT3.Identity();

            // Assert
            Assert.AreEqual(1.0, sut[0][0]);
            Assert.AreEqual(0.0, sut[0][1]);
            Assert.AreEqual(0.0, sut[0][2]);

            Assert.AreEqual(0.0, sut[1][0]);
            Assert.AreEqual(1.0, sut[1][1]);
            Assert.AreEqual(0.0, sut[1][2]);

            Assert.AreEqual(0.0, sut[2][0]);
            Assert.AreEqual(0.0, sut[2][1]);
            Assert.AreEqual(1.0, sut[2][2]);
        }

        [TestMethod]
        public void IsIdentity_WhenIdentityMatrix_ShouldReturnTrue()
        {
            // Arrange
            MAT3 sut = MAT3.Identity();

            // Act
            bool isIdentity = sut.IsIdentity;

            // Assert
            Assert.IsTrue(isIdentity);
        }

        [TestMethod]
        public void IsIdentity_WhenNotIdentityMatrix_ShouldReturnFalse()
        {
            // Arrange
            MAT3 sut = MAT3.Zeroes();

            // Act
            bool isIdentity = sut.IsIdentity;

            // Assert
            Assert.IsFalse(isIdentity);
        }

        [TestMethod]
        public void Multiply_WhenInvoked_ShouldMultiplyTwoMatrices()
        {
            // Arrange
            MAT3 identity = MAT3.Identity();
            MAT3 m = new([new(1, 2, 3), new(4, 5, 6), new(7, 8, 9)]);

            // Act
            var sut = MAT3.Multiply(m, identity);

            // Assert
            Assert.AreEqual(1, sut[0][0]);
            Assert.AreEqual(2, sut[0][1]);
            Assert.AreEqual(3, sut[0][2]);
            Assert.AreEqual(4, sut[1][0]);
            Assert.AreEqual(5, sut[1][1]);
            Assert.AreEqual(6, sut[1][2]);
            Assert.AreEqual(7, sut[2][0]);
            Assert.AreEqual(8, sut[2][1]);
            Assert.AreEqual(9, sut[2][2]);
        }

        [TestMethod]
        public void Invert_WhenInvoked_ShouldInvertMatrix()
        {
            // Arrange
            MAT3 sut = new([new(3, 2, 1), new(4, 5, 6), new(7, 5, 9)]);

            // Act
            bool inverted = MAT3.Invert(in sut, out MAT3 inverse_m);
            MAT3 identity = MAT3.Multiply(sut, inverse_m);

            // Assert
            Assert.IsTrue(inverted);
            Assert.IsTrue(identity.IsIdentity);
        }

        [TestMethod]
        public void Solve_WhenValid_ShouldSolveSystem()
        {
            // Arrange
            MAT3 matrix = new([new(3, 2, 1), new(4, 5, 6), new(7, 5, 9)]);
            VEC3 vector = new(1, 2, 7);

            // Act
            bool solved = MAT3.Solve(in matrix, in vector, out VEC3 _);

            // Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void Evaluate_WhenInvoked_ShouldReturnProductOfMatrixAndVector()
        {
            // Arrange
            MAT3 matrix = new([new VEC3(3, 2, 1), new VEC3(4, 5, 6), new VEC3(7, 5, 9)]);
            VEC3 vector = new(1, 2, 7);

            double expected_x = matrix[0][0] * vector[0] + matrix[0][1] * vector[1] + matrix[0][2] * vector[2];
            double expected_y = matrix[1][0] * vector[0] + matrix[1][1] * vector[1] + matrix[1][2] * vector[2];
            double expected_z = matrix[2][0] * vector[0] + matrix[2][1] * vector[1] + matrix[2][2] * vector[2];

            // Act
            VEC3 result = MAT3.Evaluate(in matrix, in vector);
            double actual_x = result[0];
            double actual_y = result[1];
            double actual_z = result[2];

            // Assert
            Assert.AreEqual(expected_x, actual_x, double.Epsilon);
            Assert.AreEqual(expected_y, actual_y, double.Epsilon);
            Assert.AreEqual(expected_z, actual_z, double.Epsilon);
        }
    }
}
