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
        /// <remarks>
        /// The instance created should be considered read-only for <paramref name="handle"/>
        /// values returned from <see cref="Profile.ReadTag(TagSignature)"/>.
        /// </remarks>
        public static Dict FromHandle(IntPtr handle)
        {
            return new Dict(handle, context: null, isOwner: false);
        }

        public static Dict Create(Context context)
        {
            return new Dict(Interop.DictAlloc(context?.Handle ?? IntPtr.Zero), context);
        }

        public Dict Duplicate()
        {
            EnsureNotDisposed();

            return new Dict(Interop.DictDup(_handle), Context);
        }

        public bool Add(string name, string value, MultiLocalizedUnicode displayName, MultiLocalizedUnicode displayValue)
        {
            EnsureNotDisposed();

            return Interop.DictAddEntry(_handle, name, value,
                    displayName?.Handle ?? IntPtr.Zero,
                    displayValue?.Handle ?? IntPtr.Zero) != 0;
        }

        #region IEnumerable<DictEntry> Support
        public IEnumerator<DictEntry> GetEnumerator()
        {
            EnsureNotDisposed();

            return new DictEntryEnumerator(_handle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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

        ~Dict()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public IntPtr Handle => _handle;

        private bool IsOwner { get; set; }
    }
}
