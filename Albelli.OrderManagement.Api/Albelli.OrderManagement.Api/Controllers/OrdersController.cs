using Albelli.OrderManagement.Api.Calculations;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albelli.OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private OrderRepository _orderRepository; //TODO: Use DI to avoid tight coupling, if for some reason this objects is required at controller level.
        private ProductInfoRepository _productInfoRepository;

        public OrdersController()
        {
            _orderRepository = new OrderRepository();
            _productInfoRepository = new ProductInfoRepository();
        }

        [HttpPost("orders/place")]
        public ActionResult PlaceOrder([FromBody] IEnumerable<OrderLine> items)
        {
            try
            { 
                var orderLines = items.ToList(); 
                var order = new Order { Items = orderLines, MinPackageWidth = PackageCalculator.PackageWidth(orderLines, _productInfoRepository) };

                _orderRepository.Add(order);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex); //TODO: For both action methods. Provide request data validation and provide response code which fits better.
            }
        }

        [HttpGet("order/{orderId}/retrieve")]
        public ActionResult RetrieveOrder(int orderId) // TODO: this method does not return awaitable type, so synchronous method signature was used.
        {
            try
            {
                Order order = _orderRepository.GetOrder(orderId);

                if (order == null)
                {
                    throw new ArgumentException(orderId.ToString());
                }

                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex);
            }
            catch (Exception ex)
            {
                throw ex; // TODO: In case such exception interception is required, then original one should be logged and new one should be thrown only with relevant information.
            }
        }

    }
}
