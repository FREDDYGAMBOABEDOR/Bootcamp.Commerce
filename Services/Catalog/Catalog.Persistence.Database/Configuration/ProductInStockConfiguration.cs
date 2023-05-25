using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Database.Configuration
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityBuilder) {
            entityBuilder.HasKey(x => x.ProductInStockId);

            var random = new Random();
            var stocks = new List<ProductInStock>();

            for(var i = 1; i <= 100; i++)
            {
                stocks.Add(new ProductInStock { 
                    ProductInStockId = i,
                    ProductId = i,
                    Stock = random.Next(0, 40)
                });
            }

            entityBuilder.HasData(stocks);
        }
    }
}
