﻿using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Enums;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class AssetOperation : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Ticket { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public Portfolio Portfolio { get; set; }

        public Guid PortfolioId { get; set; }

        public AssetType AssetType { get; set; }

        public AssetAction AssetAction { get; set; }
    }
}
