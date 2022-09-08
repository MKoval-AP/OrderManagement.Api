using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order GetOrder(int orderId);
    }
}