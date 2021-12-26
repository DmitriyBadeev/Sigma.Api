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
        private readonly IHistoryDataService _historyDataService;

        public TaskRegistrationService(IRefreshDataService refreshDataService, ISynchronizationService synchronizationService, 
            IConfiguration configuration, IHistoryDataService historyDataService)
        {
            _refreshDataService = refreshDataService;
            _synchronizationService = synchronizationService;
            _configuration = configuration;
            _historyDataService = historyDataService;
        }

        public void RegisterTasks()
        {
            JobStorage.Current = new PostgreSqlStorage(_configuration.GetConnectionString("HangfireConnection"));

            RecurringJob.AddOrUpdate("MoexBoardRefresh", () => MoexRefreshData(), "0 0/10 * * * *");
            RecurringJob.AddOrUpdate("MoexPaymentsRefresh", () => MoexPaymentsRefresh(), "0 0 17 * * *");
            RecurringJob.AddOrUpdate("DailyReport", () => DailyPortfolioReport(), "0 0 19 * * *");
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        public async Task MoexRefreshData()
        {
            await _refreshDataService.RefreshAssets();
            await _synchronizationService.SyncPortfolios();
            await _refreshDataService.RefreshIndexes();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public async Task MoexPaymentsRefresh()
        {
            await _refreshDataService.RefreshPayments();
            await _synchronizationService.SyncPortfolios();
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        public async Task DailyPortfolioReport()
        {
            await _historyDataService.MakePortfoliosRecord();
        }
    }
}
