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

namespace lcmsNET.Tests
{
    [TestClass()]
    public class VideoCardGammaTest
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
        public void ConstructorTest()
        {
            // Arrange
            int type = 5;
            double[] parameters = [0.45, Math.Pow(1.099, 1.0 / 0.45), 0.0, 4.5, 0.018, -0.099, 0.0];

            using var expectedRed = ToneCurve.BuildParametric(null, type, parameters);
            using var expectedGreen = ToneCurve.BuildParametric(null, type, parameters);
            using var expectedBlue = ToneCurve.BuildParametric(null, type, parameters);
            // Act
            var target = new VideoCardGamma(expectedRed, expectedGreen, expectedBlue);
            var actualRed = target.Red;
            var actualGreen = target.Green;
            var actualBlue = target.Blue;

            // Assert
            Assert.AreSame(expectedRed, actualRed);
            Assert.AreSame(expectedGreen, actualGreen);
            Assert.AreSame(expectedBlue, actualBlue);
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            int type = 5;
            double[] parameters = [0.45, Math.Pow(1.099, 1.0 / 0.45), 0.0, 4.5, 0.018, -0.099, 0.0];

            using var red = ToneCurve.BuildParametric(null, type, parameters);
            using var green = ToneCurve.BuildParametric(null, type, parameters);
            using var blue = ToneCurve.BuildParametric(null, type, parameters);
            var target = new VideoCardGamma(red, green, blue);

            using (var profile = Profile.CreatePlaceholder(null))
            {
                profile.WriteTag(TagSignature.Vcgt, target);

                // Act
                // implicit call to FromHandle
                var actual = profile.ReadTag<VideoCardGamma>(TagSignature.Vcgt);
                var actualRed = actual.Red;
                var actualGreen = actual.Green;
                var actualBlue = actual.Blue;

                // Assert
                Assert.IsNotNull(actualRed);
                Assert.IsNotNull(actualGreen);
                Assert.IsNotNull(actualBlue);
            }
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            int type = 5;
            double[] parameters = [0.45, Math.Pow(1.099, 1.0 / 0.45), 0.0, 4.5, 0.018, -0.099, 0.0];

            using var red = ToneCurve.BuildParametric(null, type, parameters);
            using var green = ToneCurve.BuildParametric(null, type, parameters);
            using var blue = ToneCurve.BuildParametric(null, type, parameters);
            var target = new VideoCardGamma(red, green, blue);

            using (var profile = Profile.CreatePlaceholder(null))
            {
                profile.WriteTag(TagSignature.Vcgt, target);

                // Act
                var actual = profile.ReadTag<VideoCardGamma>(TagSignature.Vcgt);
                var actualRed = actual.Red;
                var actualGreen = actual.Green;
                var actualBlue = actual.Blue;

                // Assert
                Assert.IsNotNull(actualRed);
                Assert.IsNotNull(actualGreen);
                Assert.IsNotNull(actualBlue);
            }
        }
    }
}
