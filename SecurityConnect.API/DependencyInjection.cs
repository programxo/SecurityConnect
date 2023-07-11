using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SecurityConnect.WebApp.Common.Errors;
using SecurityConnect.WebApp.Common.Mapping;
using SecurityConnect.WebApp.Services;

namespace SecurityConnect.WebApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Add singleton services to the container
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthenticationService>());
            services.AddSingleton<ProblemDetailsFactory, SecurityConnectProblemDetailsFactory>();

            // Configure Http clients
#if DEBUG
            services.AddHttpClient<EmployeeService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:7038");
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                };
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
#endif

            // Configure HttpContext and Controllers
            services.AddHttpContextAccessor();
            services.AddControllers();

            // Configure authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                         policy => policy.RequireRole("Administrator"));
            });

            // Configure mappings
            services.AddMappings();

            return services;
        }
    }

}
