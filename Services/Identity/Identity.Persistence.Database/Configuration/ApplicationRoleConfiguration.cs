
using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Database.Configuration
{
    public class ApplicationRoleConfiguration
    {
        public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entityBuilder) {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.HasData(new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                                    new ApplicationRole { Id =2, Name = "User", NormalizedName = "USER" });

            entityBuilder.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(e => e.RoleId);
        }
    }
}
