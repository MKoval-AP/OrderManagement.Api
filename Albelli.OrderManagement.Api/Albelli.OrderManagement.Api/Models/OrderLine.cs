using System;
using System.Collections.Generic;

namespace Albelli.OrderManagement.Api.Models
{
    public class OrderLine
    {
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
