namespace SecurityConnect.Application.Authentication.Commands.Register.Employee
{
    public record RegisterEmployeeCommand
    (
        string FirstName,
        string LastName,
        string UserName,
        string Password,

        string ManagedByAdminId

        ) : IRequest<ErrorOr<AuthenticationResult>>;

}
