using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUP(ApplicationUser user, SignUpModel model);
        public Task<SignInResult> SignIn(SignInModel model);
    }
}
