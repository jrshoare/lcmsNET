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
using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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

        [TestMethod()]
        public void Create_WhenInstantiated_ShouldHaveValidHandle()
        {
            // Act
            using var sut = ContextUtils.CreateContext();

            // Assert
            Assert.IsFalse(sut.IsInvalid);
        }

        [TestMethod()]
        public void Duplicate_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            IntPtr userData = Constants.Context.UserData;
            using var sut = ContextUtils.CreateContext();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.Duplicate(userData));
        }

        [TestMethod()]
        public void Duplicate_WhenInvoked_ShouldReturnDuplicate()
        {
            // Arrange
            IntPtr userData = Constants.Context.UserData;
            using var sut = ContextUtils.CreateContext();

            // Act
            using var duplicate = sut.Duplicate(userData);

            // Assert
            Assert.AreNotSame(duplicate, sut);
        }

        [TestMethod()]
        public void UserData_WhenGetting_ShouldReturnDataUsedToInstantiate()
        {
            // Arrange
            IntPtr plugin = Constants.Context.Plugin;
            byte[] bytes = [0xff, 0xaa, 0xdd, 0xee];
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr expected = handle.AddrOfPinnedObject();
            try
            {
                using var sut = Context.Create(plugin, expected);

                // Act
                IntPtr actual = sut.UserData;

                // Assert
                Assert.AreEqual(expected, actual);
            }
            finally
            {
                handle.Free();
            }
        }

        [TestMethod()]
        public void UserData_WhenDisposed_ShouldReturnZeroIntPtr()
        {
            // Arrange
            IntPtr plugin = Constants.Context.Plugin;
            byte[] bytes = [0xff, 0xaa, 0xdd, 0xee];
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            IntPtr userData = handle.AddrOfPinnedObject();

            // when disposed the context ID is zero which also identifies the global context so
            // the user data will be null as it is not possible to set global context user data
            IntPtr expected = IntPtr.Zero;
            try
            {
                using var sut = Context.Create(plugin, userData);
                sut.Dispose();

                // Act
                IntPtr actual = sut.UserData;

                // Assert
                Assert.AreEqual(expected, actual);
            }
            finally
            {
                handle.Free();
            }
        }

        [TestMethod()]
        public void ID_WhenGetting_ShouldReturnNonZeroIntPtr()
        {
            // Arrange
            IntPtr notExpected = IntPtr.Zero;
            using var sut = ContextUtils.CreateContext();

            // Act
            IntPtr actual = sut.ID;

            // Assert
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod()]
        public void RegisterPlugins_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            try
            {
                // Arrange
                using var sut = ContextUtils.CreateContext();
                sut.Dispose();

                var tag = PluginTagUtils.CreatePluginTag();
                tag.Descriptor.SupportedTypes[0] = TagTypeSignature.Lut16;

                MemoryUtils.UsingMemoryFor(tag, (plugin) =>
                {
                    // Act & Assert
                    Assert.ThrowsException<ObjectDisposedException>(() => sut.RegisterPlugins(plugin));
                });
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
        }

        [TestMethod()]
        public void RegisterPlugins_WhenInvoked_ShouldSucceed()
        {
            try
            {
                // Arrange
                using var sut = ContextUtils.CreateContext();

                var tag = PluginTagUtils.CreatePluginTag();
                tag.Descriptor.SupportedTypes[0] = TagTypeSignature.Lut16;

                MemoryUtils.UsingMemoryFor(tag, (plugin) =>
                {
                    // Act
                    var registered = sut.RegisterPlugins(plugin);

                    // tidy up
                    sut.UnregisterPlugins();

                    // Assert
                    Assert.IsTrue(registered);
                });
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.8 or later.");
            }
        }

        [TestMethod()]
        public void UnregisterPlugins_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.UnregisterPlugins());
        }

        [TestMethod()]
        public void UnregisterPlugins_WhenInvoked_ShouldSucceed()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();

            // Act
            sut.UnregisterPlugins();
        }

        [TestMethod()]
        public void SetErrorHandler_WhenDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.SetErrorHandler(HandleError));

            void HandleError(IntPtr contextID, int errorCode, string errorText)
            {
                TestContext.WriteLine($"contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
            }
        }

        [TestMethod()]
        public void SetErrorHandler_WhenError_ShouldInvokeHandler()
        {
            // Arrange
            bool handlerInvoked = false;
            using var sut = ContextUtils.CreateContext();

            // Act
            sut.SetErrorHandler(HandleError);

            // force error to observe output in Test Explorer results window for this test
            try { Profile.Open(sut, @"???", "r"); } catch { }

            // restore default error handler
            sut.SetErrorHandler(null);

            // Assert
            Assert.IsTrue(handlerInvoked);

            void HandleError(IntPtr contextID, int errorCode, string errorText)
            {
                handlerInvoked = true;
                TestContext.WriteLine($"contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
            }
        }

        [TestMethod()]
        public void AlarmCodes_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            ushort[] expected = Constants.Context.AlarmCodes;

            // Act
            sut.AlarmCodes = expected;
            var actual = sut.AlarmCodes;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AlarmCodes_WhenGettingDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            sut.AlarmCodes = Constants.Context.AlarmCodes;
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => { var alarmCode = sut.AlarmCodes; } );
        }

        [TestMethod()]
        public void AlarmCodes_WhenSettingDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.AlarmCodes = Constants.Context.AlarmCodes);
        }

        [TestMethod()]
        public void AlarmCodes_WhenSettingNullArray_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            ushort[] alarmCodes = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.AlarmCodes = alarmCodes);
        }

        [TestMethod()]
        public void AlarmCodes_WhenSettingIncorrectArrayLength_ShouldThrowArgumentException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            ushort[] alarmCodes = [0, 1, 2, 3, 4];

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.AlarmCodes = alarmCodes);
        }

        [TestMethod()]
        public void AdaptationState_WhenRoundTripped_ShouldHaveValueSet()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            double expected = 0.53;

            // Act
            sut.AdaptationState = expected;
            var actual = sut.AdaptationState;

            // Assert
            Assert.AreEqual(expected, actual, double.Epsilon);
        }

        [TestMethod()]
        public void AdaptationState_WhenGettingDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            sut.AdaptationState = 0.47;
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => { var adaptationState = sut.AdaptationState; });
        }

        [TestMethod()]
        public void AdaptationState_WhenSettingDisposed_ShouldThrowObjectDisposedException()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            sut.Dispose();

            // Act & Assert
            Assert.ThrowsException<ObjectDisposedException>(() => sut.AdaptationState = 0.49);
        }

        [TestMethod()]
        public void SupportedIntents_WhenInvoked_ShouldReturnNonEmptyCollection()
        {
            // Arrange
            using var sut = ContextUtils.CreateContext();
            try
            {
                // Act
                var supportedIntents = sut.SupportedIntents;

                // Assert
                Assert.IsNotNull(supportedIntents);
                Assert.AreNotEqual(0, supportedIntents.Count());

                foreach (var (code, description) in supportedIntents)
                {
                    TestContext.WriteLine($"code: {code}, description: {description}");
                }
            }
            catch (EntryPointNotFoundException)
            {
                Assert.Inconclusive("Requires Little CMS 2.6 or later.");
            }
        }
    }
}
