using System;
using System.Collections.Generic;
using System.Linq;
using Albelli.OrderManagement.Api.Infrastructure.Exceptions;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using Microsoft.VisualBasic.CompilerServices;

namespace Albelli.OrderManagement.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductInfoRepository _productInfoRepository;

        public OrderService(IOrderRepository orderRepository, IProductInfoRepository productInfoRepository)
        {
            _orderRepository = orderRepository;
            _productInfoRepository = productInfoRepository;
        }

        public Order PlaceOrder(IEnumerable<OrderLine> lines)
        {
            if (lines == null || !lines.Any())
            {
                throw new ArgumentException($"{nameof(lines)} is empty");
            }

            var order = new Order()
            {
                Items = lines.ToList(),
                MinPackageWidth = CalculatePackageWidth(lines)
            };

            _orderRepository.Add(order);
            return order;
        }

        public Order GetOrder(int id)
        {
            var order = _orderRepository.GetOrder(id);
            if (order == null)
            {
                throw new OrderNotFoundException($"The order {id} wan not found");
            }

            return order;
        }

        private double CalculatePackageWidth(IEnumerable<OrderLine> lines)
        {
            var types = lines.Select(l => l.ProductType).Distinct();
            var calculatedWidth = 0.0;
            foreach (var productType in types)
            {
                var countOfProducts = lines.Where(p => p.ProductType == productType).Sum(p => p.Quantity);
                var productInfo = _productInfoRepository.Get(productType);
                var stacks = CalculateStacks(countOfProducts, productInfo.CountInStack);
                calculatedWidth += (stacks * productInfo.WidthMm);
            }

            return calculatedWidth;
        }

        private int CalculateStacks(int actual, int countInStack)
        {
            var fullStacks = (actual / countInStack);
            var whatLeft = actual % countInStack;
            var actualStacks = whatLeft > 0 ? fullStacks + 1 : fullStacks;
            return actualStacks;
        }

    }

}