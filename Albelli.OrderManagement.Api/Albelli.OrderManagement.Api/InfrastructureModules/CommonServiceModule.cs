using Albelli.OrderManagement.Api.Configuration;
using Albelli.OrderManagement.Api.Repositories;
using Albelli.OrderManagement.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.ConfgureServices
{
    public static class CommonServiceModule
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IProductInfoRepository, ProductInfoRepository>()
                .AddScoped<IOrderService, OrderService>();
        }
    }
}
