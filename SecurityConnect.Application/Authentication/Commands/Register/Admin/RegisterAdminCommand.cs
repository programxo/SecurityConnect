namespace SecurityConnect.Application.Authentication.Commands.Register
{
    public record RegisterAdminCommand
    (
        string FirstName,
        string LastName,
        string UserName,
        string Password

    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
