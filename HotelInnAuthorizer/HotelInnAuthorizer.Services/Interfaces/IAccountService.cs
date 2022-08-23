﻿using HotelInnAuthorizer.Services.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterNewAccount(RegisterAccount registerAccount);
        Task<LoginResult> LoginAccountAsync(string username, string password);
    }
}
