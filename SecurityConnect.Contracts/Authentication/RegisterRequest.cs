using System.Security;

namespace SecurityConnect.Contracts.Authentication
{
    // RegisterRequest is a DTO used for registering a new user. 
    // It captures the necessary user details such as username, password, first name, and last name.
    public record RegisterRequest(
        string UserName,
        string Password,
        string FirstName,
        string LastName
    );
}
