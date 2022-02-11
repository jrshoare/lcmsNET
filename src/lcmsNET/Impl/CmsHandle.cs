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

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace lcmsNET.Impl
{
    /// <summary>
    /// Provides a base class for objects that wrapper handles created and managed by Little CMS.
    /// </summary>
    public abstract class CmsHandle<T> : SafeHandle
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CmsHandle&lt;T&gt;"/> class with
        /// the supplied handle.
        /// </summary>
        /// <param name="handle">A handle obtained from Little CMS.</param>
        /// <param name="context">The context, or null for the global context.</param>
        /// <param name="isOwner">true if <paramref name="handle"/> is owned by this instance.</param>
        protected CmsHandle(IntPtr handle, Context context = null, bool isOwner = true)
            : base(IntPtr.Zero, isOwner)
        {
            Helper.CheckCreated<T>(handle);

            base.handle = handle;
            Context = context;
        }

        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        /// Releases the handle and sets the context to null.
        /// </summary>
        internal protected void Release()
        {
            base.SetHandleAsInvalid();
            Context = null;
        }

        #region IDisposable Support
        /// <summary>
        /// Gets a value indicating whether the instance has been disposed.
        /// </summary>
        public bool IsDisposed => isDisposed;
        private bool isDisposed = false;

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if the instance has been disposed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void EnsureNotDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(nameof(T));
            }
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        /// <param name="disposing">true if disposing, otherwise false.</param>
        protected override void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    base.Dispose(disposing);
                }

                Context = null;
                isDisposed = true;
            }
        }
        #endregion

        /// <summary>
        /// Gets the value of the handle.
        /// </summary>
        internal IntPtr Handle => DangerousGetHandle();

        #region SafeHandle Overrides
        /// <summary>
        /// Gets a value indicating whether the handle value is invalid.
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;
        #endregion
    }
}
