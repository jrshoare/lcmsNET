﻿using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class ProfileSequenceDescriptor : IDisposable
    {
        private IntPtr _handle;

        internal ProfileSequenceDescriptor(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<Profile>(handle);

            _handle = handle;
            Context = context;

            CreateItems();
        }

        public static ProfileSequenceDescriptor Create(Context context, uint nItems)
        {
            return new ProfileSequenceDescriptor(
                    Interop.AllocProfileSequenceDescription(context?.Handle ?? IntPtr.Zero, nItems), context);
        }

        public ProfileSequenceDescriptor Duplicate()
        {
            EnsureNotDisposed();

            return new ProfileSequenceDescriptor(Interop.DupProfileSequenceDescription(_handle), Context);
        }

        public Context Context { get; private set; }

        public uint Length => GetLength();

        public ProfileSequenceItem this[int index]
        {
            get { return Items[index]; }
        }

        private unsafe Seq* SeqDesc
        {
            get
            {
                EnsureNotDisposed();
                return (Seq*)_handle.ToPointer();
            }
        }

        private unsafe uint GetLength()
        {
            return SeqDesc->n;
        }

        private unsafe void CreateItems()
        {
            Items = new ProfileSequenceItem[Length];
            int itemSize = Marshal.SizeOf(typeof(PSeqDesc));
            byte* ptr = (byte*)SeqDesc->seq.ToPointer();
            for (uint i = 0; i < Length; i++)
            {
                Items[i] = new ProfileSequenceItem(new IntPtr(ptr), this);
                ptr += itemSize;
            }
        }

        private ProfileSequenceItem[] Items { get; set; }

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(ProfileSequenceDescriptor));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.FreeProfileSequenceDescription(handle);
                Context = null;
            }
        }

        ~ProfileSequenceDescriptor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Private Classes
        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct Seq
        {
            public uint n;
            public IntPtr ContextID;
            public IntPtr seq;          // cmsPSEQDESC* aka PSeqDesc
        }
        #endregion

        public IntPtr Handle => _handle;
    }
}