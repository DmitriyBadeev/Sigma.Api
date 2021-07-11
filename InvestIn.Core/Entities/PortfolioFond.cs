using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class PortfolioFond : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public int Amount { get; set; }

        public decimal BoughtPrice { get; set; }
        
        public Portfolio Portfolio { get; set; }

        public Guid PortfolioId { get; set; }

        public Guid FondId { get; set; }
        
        public Fond Fond { get; set; }    
    }
}