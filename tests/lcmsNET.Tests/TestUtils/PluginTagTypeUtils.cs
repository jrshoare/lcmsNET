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
    public static class PluginTagTypeUtils
    {
        public static PluginTagType CreatePluginTagType(TagTypeHandler handler) =>
            new()
            {
                Base = Constants.PluginTagType.Base,
                Handler = handler
            };

        public static TagTypeHandler CreateTagTypeHandler() =>
            new()
            {
                Signature = 0x00000000,
                Read = Marshal.GetFunctionPointerForDelegate(_read),
                Write = Marshal.GetFunctionPointerForDelegate(_write),
                Duplicate = Marshal.GetFunctionPointerForDelegate(_duplicate),
                Free = Marshal.GetFunctionPointerForDelegate(_free)
            };


        // allocate unmanaged memory for a single 'uint' and reads from i/o handler into it
        public static IntPtr Read(in TagTypeHandler self, IntPtr io, out uint nItems, uint tagSize)
        {
            using var context = Context.FromHandle(self.ContextID);
            using var iohandler = IOHandler.FromHandle(io);
            nItems = 1;
            IntPtr ptr = Memory.Malloc(context, sizeof(uint));
            if (ptr == IntPtr.Zero) return IntPtr.Zero;

            // unsafe, but faster...
            unsafe
            {
                if (!iohandler.Read(ref *(uint*)ptr)) return IntPtr.Zero;
            }

            // - or -
            // verifiable, but slower...

            //uint[] arr = new uint[1];
            //if (!iohandler.Read(ref arr[0])) return IntPtr.Zero;
            //Marshal.Copy((int[])(object)arr, 0, ptr, 1);

            return ptr;
        }

        // use the i/o handler to write a single 'uint' read from unmanaged memory 'ptr'
        public static int Write(in TagTypeHandler self, IntPtr io, IntPtr ptr, uint nItems)
        {
            using var iohandler = IOHandler.FromHandle(io);
            // unsafe, but faster...
            unsafe
            {
                return iohandler.Write(*(uint*)ptr) ? 1 : 0;
            }

            // - or -
            // verifiable, but slower...

            //uint[] arr = new uint[1];
            //Marshal.Copy(ptr, (int[])(object)arr, 0, 1);
            //return iohandler.Write(arr[0]) ? 1 : 0;
        }

        // duplicate the unmanaged memory 'ptr' into a new block of size 'n x sizeof(uint)'
        public static IntPtr Duplicate(in TagTypeHandler self, IntPtr ptr, uint n)
        {
            using var context = Context.FromHandle(self.ContextID);
            return Memory.Duplicate(context, ptr, n * sizeof(uint));
        }

        // free the unmanaged memory 'ptr'
        public static void Free(in TagTypeHandler self, IntPtr ptr)
        {
            using var context = Context.FromHandle(self.ContextID);
            Memory.Free(context, ptr);
        }

        public static void HandleError(IntPtr contextID, int errorCode, string errorText)
        {
            // Console.WriteLine($"Error!!! contextID: {contextID}, errorCode: {errorCode}, errorText: '{errorText}'");
        }

        public static readonly TagTypeRead _read = new(Read);
        public static readonly TagTypeWrite _write = new(Write);
        public static readonly TagTypeDuplicate _duplicate = new(Duplicate);
        public static readonly TagTypeFree _free = new(Free);
    }
}
