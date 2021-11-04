using Microsoft.Extensions.DependencyInjection;
using Sigma.Services.Interfaces;
using Sigma.Services.Services;

namespace Sigma.Services
{
    public static class ServiceLayerServiceExtension
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IRefreshDataService, RefreshDataService>();

            return services;
        }
    }
}
