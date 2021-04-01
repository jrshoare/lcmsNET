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

        /// <summary>
        /// Initialises a new instance of the <see cref="LcmsNETException"/> class with serialised data.
        /// </summary>
        /// <param name="info">The object that holds the serialised object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected LcmsNETException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
