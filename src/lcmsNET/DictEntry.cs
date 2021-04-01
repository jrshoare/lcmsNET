using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
    /// <summary>
    /// Represents a name-value entry in a dictionary.
    /// </summary>
    public sealed class DictEntry
    {
        internal DictEntry(IntPtr handle)
        {
            var entry = Marshal.PtrToStructure<_DictEntry>(handle);

            Handle = handle;
            DisplayName = entry.DisplayName != IntPtr.Zero
                    ? MultiLocalizedUnicode.CopyRef(entry.DisplayName) : null;
            DisplayValue = entry.DisplayValue != IntPtr.Zero
                    ? MultiLocalizedUnicode.CopyRef(entry.DisplayValue) : null;
            Name = entry.Name;
            Value = entry.Value;
            Next = entry.Next;
        }

        internal IntPtr Handle { get; private set; }

        /// <summary>
        /// Gets the display name of the entry. Can be null.
        /// </summary>
        public MultiLocalizedUnicode DisplayName { get; private set; }

        /// <summary>
        /// Gets the display value of the entry. Can be null.
        /// </summary>
        public MultiLocalizedUnicode DisplayValue { get; private set; }

        /// <summary>
        /// Gets the name of the entry.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value of the entry.
        /// </summary>
        public string Value { get; private set; }

        internal IntPtr Next { get; private set; }
    }
}
