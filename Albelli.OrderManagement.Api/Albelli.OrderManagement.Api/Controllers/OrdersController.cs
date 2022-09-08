using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using Albelli.OrderManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Albelli.OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] IEnumerable<OrderLine> items)
        {
            var order = _orderService.PlaceOrder(items);
            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public IActionResult RetrieveOrder(int orderId) => Ok(_orderService.GetOrder(orderId));

    }
}
