using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class DailyPortfolioReport : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public decimal Cost { get; set; }

        public decimal PaperProfit { get; set; }

        public decimal PaymentProfit { get; set; }

        public decimal Balance { get; set; }
        
        public DateTime Time { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}