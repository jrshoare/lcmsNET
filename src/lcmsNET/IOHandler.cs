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

namespace lcmsNET
{
    /// <summary>
    /// Represents an I/O handler.
    /// </summary>
    public sealed class IOHandler : CmsHandle<IOHandler>
    {
        internal IOHandler(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        #region Access functions
        /// <summary>
        /// Creates a void <see cref="IOHandler"/>. All read operations return 0 bytes and
        /// set the EOF flag. All write operations discard the given data.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Creates a <see cref="IOHandler"/> from a file.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="filepath">Full path to the file.</param>
        /// <param name="access">"r" for read access, or "w" for write access.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context, string filepath, string access)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero, filepath, access), context);
        }
        #endregion

        /// <summary>
        /// Frees the i/o handler handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.CloseIOHandler(handle);
            return true;
        }
    }
}
