using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class GamutBoundaryDescriptor : IDisposable
    {
        private IntPtr _handle;

        internal GamutBoundaryDescriptor(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<GamutBoundaryDescriptor>(handle);

            _handle = handle;
            Context = context;
        }

        public static GamutBoundaryDescriptor Create(Context context)
        {
            return new GamutBoundaryDescriptor(Interop.GBDAlloc(context.Handle), context);
        }

        public bool AddPoint(CIELab lab)
        {
            EnsureNotDisposed();

            return Interop.GBDAddPoint(_handle, lab) != 0;
        }

        public bool Compute(uint flags = 0)
        {
            EnsureNotDisposed();

            return Interop.GBDCompute(_handle, flags) != 0;
        }

        public bool CheckPoint(CIELab lab)
        {
            EnsureNotDisposed();

            return Interop.GBDCheckPoint(_handle, lab) != 0;
        }

        public Context Context { get; private set; }

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(GamutBoundaryDescriptor));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.GBDFree(handle);
                Context = null;
            }
        }

        ~GamutBoundaryDescriptor()
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
