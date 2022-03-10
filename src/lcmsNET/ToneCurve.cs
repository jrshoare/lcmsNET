// Copyright(c) 2019-2021 John Stevenson-Hoare
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using lcmsNET.Impl;
using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Represents a segemnt of a curve.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CurveSegment
    {
        /// <summary>
        /// Domain; for <see cref="x0"/> &lt; 'x'.
        /// </summary>
        [MarshalAs(UnmanagedType.R4)]
        public float x0;
        /// <summary>
        /// Domain; for 'x' &lt;= <see cref="x1"/>.
        /// </summary>
        [MarshalAs(UnmanagedType.R4)]
        public float x1;
        /// <summary>
        /// Parametric type, where 0 means sampled and negative values are reserved.
        /// </summary>
        [MarshalAs(UnmanagedType.I4)]
        public int type;
        /// <summary>
        /// Parameters if <see cref="type"/> != 0.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.R8, SizeConst = 10)]
        public double[] parameters;
        /// <summary>
        /// Number of grid points if <see cref="type"/> == 0.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public int nGridPoints;
        /// <summary>
        /// Pointer to a floating point array of size <see cref="nGridPoints"/> if <see cref="type"/> == 0.
        /// </summary>
        public IntPtr sampledPoints;
    }

    /// <summary>
    /// Represents a tone curve.
    /// </summary>
    public sealed class ToneCurve : TagBase<ToneCurve>
    {
        internal ToneCurve(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        /// <summary>
        /// Creates a tone curve from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing tone curve.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance referencing an existing tone curve.</returns>
        internal static ToneCurve FromHandle(IntPtr handle)
        {
            return new ToneCurve(handle, context: null, isOwner: false);
        }

        #region Parametric curves
        /// <summary>
        /// Creates a new instance of the <see cref="ToneCurve"/> class for a parametric curve.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="type">The parametric tone curve number.</param>
        /// <param name="parameters">An array of 10 values defining the tone curve parameters for the given <paramref name="type"/>.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static ToneCurve BuildParametric(Context context, int type, double[] parameters)
        {
            if (parameters?.Length > 10) throw new ArgumentException($"'{nameof(parameters)}' array size must not exceed 10.");

            return new ToneCurve(Interop.BuildParametricToneCurve(context?.Handle ?? IntPtr.Zero, type, parameters), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ToneCurve"/> class for a gamma tone curve.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="gamma">The value of the gamma exponent.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static ToneCurve BuildGamma(Context context, double gamma)
        {
            return new ToneCurve(Interop.BuildGammaToneCurve(context?.Handle ?? IntPtr.Zero, gamma), context);
        }
        #endregion

        #region Segmented curves
        /// <summary>
        /// Creates a new instance of the <see cref="ToneCurve"/> class from the given segment information.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="segments">An array of segments.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static ToneCurve BuildSegmented(Context context, CurveSegment[] segments)
        {
            return new ToneCurve(Interop.BuildSegmentedToneCurve(context?.Handle ?? IntPtr.Zero, segments), context);
        }
        #endregion

        #region Tabulated curves
        /// <summary>
        /// Creates a new instance of the <see cref="ToneCurve"/> class from the given 16-bit values.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="values">An array of 16-bit values.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static ToneCurve BuildTabulated(Context context, ushort[] values)
        {
            return new ToneCurve(Interop.BuildTabulatedToneCurve(context?.Handle ?? IntPtr.Zero, values), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ToneCurve"/> class from the given floating point values.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="values">An array of floating point values.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static ToneCurve BuildTabulated(Context context, float[] values)
        {
            return new ToneCurve(Interop.BuildTabulatedToneCurve(context?.Handle ?? IntPtr.Zero, values), context);
        }
        #endregion

        /// <summary>
        /// Duplicates a tone curve.
        /// </summary>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public ToneCurve Duplicate()
        {
            EnsureNotClosed();

            return new ToneCurve(Interop.DuplicateToneCurve(handle), Context);
        }

        /// <summary>
        /// Creates a tone curve that is the inverse ƒ⁻¹ of this instance.
        /// </summary>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public ToneCurve Reverse()
        {
            EnsureNotClosed();

            return new ToneCurve(Interop.ReverseToneCurve(handle), Context);
        }

        /// <summary>
        /// Creates a tone curve that is the inverse ƒ⁻¹ of this instance or a tabulated
        /// curve of <paramref name="nResultSamples"/> if it could not be reversed analytically.
        /// </summary>
        /// <param name="nResultSamples">
        /// Number of samples to use in case the tone curve cannot be reversed analytically.
        /// </param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public ToneCurve Reverse(int nResultSamples)
        {
            EnsureNotClosed();

            return new ToneCurve(Interop.ReverseToneCurve(handle, nResultSamples), Context);
        }

        /// <summary>
        /// Composites two tone curves in the form Y⁻¹(X(t)).
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="other">The other tone curve 'X'.</param>
        /// <param name="nPoints">Sample rate for the resulting tone curve.</param>
        /// <returns>A new <see cref="ToneCurve"/> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> is null.
        /// </exception>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public ToneCurve Join(Context context, ToneCurve other, int nPoints)
        {
            EnsureNotClosed();
            if (other is null) throw new ArgumentNullException(nameof(other));
            other.EnsureNotClosed();

            return new ToneCurve(Interop.JoinToneCurve(context?.Handle ?? IntPtr.Zero, Handle, other.Handle, nPoints));
        }

        /// <summary>
        /// Smoothes the tone curve according to the <paramref name="lambda"/> parameter.
        /// </summary>
        /// <param name="lambda">The degree of smoothing.</param>
        /// <returns>true if smoothing was successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public bool Smooth(double lambda)
        {
            EnsureNotClosed();

            return Interop.SmoothToneCurve(handle, lambda) != 0;
        }

        /// <summary>
        /// Evaluates the given floating point number across the tone curve.
        /// </summary>
        /// <param name="v">The value to evaluate.</param>
        /// <returns>The value evaluated across the tone curve.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public float Evaluate(float v)
        {
            EnsureNotClosed();

            return Interop.EvaluateToneCurve(handle, v);
        }

        /// <summary>
        /// Evaluates the given 16-bit number across the tone curve.
        /// </summary>
        /// <param name="v">The value to evaluate.</param>
        /// <returns>The value evaluated across the tone curve.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        public ushort Evaluate(ushort v)
        {
            EnsureNotClosed();

            return Interop.EvaluateToneCurve(handle, v);
        }

        /// <summary>
        /// Estimates the apparent gamma of the tone curve using a least square fit to a
        /// pure exponent expression in the ƒ(x)=x^𝛾.
        /// </summary>
        /// <param name="precision">The maximum standard deviation allowed on the residuals.</param>
        /// <returns>The estimated gamma at the given precision, or -1 if the fitting has less precision.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The ToneCurve has already been disposed.
        /// </exception>
        /// <remarks>
        /// <para>
        /// A value of 0.01 is a fair value for <paramref name="precision"/>. Set to a large number to fit any
        /// curve no matter good the fit.
        /// </para>
        /// <para>
        /// The 𝛾 is estimated at the given precision.
        /// </para>
        /// </remarks>
        public double EstimateGamma(double precision)
        {
            EnsureNotClosed();

            return Interop.EstimateGamma(handle, precision);
        }

        /// <summary>
        /// Gets a value indicating whether the tone curve contains more than one segment.
        /// </summary>
        public bool IsMultisegment => Interop.IsMultiSegmentToneCurve(handle) != 0;

        /// <summary>
        /// Gets a value indicating whether the tone curve is linear.
        /// </summary>
        /// <remarks>
        /// This is just a coarse approximation with no mathematical validity that does not
        /// take unbounded parts into account.
        /// </remarks>
        public bool IsLinear => Interop.IsLinearToneCurve(handle) != 0;

        /// <summary>
        /// Gets a value indicating whether the tone curve is monotonic.
        /// </summary>
        /// <remarks>
        /// This is just a coarse approximation with no mathematical validity that does not
        /// take unbounded parts into account.
        /// </remarks>
        public bool IsMonotonic => Interop.IsMonotonicToneCurve(handle) != 0;

        /// <summary>
        /// Returns true if (0) > ƒ(1), otherwise false.
        /// </summary>
        /// <remarks>
        /// Does not take unbounded parts into account.
        /// </remarks>
        public bool IsDescending => Interop.IsDescendingToneCurve(handle) != 0;

        /// <summary>
        /// Gets the number of entries in the maintained shadow low-resolution tabulated
        /// representation of the tone curve.
        /// </summary>
        /// <remarks>
        /// Requires Little CMS version 2.4 or later.
        /// </remarks>
        public uint EstimatedTableEntries => Interop.GetEstimatedTableEntries(handle);

        /// <summary>
        /// Gets a pointer to the maintained shadow low-resolution tabulated
        /// representation of the tone curve.
        /// </summary>
        /// <remarks>
        /// Requires Little CMS version 2.4 or later.
        /// </remarks>
        public IntPtr EstimatedTable => Interop.GetEstimatedTable(handle);

        /// <summary>
        /// Frees the tone curve.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.FreeToneCurve(handle);
            return true;
        }
    }
}
