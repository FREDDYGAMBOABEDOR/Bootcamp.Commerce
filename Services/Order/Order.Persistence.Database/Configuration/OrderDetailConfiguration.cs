using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Persistence.Database.Configuration
{
    public class OrderDetailConfiguration
    {
        public OrderDetailConfiguration(EntityTypeBuilder<Domain.OrderDetail> entityBuilder)
        {
            entityBuilder.HasKey(x => x.OrderDetailId);
        }
    }
}
