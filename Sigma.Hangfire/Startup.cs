using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sigma.Hangfire.Filters;
using Sigma.Infrastructure.Services;
using Sigma.Integrations;
using Sigma.Services;

namespace Sigma.Hangfire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("FinanceDbConnection");

            services.AddFinanceInfrastructureServices(connectionString);
            services.AddIntegrationsServices();
            services.AddServiceLayerServices();
            services.AddCors();
            services.AddTransient<TaskRegistrationService>();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard(new DashboardOptions
                {
                    Authorization = new[] {new AuthorizationFilter()}
                });
            });
        }
    }
}