using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace lcmsNET
{
    /// <summary>
    /// Defines the surround(ing)s for a <see cref="ViewingConditions"/>.
    /// </summary>
    public enum Surround : int
    {
        /// <summary>
        /// Average for viewing surface colors.
        /// </summary>
        Average = 1,
        /// <summary>
        /// Dim for viewing televison.
        /// </summary>
        Dim = 2,
        /// <summary>
        /// Dark for when using a projector in a dark room.
        /// </summary>
        Dark = 3,
        /// <summary>
        /// Cut sheet.
        /// </summary>
        CutSheet = 4
    }

    /// <summary>
    /// Defines the viewing conditions for a <see cref="CAM02"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ViewingConditions
    {
        /// <summary>
        /// The white point.
        /// </summary>
        public CIEXYZ whitePoint;
        /// <summary>
        /// Yb.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double Yb;
        /// <summary>
        /// La.
        /// </summary>
        [MarshalAs(UnmanagedType.R8)]
        public double La;
        /// <summary>
        /// Surround.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public Surround surround;
        /// <summary>
        /// D value.
        /// </summary>
        /// <remarks>
        /// A value of -1 causes D to be calculated.
        /// </remarks>
        [MarshalAs(UnmanagedType.R8)]
        public double D_value;
    }

    /// <summary>
    /// Represents a CIE CAM02 color appearance model.
    /// </summary>
    public sealed class CAM02 : IDisposable
    {
        private IntPtr _handle;

        internal CAM02(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<CAM02>(handle);

            _handle = handle;
            Context = context;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CAM02"/> class.
        /// </summary>
        /// <param name="context">The context, can be null.</param>
        /// <param name="conditions">The viewing conditions.</param>
        /// <returns>A new <see cref="CAM02"/> instance.</returns>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static CAM02 Create(Context context, in ViewingConditions conditions)
        {
            return new CAM02(Interop.CIECAM02Init(context?.Handle ?? IntPtr.Zero, conditions), context);
        }

        /// <summary>
        /// Evaluates the CAM02 model in the forward direction XYZ → JCh.
        /// </summary>
        /// <param name="xyz">The input XYZ value.</param>
        /// <param name="jch">Returns the JCh value.</param>
        public void Forward(in CIEXYZ xyz, out JCh jch)
        {
            Interop.CIECAM02Forward(_handle, xyz, out jch);
        }

        /// <summary>
        /// Evaluates the CAM02 model in the reverse direction JCh → XYZ.
        /// </summary>
        /// <param name="jch">The input JCh value.</param>
        /// <param name="xyz">Returns the XYZ value.</param>
        public void Reverse(in JCh jch, out CIEXYZ xyz)
        {
            Interop.CIECAM02Reverse(_handle, jch, out xyz);
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

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~CAM02()
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
