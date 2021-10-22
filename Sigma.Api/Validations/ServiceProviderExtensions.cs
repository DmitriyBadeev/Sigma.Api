using Microsoft.Extensions.DependencyInjection;
using Sigma.Api.Validations.Interfaces;

namespace Sigma.Api.Validations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationService(this IServiceCollection services)
        {
            services.AddScoped<IValidationService, ValidationService>();
            return services;
        }
    }
}