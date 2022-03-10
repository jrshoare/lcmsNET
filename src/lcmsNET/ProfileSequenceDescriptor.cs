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
    /// Represents a profile sequence descriptor.
    /// </summary>
    public sealed class ProfileSequenceDescriptor : TagBase<ProfileSequenceDescriptor>
    {
        internal ProfileSequenceDescriptor(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
            CreateItems();
        }

        /// <summary>
        /// Creates a profile sequence descriptor from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing profile sequence descriptor.</param>
        /// <returns>
        /// A new <see cref="ProfileSequenceDescriptor"/> instance referencing an
        /// existing profile sequence descriptor.
        /// </returns>
        /// <exception cref="LcmsNETException">
        /// The <paramref name="handle"/> is <see cref="IntPtr.Zero"/>.
        /// </exception>
        internal static ProfileSequenceDescriptor FromHandle(IntPtr handle)
        {
            return new ProfileSequenceDescriptor(handle, context: null, isOwner: false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ProfileSequenceDescriptor"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nItems">The number of profiles in the sequence.</param>
        /// <returns>A new <see cref="ProfileSequenceDescriptor"/>.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static ProfileSequenceDescriptor Create(Context context, uint nItems)
        {
            return new ProfileSequenceDescriptor(
                    Interop.AllocProfileSequenceDescription(context?.Handle ?? IntPtr.Zero, nItems), context);
        }

        /// <summary>
        /// Duplicates a profile sequence descriptor.
        /// </summary>
        /// <returns>A new <see cref="ProfileSequenceDescriptor"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The ProfileSequenceDescriptor has already been disposed.
        /// </exception>
        public ProfileSequenceDescriptor Duplicate()
        {
            EnsureNotClosed();

            return new ProfileSequenceDescriptor(Interop.DupProfileSequenceDescription(handle), Context);
        }

        /// <summary>
        /// Gets the number of profiles in the sequence.
        /// </summary>
        public uint Length => GetLength();

        /// <summary>
        /// Gets the <see cref="ProfileSequenceItem"/> at a given index position.
        /// </summary>
        /// <param name="index">The index position.</param>
        /// <returns>A <see cref="ProfileSequenceItem"/>.</returns>
        public ProfileSequenceItem this[int index]
        {
            get { return Items[index]; }
        }

        private unsafe Seq* SeqDesc
        {
            get
            {
                EnsureNotClosed();
                return (Seq*)Handle.ToPointer();
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

        /// <summary>
        /// Frees the profile sequence descriptor.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.FreeProfileSequenceDescription(handle);
            return true;
        }

        #region Private Classes
        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct Seq
        {
            public uint n;
            public IntPtr ContextID;
            public IntPtr seq;          // cmsPSEQDESC* aka PSeqDesc
        }
        #endregion
    }
}
