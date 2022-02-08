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
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace lcmsNET
{
    /// <summary>
    /// Represents an International Color Consortium Profile.
    /// </summary>
    public sealed class Profile : IDisposable
    {
        private IntPtr _handle;

        internal Profile(IntPtr handle, Context context = null, IOHandler iohandler = null)
        {
            Helper.CheckCreated<Profile>(handle);

            _handle = handle;
            Context = context;
            IOHandler = iohandler;
        }

        #region Predefined virtual profiles
        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for an empty profile.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// <para>
        /// The profile must be populated before it can be used.
        /// </para>
        /// </remarks>
        public static Profile CreatePlaceholder(Context context = null)
        {
            return new Profile(Interop.CreatePlaceholder(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Create a new instance of the <see cref="Profile"/> class for a display RGB profile.
        /// </summary>
        /// <param name="whitePoint">The white point of the RGB device or color space.</param>
        /// <param name="primaries">The primaries in xyY of the device or color space.</param>
        /// <param name="transferFunction">An array of 3 tone curves describing the device or color space gamma.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context.
        /// </para>
        /// </remarks>
        public static Profile CreateRGB(in CIExyY whitePoint, in CIExyYTRIPLE primaries, ToneCurve[] transferFunction)
        {
            if (transferFunction?.Length != 3) throw new ArgumentException($"'{nameof(transferFunction)}' array size must equal 3.");

            return new Profile(Interop.CreateRGB(whitePoint, primaries, transferFunction.Select(_ => _.Handle).ToArray()));
        }

        /// <summary>
        /// Create a new instance of the <see cref="Profile"/> class for a display RGB profile in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="whitePoint">The white point of the RGB device or color space.</param>
        /// <param name="primaries">The primaries in xyY of the device or color space.</param>
        /// <param name="transferFunction">An array of 3 tone curves describing the device or color space gamma.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateRGB(Context context, in CIExyY whitePoint, in CIExyYTRIPLE primaries, ToneCurve[] transferFunction)
        {
            if (transferFunction?.Length != 3) throw new ArgumentException($"'{nameof(transferFunction)}' array size must equal 3.");

            return new Profile(Interop.CreateRGB(context?.Handle ?? IntPtr.Zero, whitePoint, primaries, transferFunction.Select(_ => _.Handle).ToArray()), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a gray profile.
        /// </summary>
        /// <param name="whitePoint">The white point of the gray device or color space.</param>
        /// <param name="transferFunction">A tone curve describing the device or color space gamma.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile CreateGray(in CIExyY whitePoint, ToneCurve transferFunction)
        {
            return new Profile(Interop.CreateGray(whitePoint, transferFunction.Handle));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a gray profile in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="whitePoint">The white point of the gray device or color space.</param>
        /// <param name="transferFunction">A tone curve describing the device or color space gamma.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateGray(Context context, in CIExyY whitePoint, ToneCurve transferFunction)
        {
            return new Profile(Interop.CreateGray(context?.Handle ?? IntPtr.Zero, whitePoint, transferFunction.Handle), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a device link profile.
        /// </summary>
        /// <param name="space">The color space.</param>
        /// <param name="transferFunction">An array of tone curves describing the device or color space linearisation.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile CreateLinearizationDeviceLink(ColorSpaceSignature space, ToneCurve[] transferFunction)
        {
            return new Profile(Interop.CreateLinearizationDeviceLink(Convert.ToUInt32(space), transferFunction.Select(_ => _.Handle).ToArray()));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a device link profile in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="space">The color space.</param>
        /// <param name="transferFunction">An array of tone curves describing the device or color space linearisation.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateLinearizationDeviceLink(Context context, ColorSpaceSignature space, ToneCurve[] transferFunction)
        {
            return new Profile(Interop.CreateLinearizationDeviceLink(context?.Handle ?? IntPtr.Zero, Convert.ToUInt32(space),
                    transferFunction.Select(_ => _.Handle).ToArray()), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a device link profile operating
        /// in CMYK for ink limiting.
        /// </summary>
        /// <param name="space">The color space.</param>
        /// <param name="limit">The amount of ink limiting in % in the range 0..400%.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Only <see cref="ColorSpaceSignature.CmykData"/> is supported.
        /// </para>
        /// <para>
        /// Creates the instance in the global context.
        /// </para>
        /// </remarks>
        public static Profile CreateInkLimitingDeviceLink(ColorSpaceSignature space, double limit)
        {
            return new Profile(Interop.CreateInkLimitingDeviceLink(Convert.ToUInt32(space), limit));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a device link profile operating
        /// in CMYK for ink limiting in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="space">The color space.</param>
        /// <param name="limit">The amount of ink limiting in % in the range 0..400%.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Only <see cref="ColorSpaceSignature.CmykData"/> is supported.
        /// </para>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Profile CreateInkLimitingDeviceLink(Context context, ColorSpaceSignature space, double limit)
        {
            return new Profile(Interop.CreateInkLimitingDeviceLink(context?.Handle ?? IntPtr.Zero, Convert.ToUInt32(space), limit), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a device link profile from a given color transform.
        /// </summary>
        /// <param name="transform">A transform.</param>
        /// <param name="version">The target device link version number in the range 1..4.3.</param>
        /// <param name="flags">Bit-wise combination of flags.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the same context as the transform.
        /// </remarks>
        public static Profile CreateDeviceLink(Transform transform, double version, CmsFlags flags)
        {
            return new Profile(Interop.Transform2DeviceLink(transform.Handle, version, Convert.ToUInt32(flags)));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a Lab → Lab identity v2 profile.
        /// </summary>
        /// <param name="whitePoint">Lab reference white.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile CreateLab2(in CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab2(whitePoint));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a Lab → Lab identity v2 profile
        /// in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="whitePoint">Lab reference white.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateLab2(Context context, in CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab2(context?.Handle ?? IntPtr.Zero, whitePoint), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a Lab → Lab identity v4 profile.
        /// </summary>
        /// <param name="whitePoint">Lab reference white.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile CreateLab4(in CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab4(whitePoint));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a Lab → Lab identity v4 profile
        /// in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="whitePoint">Lab reference white.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateLab4(Context context, in CIExyY whitePoint)
        {
            return new Profile(Interop.CreateLab4(context?.Handle ?? IntPtr.Zero, whitePoint), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a XYZ → XYZ identity v4 profile
        /// in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Uses the D50 white point in absolute colorimetric intent.
        /// </para>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Profile CreateXYZ(Context context = null)
        {
            return new Profile(Interop.CreateXYZ(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for the sRGB color space
        /// int the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// The sRGB white point is D65.
        /// </para>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Profile Create_sRGB(Context context = null)
        {
            return new Profile(Interop.Create_sRGB(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a null profile in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateNull(Context context = null)
        {
            return new Profile(Interop.CreateNull(context?.Handle ?? IntPtr.Zero), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for an abstract device link
        /// profile operating in Lab for brightness/contrast/hue/saturation and white point
        /// translation.
        /// </summary>
        /// <param name="nLutPoints">Number of LUT points.</param>
        /// <param name="bright">Brightness increment, can be negative.</param>
        /// <param name="contrast">Contrast increment, can be negative.</param>
        /// <param name="hue">Hue displacement in degrees.</param>
        /// <param name="saturation">Saturation increment, can be negative.</param>
        /// <param name="tempSrc">Source white point temperature in °K.</param>
        /// <param name="tempDest">Destination white point temparature in °K.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile CreateBCHSWabstract(int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return new Profile(Interop.CreateBCHSWabstract(nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for an abstract device link
        /// profile operating in Lab for brightness/contrast/hue/saturation and white point
        /// translation.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="nLutPoints">Number of lookup table points.</param>
        /// <param name="bright">Brightness increment, can be negative.</param>
        /// <param name="contrast">Contrast increment, can be negative.</param>
        /// <param name="hue">Hue displacement in degrees.</param>
        /// <param name="saturation">Saturation increment, can be negative.</param>
        /// <param name="tempSrc">Source white point temperature in °K.</param>
        /// <param name="tempDest">Destination white point temparature in °K.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile CreateBCHSWabstract(Context context, int nLutPoints, double bright, double contrast,
                double hue, double saturation, int tempSrc, int tempDest)
        {
            return new Profile(Interop.CreateBCHSWabstract(context?.Handle ?? IntPtr.Zero, nLutPoints, bright, contrast, hue, saturation, tempSrc, tempDest), context);
        }
        #endregion

        #region Access functions
        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a file-based ICC profile.
        /// </summary>
        /// <param name="filepath">The full path to the file.</param>
        /// <param name="access">"r" for normal operation, or "w" to create the file.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile Open(string filepath, string access)
        {
            return new Profile(Interop.OpenProfile(filepath, access));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class for a file-based ICC profile
        /// in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="filepath">The full path to the file.</param>
        /// <param name="access">"r" for normal operation, or "w" to create the file.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile Open(Context context, string filepath, string access)
        {
            return new Profile(Interop.OpenProfile(context?.Handle ?? IntPtr.Zero, filepath, access), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class from the supplied
        /// memory block.
        /// </summary>
        /// <param name="memory">A block of contiguous memory containing the entire ICC profile.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context.
        /// </remarks>
        public static Profile Open(byte[] memory)
        {
            return new Profile(Interop.OpenProfile(memory));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class from the supplied
        /// memory block in the given context.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="memory">A block of contiguous memory containing the entire ICC profile.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile Open(Context context, byte[] memory)
        {
            return new Profile(Interop.OpenProfile(context?.Handle ?? IntPtr.Zero, memory), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class where profile access is described
        /// by an <see cref="IOHandler"/>.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="iohandler">An I/O handler.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile Open(Context context, IOHandler iohandler)
        {
            return new Profile(Interop.OpenProfile(context?.Handle ?? IntPtr.Zero, iohandler?.Handle ?? IntPtr.Zero), context, iohandler);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Profile"/> class where profile access is described
        /// by an <see cref="IOHandler"/> that also allows write access to be specified.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="iohandler">An I/O handler.</param>
        /// <param name="writeable">true to grant write access, or false to open the <see cref="IOHandler"/> as read-only.</param>
        /// <returns>A new <see cref="Profile"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Profile Open(Context context, IOHandler iohandler, bool writeable)
        {
            return new Profile(Interop.OpenProfile(context?.Handle ?? IntPtr.Zero, iohandler?.Handle ?? IntPtr.Zero, writeable ? 1 : 0), context, iohandler);
        }

        /// <summary>
        /// Saves the profile to file.
        /// </summary>
        /// <param name="filepath">The full path to the file.</param>
        /// <returns>true if saved successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool Save(string filepath)
        {
            EnsureNotDisposed();

            return 0 != Interop.SaveProfile(_handle, filepath);
        }

        /// <summary>
        /// Saves the profile to a contiguous block of memory.
        /// </summary>
        /// <param name="memory">An array of bytes large enough to hold the profile, or null to calculate required size.</param>
        /// <param name="bytesNeeded">Returns the number of bytes written.</param>
        /// <returns>true if successful, othrwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        /// <remarks>
        /// The calculated size ignores the zero ('\0') terminator saved as the last byte in the memory block, so
        /// add 1 to ensure that the memory allocated to save the profile is sufficiently sized.
        /// </remarks>
        public bool Save(byte[] memory, out uint bytesNeeded)
        {
            EnsureNotDisposed();

            return 0 != Interop.SaveProfile(_handle, memory, out bytesNeeded);
        }

        /// <summary>
        /// Saves the profile to the given <see cref="IOHandler"/>.
        /// </summary>
        /// <param name="iohandler">An I/O handler or null to calculate size only.</param>
        /// <returns>The number of bytes used to save the profile, or zero on error.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public uint Save(IOHandler iohandler)
        {
            EnsureNotDisposed();

            return Interop.SaveProfile(_handle, iohandler?.Handle ?? IntPtr.Zero);
        }
        #endregion

        #region Obtain localized information
        /// <summary>
        /// Gets a Unicode (16 bit) string containing the requested information from the profile for a
        /// given language and country code.
        /// </summary>
        /// <param name="info">The information to be obtained.</param>
        /// <param name="languageCode">The ISO 639-1 language code.</param>
        /// <param name="countryCode">The ISO 3166-1 country code.</param>
        /// <returns>A string containing the requested information, or null if not found.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public string GetProfileInfo(InfoType info, string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.GetProfileInfo(_handle, Convert.ToUInt32(info), languageCode, countryCode);
        }

        /// <summary>
        /// Gets an ASCII (7 bit) string containing the requested information from the profile for a
        /// given language and country code.
        /// </summary>
        /// <param name="info">The information to be obtained.</param>
        /// <param name="languageCode">The ISO 639-1 language code.</param>
        /// <param name="countryCode">The ISO 3166-1 country code.</param>
        /// <returns>A string containing the requested information, or null if not found.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public string GetProfileInfoASCII(InfoType info, string languageCode, string countryCode)
        {
            EnsureNotDisposed();

            return Interop.GetProfileInfoASCII(_handle, Convert.ToUInt32(info), languageCode, countryCode);
        }
        #endregion

        #region Feature detection
        /// <summary>
        /// Estimates the black point of the profile.
        /// </summary>
        /// <param name="blackPoint">Returns the black point.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">Reserved (unused). Set to <see cref="CmsFlags.None"/>.</param>
        /// <returns>true if estimated successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool DetectBlackPoint(out CIEXYZ blackPoint, Intent intent, CmsFlags flags = CmsFlags.None)
        {
            EnsureNotDisposed();

            return Interop.DetectBlackPoint(_handle, out blackPoint, Convert.ToUInt32(intent), Convert.ToUInt32(flags)) != 0;
        }

        /// <summary>
        /// Estimates the black point of the profile by using the ICC black point compensation algorithm.
        /// </summary>
        /// <param name="blackPoint">Returns the black point.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">Reserved (unused). Set to <see cref="CmsFlags.None"/>.</param>
        /// <returns>true if estimated successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool DetectDestinationBlackPoint(out CIEXYZ blackPoint, Intent intent, CmsFlags flags = CmsFlags.None)
        {
            EnsureNotDisposed();

            return Interop.DetectDestinationBlackPoint(_handle, out blackPoint, Convert.ToUInt32(intent), Convert.ToUInt32(flags)) != 0;
        }
        #endregion

        #region Access profile header
        /// <summary>
        /// Gets the date and time when the profile was created.
        /// </summary>
        /// <param name="created">Returns the date and time the profile was created.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool GetHeaderCreationDateTime(out DateTime created)
        {
            EnsureNotDisposed();

            return Interop.GetHeaderCreationDateTime(_handle, out created) != 0;
        }
        #endregion

        #region Info on profile implementation
        /// <summary>
        /// Determines whether the profile contains a CLUT for the given intent and direction.
        /// </summary>
        /// <param name="intent">The intent.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>true if a CLUT is present, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool IsCLUT(Intent intent, UsedDirection direction)
        {
            EnsureNotDisposed();

            return Interop.IsCLUT(_handle, Convert.ToUInt32(intent), Convert.ToUInt32(direction)) != 0;
        }

        /// <summary>
        /// Detects whether the profile works in linear (gamma 1.0) space.
        /// </summary>
        /// <param name="threshold">The standard deviation above which gamma is returned.</param>
        /// <returns>Estimated gamma of the RGB space on success, -1 on error.</returns>
        /// <remarks>
        /// <para>
        /// Only RGB profiles, and only those that can be got in both directions.
        /// </para>
        /// <para>
        /// Requires Little CMS version 2.13 or later.
        /// </para>
        /// </remarks>
        public double DetectRGBGamma(double threshold)
        {
            EnsureNotDisposed();

            return Interop.DetectRGBProfileGamma(_handle, threshold);
        }
        #endregion

        #region Access tags
        /// <summary>
        /// Gets the tag for the given index.
        /// </summary>
        /// <param name="n">The zero-based index of the tag.</param>
        /// <returns>The tag.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public TagSignature GetTag(uint n)
        {
            EnsureNotDisposed();

            return (TagSignature)Interop.GetTagSignature(_handle, n);
        }

        /// <summary>
        /// Gets a value indicating whether the tag is present in the profile.
        /// </summary>
        /// <param name="tag">The tag signature.</param>
        /// <returns>true if the tag is present, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool HasTag(TagSignature tag)
        {
            EnsureNotDisposed();

            return Interop.IsTag(_handle, Convert.ToUInt32(tag)) != 0;
        }

        /// <summary>
        /// Gets a raw pointer to a tag with the given tag signature.
        /// </summary>
        /// <param name="tag">The tag signature.</param>
        /// <returns>A pointer to the tag, or <see cref="IntPtr.Zero"/> if not found.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public IntPtr ReadTag(TagSignature tag)
        {
            EnsureNotDisposed();

            return Interop.ReadTag(_handle, Convert.ToUInt32(tag));
        }

        /// <summary>
        /// Gets a new instance of type <typeparamref name="T"/> that represents
        /// the tag with the given tag signature.
        /// </summary>
        /// <typeparam name="T">The type for the given tag.</typeparam>
        /// <param name="tag">The tag signature.</param>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance as no tag was found with the given tag signature.
        /// </exception>
        /// <exception cref="MissingMethodException">
        /// Type <typeparamref name="T"/> does not contain a non-public static method with signature
        /// '<typeparamref name="T"/> FromHandle(<see cref="IntPtr"/>)'.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public T ReadTag<T>(TagSignature tag)
        {
            EnsureNotDisposed();

            IntPtr ptr = ReadTag(tag);
            Helper.CheckCreated<T>(ptr);

            Type t = typeof(T);
            MethodInfo method = t.GetMethod("FromHandle", BindingFlags.NonPublic | BindingFlags.Static,
                    null, new Type[] { typeof(IntPtr) }, null);
            if (method is null) throw new MissingMethodException(nameof(T), "FromHandle(IntPtr)");

            return (T)method.Invoke(null, new object[] { ptr });
        }

        /// <summary>
        /// Writes an object identified from the wrappered handle allocated by Little CMS
        /// to the profile using the given tag signature.
        /// </summary>
        /// <param name="tag">The tag signature.</param>
        /// <param name="handle">The handle to the object allocated by Little CMS.</param>
        /// <returns>true if successfully written, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool WriteTag(TagSignature tag, IntPtr handle)
        {
            EnsureNotDisposed();

            return Interop.WriteTag(_handle, Convert.ToUInt32(tag), handle) != 0;
        }

        /// <summary>
        /// Writes a structure to the profile using the given tag signature.
        /// </summary>
        /// <typeparam name="T">The structure type.</typeparam>
        /// <param name="tag">The tag signature.</param>
        /// <param name="data">The structure.</param>
        /// <returns>true if successfully written, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        /// <remarks>
        /// The layout of the structure must correspond with the definitions in Little CMS.
        /// </remarks>
        public bool WriteTag<T>(TagSignature tag, in T data)
            where T: struct
        {
            EnsureNotDisposed();

            int size = Marshal.SizeOf<T>();
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(data, ptr, false);
            try
            {
                return WriteTag(tag, ptr);
            }
            finally
            {
                Marshal.DestroyStructure<T>(ptr);
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Writes an <see cref="ICCData"/> instance to the profile using the given tag signature.
        /// </summary>
        /// <param name="tag">The tag signature.</param>
        /// <param name="data">The ICC data.</param>
        /// <returns>true if successfully written, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool WriteTag(TagSignature tag, ICCData data)
        {
            return WriteTag<ICCData>(tag, data);
        }

        /// <summary>
        /// Writes an <see cref="UcrBg"/> instance to the profile using the given tag signature.
        /// </summary>
        /// <param name="tag">The tag signature.</param>
        /// <param name="ucrBg">The under color removal and black generation instance.</param>
        /// <returns>true if successfully written, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool WriteTag(TagSignature tag, UcrBg ucrBg)
        {
            return WriteTag<UcrBg>(tag, ucrBg);
        }

        /// <summary>
        /// Writes a <see cref="VideoCardGamma"/> instance to the profile using the given tag signature.
        /// </summary>
        /// <param name="tag">The tag signature.</param>
        /// <param name="vcgt">The video card gamma table instance.</param>
        /// <returns>true if successfully written, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool WriteTag(TagSignature tag, VideoCardGamma vcgt)
        {
            return WriteTag<VideoCardGamma>(tag, vcgt);
        }

        private bool WriteTag<T>(TagSignature tag, T t)
            where T: class
        {
            EnsureNotDisposed();

            MethodInfo method = typeof(T).GetMethod("ToHandle", BindingFlags.NonPublic | BindingFlags.Instance,
                    null, new Type[] { }, null);
            if (method is null) throw new MissingMethodException(nameof(T), "ToHandle()");

            IntPtr ptr = (IntPtr)method.Invoke(t, new object[] { });
            try
            {
                return WriteTag(tag, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Creates a directory entry on tag <paramref name="tag"/> that points to the same location
        /// as tag <paramref name="dest"/> to collapse several tag entries to the same block in the
        /// profile.
        /// </summary>
        /// <param name="tag">The tag signature of the linking tag.</param>
        /// <param name="dest">The tag signature of the linked tag.</param>
        /// <returns>true if linked successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool LinkTag(TagSignature tag, TagSignature dest)
        {
            EnsureNotDisposed();

            return Interop.LinkTag(_handle, Convert.ToUInt32(tag), Convert.ToUInt32(dest)) != 0;
        }

        /// <summary>
        /// Gets the tag signature of the tag linked to the given tag.
        /// </summary>
        /// <param name="tag">The tag signature of the linking tag.</param>
        /// <returns>The tag signature of the linked tag, or (<see cref="TagSignature"/>)0 if not linked.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public TagSignature TagLinkedTo(TagSignature tag)
        {
            EnsureNotDisposed();

            return (TagSignature)Interop.TagLinkedTo(_handle, Convert.ToUInt32(tag));
        }
        #endregion

        #region Intents
        /// <summary>
        /// Gets a value indicating whether the given intent is implemented in the profile
        /// for the supplied direction.
        /// </summary>
        /// <param name="intent">The intent.</param>
        /// <param name="usedDirection">The direction.</param>
        /// <returns>true if implemented, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool IsIntentSupported(Intent intent, UsedDirection usedDirection)
        {
            EnsureNotDisposed();

            return Interop.IsIntentSupported(_handle, Convert.ToUInt32(intent), Convert.ToUInt32(usedDirection)) != 0;
        }
        #endregion

        #region MD5 message digest
        /// <summary>
        /// Calculates and sets the profile ID in the header of the profile.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public bool ComputeMD5()
        {
            EnsureNotDisposed();

            return Interop.MD5ComputeID(_handle) != 0;
        }
        #endregion

        #region PostScript generation
        /// <summary>
        /// Creates a PostScript color resource.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="type">The PostScript resource type to be created.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="handler">An I/O handler.</param>
        /// <returns>The size of the resource in bytes, or 0 on error.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public uint GetPostScriptColorResource(Context context, PostScriptResourceType type, Intent intent, CmsFlags flags, IOHandler handler)
        {
            EnsureNotDisposed();

            return Interop.GetPostScriptColorResource(_handle, context?.ID ?? IntPtr.Zero, Convert.ToUInt32(type),
                    Convert.ToUInt32(intent), Convert.ToUInt32(flags), handler?.Handle ?? IntPtr.Zero);
        }

        /// <summary>
        /// Creates and returns a contiguous block of memory containing a PostScript color space array.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>An array of bytes containing the PostScript color space array, or null on error.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public byte[] GetPostScriptColorSpaceArray(Context context, Intent intent, CmsFlags flags)
        {
            EnsureNotDisposed();

            return Interop.GetPostScriptCSA(_handle, context?.ID ?? IntPtr.Zero, Convert.ToUInt32(intent), Convert.ToUInt32(flags));
        }

        /// <summary>
        /// Creates and returns a contiguous block of memory containing a PostScript color rendering dictionary.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>An array of bytes containing the PostScript color rendering dictionary, or null on error.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public byte[] GetPostScriptColorRenderingDictionary(Context context, Intent intent, CmsFlags flags)
        {
            EnsureNotDisposed();

            return Interop.GetPostScriptCRD(_handle, context?.ID ?? IntPtr.Zero, Convert.ToUInt32(intent), Convert.ToUInt32(flags));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }

        /// <summary>
        /// Gets or sets the color space used by the profile.
        /// </summary>
        public ColorSpaceSignature ColorSpace
        {
            get { return (ColorSpaceSignature)Interop.GetColorSpace(_handle); }
            set { Interop.SetColorSpace(_handle, Convert.ToUInt32(value)); }
        }

        /// <summary>
        /// Gets or sets the profile connection space used by the profile.
        /// </summary>
        public ColorSpaceSignature PCS
        {
            get { return (ColorSpaceSignature)Interop.GetPCS(_handle); }
            set { Interop.SetPCS(_handle, Convert.ToUInt32(value)); }
        }

        /// <summary>
        /// Estimates the percentage total area coverage (total dot percentage) for the profile.
        /// </summary>
        /// <remarks>
        /// Non-output profiles yield a value of 0%.
        /// </remarks>
        public double TotalAreaCoverage => Interop.DetectTAC(_handle);

        /// <summary>
        /// Gets or sets the device class signature in the header of the profile.
        /// </summary>
        public ProfileClassSignature DeviceClass
        {
            get { return (ProfileClassSignature)Interop.GetDeviceClass(_handle); }
            set { Interop.SetDeviceClass(_handle, Convert.ToUInt32(value)); }
        }

        /// <summary>
        /// Gets or sets the flags in the header of the profile.
        /// </summary>
        public uint HeaderFlags
        {
            get { return Interop.GetHeaderFlags(_handle); }
            set { Interop.SetHeaderFlags(_handle, value); }
        }

        /// <summary>
        /// Gets or sets the signature of the device manufacturer in the header of the profile.
        /// </summary>
        public uint HeaderManufacturer
        {
            get { return Interop.GetHeaderManufacturer(_handle); }
            set { Interop.SetHeaderManufacturer(_handle, value); }
        }

        /// <summary>
        /// Gets or sets the signature of the device model in the header of the profile.
        /// </summary>
        public uint HeaderModel
        {
            get { return Interop.GetHeaderModel(_handle); }
            set { Interop.SetHeaderModel(_handle, value); }
        }

        /// <summary>
        /// Gets or sets the attributes unique to the particular device setup for which
        /// the profile is applicable in the header of the profile.
        /// </summary>
        public DeviceAttributes HeaderAttributes
        {
            get { return (DeviceAttributes)Interop.GetHeaderAttributes(_handle); }
            set { Interop.SetHeaderAttributes(_handle, (ulong)value); }
        }

        /// <summary>
        /// Gets or sets the ICC version in the header of the profile in floating point format.
        /// </summary>
        public double Version
        {
            get { return Interop.GetProfileVersion(_handle); }
            set { Interop.SetProfileVersion(_handle, value); }
        }

        /// <summary>
        /// Gets or sets the ICC version in the format as stored in the header of the profile.
        /// </summary>
        public uint EncodedICCVersion
        {
            get { return Interop.GetEncodedICCVersion(_handle); }
            set { Interop.SetEncodedICCVersion(_handle, value); }
        }

        /// <summary>
        /// Gets a value indicating whether a matrix shaper is present in the profile.
        /// </summary>
        public bool IsMatrixShaper => Interop.IsMatrixShaper(_handle) != 0;

        /// <summary>
        /// Gets the number of tags in the profile.
        /// </summary>
        public int TagCount => Interop.GetTagCount(_handle);

        /// <summary>
        /// Gets or sets the rendering intent in the header of the profile.
        /// </summary>
        public Intent HeaderRenderingIntent
        {
            get { return (Intent)Interop.GetHeaderRenderingIntent(_handle); }
            set { Interop.SetHeaderRenderingIntent(_handle, Convert.ToUInt32(value)); }
        }

        /// <summary>
        /// Gets or sets the profile ID in the header of the profile. 
        /// </summary>
        /// <value>An array of 16 bytes defining the computed MD5 value for the profile.</value>
        /// <remarks>
        /// <para>
        /// The profile ID shall be calculated using the MD5 fingerprinting method as defined in Internet RFC 1321.
        /// </para>
        /// <para>
        /// The profile ID can be calculated and set using <see cref="ComputeMD5"/>.
        /// </para>
        /// </remarks>
        public byte[] HeaderProfileID
        {
            get
            {
                byte[] profileID = new byte[16];
                Interop.GetHeaderProfileID(_handle, profileID);
                return profileID;
            }
            set
            {
                if (value?.Length != 16) throw new ArgumentException($"'{nameof(value)}' array size must equal 16.");
                Interop.SetHeaderProfileID(_handle, value);
            }
        }

        /// <summary>
        /// Gets the I/O handler used by the profile.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public IOHandler IOHandler
        {
            get
            {
                EnsureNotDisposed();

                if (_iohandler is null)
                {
                    _iohandler = new IOHandler(Interop.GetProfileIOHandler(_handle), Context, isOwner: false);
                }
                return _iohandler;
            }
            private set
            {
                _iohandler = value;
                if (!(_iohandler is null)) _iohandler.IsOwner = false; // take ownership to avoid double free()
            }
        }
        private IOHandler _iohandler;
        #endregion

        #region IDisposable Support
        /// <summary>
        /// Gets a value indicating whether the instance has been disposed.
        /// </summary>
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
                IOHandler = null; // profile closure also closes i/o handler
                Context = null;
            }
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~Profile()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        internal IntPtr Handle => _handle;
    }
}
