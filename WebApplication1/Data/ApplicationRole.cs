using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }

}
