﻿using Microsoft.Extensions.DependencyInjection;
using Sigma.Services.Interfaces;
using Sigma.Services.Services;
using Sigma.Services.Services.SynchronizationService;

namespace Sigma.Services
{
    public static class ServiceLayerServiceExtension
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IRefreshDataService, RefreshDataService>();
            services.AddScoped<IMarketDataProvider, MarketDataProvider>();
            services.AddScoped<ISynchronizationService, SynchronizationService>();

            return services;
        }
    }
}
