using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Interfaces;

namespace Sigma.Core.Entities
{
    public class Dividend : IEntity, IPayment, IRequested
    {
        public Guid Id { get; set; }
        public DateTime RegistryCloseDate { get; set; }
        public decimal Value { get; set; }

        public Currency Currency { get; set; }
        public Guid CurrencyId { get; set; }

        public Stock Stock { get; set; }
        public Guid StockId { get; set; }
    }
}
