﻿using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class Profile : IDisposable
    {
        private IntPtr _handle;

        internal Profile(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<Profile>(handle);

            _handle = handle;
            Context = context;
        }

        public static Profile Open(string filepath, string access)
        {
            return new Profile(Interop.OpenProfile(filepath, access));
        }

        public static Profile Open(Context context, string filepath, string access)
        {
            return new Profile(Interop.OpenProfile(context.Handle, filepath, access), context);
        }

        public bool Save(string filepath)
        {
            EnsureNotDisposed();

            return 0 != Interop.SaveProfile(_handle, filepath);
        }

        public Context Context { get; private set; }

        public int ColorSpaceSignature => Interop.GetColorSpaceSignature(_handle);

        public Cms.PixelType ColorSpace => (Cms.PixelType)Interop.GetColorSpace(_handle);

        public int PCSSignature => Interop.GetPCSSignature(_handle);

        public Cms.PixelType PCS => (Cms.PixelType)Interop.GetPCS(_handle);

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
            if (handle != IntPtr.Zero)
            {
                Interop.CloseProfile(handle);
                Context = null;
            }
        }

        ~Profile()
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
