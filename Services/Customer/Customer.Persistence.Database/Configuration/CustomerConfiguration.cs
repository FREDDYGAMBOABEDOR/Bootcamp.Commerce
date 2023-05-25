
using Customer.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Persistence.Database.Configuration
{
    public class CustomerConfiguration
    {
        public CustomerConfiguration(EntityTypeBuilder<Client> entityBuilder) {
            entityBuilder.HasKey(x => x.ClientId);

            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            var clients = new List<Client>();
            for (int i = 1 ; i <= 20; i++)
            {
                clients.Add(new Client { ClientId = i, Name = $"Client {i}" });
            }
            entityBuilder.HasData(clients);
        }
    }
}
