using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Core.Interfaces;

namespace Sigma.Infrastructure.Services
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddFinanceInfrastructureServices(this IServiceCollection services,
            string connectionString)
        {
            services.AddPooledDbContextFactory<FinanceDbContext>(options => options.UseNpgsql(connectionString));
            services.AddDbContext<FinanceDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped<ISeedDataService, SeedFinanceDataService>();
            
            return services;
        }
    }
}
