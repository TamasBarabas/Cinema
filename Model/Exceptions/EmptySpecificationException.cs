using System;
using System.Runtime.Serialization;

namespace Model.Exceptions
{
    [Serializable]
    public class EmptySpecificationException : Exception
    {
        public EmptySpecificationException()
        {
        }

        public EmptySpecificationException(string message) : base(message)
        {
        }

        public EmptySpecificationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptySpecificationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}