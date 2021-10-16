using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Server.Ui.Voyager;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Sigma.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations;

namespace Sigma.Api
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            
            services.AddFinanceInfrastructureServices(connectionString);
            services.AddValidationService();
            services.AddCors();
            services.AddMediatR(typeof(Startup));
            
            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
                .AddQueryType<Query>()
                .AddProjections()
                .AddMutationType<Mutation>();
            
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://identity-sigma.herokuapp.com";
                    options.ApiName = "Sigma.Api";
                    IdentityModelEventSource.ShowPII = true;
                });
            
            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(b => b
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
            
            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            });
        }
    }
    
    public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
    {
        public override ValueTask OnCreateAsync(HttpContext context,
            IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
            CancellationToken cancellationToken)
        {
            var identity = context.User.Identity;
            
            if (identity is {IsAuthenticated: true})
            {
                requestBuilder.SetProperty("UserId",
                    context.User.FindFirstValue("sub"));
            }
            
            return base.OnCreateAsync(context, requestExecutor, requestBuilder,
                cancellationToken);
        }
    }
}