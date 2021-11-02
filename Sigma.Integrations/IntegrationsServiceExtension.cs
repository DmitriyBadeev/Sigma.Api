using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

            services.AddScoped<IAssetBuilder<Stock>, StockBuilder>();
            services.AddScoped<IAssetBuilder<Fond>, FondBuilder>();
            services.AddScoped<IAssetBuilder<Bond>, BondBuilder>();

            return services;
        }
    }
}
