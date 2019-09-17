using lcmsNET.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    public enum StageLoc : int
    {
        At_Begin = 0,
        At_End = 1
    }

    public sealed class Pipeline : IEnumerable<Stage>, IDisposable
    {
        private IntPtr _handle;

        internal Pipeline(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<Profile>(handle);

            _handle = handle;
            Context = context;
        }

        public static Pipeline Create(Context context, uint inputChannels, uint outputChannels)
        {
            return new Pipeline(Interop.PipelineAlloc(context?.Handle ?? IntPtr.Zero, inputChannels, outputChannels), context);
        }

        public Pipeline Duplicate()
        {
            EnsureNotDisposed();

            return new Pipeline(Interop.PipelineDup(_handle), Context);
        }

        public bool Append(Pipeline other)
        {
            EnsureNotDisposed();

            return Interop.PipelineCat(_handle, other.Handle) != 0;
        }

        public float[] Evaluate(float[] values)
        {
            if (!(values?.Length > 0)) throw new ArgumentException($"'{nameof(values)}' array size must be greater than 0.");

            EnsureNotDisposed();

            float[] result = new float[values.Length];
            Interop.PipelineEvalFloat(_handle, values, result);
            return result;
        }

        public float[] EvaluateReverse(float[] values, float[] hint, out bool success)
        {
            if (!(values?.Length > 0)) throw new ArgumentException($"'{nameof(values)}' array size must be greater than 0.");

            EnsureNotDisposed();

            float[] result = new float[values.Length];
            success = Interop.PipelineEvalReverseFloat(_handle, values, result, hint) != 0;
            return result;
        }

        public ushort[] Evaluate(ushort[] values)
        {
            if (!(values?.Length > 0)) throw new ArgumentException($"'{nameof(values)}' array size must be greater than 0.");

            EnsureNotDisposed();

            ushort[] result = new ushort[values.Length];
            Interop.PipelineEval16(_handle, values, result);
            return result;
        }

        public bool Insert(Stage stage, StageLoc location)
        {
            EnsureNotDisposed();

            bool inserted = Interop.PipelineInsertStage(_handle, stage.Handle, Convert.ToInt32(location)) != 0;
            if (inserted) stage.Release();
            return inserted;
        }

        public Stage Unlink(StageLoc location)
        {
            EnsureNotDisposed();

            IntPtr ptr = IntPtr.Zero;
            Interop.PipelineUnlinkStage(_handle, Convert.ToInt32(location), ref ptr);
            return (ptr != IntPtr.Zero) ? new Stage(ptr) : null;
        }

        public void UnlinkAndDispose(StageLoc location)
        {
            EnsureNotDisposed();

            Interop.PipelineUnlinkStage(_handle, Convert.ToInt32(location));
        }

        public bool SetAs8BitsFlag(bool on)
        {
            EnsureNotDisposed();

            return Interop.PipelineSetSaveAs8BitsFlag(_handle, on ? 1 : 0) != 0;
        }

        public Context Context { get; private set; }

        public uint InputChannels => Interop.PipelineInputChannels(_handle);

        public uint OutputChannels => Interop.PipelineOutputChannels(_handle);

        public uint StageCount => Interop.PipelineStageCount(_handle);

        #region IEnumerable<Stage> Support
        public IEnumerator<Stage> GetEnumerator()
        {
            EnsureNotDisposed();

            return new StageEnumerator(_handle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class StageEnumerator : IEnumerator<Stage>
        {
            public StageEnumerator(IntPtr handle)
            {
                Handle = handle;
                First = GetFirst(handle);
                Last = GetLast(handle);
                Location = Position.Before;
            }
            private IntPtr Handle { get; set; }

            public Stage Current
            {
                get
                {
                    if (_current is null)
                    {
                        throw new InvalidOperationException();
                    }
                    return _current;
                }
            }
            private Stage _current = null;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (Location == Position.Before)
                {
                    if (First == IntPtr.Zero)
                    {
                        Location = Position.After;
                    }
                    else
                    {
                        _current = Stage.Copy(First);
                        Location = Position.During;
                    }
                }
                else if (Location == Position.During)
                {
                    if (_current.Handle == Last)
                    {
                        _current = null;
                        Location = Position.After;
                    }
                    else
                    {
                        _current = Stage.Copy(Interop.StageNext(_current.Handle));
                    }
                }
                return Location == Position.During;
            }

            public void Reset()
            {
                _current = null;
                Location = Position.Before;
            }

            private IntPtr First { get; set; }
            private IntPtr GetFirst(IntPtr handle)
            {
                return Interop.PipelineGetPtrToFirstStage(handle);
            }

            private IntPtr Last { get; set; }
            private IntPtr GetLast(IntPtr handle)
            {
                return Interop.PipelineGetPtrToLastStage(handle);
            }

            private enum Position { Before, During, After };
            private Position Location { get; set; }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                // Do nothing
            }

            ~StageEnumerator()
            {
                Dispose(false);
            }
        }
        #endregion

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
                Interop.PipelineFree(handle);
                Context = null;
            }
        }

        ~Pipeline()
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
