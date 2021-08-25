using System;
using System.Collections.Generic;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Albelli.OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Place([FromBody] IEnumerable<OrderLine> items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guid = orderService.Create(items);

            return Ok(guid);
        }

        [HttpGet("{orderId}")]
        public IActionResult Get([FromRoute] Guid orderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = orderService.Get(orderId);

            if (order == null)
            {
                return NoContent();
            }

            return Ok(order);
        }
    }
}
