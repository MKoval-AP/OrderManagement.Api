using Albelli.OrderManagement.Api.Models;
using System;
using System.Collections.Generic;

namespace Albelli.OrderManagement.Api.Services
{
    public interface IOrderService
    {
        Guid Create(IEnumerable<OrderLine> items);

        Order Get(Guid orderId);
    }
}
