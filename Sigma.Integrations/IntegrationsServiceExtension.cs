using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex;
using Sigma.Integrations.Moex.Buildings.AssetBuilding;
using Sigma.Integrations.Moex.Buildings.AssetBuilding.Builders;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding.Builders;
using Sigma.Integrations.Moex.Models.Payments.Coupons;
using Sigma.Integrations.Moex.Models.Payments.Dividends;

namespace Sigma.Integrations
{
    public static class IntegrationsServiceExtension
    {
        public static IServiceCollection AddIntegrationsServices(this IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddScoped<MoexApi>();
            services.AddScoped<MoexIntegrationService>();

            services.AddScoped<AssetBuilderFactory>();
            services.AddScoped<PaymentBuilderFactory>();

            return services;
        }
    }
}
