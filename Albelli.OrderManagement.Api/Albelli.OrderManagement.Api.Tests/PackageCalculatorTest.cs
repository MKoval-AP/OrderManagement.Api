using Albelli.OrderManagement.Api.Calculations;
using Albelli.OrderManagement.Api.Models;
using Albelli.OrderManagement.Api.Repositories;
using System.Collections.Generic;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{
    public class PackageCalculatorTest
    {

        [Fact]
        public void PackageWidth_TwoCalendars_TwoCalendarsWidth()
        {
            // Arrange
            var _productInfoRepository = new ProductInfoRepository();
            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Calendar, Quantity = 2 });

            // Act
            double width = PackageCalculator.PackageWidth(lines, _productInfoRepository);

            // Assert
            Assert.Equal(20, width);
        }

        [Theory]
        [InlineData(1, 94)]
        [InlineData(2, 94)]
        [InlineData(3, 94)]
        [InlineData(4, 94)]
        public void PackageWidth_OneRowSetOfMugs_OneMugWidth(int mugQuantity, double expectedWidth)
        {
            // Arrange
            var productInfoRepository = new ProductInfoRepository();
            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = mugQuantity });

            // Act
            double actualWidth = PackageCalculator.PackageWidth(lines, productInfoRepository);

            // Assert
            Assert.Equal(expectedWidth, actualWidth);
        }

        [Theory]
        [InlineData(5, 188)] // Two rows
        [InlineData(8, 188)] // Two rows
        [InlineData(9, 282)] // Three rows
        public void PackageWidth_MultipleRowsSetOfMugs_SumOfRowsWidth(int mugQuantity, double expectedWidth)
        {
            // Arrange
            var productInfoRepository = new ProductInfoRepository();
            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = mugQuantity });

            // Act
            double actualWidth = PackageCalculator.PackageWidth(lines, productInfoRepository);

            // Assert
            Assert.Equal(expectedWidth, actualWidth);
        }

        [Fact]
        public void PackageWidth_EachProductByOneItem_SumOfProductsWidths()
        {
            // Arrange
            var productInfoRepository = new ProductInfoRepository();

            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.PhotoBook, Quantity = 1 });
            lines.Add(new OrderLine { ProductType = ProductType.Calendar, Quantity = 1 });
            lines.Add(new OrderLine { ProductType = ProductType.Canvas, Quantity = 1 });
            lines.Add(new OrderLine { ProductType = ProductType.Cards, Quantity = 1 });
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = 1 });

            // Act
            double actualWidth = PackageCalculator.PackageWidth(lines, productInfoRepository);

            // Assert
            Assert.Equal(143.7, actualWidth);
        }
    }
}
