using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.repository;

namespace WebApplication1.Services
{
    public interface IAccountService
    {
        public Task<apiResult<string>> Signin(SignInModel signInModel);
        public Task<apiResult<IdentityResult>> SignUp(SignUpModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IValidator<SignInModel> _loginModelValidator;

        public AccountService(IAccountRepository accountRepository, 
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            RoleManager<ApplicationRole> roleManager,
            IValidator<SignInModel> validator)
        {
            _accountRepository = accountRepository;
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            _loginModelValidator = validator;
        }
        public async Task<apiResult<string>> Signin(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var validationResult = await _loginModelValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var result = await _accountRepository.SignIn(model);
            if (!result.Succeeded)
            {
                return new apiResult<string>(new[] { "dang nhap that bai" }, null);
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creads
                );
            return new apiResult<string>(new[] { new JwtSecurityTokenHandler().WriteToken(token) }, null);


        }

        public async Task<apiResult<IdentityResult>> SignUp(SignUpModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    EmailConfirmed = true
                };
                var result = await _accountRepository.SignUP(user, model);
                return new apiResult<IdentityResult>(new[] { "Pass" }, result);
            }
            catch (Exception ex)
            {
                return new apiResult<IdentityResult>(new[] { ex.ToString() }, null);
            }
            
        }
    }
}
