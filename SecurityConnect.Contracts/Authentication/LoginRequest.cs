using System.Security;

namespace SecurityConnect.Contracts.Authentication
{
    // LoginRequest is a DTO used for user authentication. 
    // It captures the user's username and password.
    public record LoginRequest(
        string UserName,
        SecureString Password
    );
}
