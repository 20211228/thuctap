using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
    }
}
