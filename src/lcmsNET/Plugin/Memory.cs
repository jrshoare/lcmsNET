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

using lcmsNET.Impl;
using System;

namespace lcmsNET.Plugin
{
    /// <summary>
    /// Provides access to the memory management primitives used by the core
    /// Liitle CMS engine. These use the memory management plug-in if defined.
    /// </summary>
    public static class Memory
    {
        /// <summary>
        /// Allocates <paramref name="size"/> bytes of uninitialized memory.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="size">The number of bytes of memory to be allocated.</param>
        /// <returns>A pointer to the unmanaged memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static IntPtr Malloc(Context context, uint size)
        {
            return Interop.Malloc(Helper.GetHandle(context), size);
        }

        /// <summary>
        /// Frees the unmanaged memory allocated to <paramref name="ptr"/>.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="ptr">The pointer to unmanaged memory.</param>
        /// <remarks>
        /// No action occurs if <paramref name="ptr"/> is <see cref="IntPtr.Zero"/>.
        /// </remarks>
        public static void Free(Context context, IntPtr ptr)
        {
            Interop.Free(Helper.GetHandle(context), ptr);
        }

        /// <summary>
        /// Allocates <paramref name="size"/> bytes of zeroed memory.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="size">The number of bytes of memory to be allocated.</param>
        /// <returns>A pointer to the unmanaged memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static IntPtr MallocZero(Context context, uint size)
        {
            return Interop.MallocZero(Helper.GetHandle(context), size);
        }

        /// <summary>
        /// Allocates space for an array of <paramref name="count"/> elements each of <paramref name="size"/> bytes.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="count">The number of array elements.</param>
        /// <param name="size">The size of each element in bytes.</param>
        /// <returns>A pointer to the unmanaged memory allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// <para>
        /// The memory will be initialized to zero.
        /// </para>
        /// </remarks>
        public static IntPtr Calloc(Context context, uint count, uint size)
        {
            return Interop.Calloc(Helper.GetHandle(context), count, size);
        }

        /// <summary>
        /// Reallocates the memory at <paramref name="ptr"/> to <paramref name="newSize"/> bytes.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="ptr">The pointer to existing unmanaged memory to be reallocated.</param>
        /// <param name="newSize">The new size for the memory in bytes.</param>
        /// <returns>A pointer to the unmanaged memory newly allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
        public static IntPtr Realloc(Context context, IntPtr ptr, uint newSize)
        {
            return Interop.Realloc(Helper.GetHandle(context), ptr, newSize);
        }

        /// <summary>
        /// Duplicates the memory at <paramref name="ptr"/> into a new block.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="ptr">The pointer to existing unmanaged memory to be duplicated.</param>
        /// <param name="size">The size of the memory in bytes.</param>
        /// <returns>A pointer to the unmanaged memory newly allocated, or <see cref="IntPtr.Zero"/> on error.</returns>
        public static IntPtr Duplicate(Context context, IntPtr ptr, uint size)
        {
            return Interop.DupMem(Helper.GetHandle(context), ptr, size);
        }
    }
}
