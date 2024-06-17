// Copyright(c) 2019-2024 John Stevenson-Hoare
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

using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.TestUtils
{
    public static class MemoryUtils
    {
        public static void UsingMemory(int memorySize, Action<IntPtr> action)
        {
            IntPtr memory = Marshal.AllocHGlobal(memorySize);
            try
            {
                action(memory);
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        public static void UsingMemoryFor<T>(T t, Action<IntPtr> action)
            where T : struct
        {
            int rawsize = Marshal.SizeOf(t);
            IntPtr memory = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(t, memory, false);
            try
            {
                action(memory);
            }
            finally
            {
                Marshal.DestroyStructure(memory, typeof(T));
                Marshal.FreeHGlobal(memory);
            }
        }

        public static void UsingPinnedMemory(object value, Action<IntPtr> action)
        {
            GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            try
            {
                action(ptr);
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
