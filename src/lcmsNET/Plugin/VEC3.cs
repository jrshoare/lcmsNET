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
    /// Represents a 3-component vector defined as using double precision floating point numbers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VEC3
    {
        private double X;
        private double Y;
        private double Z;

        /// <summary>
        /// Initialises the vector.
        /// </summary>
        /// <param name="x">x component of the vector.</param>
        /// <param name="y">y component of the vector.</param>
        /// <param name="z">z component of the vector.</param>
        public VEC3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets or sets the value of the vector component at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the component.</param>
        /// <returns>The value of vector component at the specified index.</returns>
        public double this[int index]
        {
            get
            {
                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => throw new IndexOutOfRangeException($"Index must be in the range [0, 2].")
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"Index must be in the range [0, 2].");
                }
            }
        }

        /// <summary>
        /// Performs vector subtraction.
        /// </summary>
        /// <param name="v1">A first vector.</param>
        /// <param name="v2">A vector containing the values to be subtracted from <paramref name="v1"/>.</param>
        /// <returns>
        /// A vector containing the difference between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </returns>
        public static VEC3 operator -(in VEC3 v1, in VEC3 v2)
        {
            VEC3 result = new(default, default, default);
            Interop.VEC3minus(ref result, in v1, in v2);
            return result;
        }

        /// <summary>
        /// Calculates the cross (vector) product of the supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>
        /// The cross product of <paramref name="v1"/> and <paramref name="v2"/>.
        /// </returns>
        public static VEC3 Cross(in VEC3 v1, in VEC3 v2)
        {
            return new VEC3(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        /// <summary>
        /// Calculates the dot (scalar) product of the supplied vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>
        /// The dot product of <paramref name="v1"/> and <paramref name="v2"/>.
        /// </returns>
        public static double Dot(in VEC3 v1, in VEC3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        /// <summary>
        /// Returns the Euclidean length of the vector.
        /// </summary>
        public double Length
        {
            get => Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Calculates the Euclidean distance between two vector points.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The Euclidean distance between the points.</returns>
        public static double Distance(in VEC3 v1, in VEC3 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            var dz = v1.Z - v2.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}
