using Sigma.Core.Entities;
using Sigma.Infrastructure.Services;

namespace Sigma.Services.Test.Mocks
{
    public static class MockCurrencies
    {
        public static readonly Currency RubCurrency = new()
        {
            Name = "Рубль",
            Ticket = SeedFinanceData.RUB_TICKET,
            Sign = "₽",
            DollarRate = (decimal) 72.73,
            EuroRate = (decimal) 84.29,
            RubRate = 1,
        };
        
        public static readonly Currency EuroCurrency = new()
        {
            Name = "Евро",
            Ticket = SeedFinanceData.EURO_TICKET,
            Sign = "€",
            DollarRate = (decimal) 1.16,
            EuroRate = 1,
            RubRate = (decimal) 0.005
        };
        
        public static readonly Currency DollarCurrency = new()
        {
            Name = "Доллар",
            Ticket = SeedFinanceData.DOLLAR_TICKET,
            Sign = "$",
            DollarRate = 1,
            EuroRate = (decimal) 0.86,
            RubRate = (decimal) 0.01,
        };
    }
}