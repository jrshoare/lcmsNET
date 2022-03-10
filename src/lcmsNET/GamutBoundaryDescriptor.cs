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

namespace lcmsNET
{
    /// <summary>
    /// Represents a gamut boundary descriptor.
    /// </summary>
    public sealed class GamutBoundaryDescriptor : CmsHandle<GamutBoundaryDescriptor>
    {
        internal GamutBoundaryDescriptor(IntPtr handle, Context context = null)
            : base(handle, context, isOwner: true)
        {
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
            EnsureNotClosed();

            return Interop.GBDAddPoint(handle, lab) != 0;
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
            EnsureNotClosed();

            return Interop.GBDCompute(handle, flags) != 0;
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
            EnsureNotClosed();

            return Interop.GBDCheckPoint(handle, lab) != 0;
        }

        /// <summary>
        /// Frees the gamut boundary descriptor handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.GBDFree(handle);
            return true;
        }
    }
}
