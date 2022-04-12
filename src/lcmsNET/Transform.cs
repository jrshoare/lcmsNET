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

namespace lcmsNET
{
    /// <summary>
    /// Represents a color transform.
    /// </summary>
    public sealed class Transform : CmsHandle<Transform>
    {
        internal Transform(IntPtr handle, Context context = null)
            : base(handle, context, isOwner: true)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class.
        /// </summary>
        /// <param name="input">A profile capable to work in the input direction.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="output">A profile capable to work in the output direction.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_Lab_8"/>.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context.
        /// </para>
        /// </remarks>
        public static Transform Create(Profile input, uint inputFormat, Profile output, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(Helper.GetHandle(input), inputFormat,
                    Helper.GetHandle(output), outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="input">A profile capable to work in the input direction.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="output">A profile capable to work in the output direction.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_Lab_8"/>.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Transform Create(Context context, Profile input, uint inputFormat, Profile output, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(Helper.GetHandle(context), Helper.GetHandle(input),
                    inputFormat, Helper.GetHandle(output), outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class for a proofing transform.
        /// </summary>
        /// <param name="input">A profile capable to work in the input direction.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="output">A profile capable to work in the output direction.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_Lab_8"/>.</param>
        /// <param name="proofing">A proofing profile.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="proofingIntent">The proofing intent.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// To enable proofing and gamut check include <see cref="CmsFlags.SoftProofing"/>|<see cref="CmsFlags.GamutCheck"/>.
        /// </para>
        /// <para>
        /// Creates the instance in the global context.
        /// </para>
        /// </remarks>
        public static Transform Create(Profile input, uint inputFormat, Profile output, uint outputFormat,
                Profile proofing, Intent intent, Intent proofingIntent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(Helper.GetHandle(input), inputFormat,
                    Helper.GetHandle(output), outputFormat, Helper.GetHandle(proofing), Convert.ToUInt32(intent),
                    Convert.ToUInt32(proofingIntent), Convert.ToUInt32(flags)));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class for a proofing transform.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="input">A profile capable to work in the input direction.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="output">A profile capable to work in the output direction.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_Lab_8"/>.</param>
        /// <param name="proofing">A proofing profile.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="proofingIntent">The proofing intent.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// To enable proofing and gamut check include <see cref="CmsFlags.SoftProofing"/>|<see cref="CmsFlags.GamutCheck"/>.
        /// </para>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Transform Create(Context context, Profile input, uint inputFormat, Profile output, uint outputFormat,
                Profile proofing, Intent intent, Intent proofingIntent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(Helper.GetHandle(context), Helper.GetHandle(input),
                    inputFormat, Helper.GetHandle(output), outputFormat, Helper.GetHandle(proofing),
                    Convert.ToUInt32(intent), Convert.ToUInt32(proofingIntent), Convert.ToUInt32(flags)), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class for a multiprofile transform.
        /// </summary>
        /// <param name="profiles">An array of profiles.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_Lab_8"/>.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context.
        /// </para>
        /// </remarks>
        public static Transform Create(Profile[] profiles, uint inputFormat, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateMultiprofileTransform(
                    profiles.Select(_ => Helper.GetHandle(_)).ToArray(),
                    inputFormat, outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class for a multiprofile transform.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="profiles">An array of profiles.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_Lab_8"/>.</param>
        /// <param name="intent">The intent.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Transform Create(Context context, Profile[] profiles, uint inputFormat, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateMultiprofileTransform(Helper.GetHandle(context),
                    profiles.Select(_ => Helper.GetHandle(_)).ToArray(),
                    inputFormat, outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)), context);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Transform"/> class for a multiprofile transform
        /// exposing all parameters for each profile in the chain.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="profiles">An array of profiles.</param>
        /// <param name="bpc">An array of black point compensation states.</param>
        /// <param name="intents">An array of intents.</param>
        /// <param name="adaptationStates">An array of adaptation states.</param>
        /// <param name="gamut">A profile holding gamut information for a gamut check, can be null.</param>
        /// <param name="gamutPCSPosition">Position in the chain of Lab/XYZ PCS to check gamut.</param>
        /// <param name="inputFormat">The input format, e.g., <see cref="Cms.TYPE_RGB_8"/>.</param>
        /// <param name="outputFormat">The output format, e.g. <see cref="Cms.TYPE_XYZ_16"/>.</param>
        /// <param name="flags">The flags to control the process.</param>
        /// <returns>A new <see cref="Transform"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="gamut"/> and <paramref name="gamutPCSPosition"/> are only used if
        /// <paramref name="flags"/> includes <see cref="CmsFlags.GamutCheck"/>.
        /// </para>
        /// <para>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </para>
        /// </remarks>
        public static Transform Create(Context context, Profile[] profiles, bool[] bpc, Intent[] intents,
                double[] adaptationStates, Profile gamut, int gamutPCSPosition, uint inputFormat, uint outputFormat, CmsFlags flags)
        {
            return new Transform(Interop.CreateExtendedTransform(Helper.GetHandle(context),
                    profiles.Select(_ => Helper.GetHandle(_)).ToArray(),
                    bpc.Select(_ => _ ? 1 : 0).ToArray(),
                    intents.Select(_ => Convert.ToUInt32(_)).ToArray(), adaptationStates, Helper.GetHandle(gamut),
                    gamutPCSPosition, inputFormat, outputFormat, Convert.ToUInt32(flags)), context);
        }

        /// <summary>
        /// Translates a bitmap according to the parameters setup when creating the transform.
        /// </summary>
        /// <param name="inputBuffer">An array of bytes containing the input bitmap.</param>
        /// <param name="outputBuffer">An array of bytes to contain the output bitmap.</param>
        /// <param name="pixelCount">The number of pixels to be transformed.</param>
        /// <exception cref="ObjectDisposedException">
        /// The Profile has already been disposed.
        /// </exception>
        public void DoTransform(byte[] inputBuffer, byte[] outputBuffer, int pixelCount)
        {
            EnsureNotClosed();

            Interop.DoTransform(handle, inputBuffer, outputBuffer, pixelCount);
        }

        /// <summary>
        /// Translates a bitmap according to the parameters setup when creating the transform.
        /// </summary>
        /// <param name="inputBuffer">An array of bytes containing the input bitmap.</param>
        /// <param name="outputBuffer">An array of bytes to contain the output bitmap.</param>
        /// <param name="pixelsPerLine">The number of pixels per line; same on input and in output.</param>
        /// <param name="lineCount">The number of lines; same on input as in output.</param>
        /// <param name="bytesPerLineIn">The distance in bytes from one line to the next on the input bitmap.</param>
        /// <param name="bytesPerLineOut">The distance in bytes from one line to the next in the output bitmap.</param>
        /// <param name="bytesPerPlaneIn">The distance in bytes from one plance to the next inside a line on the input bitmap.</param>
        /// <param name="bytesPerPlaneOut">The distance in bytes from one plance to the next inside a line in the output bitmap.</param>
        /// <exception cref="ObjectDisposedException">
        /// The Transform has already been disposed.
        /// </exception>
        /// <remarks>
        /// <para>
        /// <paramref name="bytesPerPlaneIn"/> and <paramref name="bytesPerPlaneOut"/> are only used in planar formats.
        /// </para>
        /// <para>
        /// Requires Little CMS version 2.8 or later.
        /// </para>
        /// </remarks>
        public void DoTransform(byte[] inputBuffer, byte[] outputBuffer, int pixelsPerLine, int lineCount,
                int bytesPerLineIn, int bytesPerLineOut, int bytesPerPlaneIn, int bytesPerPlaneOut)
        {
            EnsureNotClosed();

            Interop.DoTransform(handle, inputBuffer, outputBuffer, pixelsPerLine, lineCount,
                    bytesPerLineIn, bytesPerLineOut, bytesPerPlaneIn, bytesPerPlaneOut);
        }

        /// <summary>
        /// Changes the encoding of buffers in a transform originally created with at least 16 bits of precision.
        /// </summary>
        /// <param name="inputFormat">The input format.</param>
        /// <param name="outputFormat">The output format.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The Transform has already been disposed.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method is provided for backwards compatibility and should be avoided whenever possible.
        /// </para>
        /// <para>
        /// Requires Little CMS version 2.1 or later.
        /// </para>
        /// </remarks>
        public bool ChangeBuffersFormat(uint inputFormat, uint outputFormat)
        {
            EnsureNotClosed();

            return Interop.ChangeBuffersFormat(handle, inputFormat, outputFormat) != 0;
        }

        /// <summary>
        /// Gets the input format of the transform, or 0 if the instance has been disposed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Requires Little CMS version 2.2 or later.
        /// </para>
        /// </remarks>
        public uint InputFormat => Interop.GetTransformInputFormat(handle);

        /// <summary>
        /// Gets the output format of the transform, or 0 if the instance has been disposed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Requires Little CMS version 2.2 or later.
        /// </para>
        /// </remarks>
        public uint OutputFormat => Interop.GetTransformOutputFormat(handle);

        /// <summary>
        /// Gets a named color list from the transform.
        /// </summary>
        public NamedColorList NamedColorList => NamedColorList.CopyRef(Interop.GetNamedColorList(handle));

        /// <summary>
        /// Gets the pointer to the user data associated with the transform.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Requires Little CMS version 2.4 or later.
        /// </para>
        /// </remarks>
        public IntPtr UserData => Interop.GetTransformUserData(handle);

        /// <summary>
        /// Sets the user data associated with the transform.
        /// </summary>
        /// <param name="userData">The pointer to user data to be associated with the transform.</param>
        /// <param name="fn">The delegate that can be used to free the user data.</param>
        /// <exception cref="ObjectDisposedException">
        /// The Transform has already been disposed.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Requires Little CMS version 2.4 or later.
        /// </para>
        /// </remarks>
        public void SetUserData(IntPtr userData, FreeUserData fn)
        {
            EnsureNotClosed();

            Interop.SetTransformUserData(handle, userData, fn);
        }

        /// <summary>
        /// Gets the original flags used when creating the transform.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Requires Little CMS version 2.12 or later.
        /// </para>
        /// </remarks>
        public CmsFlags Flags => (CmsFlags)Interop.GetTransformFlags(handle);

        /// <summary>
        /// Frees the transform handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.DeleteTransform(handle);
            return true;
        }
    }
}
