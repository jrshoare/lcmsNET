﻿// Copyright(c) 2019-2021 John Stevenson-Hoare
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
using System.Runtime.Serialization;

namespace lcmsNET
{
    /// <summary>
    /// Serves as the base class for lcmsNET exceptions.
    /// </summary>
    public class LcmsNETException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LcmsNETException"/> class.
        /// </summary>
        public LcmsNETException()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="LcmsNETException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LcmsNETException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="LcmsNETException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> is
        /// not a null reference, the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public LcmsNETException(string message, Exception innerException) : base(message, innerException)
        {
        }

#if !NET8_0_OR_GREATER
        /// <summary>
        /// Initialises a new instance of the <see cref="LcmsNETException"/> class with serialised data.
        /// </summary>
        /// <param name="info">The object that holds the serialised object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected LcmsNETException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}
