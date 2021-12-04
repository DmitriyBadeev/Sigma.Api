using Sigma.Core.Interfaces;
using System;

namespace Sigma.Core.Entities
{
    public class Coupon : IEntity, IPayment, IRequested
    {
        public Guid Id { get; set; }
        public DateTime CouponDate { get; set; }
        public decimal Value { get; set; }
        public decimal ValuePercent { get; set; }

        public Currency Currency { get; set; }
        public Guid CurrencyId { get; set; }

        public Bond Bond { get; set; }
        public Guid BondId { get; set; }
    }
}
