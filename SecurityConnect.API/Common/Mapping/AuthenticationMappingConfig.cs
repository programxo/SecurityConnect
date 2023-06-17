using Mapster;
using SecurityConnect.Application.Authentication.Commands.Register;
using SecurityConnect.Application.Authentication.Common;
using SecurityConnect.Application.Authentication.Queries.Login;
using SecurityConnect.Contracts.Authentication;

namespace SecurityConnect.WebApp.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister // Mapster
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
        }
    }
}
