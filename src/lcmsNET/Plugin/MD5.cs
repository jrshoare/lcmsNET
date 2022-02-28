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
    /// Provides methods to expose the MD5 message digest algorithms internal
    /// to Little CMS.
    /// </summary>
    public sealed class MD5 : CmsHandle<MD5>
    {
        internal MD5(IntPtr handle, Context context)
            : base(handle, context, isOwner: true)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MD5"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="MD5"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// <para>
        /// Requires Little CMS version 2.10 or later.
        /// </para>
        /// </remarks>
        public static MD5 Create(Context context = null)
        {
            return new MD5(Interop.MD5Alloc(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Adds to the digest the contents of the supplied memory.
        /// </summary>
        /// <param name="memory">The memory holding the values to be used to compute the digest.</param>
        /// <exception cref="ObjectDisposedException">
        /// The message digest has already been disposed.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.10 or later.
        /// </remarks>
        public void Add(byte[] memory)
        {
            EnsureNotDisposed();

            Interop.MD5Add(handle, memory);
        }

        /// <summary>
        /// Computes the digest and freezes this instance.
        /// </summary>
        /// <remarks>
        /// Requires Little CMS version 2.10 or later.
        /// </remarks>
        public void Freeze()
        {
            if (!IsClosed)
            {
                Digest = ComputeAndClose();
                Release();
            }
        }

        /// <summary>
        /// Gets the value of the message digest for this instance.
        /// </summary>
        /// <remarks>
        /// The <see cref="Freeze()"/> method must be invoked before invoking this property.
        /// </remarks>
        /// <exception cref="LcmsNETException">
        /// The Freeze() method has not been invoked on this instance.
        /// </exception>
        public byte[] Digest
        {
            get
            {
                if (digest is null) throw new LcmsNETException("The Freeze() method has not been invoked on this instance.");
                return digest;
            }
            set { digest = value; }
        }
        private byte[] digest = null;

        private byte[] ComputeAndClose()
        {
            byte[] md5 = new byte[16];
            Interop.MD5Finish(handle, md5);
            return md5;
        }

        /// <summary>
        /// Frees the MD5 handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            ComputeAndClose();
            return true;
        }
    }
}
