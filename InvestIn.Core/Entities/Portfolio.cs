﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
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
        
        public List<Payment> Payments { get; set; }

        public List<DailyPortfolioReport> DailyPortfolioReports { get; set; }

        public List<PortfolioStock> PortfolioStocks { get; set; }
    }
}