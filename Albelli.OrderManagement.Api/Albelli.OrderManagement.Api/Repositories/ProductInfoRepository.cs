using System;
using System.Collections.Generic;
using Albelli.OrderManagement.Api.Infrastructure.Exceptions;
using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private static readonly IDictionary<ProductType, ProductInfo> _productInfo = new Dictionary<ProductType, ProductInfo>()
        {
            { ProductType.PhotoBook, new ProductInfo (ProductType.PhotoBook, 19) },
            { ProductType.Calendar, new ProductInfo(ProductType.Calendar, 10) },
            { ProductType.Canvas, new ProductInfo(ProductType.Canvas, 16) },
            { ProductType.Cards, new ProductInfo (ProductType.Cards, 4.7)  },
            { ProductType.Mug, new ProductInfo(ProductType.Mug, 94, 4) }
        };

        public ProductInfo Get(ProductType productType)
        {
            if (!_productInfo.ContainsKey(productType))
            {
                throw new ProductInfoNotFoundException($"No information available for product type {productType}.");
            }
            return _productInfo[productType];
        }
    }
}
