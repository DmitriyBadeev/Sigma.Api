using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Sigma.Services.Interfaces;

namespace Sigma.Hangfire
{
    public class TaskRegistrationService
    {
        private readonly IRefreshDataService _refreshDataService;
        private readonly ISynchronizationService _synchronizationService;
        private readonly IConfiguration _configuration;

        public TaskRegistrationService(IRefreshDataService refreshDataService, ISynchronizationService synchronizationService, 
            IConfiguration configuration)
        {
            _refreshDataService = refreshDataService;
            _synchronizationService = synchronizationService;
            _configuration = configuration;
        }

        public void RegisterTasks()
        {
            JobStorage.Current = new PostgreSqlStorage(_configuration.GetConnectionString("HangfireConnection"));

            RecurringJob.AddOrUpdate("MoexBoardRefresh", () => MoexRefreshData(), "0 0/5 * * * *");
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        public async Task MoexRefreshData()
        {
            await _refreshDataService.RefreshAssets();
            await _synchronizationService.SyncPortfolios();
            await _refreshDataService.RefreshPayments();
        }
    }
}
