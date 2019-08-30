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

        public Context Context { get; private set; }

        public ColorSpaceSignature ColorSpaceSignature => (ColorSpaceSignature)Interop.GetColorSpaceSignature(_handle);

        public PixelType ColorSpace => Cms.ToPixelType(ColorSpaceSignature);

        public ColorSpaceSignature PCSSignature => (ColorSpaceSignature)Interop.GetPCSSignature(_handle);

        public PixelType PCS => Cms.ToPixelType(PCSSignature);

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
