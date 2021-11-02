using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex;
using Sigma.Integrations.Moex.AssetBuilding;
using Sigma.Integrations.Moex.AssetBuilding.Builders;
using Sigma.Services.Service;

namespace Sigma.Services
{
    public static class ServiceLayerServiceExtension
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IMoexService, MoexService>();

            return services;
        }
    }
}
