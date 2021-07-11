using System;
using System.ComponentModel.DataAnnotations;
using InvestIn.Core.Enums;
using InvestIn.Core.Interfaces;

namespace InvestIn.Core.Entities
{
    public class CurrencyOperation : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyId { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        public CurrencyAction CurrencyAction { get; set; }

        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}
