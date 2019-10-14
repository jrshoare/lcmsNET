using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class IT8 : IDisposable
    {
        private IntPtr _handle;

        internal IT8(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<IT8>(handle);

            _handle = handle;
            Context = context;
        }

        public static IT8 Create(Context context)
        {
            return new IT8(Interop.IT8Alloc(context?.Handle ?? IntPtr.Zero), context);
        }

        #region Properties
        public Context Context { get; private set; }
        #endregion

        #region Tables

        public uint TableCount => Interop.IT8TableCount(_handle);

        public int SetTable(uint nTable)
        {
            EnsureNotDisposed();

            return Interop.IT8SetTable(_handle, nTable);
        }
        #endregion

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(IT8));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.IT8Free(handle);
                Context = null;
            }
        }

        ~IT8()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        internal IntPtr Handle => _handle;
    }
}
