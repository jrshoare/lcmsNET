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
    public static class PluginMemoryHandlerUtils
    {
        public static PluginMemoryHandler CreatePluginMemoryHandler() =>
            new()
            {
                Base = Constants.PluginMemoryHandler.Base,
                Malloc = Marshal.GetFunctionPointerForDelegate(_malloc),
                Free = Marshal.GetFunctionPointerForDelegate(_free),
                Realloc = Marshal.GetFunctionPointerForDelegate(_realloc),
                MallocZero = IntPtr.Zero,   // optional
                Calloc = IntPtr.Zero,       // optional
                Duplicate = IntPtr.Zero,    // optional
                NonContextualMalloc = Marshal.GetFunctionPointerForDelegate(_nonContextualMalloc),
                NonContextualFree = Marshal.GetFunctionPointerForDelegate(_nonContextualFree)
            };

        // allocate memory
        public static IntPtr Malloc(IntPtr contextID, uint size)
        {
            try
            {
                return Marshal.AllocHGlobal((int)size);
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }

        // free memory
        public static void Free(IntPtr contextID, IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        // reallocate memory
        public static IntPtr Realloc(IntPtr contextID, IntPtr ptr, uint newSize)
        {
            try
            {
                return Marshal.ReAllocHGlobal(ptr, (IntPtr)newSize);
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }

        // allocate non-contextual memory
        public static IntPtr NonContextualMalloc(IntPtr userData, uint size)
        {
            try
            {
                return Marshal.AllocHGlobal((int)size);
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }

        // free non-contextual memory
        public static void NonContextualFree(IntPtr userData, IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        public static readonly MemoryMalloc _malloc = new(Malloc);
        public static readonly MemoryFree _free = new(Free);
        public static readonly MemoryRealloc _realloc = new(Realloc);
        public static readonly MemoryNonContextualMalloc _nonContextualMalloc = new(NonContextualMalloc);
        public static readonly MemoryNonContextualFree _nonContextualFree = new(NonContextualFree);
    }
}
