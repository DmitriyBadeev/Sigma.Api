﻿using System.Threading.Tasks;

namespace Sigma.Services.Interfaces
{
    public interface ISynchronizationService
    {
        Task SyncPortfolios();
    }
}