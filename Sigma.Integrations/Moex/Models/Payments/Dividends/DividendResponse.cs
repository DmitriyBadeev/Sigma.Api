using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Models.Payments.Dividends
{
    public class DividendResponse : IResponse
    {
        public Payments.Dividends.Dividends dividends { get; set; }
    }
}
