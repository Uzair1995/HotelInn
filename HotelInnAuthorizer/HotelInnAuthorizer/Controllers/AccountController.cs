using HotelInnAuthorizer.Models;
using HotelInnAuthorizer.Repositories.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUserAccountAsync(RegisterUser registerUser)
        {
            User user = GetUserModel(registerUser, "User");
            IdentityResult result = await userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminUserAccountAsync(RegisterUser registerUser)
        {
            User user = GetUserModel(registerUser, "Admin");
            IdentityResult result = await userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAccountAsync(LoginUser loginUser)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(loginUser.Name, loginUser.Password, false, false);

            if (!result.Succeeded)
                return Unauthorized("Username and Password doesn't match!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, loginUser.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, loginUser.Name));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        /// <summary>
        /// Private
        /// </summary>
        /// <param name="registerUser"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private static User GetUserModel(RegisterUser registerUser, string role)
        {
            return new User
            {
                UserName = registerUser.Name,
                Id = Guid.NewGuid().ToString(),
                Name = registerUser.Name,
                Role = role,
                Gender = registerUser.Gender,
                Address = registerUser.Address,
                City = registerUser.City,
                Country = registerUser.Country
            };
        }
    }
}
