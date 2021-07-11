using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class Payment : IEntity
    {
        [Key]
        public Guid Id { get; set; }
    
        [Required]
        public string Ticket { get; set; }

        public int Amount { get; set; }

        public decimal PaymentPerOne { get; set; }
        
        public decimal AllPayment { get; set; }

        public DateTime Date { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}