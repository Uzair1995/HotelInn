using HotelInnAuthorizer.Models;
using HotelInnAuthorizer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUserAccountAsync([FromBody] RegisterAccount registerAccount)
        {
            Services.Models.RegisterResult result = await accountService.RegisterNewAccount(registerAccount.ToDto("User"));
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminUserAccountAsync([FromBody] RegisterAccount registerAccount)
        {
            Services.Models.RegisterResult result = await accountService.RegisterNewAccount(registerAccount.ToDto("Admin"));
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAccountAsync([FromBody] LoginUser loginUser)
        {
            Services.Models.LoginResult result = await accountService.LoginAccountAsync(loginUser.Name, loginUser.Password);

            if (!result.Succeeded)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
