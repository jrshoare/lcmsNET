using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class IOHandler : IDisposable
    {
        private IntPtr _handle;

        internal IOHandler(IntPtr handle, Context context = null, bool isOwner = true)
        {
            Helper.CheckCreated<IOHandler>(handle);

            _handle = handle;
            Context = context;
            IsOwner = isOwner;
        }

        #region Access functions
        public static IOHandler Open(Context context)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero), context);
        }

        public static IOHandler Open(Context context, string filepath, string access)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero, filepath, access), context);
        }
        #endregion

        #region Properties
        public Context Context { get; private set; }
        #endregion

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(Profile));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (IsOwner && handle != IntPtr.Zero) // only dispose undisposed objects that we own
            {
                Interop.CloseIOHandler(handle);
                Context = null;
            }
        }

        ~IOHandler()
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

        private bool IsOwner { get; set; }
    }
}
