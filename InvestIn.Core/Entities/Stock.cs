﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestIn.Core.Entities
{
    [Index(nameof(Ticket), IsUnique = true)]
    public class Stock : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Ticket { get; set; }
        
        [Required]
        public string ShortName { get; set; }

        public string MarketFullName { get; set; }

        public string FullName { get; set; }

        public string LatName { get; set; }

        public int LotSize { get; set; }

        public long IssueSize { get; set; }

        public decimal PrevClosePrice { get; set; }

        public long Capitalization { get; set; }

        public string Description { get; set; }

        public string Sector { get; set; }
        
        public decimal Price { get; set; }

        public decimal PriceChange { get; set; }
        
        public DateTime UpdateTime { get; set; }

        public List<PortfolioStock> PortfolioStocks { get; set; }
    }
}