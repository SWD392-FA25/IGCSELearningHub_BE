﻿using Application.ViewModels.Authentication;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<AuthenticatedUserDTO>> RegisterAsync(AccountRegistrationDTO registrationDto);
        Task<Result<AuthenticatedUserDTO>> LoginAsync(AccountLoginDTO loginDto);
    }
}
