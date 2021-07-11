using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class PortfolioBond : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public int Amount { get; set; }

        public decimal BoughtPrice { get; set; }
        
        public Portfolio Portfolio { get; set; }

        public Guid PortfolioId { get; set; }

        public Guid BondId { get; set; }
        
        public Bond Bond { get; set; }    
    }
}