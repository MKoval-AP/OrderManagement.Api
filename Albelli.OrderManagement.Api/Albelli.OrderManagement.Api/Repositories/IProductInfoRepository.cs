using Albelli.OrderManagement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Repositories
{
    public interface IProductInfoRepository
    {
        double GetWidth(ProductType productType);
    }
}
