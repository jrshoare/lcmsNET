using System;
using System.Diagnostics;
using System.IO;
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
            Debug.Assert(handle != IntPtr.Zero);
            _handle = handle;
            Context = context;
        }

        public static Transform Create(Profile input, int inputFormat, Profile output, int outputFormat,
                CmsIntent intent, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateTransform(input.Handle, inputFormat,
                    output.Handle, outputFormat, Convert.ToInt32(intent), Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle);
        }

        public static Transform Create(Context context, Profile input, int inputFormat, Profile output, int outputFormat,
                CmsIntent intent, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateTransform(context.Handle, input.Handle, inputFormat,
                    output.Handle, outputFormat, Convert.ToInt32(intent), Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle, context);
        }

        public static Transform Create(Profile input, int inputFormat, Profile output, int outputFormat,
                Profile proofing, CmsIntent intent, CmsIntent proofingIntent, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateTransform(input.Handle, inputFormat,
                    output.Handle, outputFormat, proofing.Handle, Convert.ToInt32(intent),
                    Convert.ToInt32(proofingIntent), Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle);
        }

        public static Transform Create(Context context, Profile input, int inputFormat, Profile output, int outputFormat,
                Profile proofing, CmsIntent intent, CmsIntent proofingIntent, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateTransform(context.Handle, input.Handle, inputFormat,
                    output.Handle, outputFormat, proofing.Handle, Convert.ToInt32(intent),
                    Convert.ToInt32(proofingIntent), Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle, context);
        }

        public static Transform Create(Profile[] profiles, int inputFormat, int outputFormat,
                CmsIntent intent, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateMultiprofileTransform(profiles.Select(_ => _.Handle).ToArray(),
                    inputFormat, outputFormat, Convert.ToInt32(intent), Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle);
        }

        public static Transform Create(Context context, Profile[] profiles, int inputFormat, int outputFormat,
                CmsIntent intent, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateMultiprofileTransform(context.Handle,
                    profiles.Select(_ => _.Handle).ToArray(),
                    inputFormat, outputFormat, Convert.ToInt32(intent), Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle, context);
        }

        public static Transform Create(Context context, Profile[] profiles, bool[] bpc, CmsIntent[] intents,
                double[] adaptationStates, Profile gamut, int gamutPCSPosition, int inputFormat, int outputFormat, CmsFlags flags)
        {
            IntPtr handle = Interop.CreateExtendedTransform(context.Handle,
                    profiles.Select(_ => _.Handle).ToArray(),
                    bpc.Select(_ => _ ? 1 : 0).ToArray(),
                    intents.Select(_ => Convert.ToInt32(_)).ToArray(), adaptationStates, gamut?.Handle ?? IntPtr.Zero,
                    gamutPCSPosition, inputFormat, outputFormat, Convert.ToInt32(flags));
            if (handle == IntPtr.Zero)
            {
                throw new IOException();
            }
            return new Transform(handle, context);
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

        public Context Context { get; private set; }

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
