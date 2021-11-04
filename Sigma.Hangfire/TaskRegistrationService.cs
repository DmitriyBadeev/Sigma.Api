using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Sigma.Services.Interfaces;

namespace Sigma.Hangfire
{
    public class TaskRegistrationService
    {
        private readonly IRefreshDataService _refreshDataService;
        private readonly IConfiguration _configuration;

        public TaskRegistrationService(IRefreshDataService refreshDataService, IConfiguration configuration)
        {
            _refreshDataService = refreshDataService;
            _configuration = configuration;
        }

        public void RegisterTasks()
        {
            JobStorage.Current = new PostgreSqlStorage(_configuration.GetConnectionString("HangfireConnection"));

            RecurringJob.AddOrUpdate("MoexBoardRefresh", () => _refreshDataService.RefreshBoards(), "0 0/5 * * * *");
        }
    }
}
