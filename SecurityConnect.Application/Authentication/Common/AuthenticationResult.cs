using SecurityConnect.Domain.Entities;

namespace SecurityConnect.Application.Authentication.Common
{
    public record AuthenticationResult(

        User User,

        string Token

        );
}
