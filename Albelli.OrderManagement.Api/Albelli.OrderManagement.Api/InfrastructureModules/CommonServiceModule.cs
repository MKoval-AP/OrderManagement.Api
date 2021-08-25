using Albelli.OrderManagement.Api.Repositories;
using Albelli.OrderManagement.Api.Services;
using Microsoft.Extensions.DependencyInjection;

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
