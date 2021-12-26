namespace Sigma.Core.Interfaces
{
    public interface IAsset
    {
        public string Ticket { get; set; }

        public string ShortName { get; set; }
        
        public decimal AverageProfit { get; set; }

        public decimal Risk { get; set; }
        
        public decimal SharpeRatio { get; set; }
    }
}
