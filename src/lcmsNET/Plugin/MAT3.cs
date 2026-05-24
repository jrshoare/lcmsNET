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

using System;
using System.Runtime.InteropServices;

namespace lcmsNET.Plugin
{
    /// <summary>
    /// Represents a 3x3 matrix formed from 3 <see cref="VEC3"/> vectors.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MAT3
    {
        // Make MAT3 blittable for source-generated P/Invoke by using three VEC3 fields
        // instead of an array field.
        private readonly VEC3 Row0;
        private readonly VEC3 Row1;
        private readonly VEC3 Row2;

        private MAT3(VEC3 row0, VEC3 row1, VEC3 row2)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MAT3"/> class.
        /// </summary>
        /// <param name="rows">An array of 3 <see cref="VEC3"/> instances.</param>
        public MAT3(VEC3[] rows)
        {
            if (rows?.Length != 3) throw new ArgumentException($"'{nameof(rows)}' array size must equal 3.");
            Row0 = rows[0];
            Row1 = rows[1];
            Row2 = rows[2];
        }

        /// <summary>
        /// Gets the vector at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the vector.</param>
        /// <returns>The vector at the specified index.</returns>
        public VEC3 this[int index]
        {
            get
            {
                return index switch
                {
                    0 => Row0,
                    1 => Row1,
                    2 => Row2,
                    _ => throw new IndexOutOfRangeException($"'{nameof(index)}' must be in the range 0 to 2.")
                };
            }
        }

        /// <summary>
        /// Creates a zeroes matrix.
        /// </summary>
        /// <returns>A new zeroes matrix.</returns>
        public static MAT3 Zeroes() => new(default, default, default);

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <returns>A new identity matrix.</returns>
        public static MAT3 Identity() => new(new(1.0, 0.0, 0.0), new(0.0, 1.0, 0.0), new(0.0, 0.0, 1.0));

        /// <summary>
        /// Returns true if the matrix is close enough to be interpreted as identity, otherwise returns false.
        /// </summary>
        public bool IsIdentity => Interop.MAT3isIdentity(in this);

        /// <summary>
        /// Muliplies two matrices.
        /// </summary>
        /// <param name="a">The first matrix.</param>
        /// <param name="b">The second matrix.</param>
        /// <returns>A matrix containing the result of the multiplication.</returns>
        public static MAT3 Multiply(in MAT3 a, in MAT3 b)
        {
            MAT3 r = default;
            Interop.MAT3multiply(ref r, in a, in b);
            return r;
        }

        /// <summary>
        /// Inverts a matrix.
        /// </summary>
        /// <param name="a">The matrix to be inverted.</param>
        /// <param name="b">Returns the inverted matrix.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <remarks>
        /// Returns false if <paramref name="a"/> is singular.
        /// </remarks>
        public static bool Invert(in MAT3 a, out MAT3 b)
        {
            b = default;
            return Interop.MAT3invert(in a, ref b);
        }

        /// <summary>
        /// Solves a system in the form Ax = b.
        /// </summary>
        /// <param name="a">The matrix, A.</param>
        /// <param name="b">The vector, b.</param>
        /// <param name="x">Returns a vector with the result.</param>
        /// <returns>true if successful, otherwise false.</returns>
        /// <remarks>
        /// Returns false if <paramref name="a"/> is singular.
        /// </remarks>
        public static bool Solve(in MAT3 a, in VEC3 b, out VEC3 x)
        {
            x = default;
            return Interop.MAT3solve(ref x, in a, in b);
        }

        /// <summary>
        /// Evaluates a matrix.
        /// </summary>
        /// <param name="a">The matrix to be evaluated.</param>
        /// <param name="v">The vector to be evaluated.</param>
        /// <returns>A vector containing the result of the evaluation.</returns>
        public static VEC3 Evaluate(in MAT3 a, in VEC3 v)
        {
            VEC3 r = default;
            Interop.MAT3eval(ref r, in a, in v);
            return r;
        }
    }
}
