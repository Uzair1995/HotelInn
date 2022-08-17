using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelInn.Contracts.User;
using HotelInn.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelInn.Presentation.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [SwaggerTag("APIs to manage users.")]
    public class UserController
    {
        private readonly Lazy<IUserService> userService;

        public UserController(Lazy<IUserService> userService)
        {
            this.userService = userService;
        }

        [SwaggerOperation(Summary = "Create new user.")]
        [HttpPost]
        public async Task<string> CreateNewUserAsync(NewUser newUser)
        {
            return await userService.Value.AddNewUserAsync(newUser);
        }

        [SwaggerOperation(Summary = "Get user details.")]
        [HttpGet]
        public async Task<User> GetUserDetailsAsync([FromQuery] string userId)
        {
            return await userService.Value.FindUserAsync(userId);
        }

        [SwaggerOperation(Summary = "List users.")]
        [HttpGet("list")]
        public async Task<List<User>> GetUsersListAsync()
        {
            return await userService.Value.ListAllUsersAsync(); ;
        }
    }
}
