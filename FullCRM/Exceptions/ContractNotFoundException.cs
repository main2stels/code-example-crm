using System;
using System.Runtime.Serialization;

namespace FullCRM.Exceptions
{
    public class ContractNotFoundException : DocumentNotFoundException
    {
        public ContractNotFoundException(string message = "Контракт не найден") : base(message)
        {
        }

        public ContractNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContractNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
