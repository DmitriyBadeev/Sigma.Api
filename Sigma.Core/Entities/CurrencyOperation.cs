using System;
using System.ComponentModel.DataAnnotations;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class CurrencyOperation : IEntity, IOperation
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CurrencyId { get; set; }
        
        public Currency Currency { get; set; }
        
        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        public OperationType OperationType { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }

        public string Ticket { get; set; }
        
        public int? Amount { get; set; }
    }
}
