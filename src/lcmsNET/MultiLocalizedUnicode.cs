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
    /// Represents a multi-localized Unicode string.
    /// </summary>
    public sealed class MultiLocalizedUnicode : TagBase<MultiLocalizedUnicode>
    {
        /// <summary>
        /// The language code for 'no language'.
        /// </summary>
        public const string NoLanguage = "\0\0";
        /// <summary>
        /// The country code for 'no country'.
        /// </summary>
        public const string NoCountry = "\0\0";

        internal MultiLocalizedUnicode(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        /// <summary>
        /// Creates a multi-localized Unicode string from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing multi-localized Unicode string.</param>
        /// <returns>A new <see cref="MultiLocalizedUnicode"/> instance referencing an existing multi-localized Unicode string.</returns>
        internal static MultiLocalizedUnicode FromHandle(IntPtr handle)
        {
            return new MultiLocalizedUnicode(handle, context: null, isOwner: false);
        }

        internal static MultiLocalizedUnicode CopyRef(IntPtr handle, Context context = null)
        {
            return new MultiLocalizedUnicode(handle, context, isOwner: false);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MultiLocalizedUnicode"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nItems">The initial number of items to be allocated.</param>
        /// <returns>A new <see cref="MultiLocalizedUnicode"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static MultiLocalizedUnicode Create(Context context, uint nItems = 0)
        {
            return new MultiLocalizedUnicode(Interop.MLUAlloc(context?.Handle ?? IntPtr.Zero, nItems), context);
        }

        /// <summary>
        /// Duplicates a multi-localized Unicode string.
        /// </summary>
        /// <returns>A new <see cref="MultiLocalizedUnicode"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        public MultiLocalizedUnicode Duplicate()
        {
            EnsureNotDisposed();

            return new MultiLocalizedUnicode(Interop.MLUDup(handle), Context);
        }

        /// <summary>
        /// Sets an ASCII (7 bit) entry for the given language and country code.
        /// </summary>
        /// <param name="languageCode">The ISO 639-1 language code.</param>
        /// <param name="countryCode">The ISO 3166-1 country code.</param>
        /// <param name="value">The value to be set.</param>
        /// <returns>true if set successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        public bool SetASCII(string languageCode, string countryCode, string value)
        {
            EnsureNotDisposed();

            return Interop.MLUSetAscii(handle, languageCode, countryCode, value) != 0;
        }

        /// <summary>
        /// Sets a Unicode wide character (16 bit) entry for the given language and country code.
        /// </summary>
        /// <param name="languageCode">The ISO 639-1 language code.</param>
        /// <param name="countryCode">The ISO 3166-1 country code.</param>
        /// <param name="value">The value to be set.</param>
        /// <returns>true if set successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        public bool SetWide(string languageCode, string countryCode, string value)
        {
            EnsureNotDisposed();

            return Interop.MLUSetWide(handle, languageCode, countryCode, value) != 0;
        }

        /// <summary>
        /// Gets the ASCII (7 bit) entry for the given language and country code.
        /// </summary>
        /// <param name="languageCode">The ISO 639-1 language code.</param>
        /// <param name="countryCode">The ISO 3166-1 country code.</param>
        /// <returns>The entry, or null if not found.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        public string GetASCII(string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.MLUGetASCII(handle, languageCode, countryCode);
        }

        /// <summary>
        /// Gets the Unicode wide character (16 bit) entry for the given language and country code.
        /// </summary>
        /// <param name="languageCode">The ISO 639-1 language code.</param>
        /// <param name="countryCode">The ISO 3166-1 country code.</param>
        /// <returns>The entry, or null if not found.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        public string GetWide(string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.MLUGetWide(handle, languageCode, countryCode);
        }

        /// <summary>
        /// Gets the translation rule for the given language and country code.
        /// </summary>
        /// <param name="languageCode">The required ISO 639-1 language code.</param>
        /// <param name="countryCode">The required ISO 3166-1 country code.</param>
        /// <param name="translationLanguage">
        /// Returns the ISO 639-1 language code obtained if successful, otherwise null.
        /// </param>
        /// <param name="translationCountry">
        /// Returns the ISO 3166-1 country code obtained if successful, otherwise null.
        /// </param>
        /// <returns>true if a translation exists, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        /// <remarks>
        /// The algorithm searches for an exact match of language and country code. If not
        /// found it attempts to match the language code, and if this does not yield a result
        /// the first entry is returned.
        /// </remarks>
        public bool GetTranslation(string languageCode, string countryCode, out string translationLanguage, out string translationCountry)
        {
            EnsureNotDisposed();

            return Interop.MLUGetTranslation(handle, languageCode, countryCode, out translationLanguage, out translationCountry) != 0;
        }

        /// <summary>
        /// Gets the language and country codes for the given translation index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <param name="languageCode">
        /// Returns the ISO 639-1 language code if successful, otherwise null.
        /// </param>
        /// <param name="countryCode">
        /// Returns the ISO 3166-1 country code if successful, otherwise null.
        /// </param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The multi-localized Unicode string has already been disposed.
        /// </exception>
        public bool TranslationsCodes(uint index, out string languageCode, out string countryCode)
        {
            EnsureNotDisposed();

            return Interop.MLUTranslationsCodes(handle, index, out languageCode, out countryCode) != 0;
        }

        /// <summary>
        /// Gets the number of translations stored in the multi-localized Unicode string.
        /// </summary>
        /// <remarks>
        /// Requires Little CMS version 2.5 or later.
        /// </remarks>
        public uint TranslationsCount => Interop.MLUTranslationsCount(handle);

        /// <summary>
        /// Frees the MLU handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.MLUFree(handle);
            return true;
        }
    }
}
