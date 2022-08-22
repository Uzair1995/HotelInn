using HotelInnAuthorizer.Repositories.Models;
using HotelInnAuthorizer.Services.Interfaces;
using HotelInnAuthorizer.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> RegisterNewAccount(RegisterAccount registerAccount)
        {
            IdentityResult result = await userManager.CreateAsync(registerAccount.ToCore(), registerAccount.Password);
            return result;
        }

        public async Task<LoginResult> LoginAccountAsync(string username, string password)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(username, password, false, false);

            if (!result.Succeeded)
                return new LoginResult
                {
                    Result = "Username and Password doesn't match!",
                    Token = null,
                    Succeeded = false
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new LoginResult
            {
                Result = "Success",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Succeeded = true
            };
        }
    }
}
