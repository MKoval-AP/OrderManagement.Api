using System;
using System.Collections.Generic;
using Albelli.OrderManagement.Api.Models;

namespace Albelli.OrderManagement.Api.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private static readonly IDictionary<ProductType, double> productInfo = new Dictionary<ProductType, double>()
        {
            { ProductType.PhotoBook, 19 },
            { ProductType.Calendar, 10 },
            { ProductType.Canvas, 16 },
            { ProductType.Cards, 4.7 },
            { ProductType.Mug, 94 }
        };

        public double GetWidth(ProductType productType)
        {
            if (!productInfo.ContainsKey(productType))
            {
                throw new Exception($"No information available for product type {productType}.");
            }

            return productInfo[productType];
        }
    }
}
