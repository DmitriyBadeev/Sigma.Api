using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InvestIn.Core.Interfaces;

namespace InvestIn.Infrastructure.Services
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddFinanceInfrastructureServices(this IServiceCollection services,
            string connectionString)
        {
            services.AddPooledDbContextFactory<FinanceDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<ISeedDataService, SeedFinanceDataService>();
            
            return services;
        }
    }
}
