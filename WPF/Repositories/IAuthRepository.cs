﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WPF.Repositories
{
    public interface IAuthRepository
    {
        Task<string> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<bool> ChangePasswordAsync(string token, ChangePasswordDto changePasswordDto);
    }
}
