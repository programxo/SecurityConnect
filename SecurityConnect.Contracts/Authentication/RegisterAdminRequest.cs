namespace SecurityConnect.Contracts.Authentication
{
    public record RegisterAdminRequest(
        
        string FirstName,
        string LastName,
        string UserName,
        string Password,
        string UserRole
    );
}
