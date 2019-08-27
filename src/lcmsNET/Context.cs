using System;
using System.Diagnostics;
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
            Debug.Assert(handle != IntPtr.Zero);
            _handle = handle;
        }

        public static Context Create(IntPtr plugin, IntPtr userData)
        {
            IntPtr handle = Interop.CreateContext(plugin, userData);
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Context(handle);
        }

        public Context Duplicate(IntPtr userData)
        {
            EnsureNotDisposed();

            IntPtr handle = Interop.DuplicateContext(_handle, userData);
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Context(handle);
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
