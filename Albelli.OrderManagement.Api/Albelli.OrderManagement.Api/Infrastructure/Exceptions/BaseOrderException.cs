using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Infrastructure.Exceptions
{
    public class BaseOrderException : Exception
    {
        public BaseOrderException()
        {
        }

        public BaseOrderException(string message) : base(message)
        {
        }

        public BaseOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BaseOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}