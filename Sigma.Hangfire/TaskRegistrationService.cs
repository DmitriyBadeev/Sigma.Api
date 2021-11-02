using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Sigma.Services.Service;

namespace Sigma.Hangfire
{
    public class TaskRegistrationService
    {
        private readonly IMoexService _moexService;
        private readonly IConfiguration _configuration;

        public TaskRegistrationService(IMoexService moexService, IConfiguration configuration)
        {
            _moexService = moexService;
            _configuration = configuration;
        }

        public void RegisterTasks()
        {
            JobStorage.Current = new PostgreSqlStorage(_configuration.GetConnectionString("HangfireConnection"));

            RecurringJob.AddOrUpdate("MoexBoardRefresh", () => _moexService.RefreshBoards(), "0 0/5 * * * *");
        }
    }
}
