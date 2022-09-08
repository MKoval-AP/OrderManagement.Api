using System.Collections.Generic;
using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Services
{
    public interface IOrderService
    {
        Order PlaceOrder(IEnumerable<OrderLine> lines);
        Order GetOrder(int id);
    }
}