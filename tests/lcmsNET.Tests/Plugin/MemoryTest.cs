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
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class MemoryTest
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
        public void MallocTest()
        {
            // Arrange
            const uint size = 0x100;

            IntPtr ptr = IntPtr.Zero;
            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            {
                try
                {
                    // Act
                    ptr = Memory.Malloc(context, size);

                    // Assert
                    Assert.AreNotEqual(IntPtr.Zero, ptr);
                }
                finally
                {
                    Memory.Free(context, ptr);
                }
            }
        }

        [TestMethod]
        public void FreeTest()
        {
            // Arrange
            IntPtr ptr = IntPtr.Zero;

            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            {
                // Act
                Memory.Free(context, ptr);
            }

            // Assert
        }

        [TestMethod]
        public void MallocZeroTest()
        {
            // Arrange
            const uint size = 0x100;
            byte[] zeroes = new byte[size];

            IntPtr ptr = IntPtr.Zero;
            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            {
                try
                {
                    // Act
                    ptr = Memory.MallocZero(context, size);
                    Marshal.Copy(ptr, zeroes, 0, (int)size);

                    // Assert
                    Assert.AreNotEqual(IntPtr.Zero, ptr);
                    Assert.IsTrue(zeroes.All(_ => _ == 0));
                }
                finally
                {
                    Memory.Free(context, ptr);
                }
            }
        }

        [TestMethod]
        public void CallocTest()
        {
            // Arrange
            const uint count = 5;
            const uint size = 0x100;

            IntPtr ptr = IntPtr.Zero;
            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            {
                try
                {
                    // Act
                    ptr = Memory.Calloc(context, count, size);

                    // Assert
                    Assert.AreNotEqual(IntPtr.Zero, ptr);
                }
                finally
                {
                    Memory.Free(context, ptr);
                }
            }
        }

        [TestMethod]
        public void ReallocTest()
        {
            // Arrange
            const uint size = 0x100;
            const uint newSize = 0x180;

            IntPtr origPtr = IntPtr.Zero, reallocPtr = IntPtr.Zero;
            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            {
                try
                {
                    origPtr = Memory.Malloc(context, size);

                    // Act
                    reallocPtr = Memory.Realloc(context, origPtr, newSize);
                    origPtr = IntPtr.Zero;

                    // Assert
                    Assert.AreNotEqual(IntPtr.Zero, reallocPtr);
                }
                finally
                {
                    Memory.Free(context, reallocPtr);
                    Memory.Free(context, origPtr);
                }
            }
        }

        [TestMethod]
        public void Duplicate()
        {
            // Arrange
            const uint size = 0x100;
            byte[] ones = new byte[size];

            IntPtr origPtr = IntPtr.Zero, duplicatePtr = IntPtr.Zero;
            using (var context = Context.Create(IntPtr.Zero, IntPtr.Zero))
            {
                try
                {
                    origPtr = Memory.Malloc(context, size);
                    byte[] tmp = Enumerable.Repeat<byte>(1, (int)size).ToArray();
                    Marshal.Copy(tmp, 0, origPtr, (int)size);

                    // Act
                    duplicatePtr = Memory.Duplicate(context, origPtr, size);
                    Marshal.Copy(duplicatePtr, ones, 0, (int)size);

                    // Assert
                    Assert.AreNotEqual(IntPtr.Zero, duplicatePtr);
                    Assert.IsTrue(ones.All(_ => _ == 1));
                }
                finally
                {
                    Memory.Free(context, duplicatePtr);
                    Memory.Free(context, origPtr);
                }
            }
        }
    }
}
