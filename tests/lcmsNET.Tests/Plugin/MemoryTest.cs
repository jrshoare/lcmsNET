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
using lcmsNET.Tests.TestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.Plugin
{
    [TestClass()]
    public class MemoryTest
    {
        [TestMethod]
        public void Malloc_WhenInvoked_ShouldAllocateMemory()
        {
            // Arrange
            const uint size = 0x100;

            IntPtr sut = IntPtr.Zero;
            using var context = ContextUtils.CreateContext();

            try
            {
                // Act
                sut = Memory.Malloc(context, size);

                // Assert
                Assert.AreNotEqual(IntPtr.Zero, sut);
            }
            finally
            {
                Memory.Free(context, sut);
            }
        }

        [TestMethod]
        public void Free_WhenPointerIsZero_ShouldSucceed()
        {
            // Arrange
            IntPtr ptr = IntPtr.Zero;
            using var context = ContextUtils.CreateContext();

            // Act
            Memory.Free(context, ptr);
        }

        [TestMethod]
        public void MallocZero_WhenInvoked_ShouldAllocateZeroedMemory()
        {
            // Arrange
            const uint size = 0x100;
            byte[] zeroes = new byte[size];

            IntPtr sut = IntPtr.Zero;
            using var context = ContextUtils.CreateContext();

            try
            {
                // Act
                sut = Memory.MallocZero(context, size);
                Marshal.Copy(sut, zeroes, 0, (int)size);

                // Assert
                Assert.AreNotEqual(IntPtr.Zero, sut);
                Assert.IsTrue(zeroes.All(_ => _ == 0));
            }
            finally
            {
                Memory.Free(context, sut);
            }
        }

        [TestMethod]
        public void Calloc_WhenInvoked_ShouldAllocateArraySpace()
        {
            // Arrange
            const uint count = 5;
            const uint size = 0x100;

            IntPtr sut = IntPtr.Zero;
            using var context = ContextUtils.CreateContext();

            try
            {
                // Act
                sut = Memory.Calloc(context, count, size);

                // Assert
                Assert.AreNotEqual(IntPtr.Zero, sut);
            }
            finally
            {
                Memory.Free(context, sut);
            }
        }

        [TestMethod]
        public void Realloc_WhenInvoked_ShouldReallocateMemoryWithNewSize()
        {
            // Arrange
            const uint size = 0x100;
            const uint newSize = 0x180;

            IntPtr origPtr = IntPtr.Zero, sut = IntPtr.Zero;
            using var context = ContextUtils.CreateContext();

            try
            {
                origPtr = Memory.Malloc(context, size);

                // Act
                sut = Memory.Realloc(context, origPtr, newSize);
                origPtr = IntPtr.Zero;

                // Assert
                Assert.AreNotEqual(IntPtr.Zero, sut);
            }
            finally
            {
                Memory.Free(context, sut);
                Memory.Free(context, origPtr);
            }
        }

        [TestMethod]
        public void Duplicate_WhenInvoked_ShouldDuplicateMemory()
        {
            // Arrange
            const uint size = 0x100;
            byte[] ones = new byte[size];

            IntPtr origPtr = IntPtr.Zero, sut = IntPtr.Zero;
            using var context = ContextUtils.CreateContext();

            try
            {
                origPtr = Memory.Malloc(context, size);
                byte[] tmp = Enumerable.Repeat<byte>(1, (int)size).ToArray();
                Marshal.Copy(tmp, 0, origPtr, (int)size);

                // Act
                sut = Memory.Duplicate(context, origPtr, size);
                Marshal.Copy(sut, ones, 0, (int)size);

                // Assert
                Assert.AreNotEqual(IntPtr.Zero, sut);
                Assert.IsTrue(ones.All(_ => _ == 1));
            }
            finally
            {
                Memory.Free(context, sut);
                Memory.Free(context, origPtr);
            }
        }
    }
}
