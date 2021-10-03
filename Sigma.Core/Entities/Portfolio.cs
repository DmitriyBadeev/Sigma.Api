﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class Portfolio : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public string UserId { get; set; }
        
        public string Name { get; set; }

        public Guid PortfolioTypeId { get; set; }

        public PortfolioType PortfolioType { get; set; }

        public List<AssetOperation> AssetOperations { get; set; }

        public List<CurrencyOperation> CurrencyOperations { get; set; }
        
        public List<DailyPortfolioReport> DailyPortfolioReports { get; set; }

        public List<PortfolioStock> PortfolioStocks { get; set; }
        
        public List<PortfolioFond> PortfolioFonds { get; set; }
        
        public List<PortfolioBond> PortfolioBonds { get; set; }
    }
}