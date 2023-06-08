using ErrorOr;
using MediatR;
using SecurityConnect.Application.Authentication.Common;
using System.Security;

namespace SecurityConnect.Application.Authentication.Queries.Login
{
    public record LoginQuery
    (
        string UserName,
        SecureString Password) : IRequest<ErrorOr<AuthenticationResult>>;

}
