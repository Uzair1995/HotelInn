using HotelInnAuthorizer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<bool> RegisterNewUserAccountAsync(RegisterUser registerUser)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = registerUser.Name,
                Id = Guid.NewGuid().ToString()
            };
            IdentityResult result = await userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
                return true;
            return false;
        }

        [HttpPost("Login")]
        public async Task<bool> LoginUserAccountAsync(RegisterUser registerUser)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = registerUser.Name,
                Id = Guid.NewGuid().ToString()
            };
            IdentityResult result = await userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
                return true;
            return false;
        }
    }
}
