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
    /// Represents an item in a profile sequence.
    /// </summary>
    public sealed class ProfileSequenceItem
    {
        internal ProfileSequenceItem(IntPtr ptr, ProfileSequenceDescriptor parent)
        {
            Ptr = ptr;
            Parent = parent;
        }

        /// <summary>
        /// Gets or sets the signature of the device manufacturer of the profile.
        /// </summary>
        public uint DeviceMfg
        {
            get { return GetDeviceMfg(); }
            set { SetDeviceMfg(value); }
        }

        /// <summary>
        /// Gets or sets the signature of the device model of the profile.
        /// </summary>
        public uint DeviceModel
        {
            get { return GetDeviceModel(); }
            set { SetDeviceModel(value); }
        }

        /// <summary>
        /// Gets or sets the attributes unique to the particular device setup for which
        /// the profile is applicable.
        /// </summary>
        public DeviceAttributes Attributes
        {
            get { return GetAttributes(); }
            set { SetAttributes(value); }
        }

        /// <summary>
        /// Gets or sets the ICC technology of the profile.
        /// </summary>
        public TechnologySignature Technology
        {
            get { return GetTechnology(); }
            set { SetTechnology(value); }
        }

        /// <summary>
        /// Gets or sets the profile ID of the profile.
        /// </summary>
        public byte[] ProfileID
        {
            get { return GetProfileID(); }
            set { SetProfileID(value); }
        }

        /// <summary>
        /// Gets or sets the manufacturer string of the profile.
        /// </summary>
        /// <remarks>
        /// Responsibility for freeing resources associated with the <see cref="MultiLocalizedUnicode"/>
        /// object used to set the value is passed to the <see cref="ProfileSequenceItem"/>.
        /// </remarks>
        public MultiLocalizedUnicode Manufacturer
        {
            get
            {
                if (this.manufacturer is null)
                {
                    this.manufacturer = GetManufacturer();
                }
                return this.manufacturer;
            }
            set
            {
                SetManufacturer(value);
                value?.Release(); // object is now owned by the profile sequence descriptor
                this.manufacturer = null; // force re-load
            }
        }
        private MultiLocalizedUnicode manufacturer;

        /// <summary>
        /// Gets or sets the model string of the profile.
        /// </summary>
        /// <remarks>
        /// Responsibility for freeing resources associated with the <see cref="MultiLocalizedUnicode"/>
        /// object used to set the value is passed to the <see cref="ProfileSequenceItem"/>.
        /// </remarks>
        public MultiLocalizedUnicode Model
        {
            get
            {
                if (this.model is null)
                {
                    this.model = GetModel();
                }
                return this.model;
            }
            set
            {
                SetModel(value);
                value?.Release(); // object is now owned by the profile sequence descriptor
                this.model = null; // force re-load
            }
        }
        private MultiLocalizedUnicode model;

        /// <summary>
        /// Gets or sets the description of the profile.
        /// </summary>
        /// <remarks>
        /// Responsibility for freeing resources associated with the <see cref="MultiLocalizedUnicode"/>
        /// object used to set the value is passed to the <see cref="ProfileSequenceItem"/>.
        /// </remarks>
        public MultiLocalizedUnicode Description
        {
            get
            {
                if (this.description is null)
                {
                    this.description = GetDescription();
                }
                return this.description;
            }
            set
            {
                SetDescription(value);
                value?.Release(); // object is now owned by the profile sequence descriptor
                this.description = null; // force re-load
            }
        }
        private MultiLocalizedUnicode description;

        private unsafe PSeqDesc* SeqDesc
        {
            get
            {
                EnsureNotClosed();
                PSeqDesc* pSeqDesc = (PSeqDesc*)Ptr.ToPointer();
                return pSeqDesc;
            }
        }

        private unsafe uint GetDeviceMfg() => SeqDesc->deviceMfg;
        private unsafe void SetDeviceMfg(uint value) => SeqDesc->deviceMfg = value;
        private unsafe uint GetDeviceModel() => SeqDesc->deviceModel;
        private unsafe void SetDeviceModel(uint value) => SeqDesc->deviceModel = value;
        private unsafe DeviceAttributes GetAttributes() => (DeviceAttributes)SeqDesc->attributes;
        private unsafe void SetAttributes(DeviceAttributes value) => SeqDesc->attributes = Convert.ToUInt64(value);
        private unsafe TechnologySignature GetTechnology() => SeqDesc->technology;
        private unsafe void SetTechnology(TechnologySignature value) => SeqDesc->technology = value;
        private unsafe byte[] GetProfileID()
        {
            byte[] profileID = new byte[16];
            IntPtr ptr = new IntPtr(SeqDesc->ProfileID);
            Marshal.Copy(ptr, profileID, 0, 16);
            return profileID;
        }
        private unsafe void SetProfileID(byte[] value)
        {
            if (value?.Length != 16) throw new ArgumentException($"'{nameof(value)}' array size must equal 16.");
            IntPtr ptr = new IntPtr(SeqDesc->ProfileID);
            Marshal.Copy(value, 0, ptr, 16);
        }
        private unsafe MultiLocalizedUnicode GetManufacturer()
        {
            IntPtr ptr = SeqDesc->Manufacturer;
            return (ptr != IntPtr.Zero) ? MultiLocalizedUnicode.CopyRef(ptr) : null;
        }
        private unsafe void SetManufacturer(MultiLocalizedUnicode value)
        {
            SeqDesc->Manufacturer = Helper.GetHandle(value);
        }
        private unsafe MultiLocalizedUnicode GetModel()
        {
            IntPtr ptr = SeqDesc->Model;
            return (ptr != IntPtr.Zero) ? MultiLocalizedUnicode.CopyRef(ptr) : null;
        }
        private unsafe void SetModel(MultiLocalizedUnicode value)
        {
            SeqDesc->Model = Helper.GetHandle(value);
        }
        private unsafe MultiLocalizedUnicode GetDescription()
        {
            IntPtr ptr = SeqDesc->Description;
            return (ptr != IntPtr.Zero) ? MultiLocalizedUnicode.CopyRef(ptr) : null;
        }
        private unsafe void SetDescription(MultiLocalizedUnicode value)
        {
            SeqDesc->Description = Helper.GetHandle(value);
        }

        private void EnsureNotClosed()
        {
            if (Parent.IsClosed)
            {
                throw new ObjectDisposedException(nameof(ProfileSequenceItem));
            }
        }

        private ProfileSequenceDescriptor Parent { get; set; }

        private IntPtr Ptr { get; set; }
    }
}
