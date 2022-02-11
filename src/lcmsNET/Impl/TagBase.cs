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

namespace lcmsNET.Impl
{
    /// <summary>
    /// Provides a base class for tags that can be read from or written to a profile.
    /// </summary>
    public abstract class TagBase<T> : CmsHandle<T>
        where T: class
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TagBase&lt;T&gt;"/> class with
        /// the supplied handle.
        /// </summary>
        /// <param name="handle">A handle obtained from Little CMS.</param>
        /// <param name="context">The context, or null for the global context.</param>
        /// <param name="isOwner">true if <paramref name="handle"/> is owned by this instance.</param>
        protected TagBase(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }
    }
}
