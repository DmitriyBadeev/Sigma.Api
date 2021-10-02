using System;
using System.Collections.Generic;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class Currency : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ticket { get; set; }

        public decimal RubRate { get; set; }
        
        public decimal DollarRate { get; set; }
        
        public decimal EuroRate { get; set; }

        public List<CurrencyOperation> CurrencyOperations { get; set; }
        
        public List<AssetOperation> AssetOperations { get; set; }
    }
}