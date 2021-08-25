using Albelli.OrderManagement.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albelli.OrderManagement.Api.Validators
{
    public class OrderLineValidator
    {
        public static void Validate(IEnumerable<OrderLine> items)
        {
            if (items?.Any(x => !Enum.IsDefined(typeof(ProductType), x.ProductType)) ?? false)
            {
                var invalidObjects = JsonConvert.SerializeObject(items.Where(x => !Enum.IsDefined(typeof(ProductType), x.ProductType)));
                var minValue = Enum.GetValues(typeof(ProductType)).Cast<int>().Min();
                var maxValue = Enum.GetValues(typeof(ProductType)).Cast<int>().Max();

                throw new ArgumentOutOfRangeException($"Argument 'ProductType' is out of range for {invalidObjects}.\n Should be in range: {minValue} - {maxValue}");
            }
        }
    }
}
