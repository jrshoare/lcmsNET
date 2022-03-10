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

using lcmsNET.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests
{
    [TestClass()]
    public class ContextTest
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

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                // Assert
                Assert.IsNotNull(context);
            }
        }

        [TestMethod()]
        public void DuplicateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                using (var duplicate = context.Duplicate(userData))
                {
                    // Assert
                    Assert.IsNotNull(duplicate);
                    Assert.AreNotSame(duplicate, context);
                }
            }
        }

        [TestMethod()]
        public void GetUserDataTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            byte[] bytes = new byte[] { 0xff, 0xaa, 0xdd, 0xee };
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr expected = handle.AddrOfPinnedObject();
            try
            {
                using (var context = Context.Create(plugin, expected))
                {
                    // Act
                    IntPtr actual = context.UserData;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        [TestMethod()]
        public void GetUserDataTest2()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr expected = IntPtr.Zero;

            using (var context = Context.Create(plugin, expected))
            {
                // Act
                IntPtr actual = context.UserData;

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void GetUserDataTestDisposed()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            byte[] bytes = new byte[] { 0xff, 0xaa, 0xdd, 0xee };
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr userData = handle.AddrOfPinnedObject();

            // when disposed the context ID is zero which also identifies the global context so
            // the user data will be null as it is not possible to set global context user data
            IntPtr expected = IntPtr.Zero;
            try
            {
                using (var context = Context.Create(plugin, userData))
                {
                    context.Dispose();

                    // Act
                    IntPtr actual = context.UserData;

                    // Assert
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        [TestMethod()]
        public void IDTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;
            IntPtr notExpected = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                IntPtr actual = context.ID;

                // Assert
                Assert.AreNotEqual(notExpected, actual);
            }
        }

        [TestMethod()]
        public void RegisterPluginsTest()
        {
            try
            {
                // Arrange
                IntPtr plugin = IntPtr.Zero;
                IntPtr userData = IntPtr.Zero;

                // Act
                using (var context = Context.Create(plugin, userData))
                {
                    PluginTag tag = new PluginTag
                    {
                        Base = new PluginBase
                        {
                            Magic = Cms.PluginMagicNumber,
                            ExpectedVersion = (uint)Cms.EncodedCMMVersion,    // >= 2.8
                            Type = PluginType.Tag,
                            Next = IntPtr.Zero
                        },
                        Signature = (TagSignature)0x696e6b63,   // 'inkc'
                        Descriptor = new TagDescriptor
                        {
                            ElemCount = 1,
                            nSupportedTypes = 1,
                            SupportedTypes = new TagTypeSignature[TagDescriptor.MAX_TYPES_IN_LCMS_PLUGIN],
                            Decider = null
                        }
                    };
                    tag.Descriptor.SupportedTypes[0] = TagTypeSignature.Lut16;

                    int rawsize = Marshal.SizeOf(tag);
                    IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                    Marshal.StructureToPtr(tag, buffer, false);
                    try
                    {
                        var registered = context.RegisterPlugins(buffer);

                        // Assert
                        Assert.IsTrue(registered);
                    }
                    finally
                    {
                        context.UnregisterPlugins();
                        Marshal.DestroyStructure(buffer, typeof(PluginTag));
                        Marshal.FreeHGlobal(buffer);
                    }
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
        }

        [TestMethod()]
        public void UnregisterPluginsTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            // Act
            using (var context = Context.Create(plugin, userData))
            {
                context.UnregisterPlugins();

                // Assert
            }
        }

        [TestMethod()]
        public void SetErrorHandlerTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            {
                // Act
                context.SetErrorHandler(HandleError);

                TestContext.WriteLine($"context.ID: {context.ID}");
                // force error to observe output in Test Explorer results window for this test
                try { Profile.Open(context, @"???", "r"); } catch { }

                // restore default error handler
                context.SetErrorHandler(null);
            }

            // Assert
            void HandleError(IntPtr contextID, int errorCode, string errorText)
            {
                TestContext.WriteLine($"contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
            }
        }

        [TestMethod()]
        public void AlarmCodesTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            {
                ushort[] alarmCodes = new ushort[16] { 10, 23, 46, 92, 1007, 2009, 6789, 7212, 8114, 9032, 10556, 11267, 12980, 13084, 14112, 15678 };

                // Act
                context.AlarmCodes = alarmCodes;
                var values = context.AlarmCodes;

                // Assert
                for (int i = 0; i < alarmCodes.Length; i++)
                {
                    Assert.AreEqual(alarmCodes[i], values[i]);
                }
            }
        }

        [TestMethod()]
        public void AdaptationStateTest()
        {
            // Arrange
            IntPtr plugin = IntPtr.Zero;
            IntPtr userData = IntPtr.Zero;

            using (var context = Context.Create(plugin, userData))
            {
                double expected = 0.53;

                // Act
                context.AdaptationState = expected;

                // Assert
                double actual = context.AdaptationState;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
