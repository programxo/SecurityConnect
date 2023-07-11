using Mapster;
using SecurityConnect.Application.Authentication.Commands.Register.Employee;
using SecurityConnect.Application.Authentication.Commands.Register;
using SecurityConnect.Application.Authentication.Queries.Login;
using SecurityConnect.Contracts.Authentication;
using SecurityConnect.Application.Authentication.Common;

public class AuthenticationMappingConfig : IRegister // Mapster
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterAdminRequest, RegisterAdminCommand>()
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);

        config.NewConfig<RegisterEmployeeRequest, RegisterEmployeeCommand>()
            .IgnoreNonMapped(true)
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.ManagedByAdminId, src => src.ManagedByAdminId);

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.ToString())
            .Map(dest => dest.UserRole, src => src.User.UserRole.ToString())
            .Map(dest => dest.FirstName, src => src.User.FirstName)
            .Map(dest => dest.LastName, src => src.User.LastName)
            .Map(dest => dest.UserName, src => src.User.UserName)
            .Map(dest => dest.ManagedByAdminId, src => src.User.ManagedByAdminId);

    }
}
