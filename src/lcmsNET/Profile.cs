using lcmsNET.Impl;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class Profile : IDisposable
    {
        private IntPtr _handle;

        internal Profile(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<Profile>(handle);

            _handle = handle;
            Context = context;
        }

        #region Predefined virtual profiles
        public static Profile CreatePlaceholder(Context context)
        {
            return new Profile(Interop.CreatePlaceholder(context.Handle));
        }

        public static Profile CreateRGB(CIExyY whitePoint, CIExyYTRIPLE primaries, ToneCurve[] transferFunction)
        {
            if (transferFunction?.Length != 3) throw new ArgumentException($"'{nameof(transferFunction)}' array size must equal 3.");

            return new Profile(Interop.CreateRGB(whitePoint, primaries, transferFunction.Select(_ => _.Handle).ToArray()));
        }

        public static Profile CreateRGB(Context context, CIExyY whitePoint, CIExyYTRIPLE primaries, ToneCurve[] transferFunction)
        {
            if (transferFunction?.Length != 3) throw new ArgumentException($"'{nameof(transferFunction)}' array size must equal 3.");

            return new Profile(Interop.CreateRGB(context.Handle, whitePoint, primaries, transferFunction.Select(_ => _.Handle).ToArray()), context);
        }

        public static Profile CreateGray(CIExyY whitePoint, ToneCurve transferFunction)
        {
            return new Profile(Interop.CreateGray(whitePoint, transferFunction.Handle));
        }

        public static Profile CreateGray(Context context, CIExyY whitePoint, ToneCurve transferFunction)
        {
            return new Profile(Interop.CreateGray(context.Handle, whitePoint, transferFunction.Handle), context);
        }

        public static Profile CreateLinearizationDeviceLink(ColorSpaceSignature space, ToneCurve[] transferFunction)
        {
            return new Profile(Interop.CreateLinearizationDeviceLink(Convert.ToInt32(space), transferFunction.Select(_ => _.Handle).ToArray()));
        }

        public static Profile CreateLinearizationDeviceLink(Context context, ColorSpaceSignature space, ToneCurve[] transferFunction)
        {
            return new Profile(Interop.CreateLinearizationDeviceLink(context.Handle, Convert.ToInt32(space),
                    transferFunction.Select(_ => _.Handle).ToArray()), context);
        }

        public static Profile CreateInkLimitingDeviceLink(ColorSpaceSignature space, double limit)
        {
            return new Profile(Interop.CreateInkLimitingDeviceLink(Convert.ToInt32(space), limit));
        }

        public static Profile CreateInkLimitingDeviceLink(Context context, ColorSpaceSignature space, double limit)
        {
            return new Profile(Interop.CreateInkLimitingDeviceLink(context.Handle, Convert.ToInt32(space), limit));
        }

        public static Profile CreateDeviceLink(Transform transform, double version, CmsFlags flags)
        {
            return new Profile(Interop.Transform2DeviceLink(transform.Handle, version, Convert.ToInt32(flags)));
        }

        public static Profile CreateLab2(CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab2(whitePoint));
        }

        public static Profile CreateLab2(Context context, CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab2(context.Handle, whitePoint));
        }

        public static Profile CreateLab4(CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab4(whitePoint));
        }

        public static Profile CreateLab4(Context context, CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab4(context.Handle, whitePoint));
        }

        public static Profile CreateXYZ()
        {
            return new Profile(Interop.CreateXYZ());
        }

        public static Profile CreateXYZ(Context context)
        {
            return new Profile(Interop.CreateXYZ(context.Handle));
        }

        public static Profile Create_sRGB()
        {
            return new Profile(Interop.Create_sRGB());
        }

        public static Profile Create_sRGB(Context context)
        {
            return new Profile(Interop.Create_sRGB(context.Handle));
        }

        public static Profile CreateNull()
        {
            return new Profile(Interop.CreateNull());
        }

        public static Profile CreateNull(Context context)
        {
            return new Profile(Interop.CreateNull(context.Handle));
        }

        public static Profile CreateBCHSWabstract(int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return new Profile(Interop.CreateBCHSWabstract(nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest));
        }

        public static Profile CreateBCHSWabstract(Context context, int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return new Profile(Interop.CreateBCHSWabstract(context.Handle, nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest));
        }
        #endregion

        #region Access functions
        public static Profile Open(string filepath, string access)
        {
            return new Profile(Interop.OpenProfile(filepath, access));
        }

        public static Profile Open(Context context, string filepath, string access)
        {
            return new Profile(Interop.OpenProfile(context.Handle, filepath, access), context);
        }

        public static Profile Open(byte[] memory)
        {
            return new Profile(Interop.OpenProfile(memory));
        }

        public static Profile Open(Context context, byte[] memory)
        {
            return new Profile(Interop.OpenProfile(context.Handle, memory));
        }

        public bool Save(string filepath)
        {
            EnsureNotDisposed();

            return 0 != Interop.SaveProfile(_handle, filepath);
        }

        public bool Save(byte[] profile, out int bytesNeeded)
        {
            EnsureNotDisposed();

            return 0 != Interop.SaveProfile(_handle, profile, out bytesNeeded);
        }
        #endregion

        #region Obtain localized information
        public string GetProfileInfo(InfoType info, string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.GetProfileInfo(_handle, Convert.ToInt32(info), languageCode, countryCode);
        }

        public string GetProfileInfoASCII(InfoType info, string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.GetProfileInfoASCII(_handle, Convert.ToInt32(info), languageCode, countryCode);
        }
        #endregion

        #region Feature detection
        public bool DetectBlackPoint(ref CIEXYZ blackPoint, Intent intent, CmsFlags flags = CmsFlags.None)
        {
            return Interop.DetectBlackPoint(_handle, ref blackPoint, Convert.ToInt32(intent), Convert.ToInt32(flags)) != 0;
        }

        public bool DetectDestinationBlackPoint(ref CIEXYZ blackPoint, Intent intent, CmsFlags flags = CmsFlags.None)
        {
            return Interop.DetectDestinationBlackPoint(_handle, ref blackPoint, Convert.ToInt32(intent), Convert.ToInt32(flags)) != 0;
        }
        #endregion

        #region Access profile header
        public bool GetHeaderCreationDateTime(out DateTime dest)
        {
            EnsureNotDisposed();

            return Interop.GetHeaderCreationDateTime(_handle, out dest) != 0;
        }
        #endregion

        #region Info on profile implementation
        public bool IsCLUT(Intent intent, UsedDirection direction)
        {
            EnsureNotDisposed();

            return Interop.IsCLUT(_handle, Convert.ToInt32(intent), Convert.ToInt32(direction)) != 0;
        }
        #endregion

        #region Access tags
        public TagSignature GetTag(uint n)
        {
            EnsureNotDisposed();

            return (TagSignature)Interop.GetTagSignature(_handle, n);
        }

        public bool HasTag(TagSignature tag)
        {
            EnsureNotDisposed();

            return Interop.IsTag(_handle, Convert.ToInt32(tag)) != 0;
        }

        public IntPtr ReadTag(TagSignature tag)
        {
            EnsureNotDisposed();

            return Interop.ReadTag(_handle, Convert.ToInt32(tag));
        }

        public bool WriteTag(TagSignature tag, IntPtr data)
        {
            EnsureNotDisposed();

            return Interop.WriteTag(_handle, Convert.ToInt32(tag), data) != 0;
        }

        public bool LinkTag(TagSignature tag, TagSignature dest)
        {
            EnsureNotDisposed();

            return Interop.LinkTag(_handle, Convert.ToInt32(tag), Convert.ToInt32(dest)) != 0;
        }

        public TagSignature TagLinkedTo(TagSignature tag)
        {
            EnsureNotDisposed();

            return (TagSignature)Interop.TagLinkedTo(_handle, Convert.ToInt32(tag));
        }
        #endregion

        #region Intents
        public bool IsIntentSupported(Intent intent, UsedDirection usedDirection)
        {
            EnsureNotDisposed();

            return Interop.IsIntentSupported(_handle, Convert.ToInt32(intent), Convert.ToInt32(usedDirection)) != 0;
        }
        #endregion

        #region Properties
        public Context Context { get; private set; }

        public ColorSpaceSignature ColorSpace
        {
            get { return (ColorSpaceSignature)Interop.GetColorSpace(_handle); }
            set { Interop.SetColorSpace(_handle, Convert.ToInt32(value)); }
        }

        public ColorSpaceSignature PCS
        {
            get { return (ColorSpaceSignature)Interop.GetPCS(_handle); }
            set { Interop.SetPCS(_handle, Convert.ToInt32(value)); }
        }

        public double TotalAreaCoverage => Interop.DetectTAC(_handle);

        public ProfileClassSignature DeviceClass
        {
            get { return (ProfileClassSignature)Interop.GetDeviceClass(_handle); }
            set { Interop.SetDeviceClass(_handle, (int)value); }
        }

        public uint HeaderFlags
        {
            get { return Interop.GetHeaderFlags(_handle); }
            set { Interop.SetHeaderFlags(_handle, value); }
        }

        public uint HeaderManufacturer
        {
            get { return Interop.GetHeaderManufacturer(_handle); }
            set { Interop.SetHeaderManufacturer(_handle, value); }
        }

        public uint HeaderModel
        {
            get { return Interop.GetHeaderModel(_handle); }
            set { Interop.SetHeaderModel(_handle, value); }
        }

        public DeviceAttributes HeaderAttributes
        {
            get { return (DeviceAttributes)Interop.GetHeaderAttributes(_handle); }
            set { Interop.SetHeaderAttributes(_handle, (ulong)value); }
        }

        public double Version
        {
            get { return Interop.GetProfileVersion(_handle); }
            set { Interop.SetProfileVersion(_handle, value); }
        }

        public uint EncodedICCVersion
        {
            get { return Interop.GetEncodedICCVersion(_handle); }
            set { Interop.SetEncodedICCVersion(_handle, value); }
        }

        public bool IsMatrixShaper => Interop.IsMatrixShaper(_handle) != 0;

        public int TagCount => Interop.GetTagCount(_handle);

        public Intent HeaderRenderingIntent
        {
            get { return (Intent)Interop.GetHeaderRenderingIntent(_handle); }
            set { Interop.SetHeaderRenderingIntent(_handle, Convert.ToInt32(value)); }
        }
        #endregion

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(Profile));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.CloseProfile(handle);
                Context = null;
            }
        }

        ~Profile()
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
