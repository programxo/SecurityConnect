using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using SecurityConnect.Application.Common.Interfaces.Authentication;
using SecurityConnect.Application.Common.Interfaces.Services;
using SecurityConnect.Infrastructure.Authentication;
using SecurityConnect.Infrastructure.Services;
using SecurityConnect.Application.Common.Interfaces.Persistence;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using SecurityConnect.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using SecurityConnect.Infrastructure.Persistence.Interceptors;
using SecurityConnect.Infrastructure.Persistence;

namespace SecurityConnect.Infrastructure
{
    public static class DependencyInjection
    {
        #region Method to add infrastructure services to the DI container
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           ConfigurationManager configuration)
        {
            // Add authentication services
            services.AddAuth(configuration)
                .AddPersistance();

            // Add a singleton of DateTimeProvider
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
        #endregion

        public static IServiceCollection AddPersistance(
        this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=localhost;Database=SecurityConnect;User Id=.;TrustServerCertificate=true;persist security info=True;Integrated Security=SSPI"));

            services.AddScoped<PublishDomainEventsInterceptor>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        #region Method to add authentication services to the DI container
        public static IServiceCollection AddAuth(this IServiceCollection services,
                                                 ConfigurationManager configuration)
        {
            /* Initialize JWT settings object */
            var jwtSettings = new JwtSettings();

            /* Bind JWT settings from the configuration */
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            /* Add JWT settings as a singleton */
            services.AddSingleton(Options.Create(jwtSettings));

            /* Add JwtTokenGenerator as a singleton */
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            #region Add JWT Bearer authentication
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Enable validation of the JWT Issuer (the server that created the token)
                        ValidateIssuer = true,

                        // Enable validation of the JWT Audience (the recipients that the token is intended for)
                        ValidateAudience = true,

                        // Enable validation of the JWT lifetime (the time during which the token is valid)
                        ValidateLifetime = true,

                        // Enable validation of the JWT signing key (the secret key used to sign the token)
                        ValidateIssuerSigningKey = true,

                        // Specify the valid JWT Issuer
                        ValidIssuer = jwtSettings.Issuer,

                        // Specify the valid JWT Audience
                        ValidAudience = jwtSettings.Audience,

                        // Specify the JWT signing key
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    });
            #endregion

            return services;
        }
        #endregion
    }
}
