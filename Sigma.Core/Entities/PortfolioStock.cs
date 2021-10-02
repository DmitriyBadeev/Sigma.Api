using System;
using System.ComponentModel.DataAnnotations;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class PortfolioStock : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public int Amount { get; set; }

        public decimal BoughtPrice { get; set; }
        
        public Portfolio Portfolio { get; set; }

        public Guid PortfolioId { get; set; }

        public Guid StockId { get; set; }
        
        public Stock Stock { get; set; }    
    }
}