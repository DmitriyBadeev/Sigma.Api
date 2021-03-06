using System;
using System.ComponentModel.DataAnnotations;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class AssetOperation : IEntity, IOperation
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Ticket { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

        public Guid CurrencyId { get; set; }
        
        public Currency Currency { get; set; }

        public DateTime Date { get; set; }

        public Portfolio Portfolio { get; set; }

        public Guid PortfolioId { get; set; }

        public AssetType AssetType { get; set; }

        public AssetAction AssetAction { get; set; }
    }
}
