using SecurityConnect.Contracts.Admin.Employee;
using SecurityConnect.Contracts.Authentication;
using SecurityConnect.WebApp.Services.Base;

namespace SecurityConnect.WebApp.Services;
public class EmployeeService : HttpBaseService
{
    public EmployeeService(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        return PostAsync<AuthenticationResponse, LoginRequest>("auth/login", loginRequest);
    }

    public async Task<List<EmployeeResponse>> ListEmployees()
    {
        return await GetListAsync<EmployeeResponse>("admin/listemployees");
    }

    public Task<EmployeeResponse?> RegisterEmployee(RegisterEmployeeRequest employeeRequest)
    {
        return PostAsync<EmployeeResponse, RegisterEmployeeRequest>("admin/registeremployee", employeeRequest);
    }

    public Task UpdateEmployee(string id, RegisterEmployeeRequest employeeRequest)
    {
        return PutAsync($"admin/updateemployee/{id}", employeeRequest);
    }

    public Task DeleteEmployee(string id)
    {
        return DeleteAsync($"admin/deleteemployee/{id}");
    }
}
