using System;
using System.Runtime.Serialization;

namespace FullCRM.Exceptions
{
    public class OrderNotFoundException : DocumentNotFoundException
    {
        public OrderNotFoundException(string message = "Заказ не найден") : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
