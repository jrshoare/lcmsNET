using lcmsNET.Impl;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class Context : IDisposable
    {
        private IntPtr _handle;

        internal Context(IntPtr handle)
        {
            Helper.CheckCreated<Context>(handle);

            _handle = handle;
        }

        public static Context Create(IntPtr plugin, IntPtr userData)
        {
            return new Context(Interop.CreateContext(plugin, userData));
        }

        public Context Duplicate(IntPtr userData)
        {
            EnsureNotDisposed();

            return new Context(Interop.DuplicateContext(_handle, userData));
        }

        public IntPtr UserData => Interop.GetContextUserData(_handle);

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(Context));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.DeleteContext(handle);
            }
        }

        ~Context()
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
