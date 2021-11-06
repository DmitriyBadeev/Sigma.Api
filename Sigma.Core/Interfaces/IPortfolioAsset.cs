namespace Sigma.Core.Interfaces
{
    public interface IPortfolioAsset
    {
        public int Amount { get; set; }

        public decimal BoughtPrice { get; set; }

        public decimal Cost { get; set; }

        public decimal PaperProfit { get; set; }
        
        public decimal PaperProfitPercent { get; set; }
    }
}