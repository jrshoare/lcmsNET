using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class NamedColorList : IDisposable, IWrapper
    {
        private IntPtr _handle;

        internal NamedColorList(IntPtr handle, Context context = null, bool isOwner = true)
        {
            Helper.CheckCreated<NamedColorList>(handle);

            _handle = handle;
            Context = context;
            IsOwner = isOwner;
        }

        /// <summary>
        /// Creates a named color list from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing named color list.</param>
        /// <returns>A new <see cref="NamedColorList"/> instance referencing an existing named color list.</returns>
        /// <remarks>
        /// The instance created should be considered read-only for <paramref name="handle"/>
        /// values returned from <see cref="Profile.ReadTag(TagSignature)"/>.
        /// </remarks>
        public static NamedColorList FromHandle(IntPtr handle)
        {
            return new NamedColorList(handle, context: null, isOwner: false);
        }

        internal static NamedColorList CopyRef(IntPtr handle, Context context = null)
        {
            return new NamedColorList(handle, context, isOwner: false);
        }

        public static NamedColorList Create(Context context, uint n, uint colorantCount, string prefix, string suffix)
        {
            return new NamedColorList(
                    Interop.AllocNamedColorList(context?.Handle ?? IntPtr.Zero,
                            n, colorantCount, prefix, suffix), context);
        }

        public NamedColorList Duplicate()
        {
            EnsureNotDisposed();

            return new NamedColorList(Interop.DupNamedColorList(_handle), Context);
        }

        public bool Add(string name, ushort[] pcs, ushort[] colorant)
        {
            if (pcs?.Length != 3) throw new ArgumentException($"'{nameof(pcs)}' array size must equal 3.");
            if (colorant?.Length != 16) throw new ArgumentException($"'{nameof(colorant)}' array size must equal 16.");

            EnsureNotDisposed();

            return Interop.AppendNamedColor(_handle, name, pcs, colorant) != 0;
        }

        public bool GetInfo(uint nColor, out string name, out string prefix, out string suffix,
                out ushort[] pcs, out ushort[] colorant)
        {
            pcs = new ushort[3];
            colorant = new ushort[16];

            return Interop.NamedColorInfo(_handle, nColor, out name, out prefix, out suffix, pcs, colorant) != 0;
        }

        public int this[string name] => Interop.NamedColorIndex(_handle, name);

        public Context Context { get; private set; }

        public uint Count => Interop.NamedColorCount(_handle);

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(NamedColorList));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (IsOwner && handle != IntPtr.Zero) // only dispose undisposed objects that we own
            {
                Interop.FreeNamedColorList(handle);
                Context = null;
            }
        }

        ~NamedColorList()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public IntPtr Handle => _handle;

        private bool IsOwner { get; set; }
    }
}
