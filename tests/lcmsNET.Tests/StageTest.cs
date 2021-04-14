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
    public class StageTest
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
        public void CreateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nChannels = 3;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, nChannels))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void CreateTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nChannels = 3;

            using (var context = Context.Create(plugin, userData))
            using (var toneCurve = ToneCurve.BuildGamma(context, 2.2))
            {
                ToneCurve[] curves = new ToneCurve[3] { toneCurve, toneCurve, toneCurve };

                // Act
                using (var stage = Stage.Create(context, nChannels, curves))
                {
                    // Assert
                    Assert.IsNotNull(stage);
                }
            }
        }

        [TestMethod()]
        public void CreateTest2a()
        {
            // Arrange
            uint nChannels = 3;

            // Act
            using (var stage = Stage.Create(null, nChannels, null))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void CreateTest3()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            double[,] matrix = new double[3, 3]
            {
                { 1.0, 0.0, 0.0 },
                { 0.0, 1.0, 0.0 },
                { 0.0, 0.0, 1.0 }
            };
            double[] offset = new double[] { 0, 0, 0 };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, matrix, offset))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void CreateTest4()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nGridPoints = 2;
            uint inputChannels = 3;
            uint outputChannels = 3;
            ushort[] table = new ushort[]
            {
                0,    0,   0,                 // 0 0 0
                0,    0,   0xffff,            // 0 0 1

                0,    0xffff,    0,           // 0 1 0
                0,    0xffff,    0xffff,      // 0 1 1

                0xffff,    0,    0,           // 1 0 0
                0xffff,    0,    0xffff,      // 1 0 1

                0xffff,    0xffff,   0,       // 1 1 0
                0xffff,    0xffff,   0xffff,  // 1 1 1
            };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void CreateTest5()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nGridPoints = 2;
            uint inputChannels = 3;
            uint outputChannels = 3;
            float[] table = new float[]
            {
                0,    0,    0,
                0,    0,    1.0f,

                0,    1.0f,    0,
                0,    1.0f,    1.0f,

                1.0f,    0,    0,
                1.0f,    0,    1.0f,

                1.0f,    1.0f,    0,
                1.0f,    1.0f,    1.0f
            };

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void CreateTest6()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint[] clutPoint = new uint[] { 7, 8, 9 };
            uint outputChannels = 3;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, clutPoint, outputChannels, (ushort[])null))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void CreateTest7()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint[] clutPoint = new uint[] { 7, 8, 9 };
            uint outputChannels = 3;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, clutPoint, outputChannels, (float[])null))
            {
                // Assert
                Assert.IsNotNull(stage);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nChannels = 3;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, nChannels))
            using (var duplicate = stage.Duplicate())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void StageTypeTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nChannels = 3;
            StageSignature expected = StageSignature.IdentityElemType;

            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, nChannels))
            {
                // Act
                StageSignature actual = stage.StageType;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void InputChannelsTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 3;

            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, expected))
            {
                // Act
                uint actual = stage.InputChannels;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void OutputChannelsTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 3;

            using (var context = Context.Create(plugin, userData))
            using (var stage = Stage.Create(context, expected))
            {
                // Act
                uint actual = stage.OutputChannels;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        private static ushort Fn8D1(ushort a1, ushort a2, ushort a3, ushort a4, ushort a5, ushort a6, ushort a7, ushort a8, uint m)
        {
            return (ushort)((a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8) / m);
        }

        private static ushort Fn8D2(ushort a1, ushort a2, ushort a3, ushort a4, ushort a5, ushort a6, ushort a7, ushort a8, uint m)
        {
            return (ushort)((a1 + 3 * a2 + 3 * a3 + a4 + a5 + a6 + a7 + a8) / (m + 4));
        }

        private static ushort Fn8D3(ushort a1, ushort a2, ushort a3, ushort a4, ushort a5, ushort a6, ushort a7, ushort a8, uint m)
        {
            return (ushort)((3 * a1 + 2 * a2 + 3 * a3 + a4 + a5 + a6 + a7 + a8) / (m + 5));
        }

        private static int Sampler3D16Bit(ushort[] input, ushort[] output, IntPtr cargo)
        {
            output[0] = Fn8D1(input[0], input[1], input[2], 0, 0, 0, 0, 0, 3);
            output[1] = Fn8D2(input[0], input[1], input[2], 0, 0, 0, 0, 0, 3);
            output[2] = Fn8D3(input[0], input[1], input[2], 0, 0, 0, 0, 0, 3);

            return 1; // 1 = true, 0 = false
        }

        [TestMethod()]
        public void SampleCLUTTest1()
        {
            // Arrange
            using (var stage = Stage.Create(null, 9, 3, 3, (ushort[])null))
            {
                // Act
                var actual = stage.SampleCLUT(Sampler3D16Bit, IntPtr.Zero, StageSamplingFlags.None);

                // Assert
                Assert.IsTrue(actual);
            }
        }

        private static int Sampler3DFloat(float[] input, float[] output, IntPtr cargo)
        {
            return 1; // 1 = true, 0 = false
        }

        [TestMethod()]
        public void SampleCLUTTest2()
        {
            // Arrange
            using (var stage = Stage.Create(null, 9, 3, 3, (float[])null))
            {
                // Act
                var actual = stage.SampleCLUT(Sampler3DFloat, IntPtr.Zero, StageSamplingFlags.None);

                // Assert
                Assert.IsTrue(actual);
            }
        }

        private static int EstimateTAC16Bit(ushort[] input, ushort[] output, IntPtr cargo)
        {
            Assert.IsNull(output);

            return 1; // 1 = true, 0 = false
        }

        [TestMethod()]
        public void SliceSpaceTest1()
        {
            // Arrange
            uint[] gridPoints = { 6, 74, 74 };

            // Act
            var actual = Stage.SliceSpace(gridPoints, EstimateTAC16Bit, IntPtr.Zero);

            // Assert
            // (in callback)
        }

        private static int EstimateTACFloat(float[] input, float[] output, IntPtr cargo)
        {
            Assert.IsNull(output);

            return 1; // 1 = true, 0 = false
        }

        [TestMethod()]
        public void SliceSpaceTest2()
        {
            // Arrange
            uint[] gridPoints = { 2, 16, 16 };

            // Act
            var actual = Stage.SliceSpace(gridPoints, EstimateTACFloat, IntPtr.Zero);

            // Assert
            // (in callback)
        }
    }
}
