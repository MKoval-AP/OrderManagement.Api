using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Infrastructure.Exceptions
{
    public class ProductInfoNotFoundException : BaseOrderException
    {
        public ProductInfoNotFoundException(string message) : base(message)
        {
        }
    }
}