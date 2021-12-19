using System;
using System.ComponentModel.DataAnnotations;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class DailyPortfolioReport : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Cost { get; set; }

        public decimal InvestedSum { get; set; }

        public decimal PaperProfit { get; set; }

        public decimal PaperProfitPercent { get; set; }

        public decimal DividendProfit { get; set; }
        
        public decimal DividendProfitPercent { get; set; }

        public decimal RubBalance { get; set; }

        public decimal DollarBalance { get; set; }

        public decimal EuroBalance { get; set; }

        public DateTime Date { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}