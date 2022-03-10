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
        /// Degree of chromatic adaptation.
        /// </summary>
        /// <remarks>
        /// A value of -1 causes D to be calculated, otherwise specify in the range 0..1.0.
        /// </remarks>
        [MarshalAs(UnmanagedType.R8)]
        public double D_value;
    }

    /// <summary>
    /// Represents a CIE CAM02 color appearance model.
    /// </summary>
    public sealed class CAM02 : CmsHandle<CAM02>
    {
        internal CAM02(IntPtr handle, Context context = null)
            : base(handle, context, isOwner: true)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CAM02"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="conditions">The viewing conditions.</param>
        /// <returns>A new <see cref="CAM02"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
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
        /// <exception cref="ObjectDisposedException">
        /// The model has already been disposed.
        /// </exception>
        public void Forward(in CIEXYZ xyz, out JCh jch)
        {
            EnsureNotClosed();

            Interop.CIECAM02Forward(handle, xyz, out jch);
        }

        /// <summary>
        /// Evaluates the CAM02 model in the reverse direction JCh → XYZ.
        /// </summary>
        /// <param name="jch">The input JCh value.</param>
        /// <param name="xyz">Returns the XYZ value.</param>
        /// <exception cref="ObjectDisposedException">
        /// The model has already been disposed.
        /// </exception>
        public void Reverse(in JCh jch, out CIEXYZ xyz)
        {
            EnsureNotClosed();

            Interop.CIECAM02Reverse(handle, jch, out xyz);
        }

        /// <summary>
        /// Frees the CAM02 handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.CIECAM02Done(handle);
            return true;
        }
    }
}
