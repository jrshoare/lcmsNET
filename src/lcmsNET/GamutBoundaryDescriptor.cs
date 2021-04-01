using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    /// <summary>
    /// Represents a gamut boundary descriptor.
    /// </summary>
    public sealed class GamutBoundaryDescriptor : IDisposable
    {
        private IntPtr _handle;

        internal GamutBoundaryDescriptor(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<GamutBoundaryDescriptor>(handle);

            _handle = handle;
            Context = context;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GamutBoundaryDescriptor"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="CAM02"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static GamutBoundaryDescriptor Create(Context context)
        {
            return new GamutBoundaryDescriptor(Interop.GBDAlloc(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Adds a new point for computing the gamut boundary descriptor.
        /// </summary>
        /// <param name="lab">A <see cref="CIELab"/> value defining the point.</param>
        /// <returns>true if the point was added successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The descriptor has already been disposed.
        /// </exception>
        /// <remarks>
        /// This method can be invoked as many times as known points. The gamut boundary
        /// descriptor cannot be checked until <see cref="Compute(uint)"/> is invoked.
        /// </remarks>
        public bool AddPoint(in CIELab lab)
        {
            EnsureNotDisposed();

            return Interop.GBDAddPoint(_handle, lab) != 0;
        }

        /// <summary>
        /// Computes the gamut boundary descriptor using all known points and interpolating
        /// any missing sector(s).
        /// </summary>
        /// <param name="flags">Reserved. Set to 0.</param>
        /// <returns>true if computed successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The descriptor has already been disposed.
        /// </exception>
        /// <remarks>
        /// Call this method after adding all known points with <see cref="AddPoint(in CIELab)"/>
        /// and before invoking <see cref="CheckPoint(in CIELab)"/>.
        /// </remarks>
        public bool Compute(uint flags = 0)
        {
            EnsureNotDisposed();

            return Interop.GBDCompute(_handle, flags) != 0;
        }

        /// <summary>
        /// Checks whether the specified Lab value is inside the gamut boundary descriptor.
        /// </summary>
        /// <param name="lab">A <see cref="CIELab"/> point.</param>
        /// <returns>true if the point is inside the gamut, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The descriptor has already been disposed.
        /// </exception>
        /// <remarks>
        /// Call this method after adding all known points with <see cref="AddPoint(in CIELab)"/>
        /// and computing the gamut boundary descriptor with <see cref="Compute(uint)"/>.
        /// </remarks>
        public bool CheckPoint(in CIELab lab)
        {
            EnsureNotDisposed();

            return Interop.GBDCheckPoint(_handle, lab) != 0;
        }

        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }

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

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~GamutBoundaryDescriptor()
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

        internal IntPtr Handle => _handle;
    }
}
