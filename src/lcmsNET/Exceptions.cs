using System;
using System.Runtime.Serialization;

namespace lcmsNET
{
    public class LcmsNETException : Exception
    {
        public LcmsNETException()
        {
        }

        public LcmsNETException(string message) : base(message)
        {
        }

        public LcmsNETException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LcmsNETException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
