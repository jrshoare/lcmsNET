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

        [TestMethod]
        public void ZeroesTest()
        {
            // Arrange
            double expected = 0.0;

            // Act
            MAT3 m = MAT3.Zeroes();

            // Assert
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    double actual = m[row][column];
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void IdentityTest()
        {
            // Arrange

            // Act
            MAT3 m = MAT3.Identity();

            // Assert
            Assert.AreEqual(1.0, m[0][0]);
            Assert.AreEqual(0.0, m[0][1]);
            Assert.AreEqual(0.0, m[0][2]);

            Assert.AreEqual(0.0, m[1][0]);
            Assert.AreEqual(1.0, m[1][1]);
            Assert.AreEqual(0.0, m[1][2]);

            Assert.AreEqual(0.0, m[2][0]);
            Assert.AreEqual(0.0, m[2][1]);
            Assert.AreEqual(1.0, m[2][2]);
        }

        [TestMethod]
        public void IsIdentityTest()
        {
            // Arrange
            MAT3 m = MAT3.Identity();

            // Act
            bool isIdentity = m.IsIdentity;

            // Assert
            Assert.IsTrue(isIdentity);
        }

        [TestMethod]
        public void IsIdentityTest2()
        {
            // Arrange
            MAT3 m = MAT3.Zeroes();

            // Act
            bool isIdentity = m.IsIdentity;

            // Assert
            Assert.IsFalse(isIdentity);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            // Arrange
            MAT3 i = MAT3.Identity();
            MAT3 m = new MAT3(
                new VEC3[3]
                {
                    new VEC3(1, 2, 3),
                    new VEC3(4, 5, 6),
                    new VEC3(7, 8, 9)
                });

            // Act
            var r = MAT3.Multiply(m, i);

            // Assert
            Assert.AreEqual(1, r[0][0]);
            Assert.AreEqual(2, r[0][1]);
            Assert.AreEqual(3, r[0][2]);
            Assert.AreEqual(4, r[1][0]);
            Assert.AreEqual(5, r[1][1]);
            Assert.AreEqual(6, r[1][2]);
            Assert.AreEqual(7, r[2][0]);
            Assert.AreEqual(8, r[2][1]);
            Assert.AreEqual(9, r[2][2]);
        }

        [TestMethod]
        public void InvertTest()
        {
            // Arrange
            MAT3 m = new MAT3(
                new VEC3[3]
                {
                    new VEC3(3, 2, 1),
                    new VEC3(4, 5, 6),
                    new VEC3(7, 5, 9)
                });

            // Act
            bool inverted = MAT3.Invert(in m, out MAT3 inverse_m);

            // Assert
            Assert.IsTrue(inverted);
            MAT3 identity = MAT3.Multiply(m, inverse_m);
            Assert.IsTrue(identity.IsIdentity);
        }

        [TestMethod]
        public void SolveTest()
        {
            // Arrange
            MAT3 a = new MAT3(
                new VEC3[3]
                {
                    new VEC3(3, 2, 1),
                    new VEC3(4, 5, 6),
                    new VEC3(7, 5, 9)
                });

            VEC3 b = new VEC3(1, 2, 7);

            // Act
            bool solved = MAT3.Solve(in a, in b, out VEC3 _);

            // Assert
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void EvaluateTest()
        {
            // Arrange
            MAT3 a = new MAT3(
                new VEC3[3]
                {
                    new VEC3(3, 2, 1),
                    new VEC3(4, 5, 6),
                    new VEC3(7, 5, 9)
                });

            VEC3 v = new VEC3(1, 2, 7);
            double expected_x = a[0][0] * v[0] + a[0][1] * v[1] + a[0][2] * v[2];
            double expected_y = a[1][0] * v[0] + a[1][1] * v[1] + a[1][2] * v[2];
            double expected_z = a[2][0] * v[0] + a[2][1] * v[1] + a[2][2] * v[2];

            // Act
            VEC3 r = MAT3.Evaluate(in a, in v);
            double actual_x = r[0];
            double actual_y = r[1];
            double actual_z = r[2];

            // Assert
            Assert.AreEqual(expected_x, actual_x, double.Epsilon);
            Assert.AreEqual(expected_y, actual_y, double.Epsilon);
            Assert.AreEqual(expected_z, actual_z, double.Epsilon);
        }
    }
}
