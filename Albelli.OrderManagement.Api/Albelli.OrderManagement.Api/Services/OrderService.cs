using Albelli.OrderManagement.Api.Configuration;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using Albelli.OrderManagement.Api.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albelli.OrderManagement.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductInfoRepository productInfoRepository;
        private readonly ApplicationConfig config;

        public OrderService(IOrderRepository orderRepository, ApplicationConfig config, IProductInfoRepository productInfoRepository)
        {
            this.orderRepository = orderRepository;
            this.config = config;
            this.productInfoRepository = productInfoRepository;
        }

        public Guid Create(IEnumerable<OrderLine> items)
        {
            OrderLineValidator.Validate(items);
            var order = new Order { Items = items, MinPackageWidth = CalculateWidth(items) };

            return orderRepository.Add(order);
        }

        public Order Get(Guid orderId)
        { 
            return orderRepository.Get(orderId);
        }

        private double CalculateWidth(IEnumerable<OrderLine> items)
        {
            return items.Sum(item =>
            {
                return item.ProductType switch
                {
                    ProductType.Mug => Math.Ceiling(item.Quantity / (float)config.MaxMagsPerStack) * productInfoRepository.GetWidth(ProductType.Mug),
                    _ => item.Quantity * productInfoRepository.GetWidth(item.ProductType)
                };
            });
        }
    }
}
