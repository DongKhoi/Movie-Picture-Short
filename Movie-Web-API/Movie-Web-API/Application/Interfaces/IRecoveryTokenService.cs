﻿using Domain.Common;
using Domain.DTOs;

namespace Application.Interfaces
{
    public interface IRecoveryTokenService
    {
        Task<Response<string>> Authenticate(AuthenticateRequest authenticate);

    }
}
