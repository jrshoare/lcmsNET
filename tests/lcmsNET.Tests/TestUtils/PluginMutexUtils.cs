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

using lcmsNET.Plugin;
using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Tests.TestUtils
{
    public static class PluginMutexUtils
    {
        public static PluginMutex CreatePluginMutex() =>
            new()
            {
                Base = Constants.PluginMutex.Base,
                Create = Marshal.GetFunctionPointerForDelegate(_create),
                Destroy = Marshal.GetFunctionPointerForDelegate(_destroy),
                Lock = Marshal.GetFunctionPointerForDelegate(_locker),
                Unlock = Marshal.GetFunctionPointerForDelegate(_unlocker)
            };

        private static IntPtr MyCreateMutex(IntPtr ContextID)
        {
            var myMutex = new MyMutex();
            int size = Marshal.SizeOf(myMutex);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(myMutex, ptr, false);

            return ptr;
        }

        private struct MyMutex
        {
            public int nLocks;
        }

        private static void MyDestroyMutex(IntPtr ContextID, IntPtr mutex)
        {
            //MyMutex myMutex = Marshal.PtrToStructure<MyMutex>(mutex);
            //if (myMutex.nLocks != 0) Console.WriteLine("Destroying mutex when number of locks is non-zero.");
            Marshal.FreeHGlobal(mutex);
        }

        private static int MyLockMutex(IntPtr ContextID, IntPtr mutex)
        {
            MyMutex myMutex = Marshal.PtrToStructure<MyMutex>(mutex);
            myMutex.nLocks++;

            return 1;
        }

        private static void MyUnlockMutex(IntPtr ContextID, IntPtr mutex)
        {
            MyMutex myMutex = Marshal.PtrToStructure<MyMutex>(mutex);
            myMutex.nLocks--;
        }

        private static readonly CreateMutexFn _create = new(MyCreateMutex);
        private static readonly DestroyMutexFn _destroy = new(MyDestroyMutex);
        private static readonly LockMutexFn _locker = new(MyLockMutex);
        private static readonly UnlockMutexFn _unlocker = new(MyUnlockMutex);
    }
}
