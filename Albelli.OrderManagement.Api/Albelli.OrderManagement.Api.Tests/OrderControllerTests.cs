using System;
using System.Collections.Generic;
using System.Net;
using Albelli.OrderManagement.Api.Controllers;
using Albelli.OrderManagement.Api.Infrastructure.Exceptions;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using Albelli.OrderManagement.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void PackageWidth_Success()
        {
            var orderController = PrepareController();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Calendar, Quantity = 2 });

            var result = orderController.PlaceOrder(lines) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            (result.Value as Order).Should().NotBeNull();
            var order = result.Value as Order;
            order.MinPackageWidth.Should().Be(20);
        }

        [Fact]
        public void MugPackageWidth_Success()
        {
            var orderController = PrepareController();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = 5 });

            var result = orderController.PlaceOrder(lines) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            (result.Value as Order).Should().NotBeNull();
            var order = result.Value as Order;
            order.MinPackageWidth.Should().Be(188);
        }

        [Fact]
        public void DifferentProductsWidth_Success()
        {
            var orderController = PrepareController();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = 5 });
            lines.Add(new OrderLine() { ProductType = ProductType.Cards, Quantity = 1 });
            lines.Add(new OrderLine() { ProductType = ProductType.Canvas, Quantity = 2 });

            var result = orderController.PlaceOrder(lines) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            (result.Value as Order).Should().NotBeNull();
            var order = result.Value as Order;
            order.MinPackageWidth.Should().Be(224.7);
        }

        [Fact]
        public void NoLinesHasBeenSent_ThrowException()
        {
            var orderController = PrepareController();

            Action result = () => orderController.PlaceOrder(null);

            result.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void NoOrder_ThrowException()
        {
            var orderController = PrepareController();

            Action action = () => orderController.RetrieveOrder(0);

            action.Should().Throw<OrderNotFoundException>();
        }

        [Fact]
        public void UnknowProduct_ThrowException()
        {
            var orderController = PrepareController();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = (ProductType)(-1), Quantity = 1 });

            Action result = () => orderController.PlaceOrder(lines);

            result.Should().Throw<ProductInfoNotFoundException>();
        }

        private OrdersController PrepareController()
        {
            return new OrdersController(new OrderService(new OrderRepository(), new ProductInfoRepository()));
        }
    }
}
