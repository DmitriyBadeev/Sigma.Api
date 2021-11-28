using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Models.Payments.Coupons
{
    public class CouponResponse : IResponse
    {
        public Payments.Coupons.Coupons coupons { get; set; }
        public Amortizations amortizations { get; set; }
    }
}
