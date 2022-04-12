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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct _DictEntry
    {
        public IntPtr Next;         // struct cmsDICTentry struct *
        public IntPtr DisplayName;  // cmsMLU *
        public IntPtr DisplayValue; // cmsMLU *
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Name;         // wchar_t *
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Value;        // wchar_t *
    }

    /// <summary>
    /// Represents a dictionary of <see cref="DictEntry"/> items.
    /// </summary>
    public sealed class Dict : TagBase<Dict>, IEnumerable<DictEntry>
    {
        internal Dict(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        /// <summary>
        /// Creates a dictionary from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing dictionary.</param>
        /// <returns>
        /// A new <see cref="Dict"/> instance referencing an existing dictionary.
        /// </returns>
        /// <exception cref="LcmsNETException">
        /// The <paramref name="handle"/> is <see cref="IntPtr.Zero"/>.
        /// </exception>
        internal static Dict FromHandle(IntPtr handle)
        {
            return new Dict(handle, context: null, isOwner: false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Dict"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="Dict"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// <para>
        /// Requires Little CMS version 2.2 or later.
        /// </para>
        /// </remarks>
        public static Dict Create(Context context)
        {
            return new Dict(Interop.DictAlloc(Helper.GetHandle(context)), context);
        }

        /// <summary>
        /// Duplicates a dictionary.
        /// </summary>
        /// <returns>A new <see cref="Dict"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The Dict has already been disposed.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.2 or later.
        /// </remarks>
        public Dict Duplicate()
        {
            EnsureNotClosed();

            return new Dict(Interop.DictDup(handle), Context);
        }

        /// <summary>
        /// Adds an item to the dictionary.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="value">The value of the item.</param>
        /// <param name="displayName">The display name of the item. Can be null.</param>
        /// <param name="displayValue">The display value of the item. Can be null.</param>
        /// <returns>true if the item has been added, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Dict has already been disposed.
        /// </exception>
        /// <remarks>
        /// <para>
        /// No check is made for duplicate entries.
        /// </para>
        /// <para>
        /// Requires Little CMS version 2.2 or later.
        /// </para>
        /// </remarks>
        public bool Add(string name, string value, MultiLocalizedUnicode displayName, MultiLocalizedUnicode displayValue)
        {
            EnsureNotClosed();

            return Interop.DictAddEntry(handle, name, value, Helper.GetHandle(displayName), Helper.GetHandle(displayValue)) != 0;
        }

        #region IEnumerable<DictEntry> Support
        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        public IEnumerator<DictEntry> GetEnumerator()
        {
            EnsureNotClosed();

            return new DictEntryEnumerator(handle);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class DictEntryEnumerator : IEnumerator<DictEntry>
        {
            public DictEntryEnumerator(IntPtr handle)
            {
                Handle = handle;
                First = GetFirst(handle);
                Location = Position.Before;
            }
            private IntPtr Handle { get; set; }

            public DictEntry Current
            {
                get
                {
                    if (_current is null)
                    {
                        throw new InvalidOperationException();
                    }
                    return _current;
                }
            }
            private DictEntry _current = null;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (Location == Position.Before)
                {
                    if (First == IntPtr.Zero)
                    {
                        Location = Position.After;
                    }
                    else
                    {
                        _current = new DictEntry(First);
                        Location = Position.During;
                    }
                }
                else if (Location == Position.During)
                {
                    if (_current.Next == IntPtr.Zero)
                    {
                        _current = null;
                        Location = Position.After;
                    }
                    else
                    {
                        _current = new DictEntry(Interop.DictNextEntry(_current.Handle));
                    }
                }
                return Location == Position.During;
            }

            public void Reset()
            {
                _current = null;
                Location = Position.Before;
            }

            private IntPtr First { get; set; }
            private IntPtr GetFirst(IntPtr handle)
            {
                return Interop.DictGetEntryList(handle);
            }

            private enum Position { Before, During, After };
            private Position Location { get; set; }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                // Do nothing
            }

            ~DictEntryEnumerator()
            {
                Dispose(false);
            }
        }
        #endregion

        /// <summary>
        /// Frees the dictionary handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.DictFree(handle);
            return true;
        }
    }
}
