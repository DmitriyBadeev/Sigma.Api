using Microsoft.Extensions.DependencyInjection;
using Sigma.Services.Interfaces;
using Sigma.Services.Service;

namespace Sigma.Services
{
    public static class ServiceLayerServiceExtension
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IRefreshDataService, RefreshDataService>();
            services.AddScoped<IExcelService, ExcelService>();

            return services;
        }
    }
}
