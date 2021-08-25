using Albelli.OrderManagement.Api.Models;
using System;

namespace Albelli.OrderManagement.Api.Repositories
{
    public interface IOrderRepository
    {
        Order Get(Guid orderId);

        Guid Add(Order order);
    }
}
