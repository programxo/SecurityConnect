namespace SecurityConnect.Application.Admin.Employee.Queries.ListEmployees
{
    public record ListEmployeesQuery(string AdminId) : IRequest<List<User>>;
}
