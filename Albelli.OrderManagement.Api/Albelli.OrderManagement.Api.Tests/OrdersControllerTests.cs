using Albelli.OrderManagement.Api.ConfgureServices;
using Albelli.OrderManagement.Api.Configuration;
using Albelli.OrderManagement.Api.Controllers;
using Albelli.OrderManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{
    public class OrdersControllerTests
    {
        private readonly OrdersController sut;
        public OrdersControllerTests()
        {
            var applicationSettings = new ApplicationConfig()
            {
                MaxMagsPerStack = 4
            };
            var serviceProvider = new ServiceCollection()
                .AddCommonServices()
                .AddScoped<OrdersController>()
                .AddScoped(c => applicationSettings)
                .BuildServiceProvider();

            sut = serviceProvider.GetRequiredService<OrdersController>();
        }

        [Fact]
        public void Place_ValidInput_Ok()
        {
            var lines = new List<OrderLine>
            {
                new OrderLine
                {
                    ProductType = ProductType.Calendar,
                    Quantity = 2
                }
            };

            var result = sut.Place(lines) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void Place_InvalidProductType_ThrowsException()
        {
            var lines = new List<OrderLine>
            {
                new OrderLine
                {
                    ProductType = (ProductType)5,
                    Quantity = 4
                }
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Place(lines));
        }

        [Fact]
        public void Get_ValidInput_Ok()
        {
            var lines = new List<OrderLine>
            {
                new OrderLine
                {
                    ProductType = ProductType.Calendar,
                    Quantity = 2
                },
                new OrderLine
                {
                    ProductType = ProductType.Mug,
                    Quantity = 7
                }
            };

            var placingResult = sut.Place(lines) as OkObjectResult;
            var guid = Guid.Parse(placingResult.Value.ToString());
            var result = sut.Get(guid) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(208, ((Order)result.Value).MinPackageWidth);
        }

        [Fact]
        public void Get_UnexistingGuid_NoContent()
        {
            var guid = Guid.NewGuid();
            var result = sut.Get(guid) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }
    }
}
