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
    public class ScreeningTest
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
            ScreeningFlags expectedFlag = ScreeningFlags.FrequencyUnitLinesInch;
            uint expectedNChannels = 1;
            ScreeningChannel[] expectedChannels = new ScreeningChannel[16];
            expectedChannels[0].Frequency = 2.0;
            expectedChannels[0].ScreenAngle = 3.0;
            expectedChannels[0].SpotShape = SpotShape.Ellipse;

            // Act
            var target = new Screening(expectedFlag, expectedNChannels, expectedChannels);
            var actualFlag = target.Flag;
            var actualNChannels = target.nChannels;
            var actualChannels = target.Channels;

            // Assert
            Assert.AreEqual(expectedFlag, actualFlag);
            Assert.AreEqual(expectedNChannels, actualNChannels);
            Assert.AreEqual(expectedChannels, actualChannels);
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            using (var profile = Profile.CreatePlaceholder(null))
            {
                ScreeningFlags expectedFlag = ScreeningFlags.FrequencyUnitLinesCm;
                uint expectedNChannels = 1;
                ScreeningChannel[] expectedChannels = new ScreeningChannel[16];
                expectedChannels[0].Frequency = 2.0;
                expectedChannels[0].ScreenAngle = 3.0;
                expectedChannels[0].SpotShape = SpotShape.Ellipse;

                // Act
                var expected = new Screening(expectedFlag, expectedNChannels, expectedChannels);
                int size = Marshal.SizeOf(expected);
                IntPtr data = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(expected, data, false);
                try
                {
                    profile.WriteTag(TagSignature.Screening, data);

                    // Act
                    // implicit call to FromHandle
                    var target = profile.ReadTag<Screening>(TagSignature.Screening);
                    var actualFlag = target.Flag;
                    var actualNChannels = target.nChannels;
                    var actualChannels = target.Channels;

                    // Assert
                    Assert.AreEqual(expectedFlag, actualFlag);
                    Assert.AreEqual(expectedNChannels, actualNChannels);
                    for (int i = 0; i < 16; i++)
                    {
                        Assert.AreEqual(expectedChannels[i], actualChannels[i]);
                    }
                }
                finally
                {
                    Marshal.DestroyStructure(data, typeof(Screening));
                    Marshal.FreeHGlobal(data);
                }
            }
        }
    }
}
