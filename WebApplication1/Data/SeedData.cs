using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            // tạo role
            var roles = new[]
            {
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Administrator role"
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "User",
                    Description = "User role"
                }
            };

            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var administrator = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "User",
                UserName = "admin@example.com",
                NormalizedUserName = "Admin",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true
            };

            if(await userManager.FindByNameAsync(administrator.UserName) == null)
            {
                var result = await userManager.CreateAsync(administrator, "Admin@123");
                if (result.Succeeded)
                {
                    var adminRole = await roleManager.FindByNameAsync("Admin");
                    if (adminRole != null)
                    {
                        await userManager.AddToRoleAsync(administrator,adminRole.Name);
                    }
                }
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "User",
                UserName = "user1@example.com",
                NormalizedUserName = "User",
                Email = "user1@example.com",
                NormalizedEmail = " USER@EXAMPLE.COM",
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(user.UserName) == null)
            {
                var result = await userManager.CreateAsync(user, "User1@123");
                if (result.Succeeded)
                {
                    var userrole = await roleManager.FindByNameAsync("User");
                    if (userrole != null)
                    {
                        await userManager.AddToRoleAsync(user, userrole.Name);
                    }
                }
            }
        }
    }
}
