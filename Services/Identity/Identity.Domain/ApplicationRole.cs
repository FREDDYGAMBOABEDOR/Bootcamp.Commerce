using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
