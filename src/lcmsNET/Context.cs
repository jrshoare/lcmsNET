using lcmsNET.Impl;
using System;
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

        public bool RegisterPlugins(IntPtr plugin)
        {
            EnsureNotDisposed();

            return Interop.RegisterContextPlugins(_handle, plugin) == 1;
        }

        public void UnregisterPlugins()
        {
            EnsureNotDisposed();

            Interop.UnregisterContextPlugins(_handle);
        }

        public void SetErrorHandler(ErrorHandler handler)
        {
            EnsureNotDisposed();

            Interop.SetContextErrorHandler(_handle, handler);
        }

        public ushort[] AlarmCodes
        {
            get
            {
                EnsureNotDisposed();

                ushort[] alarmCodes = new ushort[16];
                Interop.GetAlarmCodesTHR(_handle, alarmCodes);
                return alarmCodes;
            }
            set
            {
                if (value?.Length != 16) throw new ArgumentException($"'{nameof(value)}' array size must equal 16.");

                EnsureNotDisposed();

                Interop.SetAlarmCodesTHR(_handle, value);
            }
        }

        public double AdaptationState
        {
            get
            {
                EnsureNotDisposed();

                return Interop.SetAdaptationStateTHR(_handle, -1.0);
            }
            set
            {
                EnsureNotDisposed();

                Interop.SetAdaptationStateTHR(_handle, value);
            }
        }

        public IntPtr UserData => Interop.GetContextUserData(_handle);

        public IntPtr ID => _handle;

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
