using SecurityConnect.Domain.Entities.UserAggregate;

namespace SecurityConnect.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
