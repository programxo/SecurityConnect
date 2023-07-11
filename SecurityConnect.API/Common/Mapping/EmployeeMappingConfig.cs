using Mapster;
using SecurityConnect.Contracts.Admin.Employee;
using SecurityConnect.Domain.Entities.Users;

namespace SecurityConnect.WebApp.Common.Mapping
{
    public class EmployeeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, EmployeeResponse>()
                .IgnoreNonMapped(true)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.UserRole, src => src.UserRole)
                .Map(dest => dest.ManagedByAdminId, src => src.ManagedByAdminId);
        }
    }

}
