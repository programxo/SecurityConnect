using SecurityConnect.Domain.Entities.UserAggregate;

namespace SecurityConnect.Application.Authentication.Common
{
    public record AuthenticationResult(

        User User,

        string Token

        );
}
