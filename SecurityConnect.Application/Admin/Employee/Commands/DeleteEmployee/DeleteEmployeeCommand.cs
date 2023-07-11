namespace SecurityConnect.Application.Admin.Employee.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand
    (
        string Id
    )
    : IRequest<bool>;

}
