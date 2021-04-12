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
    }
}
