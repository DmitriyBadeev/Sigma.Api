using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class Fond : IEntity
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
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }

        public decimal PriceChange { get; set; }
        
        public DateTime UpdateTime { get; set; }
    }
}