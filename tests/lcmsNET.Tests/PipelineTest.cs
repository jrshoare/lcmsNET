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
using System.Linq;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class PipelineTest
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
            uint inputChannels = 3;
            uint outputChannels = 3;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                // Assert
                Assert.IsNotNull(pipeline);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint inputChannels = 3;
            uint outputChannels = 4;

            // Act
            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            using (var duplicate = pipeline.Duplicate())
            {
                // Assert
                Assert.IsNotNull(duplicate);
            }
        }

        [TestMethod()]
        public void AppendTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint inputChannels = 3;
            uint outputChannels = 4;

            using (var context = Context.Create(plugin, userData))
            using (var pipeline1 = Pipeline.Create(context, inputChannels, outputChannels))
            using (var pipeline2 = pipeline1.Duplicate())
            {
                // Act
                bool appended = pipeline1.Append(pipeline2);

                // Assert
                Assert.IsTrue(appended);
            }
        }

        [TestMethod()]
        public void EvaluateTest()
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

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                pipeline.Insert(stage, StageLoc.At_End);    // stage is not usable after insertion

                float[] values = new float[4] { 10 / 100.0f, 10 / 100.0f, 0, 12 };

                // Act
                float[] result = pipeline.Evaluate(values);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(values.Length, result.Length);
            }
        }

        [TestMethod()]
        public void EvaluateTest2()
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

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                pipeline.Insert(stage, StageLoc.At_End);    // stage is not usable after insertion

                ushort[] values = new ushort[3] { 0x1234, 0x5678, 0x9ABC };

                // Act
                ushort[] result = pipeline.Evaluate(values);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(values.Length, result.Length);
            }
        }

        [TestMethod()]
        public void EvaluateReverseTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint nGridPoints = 2;
            uint inputChannels = 4;
            uint outputChannels = 3;
            ushort[] table = new ushort[]
            {
                0,         0,         0,          //  0 0 0 0   = ( 0, 0, 0)
                0,         0,         0,          //  0 0 0 1   = ( 0, 0, 0)

                0,         0,         0xffff,     //  0 0 1 0   = ( 0, 0, 1)
                0,         0,         0xffff,     //  0 0 1 1   = ( 0, 0, 1)

                0,         0xffff,    0,          //  0 1 0 0   = ( 0, 1, 0)
                0,         0xffff,    0,          //  0 1 0 1   = ( 0, 1, 0)

                0,         0xffff,    0xffff,     //  0 1 1 0    = ( 0, 1, 1)
                0,         0xffff,    0xffff,     //  0 1 1 1    = ( 0, 1, 1)

                0xffff,    0,         0,          //  1 0 0 0    = ( 1, 0, 0)
                0xffff,    0,         0,          //  1 0 0 1    = ( 1, 0, 0)

                0xffff,    0,         0xffff,     //  1 0 1 0    = ( 1, 0, 1)
                0xffff,    0,         0xffff,     //  1 0 1 1    = ( 1, 0, 1)

                0xffff,    0xffff,    0,          //  1 1 0 0    = ( 1, 1, 0)
                0xffff,    0xffff,    0,          //  1 1 0 1    = ( 1, 1, 0)

                0xffff,    0xffff,    0xffff,     //  1 1 1 0    = ( 1, 1, 1)
                0xffff,    0xffff,    0xffff,     //  1 1 1 1    = ( 1, 1, 1)
            };

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                pipeline.Insert(stage, StageLoc.At_Begin);    // stage is not usable after insertion

                float[] values = new float[4] { 0, 0, 0, 0 };
                float[] hint = new float[3] { 0.1f, 0.1f, 0.1f };

                // Act
                float[] result = pipeline.EvaluateReverse(values, hint, out bool success);

                // Assert
                Assert.IsTrue(success);
                Assert.AreEqual(values.Length, result.Length);
            }
        }

        [TestMethod()]
        public void InsertTest()
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

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                // Act
                bool inserted = pipeline.Insert(stage, StageLoc.At_End);

                // Assert
                Assert.IsTrue(inserted);
                Assert.IsTrue(stage.IsDisposed);    // stage is not usable after insertion
            }
        }

        [TestMethod()]
        public void InputChannelsTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint expected = 3;
            uint outputChannels = 4;

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, expected, outputChannels))
            {
                // Act
                uint actual = pipeline.InputChannels;

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
            uint inputChannels = 3;
            uint expected = 4;

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, expected))
            {
                // Act
                uint actual = pipeline.OutputChannels;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void StageCountTest()
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
            uint expectedBefore = 0, expectedAfter = 1;

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                // Act
                uint actualBefore = pipeline.StageCount;
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                pipeline.Insert(stage, StageLoc.At_End);
                uint actualAfter = pipeline.StageCount;

                // Assert
                Assert.AreEqual(expectedBefore, actualBefore);
                Assert.AreEqual(expectedAfter, actualAfter);
            }
        }

        [TestMethod()]
        public void UnlinkTest()
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

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                bool inserted = pipeline.Insert(stage, StageLoc.At_End);

                // Act
                using (Stage unlinkedStage = pipeline.Unlink(StageLoc.At_Begin))
                {
                    Assert.IsNotNull(unlinkedStage);
                }
            }
        }
        [TestMethod()]
        public void UnlinkAndDisposeTest()
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

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                var stage = Stage.Create(context, nGridPoints, inputChannels, outputChannels, table);
                bool inserted = pipeline.Insert(stage, StageLoc.At_End);
                uint expected = 0;

                // Act
                pipeline.UnlinkAndDispose(StageLoc.At_Begin);

                // Assert
                var actual = pipeline.StageCount;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void EnumerateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint inputChannels = 3;
            uint outputChannels = 3;

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                pipeline.Insert(Stage.Create(context, inputChannels), StageLoc.At_Begin);
                pipeline.Insert(Stage.Create(context, inputChannels), StageLoc.At_Begin);
                int expected = 2;

                // Act
                var actual = pipeline.Count();

                var list = pipeline.ToArray();

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SetAs8BitsFlagTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            uint inputChannels = 3;
            uint outputChannels = 3;

            using (var context = Context.Create(plugin, userData))
            using (var pipeline = Pipeline.Create(context, inputChannels, outputChannels))
            {
                // Act
                // api document states return is TRUE on success, FALSE on error
                // but code at v2.9 returns previous state of flag
                bool previous = pipeline.SetAs8BitsFlag(true);

                // Assert
                Assert.IsFalse(previous);
            }
        }

        [TestMethod()]
        public void FromHandleTest()
        {
            // Arrange
            using (var profile = Profile.CreateInkLimitingDeviceLink(ColorSpaceSignature.CmykData, 150.0))
            {
                profile.LinkTag(TagSignature.AToB1, TagSignature.AToB0);

                // Act
                using (var roPipeline = Pipeline.FromHandle(profile.ReadTag(TagSignature.AToB1)))
                {
                    // Assert
                    Assert.IsNotNull(roPipeline);
                }
            }
        }

        [TestMethod()]
        public void ReadTagTest()
        {
            // Arrange
            using (var profile = Profile.CreateInkLimitingDeviceLink(ColorSpaceSignature.CmykData, 150.0))
            {
                profile.LinkTag(TagSignature.AToB1, TagSignature.AToB0);

                // Act
                using (var pipeline = profile.ReadTag<Pipeline>(TagSignature.AToB1))
                {
                    // Assert
                    Assert.IsNotNull(pipeline);
                }
            }
        }
    }
}
