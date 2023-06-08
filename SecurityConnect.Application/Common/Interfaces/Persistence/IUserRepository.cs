﻿using SecurityConnect.Domain.Entities;

namespace SecurityConnect.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByUserName(string userName); /* nullable (?) entweder ein User-Objekt oder null,
                                                     anstatt einen Fehler */
        void Add(User user); // sync: andere Operationen müssen warten
    }
}