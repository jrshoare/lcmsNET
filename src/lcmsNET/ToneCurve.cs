using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace lcmsNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CurveSegment
    {
        [MarshalAs(UnmanagedType.R4)]
        public float x0;
        [MarshalAs(UnmanagedType.R4)]
        public float x1;
        [MarshalAs(UnmanagedType.I4)]
        public int type;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.R8, SizeConst = 10)]
        public double[] parameters;
        [MarshalAs(UnmanagedType.U4)]
        public int nGridPoints;
        // Pointer to a float array of size 'nGridPoints' if 'type' == 0.
        public IntPtr sampledPoints;
    }

    public sealed class ToneCurve : IDisposable
    {
        private IntPtr _handle;

        internal ToneCurve(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<ToneCurve>(handle);

            _handle = handle;
            Context = context;
        }

        #region Parametric curves
        public static ToneCurve BuildParametric(Context context, int type, double[] parameters)
        {
            if (parameters?.Length > 10) throw new ArgumentException($"'{nameof(parameters)}' array size must not exceed 10.");

            return new ToneCurve(Interop.BuildParametricToneCurve(context?.Handle ?? IntPtr.Zero, type, parameters), context);
        }

        public static ToneCurve BuildGamma(Context context, double gamma)
        {
            return new ToneCurve(Interop.BuildGammaToneCurve(context?.Handle ?? IntPtr.Zero, gamma), context);
        }
        #endregion

        #region Segmented curves
        public static ToneCurve BuildSegmented(Context context, CurveSegment[] segments)
        {
            return new ToneCurve(Interop.BuildSegmentedToneCurve(context?.Handle ?? IntPtr.Zero, segments), context);
        }
        #endregion

        #region Tabulated curves
        public static ToneCurve BuildTabulated(Context context, ushort[] values)
        {
            return new ToneCurve(Interop.BuildTabulatedToneCurve(context?.Handle ?? IntPtr.Zero, values), context);
        }

        public static ToneCurve BuildTabulated(Context context, float[] values)
        {
            return new ToneCurve(Interop.BuildTabulatedToneCurve(context?.Handle ?? IntPtr.Zero, values), context);
        }
        #endregion

        public ToneCurve Duplicate()
        {
            return new ToneCurve(Interop.DuplicateToneCurve(_handle), Context);
        }

        public ToneCurve Reverse()
        {
            return new ToneCurve(Interop.ReverseToneCurve(_handle), Context);
        }

        public ToneCurve Reverse(int nResultSamples)
        {
            return new ToneCurve(Interop.ReverseToneCurve(_handle, nResultSamples), Context);
        }

        public ToneCurve Join(Context context, ToneCurve other, int nPoints)
        {
            return new ToneCurve(Interop.JoinToneCurve(context?.Handle ?? IntPtr.Zero, _handle, other.Handle, nPoints));
        }

        public bool Smooth(double lambda)
        {
            return Interop.SmoothToneCurve(_handle, lambda) != 0;
        }

        public float Evaluate(float v)
        {
            EnsureNotDisposed();

            return Interop.EvaluateToneCurve(_handle, v);
        }

        public ushort Evaluate(ushort v)
        {
            EnsureNotDisposed();

            return Interop.EvaluateToneCurve(_handle, v);
        }

        public double EstimateGamma(double precision)
        {
            EnsureNotDisposed();

            return Interop.EstimateGamma(_handle, precision);
        }

        public Context Context { get; private set; }

        public bool IsMultisegment => Interop.IsMultiSegmentToneCurve(_handle) != 0;

        public bool IsLinear => Interop.IsLinearToneCurve(_handle) != 0;

        public bool IsMonotonic => Interop.IsMonotonicToneCurve(_handle) != 0;

        public bool IsDescending => Interop.IsDescendingToneCurve(_handle) != 0;

        public uint EstimatedTableEntries => Interop.GetEstimatedTableEntries(_handle);

        public IntPtr EstimatedTable => Interop.GetEstimatedTable(_handle);

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(ToneCurve));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.FreeToneCurve(handle);
                Context = null;
            }
        }

        ~ToneCurve()
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
