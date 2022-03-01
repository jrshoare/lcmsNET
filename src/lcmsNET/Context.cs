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
    /// Defines a delegate that can be used to free user data.
    /// </summary>
    /// <param name="contextID">The handle to the <see cref="Context"/> with which the user data is associated.</param>
    /// <param name="userData">The pointer to the user data to be freed.</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeUserData(IntPtr contextID, IntPtr userData);

    /// <summary>
    /// Represents a context.
    /// </summary>
    public sealed class Context : CmsHandle<Context>
    {
        internal Context(IntPtr handle, bool isOwner = true)
            : base(handle, context: null, isOwner: isOwner)
        {
        }

        internal static Context CopyRef(IntPtr handle)
        {
            return new Context(handle, isOwner: false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="plugin">
        /// A pointer to a collection of plug-ins, or <see cref="IntPtr.Zero"/> if no plug-ins.
        /// </param>
        /// <param name="userData">
        /// A pointer to user-defined data that will be forwarded to plug-ins and the
        /// context-specific logger, or <see cref="IntPtr.Zero"/> if none.
        /// </param>
        /// <returns>A new <see cref="Context"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public static Context Create(IntPtr plugin, IntPtr userData)
        {
            return new Context(Interop.CreateContext(plugin, userData));
        }

        /// <summary>
        /// Duplicates a context with all associated plug-ins.
        /// </summary>
        /// <param name="userData">
        /// A pointer to user-defined data that will be forwarded to plug-ins and the
        /// context-specific logger, or <see cref="IntPtr.Zero"/> to use the user-defined
        /// data from this <see cref="Context"/> instance.
        /// </param>
        /// <returns>A new <see cref="Context"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The Context has already been disposed.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public Context Duplicate(IntPtr userData)
        {
            EnsureNotDisposed();

            return new Context(Interop.DuplicateContext(handle, userData));
        }

        /// <summary>
        /// Installs a collection of plug-ins to the context.
        /// </summary>
        /// <param name="plugin">A pointer to the collection of plug-ins.</param>
        /// <returns>true if successfule, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Context has already been disposed.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public bool RegisterPlugins(IntPtr plugin)
        {
            EnsureNotDisposed();

            return Interop.RegisterContextPlugins(handle, plugin) == 1;
        }

        /// <summary>
        /// Uninstalls all plug-ins from the context.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// The Context has already been disposed.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public void UnregisterPlugins()
        {
            EnsureNotDisposed();

            Interop.UnregisterContextPlugins(handle);
        }

        /// <summary>
        /// Sets the error handler for the context.
        /// </summary>
        /// <param name="handler">The error handler to be set or null to reset to default.</param>
        /// <exception cref="ObjectDisposedException">
        /// The Context has already been disposed.
        /// </exception>
        /// <remarks>
        /// The default error handler does nothing.
        /// </remarks>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public void SetErrorHandler(ErrorHandler handler)
        {
            EnsureNotDisposed();

            Interop.SetContextErrorHandler(handle, handler);
        }

        /// <summary>
        /// Gets or sets the codes used to mark out-of-gamut on proofing transforms
        /// for the context.
        /// </summary>
        /// <value>
        /// An array of 16 values.
        /// </value>
        /// <exception cref="ObjectDisposedException">
        /// The Context has already been disposed.
        /// </exception>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public ushort[] AlarmCodes
        {
            get
            {
                EnsureNotDisposed();

                ushort[] alarmCodes = new ushort[16];
                Interop.GetAlarmCodesTHR(handle, alarmCodes);
                return alarmCodes;
            }
            set
            {
                if (value?.Length != 16) throw new ArgumentException($"'{nameof(value)}' array size must equal 16.");

                EnsureNotDisposed();

                Interop.SetAlarmCodesTHR(handle, value);
            }
        }

        /// <summary>
        /// Gets or sets the adaptation state for absolute colorimetric intent for the context.
        /// </summary>
        /// <value>
        /// <list type="bullet">
        /// <item>d = Degree on adaptation.</item>
        /// <item>0 = Not adapted.</item>
        /// <item>1 = Complete adaptation.</item>
        /// <item>in-between = Partial adaptation.</item>
        /// </list>
        /// </value>
        /// <exception cref="ObjectDisposedException">
        /// The Context has already been disposed.
        /// </exception>
        /// <remarks>
        /// Ignored for transforms created using
        /// <see cref="Transform.Create(Context, Profile[], bool[], Intent[], double[], Profile, int, uint, uint, CmsFlags)"/>.
        /// </remarks>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public double AdaptationState
        {
            get
            {
                EnsureNotDisposed();

                return Interop.SetAdaptationStateTHR(handle, -1.0);
            }
            set
            {
                EnsureNotDisposed();

                Interop.SetAdaptationStateTHR(handle, value);
            }
        }

        /// <summary>
        /// Gets the user data associated with this context, or <see cref="IntPtr.Zero"/>
        /// if no user was attached on creation or the instance has been disposed.
        /// </summary>
        /// <remarks>
        /// Requires Little CMS version 2.6 or later.
        /// </remarks>
        public IntPtr UserData => Interop.GetContextUserData(handle);

        /// <summary>
        /// Gets the identifier of this context.
        /// </summary>
        public IntPtr ID => Handle;

        /// <summary>
        /// Frees the context handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.DeleteContext(handle);
            return true;
        }
    }
}
