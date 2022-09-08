namespace Albelli.OrderManagement.Api.Models
{
    public class ProductInfo
    {
        public ProductInfo(ProductType productType, double widthMm, int countInStack = 1)
        {
            ProductType = productType;
            WidthMm = widthMm;
            CountInStack = countInStack;
        }

        public ProductType ProductType { get; }
        public double WidthMm { get; }

        public int CountInStack { get; }
    }
}
