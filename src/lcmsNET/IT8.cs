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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace lcmsNET
{
    /// <summary>
    /// Defines methods and properties for reading and writing ANSI CGATS.17 text files.
    /// </summary>
    public sealed class IT8 : IDisposable
    {
        private IntPtr _handle;

        internal IT8(IntPtr handle, Context context = null)
        {
            Helper.CheckCreated<IT8>(handle);

            _handle = handle;
            Context = context;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IT8"/> class.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <returns>A new <see cref="IT8"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IT8 Create(Context context)
        {
            return new IT8(Interop.IT8Alloc(context?.Handle ?? IntPtr.Zero), context);
        }

        #region Properties
        /// <summary>
        /// Gets the context in which the instance was created.
        /// </summary>
        public Context Context { get; private set; }
        #endregion

        #region Tables
        /// <summary>
        /// Gets the number of tables in this instance.
        /// </summary>
        /// <remarks>
        /// An <see cref="IT8"/> instance created with <see cref="Create(Context)"/> initially
        /// has one table allocated.
        /// </remarks>
        public uint TableCount => Interop.IT8TableCount(_handle);

        /// <summary>
        /// Sets the current table.
        /// </summary>
        /// <param name="nTable">Zero-based index of the table.</param>
        /// <returns>The index of the current table, or -1 on error.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The object has already been disposed.
        /// </exception>
        /// <remarks>
        /// Set <paramref name="nTable"/> to <see cref="TableCount"/> to add a new table.
        /// </remarks>
        public int SetTable(uint nTable)
        {
            EnsureNotDisposed();

            return Interop.IT8SetTable(_handle, nTable);
        }
        #endregion

        #region Persistence
        /// <summary>
        /// Opens and populates a new instance of the <see cref="IT8"/> class from the contents
        /// of an existing CGATS file.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="filepath">The full path to the file.</param>
        /// <returns>A new <see cref="IT8"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IT8 Open(Context context, string filepath)
        {
            return new IT8(Interop.IT8LoadFromFile(context?.Handle ?? IntPtr.Zero, filepath), context);
        }

        /// <summary>
        /// Opens and populates a new instance of the <see cref="IT8"/> class from the supplied
        /// memory block.
        /// </summary>
        /// <param name="context">A <see cref="Context"/>, or null for the global context.</param>
        /// <param name="memory">A block of contiguous memory containing the CGATS.17 data.</param>
        /// <returns>A new <see cref="IT8"/> instance.</returns>
        /// <exception cref="LcmsNETException">
        /// Failed to create instance.
        /// </exception>
        /// <remarks>
        /// Creates the instance in the global context if <paramref name="context"/> is null.
        /// </remarks>
        public static IT8 Open(Context context, byte[] memory)
        {
            return new IT8(Interop.IT8LoadFromMem(context?.Handle ?? IntPtr.Zero, memory), context);
        }

        /// <summary>
        /// Saves the instance to file in CGATS.17 format.
        /// </summary>
        /// <param name="filepath">The full path to the file.</param>
        /// <returns>true if saved successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The object has already been disposed.
        /// </exception>
        public bool Save(string filepath)
        {
            EnsureNotDisposed();

            return 0 != Interop.IT8SaveToFile(_handle, filepath);
        }

        /// <summary>
        /// Saves the instance to a contiguous memory block in CGATS.17 format.
        /// </summary>
        /// <param name="it8">
        /// A byte array sufficiently sized to contain the data, or null to calculate required size.
        /// </param>
        /// <param name="bytesNeeded">
        /// On return defines the number of bytes required to contain the data.
        /// </param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The object has already been disposed.
        /// </exception>
        public bool Save(byte[] it8, out uint bytesNeeded)
        {
            EnsureNotDisposed();

            return 0 != Interop.IT8SaveToMem(_handle, it8, out bytesNeeded);
        }
        #endregion

        #region Type and Comments
        /// <summary>
        /// Gets or sets the identifier placed on the very first line of the CGATS.17 file.
        /// </summary>
        /// <exception cref="LcmsNETException">
        /// Failed to set the sheet type.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// The object has already been disposed.
        /// </exception>
        public string SheetType
        {
            get { return Interop.IT8GetSheetType(_handle); }
            set
            {
                EnsureNotDisposed();
                if (0 == Interop.IT8SetSheetType(_handle, value))
                {
                    throw new LcmsNETException($"Failed to set sheet type: '{value}'.");
                }
            }
        }

        /// <summary>
        /// Adds a comment to the file.
        /// </summary>
        /// <param name="comment">The comment to be inserted.</param>
        /// <returns>true if added successfully, otherwise false.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The object has already been disposed.
        /// </exception>
        /// <remarks>
        /// Successive calls to this method add comments in the same order as invoked.
        /// </remarks>
        public bool AddComment(string comment)
        {
            EnsureNotDisposed();
            return Interop.IT8SetComment(_handle, comment) != 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the value of a property in the current table as a literal string.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The literal string value of the property, or null on error.</returns>
        public string GetProperty(string name)
        {
            return Interop.IT8GetProperty(_handle, name);
        }

        /// <summary>
        /// Gets the value of a property in the current table as a floating point number.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The floating point value of the property, or 0 on error.</returns>
        public double GetDoubleProperty(string name)
        {
            return Interop.IT8GetPropertyDouble(_handle, name);
        }

        /// <summary>
        /// Sets a string property in the current table.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The property value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <remarks>
        /// <para>
        /// Strings are enclosed in quotes "".
        /// </para>
        /// <para>
        /// Sub-properties are allowed if <paramref name="value"/> is a string
        /// in the form:
        /// </para>
        /// <para>
        /// "SUBPROP1,1;SUBPROP2,2;...".
        /// </para>
        /// </remarks>
        public bool SetProperty(string name, string value)
        {
            return Interop.IT8SetProperty(_handle, name, value) != 0;
        }

        /// <summary>
        /// Sets a floating point property in the current table.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The property value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetProperty(string name, double value)
        {
            return Interop.IT8SetPropertyDouble(_handle, name, value) != 0;
        }

        /// <summary>
        /// Sets a hexadecimal constant (appends 0x) property in the current table.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="hex">The property value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetProperty(string name, uint hex)
        {
            return Interop.IT8SetPropertyHex(_handle, name, hex) != 0;
        }

        /// <summary>
        /// Sets a property with no interpretation in the current table.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The property value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <remarks>
        /// <para>
        /// No quotes "" are added. No checking is performed, so it is upto
        /// the caller to ensure that the string is valid.
        /// </para>
        /// <para>
        /// Special prefixes:
        /// <list type="bullet">
        /// <item>0b: Binary</item>
        /// <item>0x: Hexadecimal</item>
        /// </list>
        /// </para>
        /// </remarks>
        public bool SetUncookedProperty(string name, string value)
        {
            return Interop.IT8SetPropertyUncooked(_handle, name, value) != 0;
        }

        /// <summary>
        /// Adds a new sub-property to an existing property in the current table.
        /// </summary>
        /// <param name="key">An existing property in the current table.</param>
        /// <param name="subkey">The name of the sub-property.</param>
        /// <param name="value">The uncooked value value of the sub-property.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetProperty(string key, string subkey, string value)
        {
            return Interop.IT8SetProperty(_handle, key, subkey, value) != 0;
        }

        /// <summary>
        /// Gets an object that can be used to enumerate all properties in the current table.
        /// </summary>
        public IEnumerable<string> Properties => Interop.IT8EnumProperties(_handle);

        /// <summary>
        /// Gets an object that can be used to enumerate all sub-property names for a
        /// multi-value property in the current table.
        /// </summary>
        /// <param name="name">The name of the multi-value property.</param>
        /// <returns>An object that can be used to enumerate the sub-property names.</returns>
        /// <exception cref="ObjectDisposedException">
        /// The object has already been disposed.
        /// </exception>
        public IEnumerable<string> GetProperties(string name)
        {
            EnsureNotDisposed();

            return Interop.IT8EnumPropertyMulti(_handle, name);
        }
        #endregion

        #region Datasets
        /// <summary>
        /// Gets a cell [row, column] value as a literal string in the current table.
        /// </summary>
        /// <param name="row">The zero-based row.</param>
        /// <param name="column">The zero-based column.</param>
        /// <returns>The literal string value of the cell, or null on error.</returns>
        public string GetData(int row, int column)
        {
            return Interop.IT8GetDataRowCol(_handle, row, column);
        }

        /// <summary>
        /// Gets a cell [patch, sample] value as a literal string in the current table.
        /// </summary>
        /// <param name="patch">The intended patch name (row).</param>
        /// <param name="sample">The intended sample name (column).</param>
        /// <returns>The literal string value of the cell, or null on error.</returns>
        public string GetData(string patch, string sample)
        {
            return Interop.IT8GetData(_handle, patch, sample);
        }

        /// <summary>
        /// Gets a cell [row, column] value as a floating point number in the current table.
        /// </summary>
        /// <param name="row">The zero-based row.</param>
        /// <param name="column">The zero-based column.</param>
        /// <returns>The floating point value of the cell, or 0 on error.</returns>
        public double GetDoubleData(int row, int column)
        {
            return Interop.IT8GetDataRowColDouble(_handle, row, column);
        }

        /// <summary>
        /// Gets a cell [patch, sample] value as a floating point number in the current table.
        /// </summary>
        /// <param name="patch">The intended patch name (row).</param>
        /// <param name="sample">The intended sample name (column).</param>
        /// <returns>The floating point value of the cell, or 0 on error.</returns>
        public double GetDoubleData(string patch, string sample)
        {
            return Interop.IT8GetDataDbl(_handle, patch, sample);
        }

        /// <summary>
        /// Sets a cell [row, column] value to a literal string in the current table.
        /// </summary>
        /// <param name="row">The zero-based row.</param>
        /// <param name="column">The zero-based column.</param>
        /// <param name="value">The literal string value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetData(int row, int column, string value)
        {
            return Interop.IT8SetDataRowCol(_handle, row, column, value) != 0;
        }

        /// <summary>
        /// Sets a cell [patch, sample] value to a literal string in the current table.
        /// </summary>
        /// <param name="patch">The intended patch name (row).</param>
        /// <param name="sample">The intended sample name (column).</param>
        /// <param name="value">The literal string value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetData(string patch, string sample, string value)
        {
            return Interop.IT8SetData(_handle, patch, sample, value) != 0;
        }

        /// <summary>
        /// Sets a cell [row, column] value to a floating point number in the current table.
        /// </summary>
        /// <param name="row">The zero-based row.</param>
        /// <param name="column">The zero-based column.</param>
        /// <param name="value">The literal string value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetData(int row, int column, double value)
        {
            return Interop.IT8SetDataRowColDbl(_handle, row, column, value) != 0;
        }

        /// <summary>
        /// Sets a cell [patch, column] value to a floating point number in the current table.
        /// </summary>
        /// <param name="patch">The intended patch name (row).</param>
        /// <param name="sample">The intended sample name (column).</param>
        /// <param name="value">The literal string value.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public bool SetData(string patch, string sample, double value)
        {
            return Interop.IT8SetDataDbl(_handle, patch, sample, value) != 0;
        }

        /// <summary>
        /// Gets the zero-based column position of a given data sample name in the current table.
        /// </summary>
        /// <param name="sample">The sample name.</param>
        /// <returns>The column number, or -1 if not found.</returns>
        public int FindDataFormat(string sample)
        {
            return Interop.IT8FindDataFormat(_handle, sample);
        }

        /// <summary>
        /// Sets the column names in the current table.
        /// </summary>
        /// <param name="column">The zero-based column number.</param>
        /// <param name="sample">The sample name for the column.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <remarks>
        /// <para>
        /// The first column is "SAMPLE_ID".
        /// </para>
        /// The special property "NUMBER_OF_FIELDS" must be set for the current table
        /// before calling this method.
        /// </remarks>
        public bool SetDataFormat(int column, string sample)
        {
            return Interop.IT8SetDataFormat(_handle, column, sample) != 0;
        }

        /// <summary>
        /// Gets an object that can be used to enumerate the sample names for the columns
        /// in the current table.
        /// </summary>
        public IEnumerable<string> SampleNames => Interop.IT8EnumDataFormat(_handle);

        /// <summary>
        /// Gets the value of the first column (patch name) for the given set number.
        /// </summary>
        /// <param name="nPatch">The zero-based set number.</param>
        /// <returns>The patch name, or null on error.</returns>
        public string GetPatchName(int nPatch)
        {
            return Interop.IT8GetPatchName(_handle, nPatch);
        }

        /// <summary>
        /// Sets the format string for floating point numbers using the "C" sprintf convention.
        /// </summary>
        /// <remarks>
        /// The default format is "%.10g".
        /// </remarks>
        public string DoubleFormat
        {
            set => Interop.IT8DefineDblFormat(_handle, value);
        }
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
                throw new ObjectDisposedException(nameof(IT8));
            }
        }

        private void Dispose(bool disposing)
        {
            var handle = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
            {
                Interop.IT8Free(handle);
                Context = null;
            }
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~IT8()
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
