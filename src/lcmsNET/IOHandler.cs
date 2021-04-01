using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    /// <summary>
    /// Represents an I/O handler.
    /// </summary>
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
        /// <summary>
        /// Creates a void <see cref="IOHandler"/>. All read operations return 0 bytes and
        /// set the EOF flag. All write operations discard the given data.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Creates a <see cref="IOHandler"/> from a file.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="filepath">Full path to the file.</param>
        /// <param name="access">"r" for read access, or "w" for write access.</param>
        /// <returns>A new <see cref="IOHandler"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IOHandler Open(Context context, string filepath, string access)
        {
            return new IOHandler(Interop.OpenIOHandler(context?.Handle ?? IntPtr.Zero, filepath, access), context);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }
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

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~IOHandler()
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
        // visibility must be 'internal' to allow ownership to be taken when used with Profile.Open(Context, IOHandler...)
        internal bool IsOwner { get; set; }
    }
}
