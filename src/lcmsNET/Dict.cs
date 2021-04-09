using lcmsNET.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

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
    public sealed class Dict : IEnumerable<DictEntry>, IDisposable, IWrapper
    {
        private IntPtr _handle;

        internal Dict(IntPtr handle, Context context = null, bool isOwner = true)
        {
            Helper.CheckCreated<Dict>(handle);

            _handle = handle;
            Context = context;
            IsOwner = isOwner;
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
        public static Dict FromHandle(IntPtr handle)
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
            return new Dict(Interop.DictAlloc(context?.Handle ?? IntPtr.Zero), context);
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
            EnsureNotDisposed();

            return new Dict(Interop.DictDup(_handle), Context);
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
            EnsureNotDisposed();

            return Interop.DictAddEntry(_handle, name, value,
                    displayName?.Handle ?? IntPtr.Zero,
                    displayValue?.Handle ?? IntPtr.Zero) != 0;
        }

        #region IEnumerable<DictEntry> Support
        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        public IEnumerator<DictEntry> GetEnumerator()
        {
            EnsureNotDisposed();

            return new DictEntryEnumerator(_handle);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }

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
                throw new ObjectDisposedException(nameof(Dict));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (IsOwner && handle != IntPtr.Zero) // only dispose undisposed objects that we own
            {
                Interop.DictFree(handle);
                Context = null;
            }
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~Dict()
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
        /// Gets the handle to the dictionary.
        /// </summary>
        public IntPtr Handle => _handle;

        private bool IsOwner { get; set; }
    }
}
