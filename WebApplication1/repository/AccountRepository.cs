using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> SignIn(SignInModel model)
        {
            var result = await signInManager.PasswordSignInAsync
                (model.Email, model.Password, false, false);
            return result; 
        }

        public async Task<IdentityResult> SignUP(ApplicationUser user, SignUpModel model)
        {
            

            var result =  await userManager.CreateAsync(user,model.Password);
            
            return result;
        }
    }
}
