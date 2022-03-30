using Albelli.OrderManagement.Api.Models;
using System;
using System.Collections.Generic;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class ProductInfoRepository
    {
        private static readonly IDictionary<ProductType, ProductInfo> _productInfo = new Dictionary<ProductType, ProductInfo>()
        {
            { ProductType.PhotoBook, new ProductInfo { WidthMm = 19 } },
            { ProductType.Calendar, new ProductInfo { WidthMm = 10 } },
            { ProductType.Canvas, new ProductInfo { WidthMm = 16 } },
            { ProductType.Cards, new ProductInfo { WidthMm = 4.7 } },
            { ProductType.Mug, new ProductInfo { WidthMm = 94 } }
        };

        public ProductInfo Get(ProductType productType)
        {
            if (!_productInfo.ContainsKey(productType))
            {
                throw new Exception($"No information available for product type {productType}.", null);
            }

            var info = _productInfo[productType];

            return new ProductInfo
            {
                ProductType = productType,
                WidthMm = info.WidthMm
            };
        }
    }
}
