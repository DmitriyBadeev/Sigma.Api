using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex;
using Sigma.Integrations.Moex.AssetBuilding;
using Sigma.Integrations.Moex.AssetBuilding.Builders;

namespace Sigma.Integrations
{
    public static class IntegrationsServiceExtension
    {
        public static IServiceCollection AddIntegrationsServices(this IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddScoped<MoexApi>();
            services.AddScoped<AssetBuilderFactory>();
            services.AddScoped<MoexIntegrationService>();

            services.AddScoped<IAssetBuilder<Stock>, StockBuilder>();
            services.AddScoped<IAssetBuilder<Fond>, FondBuilder>();
            services.AddScoped<IAssetBuilder<Bond>, BondBuilder>();

            return services;
        }
    }
}
