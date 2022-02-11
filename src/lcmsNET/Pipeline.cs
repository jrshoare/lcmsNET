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
using System.Collections;
using System.Collections.Generic;

namespace lcmsNET
{
    /// <summary>
    /// Defines a location of a <see cref="Stage"/> within a <see cref="Pipeline"/>.
    /// </summary>
    public enum StageLoc : int
    {
        /// <summary>
        /// The beginning of the pipeline.
        /// </summary>
        At_Begin = 0,
        /// <summary>
        /// The end of the pipeline.
        /// </summary>
        At_End = 1
    }

    /// <summary>
    /// Represents an ordered collection of <see cref="Stage"/> instances where each
    /// stage performs a single operation on image data. The output of a first stage
    /// provides the input to the next and so on through the pipeline.
    /// </summary>
    public sealed class Pipeline : TagBase<Pipeline>, IEnumerable<Stage>
    {
        internal Pipeline(IntPtr handle, Context context = null, bool isOwner = true)
            : base(handle, context, isOwner)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Pipeline"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="inputChannels">The number of input channels.</param>
        /// <param name="outputChannels">The number of output channels.</param>
        /// <returns>A new <see cref="Pipeline"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static Pipeline Create(Context context, uint inputChannels, uint outputChannels)
        {
            return new Pipeline(Interop.PipelineAlloc(context?.Handle ?? IntPtr.Zero, inputChannels, outputChannels), context);
        }

        /// <summary>
        /// Creates a pipeline from the supplied handle.
        /// </summary>
        /// <param name="handle">A handle to an existing pipeline.</param>
        /// <returns>A new <see cref="Pipeline"/> instance referencing an existing pipeline.</returns>
        /// <exception cref="LcmsNETException">
        /// The <paramref name="handle"/> is <see cref="IntPtr.Zero"/>.
        /// </exception>
        internal static Pipeline FromHandle(IntPtr handle)
        {
            return new Pipeline(handle, context: null, isOwner: false);
        }

        /// <summary>
        /// Duplicates a pipeline.
        /// </summary>
        /// <returns>A new <see cref="Pipeline"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        public Pipeline Duplicate()
        {
            EnsureNotDisposed();

            return new Pipeline(Interop.PipelineDup(handle), Context);
        }

        /// <summary>
        /// Appends the supplied <see cref="Pipeline"/> to the end of this pipeline.
        /// </summary>
        /// <param name="other">The pipeline to be appended to the end of this pipeline.</param>
        /// <returns>true if appended successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        public bool Append(Pipeline other)
        {
            EnsureNotDisposed();

            return Interop.PipelineCat(handle, other.handle) != 0;
        }

        /// <summary>
        /// Evaluates the pipeline using the supplied floating point values.
        /// </summary>
        /// <param name="values">The values to supply to the pipeline.</param>
        /// <returns>The values resulting from evaluation of the pipeline.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        public float[] Evaluate(float[] values)
        {
            if (!(values?.Length > 0)) throw new ArgumentException($"'{nameof(values)}' array size must be greater than 0.");

            EnsureNotDisposed();

            float[] result = new float[values.Length];
            Interop.PipelineEvalFloat(handle, values, result);
            return result;
        }

        /// <summary>
        /// Evaluates the pipeline in the reverse direction for the supplied floating point values using Newton's method.
        /// </summary>
        /// <param name="values">The values to supply to the pipeline.</param>
        /// <param name="hint">An array of hint values where to begin the search, can be null.</param>
        /// <param name="success">Returns true if the pipeline was evaluated successfully, otherwise false.</param>
        /// <returns>The values resulting from evaluation of the pipeline.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        public float[] EvaluateReverse(float[] values, float[] hint, out bool success)
        {
            if (!(values?.Length > 0)) throw new ArgumentException($"'{nameof(values)}' array size must be greater than 0.");

            EnsureNotDisposed();

            float[] result = new float[values.Length];
            success = Interop.PipelineEvalReverseFloat(handle, values, result, hint) != 0;
            return result;
        }

        /// <summary>
        /// Evaluates the pipeline using the supplied unsigned 16-bit integer values.
        /// </summary>
        /// <param name="values">The values to supply to the pipeline.</param>
        /// <returns>The values resulting from evaluation of the pipeline.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        public ushort[] Evaluate(ushort[] values)
        {
            if (!(values?.Length > 0)) throw new ArgumentException($"'{nameof(values)}' array size must be greater than 0.");

            EnsureNotDisposed();

            ushort[] result = new ushort[values.Length];
            Interop.PipelineEval16(handle, values, result);
            return result;
        }

        /// <summary>
        /// Inserts a <see cref="Stage"/> to the start or end of the pipeline.
        /// </summary>
        /// <param name="stage">The <see cref="Stage"/> to be inserted.</param>
        /// <param name="location">The location where the stage is to be inserted.</param>
        /// <returns>true if the pipeline is inserted, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        /// <remarks>
        /// Ownership of the <see cref="Stage"/> passes to the pipeline so its resources
        /// will be freed when the pipeline is disposed.
        /// </remarks>
        public bool Insert(Stage stage, StageLoc location)
        {
            EnsureNotDisposed();

            bool inserted = Interop.PipelineInsertStage(handle, stage.Handle, Convert.ToInt32(location)) != 0;
            if (inserted) stage.Release();
            return inserted;
        }

        /// <summary>
        /// Removes a <see cref="Stage"/> from the start or end of the pipeline.
        /// </summary>
        /// <param name="location">The location from where the stage is to be removed.</param>
        /// <returns>The <see cref="Stage"/> that has been removed. Can be null.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        /// <remarks>
        /// The caller is responsible for disposing any <see cref="Stage"/> removed using this method.
        /// </remarks>
        public Stage Unlink(StageLoc location)
        {
            EnsureNotDisposed();

            IntPtr ptr = IntPtr.Zero;
            Interop.PipelineUnlinkStage(handle, Convert.ToInt32(location), ref ptr);

            if (ptr != IntPtr.Zero)
            {
                IntPtr handle = Interop.GetStageContextID(ptr);
                Context context = handle != IntPtr.Zero ? Context.CopyRef(handle) : null;
                return new Stage(ptr, context);
            }

            return null;
        }

        /// <summary>
        /// Removes and disposes a <see cref="Stage"/> from the start or end of the pipeline.
        /// </summary>
        /// <param name="location">The location from where the stage is to be removed.</param>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        public void UnlinkAndDispose(StageLoc location)
        {
            EnsureNotDisposed();

            Interop.PipelineUnlinkStage(handle, Convert.ToInt32(location));
        }

        /// <summary>
        /// Sets in internal flag that marks the pipeline to be saved in 8- or 16-bit precision.
        /// </summary>
        /// <param name="on">true sets 8-bit precision, false sets 16-bit precision.</param>
        /// <returns>true if set successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The pipeline has already been disposed.
        /// </exception>
        /// <remarks>
        /// By default all pipelines are saved as 16-bit precision for AtoB/BtoA tags and as
        /// floating point precision for DtoB/BtoD tags.
        /// </remarks>
        public bool SetAs8BitsFlag(bool on)
        {
            EnsureNotDisposed();

            return Interop.PipelineSetSaveAs8BitsFlag(handle, on ? 1 : 0) != 0;
        }

        /// <summary>
        /// Gets the number of input channels for the pipeline.
        /// </summary>
        public uint InputChannels => Interop.PipelineInputChannels(handle);

        /// <summary>
        /// Gets the number of output channels for the pipeline.
        /// </summary>
        public uint OutputChannels => Interop.PipelineOutputChannels(handle);

        /// <summary>
        /// Gets the number of stages in the pipeline.
        /// </summary>
        public uint StageCount => Interop.PipelineStageCount(handle);

        #region IEnumerable<Stage> Support
        /// <summary>
        /// Returns an enumerator that iterates through the stages in the pipeline.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the stages in the pipeline.</returns>
        public IEnumerator<Stage> GetEnumerator()
        {
            EnsureNotDisposed();

            return new StageEnumerator(handle);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the stages in the pipeline.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the stages in the pipeline.</returns>
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
                        _current = Stage.CopyRef(First);
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
                        _current = Stage.CopyRef(Interop.StageNext(_current.Handle));
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

        /// <summary>
        /// Frees the pipeline handle.
        /// </summary>
        protected override bool ReleaseHandle()
        {
            Interop.PipelineFree(handle);
            return true;
        }
    }
}
