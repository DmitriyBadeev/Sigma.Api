using System;
using System.ComponentModel.DataAnnotations;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class PortfolioBond : IEntity, IPortfolioAsset
    {
        [Key]
        public Guid Id { get; set; }
        
        public int Amount { get; set; }

        public decimal BoughtPrice { get; set; }
        
        public decimal Cost { get; set; }

        public decimal PaperProfit { get; set; }
        
        public decimal PaperProfitPercent { get; set; }
        
        public Portfolio Portfolio { get; set; }

        public Guid PortfolioId { get; set; }

        public Guid BondId { get; set; }
        
        public Bond Bond { get; set; }    
    }
}