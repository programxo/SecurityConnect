﻿using ErrorOr;
using MediatR;
using SecurityConnect.Application.Authentication.Common;
using System.Security;

namespace SecurityConnect.Application.Authentication.Commands.Register
{
    public record RegisterCommand
    (
        string UserName,
        string Password,
        string FirstName,
        string LastName) : IRequest<ErrorOr<AuthenticationResult>>;


}