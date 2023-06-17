using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SecurityConnect.WebApp.Common.Errors;
using SecurityConnect.WebApp.Common.Mapping;
using System.Reflection;

namespace SecurityConnect.WebApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Add services to the container

            // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
            services.AddControllers();

            // Overriding the default problem details factory method
            services.AddSingleton<ProblemDetailsFactory, SecurityConnectProblemDetailsFactory>();

            services.AddMappings();

            return services;
        }
    }
}
