using System;
using System.Runtime.InteropServices;

namespace lcmsNET
{
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

        public MultiLocalizedUnicode DisplayName { get; private set; }

        public MultiLocalizedUnicode DisplayValue { get; private set; }

        public string Name { get; private set; }

        public string Value { get; private set; }

        internal IntPtr Next { get; private set; }
    }
}
