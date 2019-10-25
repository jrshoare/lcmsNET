using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class IT8 : IDisposable
    {
        private IntPtr _handle;

        internal IT8(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<IT8>(handle);

            _handle = handle;
            Context = context;
        }

        public static IT8 Create(Context context)
        {
            return new IT8(Interop.IT8Alloc(context?.Handle ?? IntPtr.Zero), context);
        }

        #region Properties
        public Context Context { get; private set; }
        #endregion

        #region Tables
        public uint TableCount => Interop.IT8TableCount(_handle);

        public int SetTable(uint nTable)
        {
            EnsureNotDisposed();

            return Interop.IT8SetTable(_handle, nTable);
        }
        #endregion

        #region Persistence
        public static IT8 Open(Context context, string filepath)
        {
            return new IT8(Interop.IT8LoadFromFile(context?.Handle ?? IntPtr.Zero, filepath), context);
        }

        public static IT8 Open(Context context, byte[] memory)
        {
            return new IT8(Interop.IT8LoadFromMem(context?.Handle ?? IntPtr.Zero, memory), context);
        }

        public bool Save(string filepath)
        {
            EnsureNotDisposed();

            return 0 != Interop.IT8SaveToFile(_handle, filepath);
        }

        public bool Save(byte[] it8, out int bytesNeeded)
        {
            EnsureNotDisposed();

            return 0 != Interop.IT8SaveToMem(_handle, it8, out bytesNeeded);
        }
        #endregion

        #region Type and Comments
        public string SheetType
        {
            get { return Interop.IT8GetSheetType(_handle); }
            set
            {
                EnsureNotDisposed();
                if (0 == Interop.IT8SetSheetType(_handle, value))
                {
                    throw new LcmsNETException($"Failed to set sheet type: '{value}'.");
                }
            }
        }

        public bool AddComment(string comment)
        {
            EnsureNotDisposed();
            return Interop.IT8SetComment(_handle, comment) != 0;
        }
        #endregion

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(IT8));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.IT8Free(handle);
                Context = null;
            }
        }

        ~IT8()
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
