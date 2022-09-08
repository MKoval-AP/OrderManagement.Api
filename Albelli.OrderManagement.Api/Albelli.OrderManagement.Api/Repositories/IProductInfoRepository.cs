using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public interface IProductInfoRepository
    {
        ProductInfo Get(ProductType productType);
    }
}