﻿using SecurityConnect.Domain.Entities;

namespace SecurityConnect.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}