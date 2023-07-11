namespace SecurityConnect.Contracts.Admin.Employee
{
    public record EmployeeResponse
    (
        string Id,
        string FirstName,
        string LastName,
        string UserName,
        string UserRole,
        string ManagedByAdminId
    );
}
