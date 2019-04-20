using System;
using System.Runtime.Serialization;

namespace FullCRM.Exceptions
{
    public class DocumentNotFoundException : ApplicationException
    {
        public DocumentNotFoundException()
        {
        }

        public DocumentNotFoundException(string message) : base(message)
        {
        }

        public DocumentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DocumentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
