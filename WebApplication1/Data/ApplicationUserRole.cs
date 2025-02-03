using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class ApplicationUserRole: IdentityUserRole<Guid>
    {
        public DateTime? Created { get; set; }
    }
}
