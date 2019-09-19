using lcmsNET.Impl;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class Transform : IDisposable
    {
        private IntPtr _handle;

        internal Transform(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<Transform>(handle);

            _handle = handle;
            Context = context;
        }

        public static Transform Create(Profile input, uint inputFormat, Profile output, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(input.Handle, inputFormat,
                    output.Handle, outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)));
        }

        public static Transform Create(Context context, Profile input, uint inputFormat, Profile output, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(context?.Handle ?? IntPtr.Zero, input.Handle, inputFormat,
                    output.Handle, outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)), context);
        }

        public static Transform Create(Profile input, uint inputFormat, Profile output, uint outputFormat,
                Profile proofing, Intent intent, Intent proofingIntent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(input.Handle, inputFormat,
                    output.Handle, outputFormat, proofing.Handle, Convert.ToUInt32(intent),
                    Convert.ToUInt32(proofingIntent), Convert.ToUInt32(flags)));
        }

        public static Transform Create(Context context, Profile input, uint inputFormat, Profile output, uint outputFormat,
                Profile proofing, Intent intent, Intent proofingIntent, CmsFlags flags)
        {
            return new Transform(Interop.CreateTransform(context?.Handle ?? IntPtr.Zero, input.Handle, inputFormat,
                    output.Handle, outputFormat, proofing.Handle, Convert.ToUInt32(intent),
                    Convert.ToUInt32(proofingIntent), Convert.ToUInt32(flags)), context);
        }

        public static Transform Create(Profile[] profiles, uint inputFormat, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateMultiprofileTransform(profiles.Select(_ => _.Handle).ToArray(),
                    inputFormat, outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)));
        }

        public static Transform Create(Context context, Profile[] profiles, uint inputFormat, uint outputFormat,
                Intent intent, CmsFlags flags)
        {
            return new Transform(Interop.CreateMultiprofileTransform(context?.Handle ?? IntPtr.Zero,
                    profiles.Select(_ => _.Handle).ToArray(),
                    inputFormat, outputFormat, Convert.ToUInt32(intent), Convert.ToUInt32(flags)), context);
        }

        public static Transform Create(Context context, Profile[] profiles, bool[] bpc, Intent[] intents,
                double[] adaptationStates, Profile gamut, int gamutPCSPosition, uint inputFormat, uint outputFormat, CmsFlags flags)
        {
            return new Transform(Interop.CreateExtendedTransform(context?.Handle ?? IntPtr.Zero,
                    profiles.Select(_ => _.Handle).ToArray(),
                    bpc.Select(_ => _ ? 1 : 0).ToArray(),
                    intents.Select(_ => Convert.ToUInt32(_)).ToArray(), adaptationStates, gamut?.Handle ?? IntPtr.Zero,
                    gamutPCSPosition, inputFormat, outputFormat, Convert.ToUInt32(flags)), context);
        }

        public void DoTransform(byte[] inputBuffer, byte[] outputBuffer, int pixelCount)
        {
            EnsureNotDisposed();

            Interop.DoTransform(Handle, inputBuffer, outputBuffer, pixelCount);
        }

        public void DoTransform(byte[] inputBuffer, byte[] outputBuffer, int pixelsPerLine, int lineCount,
                int bytesPerLineIn, int bytesPerLineOut, int bytesPerPlaneIn, int bytesPerPlaneOut)
        {
            EnsureNotDisposed();

            Interop.DoTransform(Handle, inputBuffer, outputBuffer, pixelsPerLine, lineCount,
                    bytesPerLineIn, bytesPerLineOut, bytesPerPlaneIn, bytesPerPlaneOut);
        }

        public bool ChangeBuffersFormat(uint inputFormat, uint outputFormat)
        {
            EnsureNotDisposed();

            return Interop.ChangeBuffersFormat(_handle, inputFormat, outputFormat) != 0;
        }

        public Context Context { get; private set; }

        public uint InputFormat => Interop.GetTransformInputFormat(_handle);

        public uint OutputFormat => Interop.GetTransformOutputFormat(_handle);

        public NamedColorList NamedColorList => NamedColorList.CopyRef(Interop.GetNamedColorList(_handle));

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
                Interop.DeleteTransform(handle);
                Context = null;
            }
        }

        ~Transform()
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
