using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ViewingConditions
    {
        public CIEXYZ whitePoint;
        [MarshalAs(UnmanagedType.R8)]
        public double Yb;
        [MarshalAs(UnmanagedType.R8)]
        public double La;
        [MarshalAs(UnmanagedType.I4)]
        public int surround;
        [MarshalAs(UnmanagedType.R8)]
        public double D_value;
    }

    public sealed class CAM02 : IDisposable
    {
        private IntPtr _handle;

        internal CAM02(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<CAM02>(handle);

            _handle = handle;
            Context = context;
        }

        public static CAM02 Create(Context context, in ViewingConditions conditions)
        {
            return new CAM02(Interop.CIECAM02Init(context.Handle, conditions), context);
        }

        public void Forward(in CIEXYZ xyz, out JCh jch)
        {
            Interop.CIECAM02Forward(_handle, xyz, out jch);
        }

        public void Reverse(in JCh jch, out CIEXYZ xyz)
        {
            Interop.CIECAM02Reverse(_handle, jch, out xyz);
        }

        public Context Context { get; private set; }

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(CAM02));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.CIECAM02Done(handle);
                Context = null;
            }
        }

        ~CAM02()
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
