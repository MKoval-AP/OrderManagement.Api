﻿using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using System.Collections.Generic;

namespace Albelli.OrderManagement.Api.Calculations
{
    public static class PackageCalculator
    {
        public const int MugsInOneRowStack = 4;

        public static double PackageWidth(IEnumerable<OrderLine> items, ProductInfoRepository productInfoRepository)
        {
            double pw = 0;

            foreach (OrderLine item in items)
            {
                ProductType t = item.ProductType;
                int itemQuantity = item.Quantity;
                ProductInfo info = productInfoRepository.Get(t);

                if (item.ProductType is ProductType.Mug)
                {
                    pw += itemQuantity > MugsInOneRowStack ?
                            info.WidthMm * (itemQuantity / MugsInOneRowStack + itemQuantity % MugsInOneRowStack) :
                            info.WidthMm;
                }
                else
                {
                    pw += info.WidthMm * itemQuantity;
                }
            }

            return pw;
        }
    }
}