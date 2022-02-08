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

using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    /// <summary>
    /// Represents a named color list.
    /// </summary>
    public sealed class NamedColorList : IDisposable
    {
        private IntPtr _handle;

        internal NamedColorList(IntPtr handle, Context context = null, bool isOwner = true)
        {
            Helper.CheckCreated<NamedColorList>(handle);

            _handle = handle;
            Context = context;
            IsOwner = isOwner;
        }

        /// <summary>
        /// Creates a named color list from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing named color list.</param>
        /// <returns>A new <see cref="NamedColorList"/> instance referencing an existing named color list.</returns>
        internal static NamedColorList FromHandle(IntPtr handle)
        {
            return new NamedColorList(handle, context: null, isOwner: false);
        }

        internal static NamedColorList CopyRef(IntPtr handle, Context context = null)
        {
            return new NamedColorList(handle, context, isOwner: false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="NamedColorList"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="n">The initial number of spot colors in the list.</param>
        /// <param name="colorantCount">The number of channels in the device space.</param>
        /// <param name="prefix">Prefix for all spot color names.</param>
        /// <param name="suffix">Suffix for all spot color names.</param>
        /// <returns>A new <see cref="NamedColorList"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static NamedColorList Create(Context context, uint n, uint colorantCount, string prefix, string suffix)
        {
            return new NamedColorList(
                    Interop.AllocNamedColorList(context?.Handle ?? IntPtr.Zero,
                            n, colorantCount, prefix ?? string.Empty, suffix ?? string.Empty), context);
        }

        /// <summary>
        /// Duplicates a named color list.
        /// </summary>
        /// <returns>A new <see cref="NamedColorList"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The named color list has already been disposed.
        /// </exception>
        public NamedColorList Duplicate()
        {
            EnsureNotDisposed();

            return new NamedColorList(Interop.DupNamedColorList(_handle), Context);
        }

        /// <summary>
        /// Adds a new spot color to the list.
        /// </summary>
        /// <param name="name">The spot color name.</param>
        /// <param name="pcs">An array of 3 values encoding the PCS coordinates.</param>
        /// <param name="colorant">An array of 16 values encoding the device colorant.</param>
        /// <returns>true if the spot color was added to the list, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The named color list has already been disposed.
        /// </exception>
        public bool Add(string name, ushort[] pcs, ushort[] colorant)
        {
            if (pcs?.Length != 3) throw new ArgumentException($"'{nameof(pcs)}' array size must equal 3.");
            if (colorant?.Length != 16) throw new ArgumentException($"'{nameof(colorant)}' array size must equal 16.");

            EnsureNotDisposed();

            return Interop.AppendNamedColor(_handle, name, pcs, colorant) != 0;
        }

        /// <summary>
        /// Gets information for a spot color with the given index.
        /// </summary>
        /// <param name="nColor">The index of the spot color.</param>
        /// <param name="name">Returns the name of the spot color.</param>
        /// <param name="prefix">Returns the prefix for the spot color.</param>
        /// <param name="suffix">Returns the suffix for the spot color.</param>
        /// <param name="pcs">Returns an array of 3 values encoding the PCS coordinates for the spot color.</param>
        /// <param name="colorant">Returns an array of 16 values encoding the device colorant for the spot color.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool GetInfo(uint nColor, out string name, out string prefix, out string suffix,
                out ushort[] pcs, out ushort[] colorant)
        {
            pcs = new ushort[3];
            colorant = new ushort[16];

            return Interop.NamedColorInfo(_handle, nColor, out name, out prefix, out suffix, pcs, colorant) != 0;
        }

        /// <summary>
        /// Gets the index of the given spot color name.
        /// </summary>
        /// <param name="name">The name of the spot color.</param>
        /// <returns>The zero-based index of the spot color, or -1 if not found.</returns>
        public int this[string name] => Interop.NamedColorIndex(_handle, name);

        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        /// Gets the number of spot colors in the named color list.
        /// </summary>
        public uint Count => Interop.NamedColorCount(_handle);

        #region IDisposable Support
        /// <summary>
        /// Gets a value indicating whether the instance has been disposed.
        /// </summary>
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(NamedColorList));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (IsOwner && handle != IntPtr.Zero) // only dispose undisposed objects that we own
            {
                Interop.FreeNamedColorList(handle);
                Context = null;
            }
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~NamedColorList()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Gets the handle to the named color list.
        /// </summary>
        public IntPtr Handle => _handle;

        private bool IsOwner { get; set; }
    }
}
