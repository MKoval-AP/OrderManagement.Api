using System;
using System.Collections.Generic;
using System.Linq;
using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly IList<Order> orders = new List<Order>
        {
            new Order { OrderId = new Guid(), MinPackageWidth = 19, Items = new List<OrderLine>
            {
                new OrderLine { ProductType = ProductType.PhotoBook, Quantity = 1 }
            }}
        };

        public Guid Add(Order order)
        {
            var guid = Guid.NewGuid();
            order.OrderId = guid;
            orders.Add(order);

            return guid;
        }

        public Order Get(Guid orderId)
        {
            return orders.FirstOrDefault(x => x.OrderId == orderId);
        }
    }
}
