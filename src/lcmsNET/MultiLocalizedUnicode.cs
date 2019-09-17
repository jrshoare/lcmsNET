using lcmsNET.Impl;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class MultiLocalizedUnicode : IDisposable
    {
        public const string NoLanguage = "\0\0";
        public const string NoCountry = "\0\0";

        private IntPtr _handle;

        internal MultiLocalizedUnicode(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<MultiLocalizedUnicode>(handle);

            _handle = handle;
            Context = context;
        }

        public static MultiLocalizedUnicode Create(Context context, uint nItems)
        {
            return new MultiLocalizedUnicode(Interop.MLUAlloc(context?.Handle ?? IntPtr.Zero, nItems), context);
        }

        public MultiLocalizedUnicode Duplicate()
        {
            EnsureNotDisposed();

            return new MultiLocalizedUnicode(Interop.MLUDup(_handle), Context);
        }

        public bool SetASCII(string languageCode, string countryCode, string value)
        {
            EnsureNotDisposed();

            return Interop.MLUSetAscii(_handle, languageCode, countryCode, value) != 0;
        }

        public bool SetWide(string languageCode, string countryCode, string value)
        {
            EnsureNotDisposed();

            return Interop.MLUSetWide(_handle, languageCode, countryCode, value) != 0;
        }

        public string GetASCII(string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.MLUGetASCII(_handle, languageCode, countryCode);
        }

        public string GetWide(string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.MLUGetWide(_handle, languageCode, countryCode);
        }

        public bool GetTranslation(string languageCode, string countryCode, out string translationLanguage, out string translationCountry)
        {
            EnsureNotDisposed();

            return Interop.MLUGetTranslation(_handle, languageCode, countryCode, out translationLanguage, out translationCountry) != 0;
        }

        public bool TranslationsCodes(uint index, out string languageCode, out string countryCode)
        {
            EnsureNotDisposed();

            return Interop.MLUTranslationsCodes(_handle, index, out languageCode, out countryCode) != 0;
        }

        public Context Context { get; private set; }

        public uint TranslationsCount => Interop.MLUTranslationsCount(_handle);

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(Pipeline));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.MLUFree(handle);
                Context = null;
            }
        }

        ~MultiLocalizedUnicode()
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
