using lcmsNET.Impl;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public sealed class Stage : IDisposable
    {
        private IntPtr _handle;

        internal Stage(IntPtr handle, Context context = null, bool isOwner = true)
        {
            Helper.CheckCreated<Stage>(handle);

            _handle = handle;
            Context = context;
            IsOwner = isOwner;
        }

        internal static Stage CopyRef(IntPtr handle, Context context = null)
        {
            return new Stage(handle, context, isOwner: false);
        }

        internal void Release()
        {
            Interlocked.Exchange(ref _handle, IntPtr.Zero);
            Context = null;
        }

        public static Stage Create(Context context, uint nChannels)
        {
            return new Stage(Interop.StageAllocIdentity(context?.Handle ?? IntPtr.Zero, nChannels), context);
        }

        public static Stage Create(Context context, uint nChannels, ToneCurve[] curves)
        {
            return new Stage(Interop.StageAllocToneCurves(context?.Handle ?? IntPtr.Zero, nChannels, curves.Select(_ => _.Handle).ToArray()), context);
        }

        public static Stage Create(Context context, double[,] matrix, double[] offset)
        {
            if (!(offset is null) && offset.Length != (matrix?.GetUpperBound(1) + 1))
            {
                throw new ArgumentException($"'{nameof(offset)}' array size must equal size of '{nameof(matrix)}' second dimension.");
            }

            return new Stage(Interop.StageAllocMatrix(context?.Handle ?? IntPtr.Zero, matrix, offset), context);
        }

        public static Stage Create(Context context, uint nGridPoints, uint inputChannels, uint outputChannels, ushort[] table)
        {
            return new Stage(Interop.StageAllocCLut16bit(context?.Handle ?? IntPtr.Zero, nGridPoints, inputChannels, outputChannels, table), context);
        }

        public static Stage Create(Context context, uint nGridPoints, uint inputChannels, uint outputChannels, float[] table)
        {
            return new Stage(Interop.StageAllocCLutFloat(context?.Handle ?? IntPtr.Zero, nGridPoints, inputChannels, outputChannels, table), context);
        }

        public static Stage Create(Context context, uint[] clutPoints, uint outputChannels, ushort[] table)
        {
            if (!(clutPoints?.Length > 0)) throw new ArgumentException($"'{nameof(clutPoints)}' array size must be greater than 0.");

            return new Stage(Interop.StageAllocCLut16bitGranular(context?.Handle ?? IntPtr.Zero, clutPoints, outputChannels, table), context);
        }

        public static Stage Create(Context context, uint[] clutPoints, uint outputChannels, float[] table)
        {
            if (!(clutPoints?.Length > 0)) throw new ArgumentException($"'{nameof(clutPoints)}' array size must be greater than 0.");

            return new Stage(Interop.StageAllocCLutFloatGranular(context?.Handle ?? IntPtr.Zero, clutPoints, outputChannels, table), context);
        }

        public Stage Duplicate()
        {
            return new Stage(Interop.StageDup(_handle), Context);
        }

        public Context Context { get; private set; }

        public uint InputChannels => Interop.StageInputChannels(_handle);

        public uint OutputChannels => Interop.StageOutputChannels(_handle);

        public StageSignature StageType => (StageSignature)Interop.StageType(_handle);

        #region IDisposable Support
        public bool IsDisposed => _handle == IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureNotDisposed()
        {
            if (_handle == IntPtr.Zero)
            {
                throw new ObjectDisposedException(nameof(Stage));
            }
        }

        private void Dispose(bool disposing)
        {
            if (IsOwner)    // only dispose objects that we own
            {
                var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
                if (handle != IntPtr.Zero)
                {
                    Interop.StageFree(handle);
                    Context = null;
                }
            }
        }

        ~Stage()
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

        private bool IsOwner { get; set; }
    }
}
