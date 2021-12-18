using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

namespace Sigma.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly FinanceDbContext _financeDbContext;

        public PaymentService(FinanceDbContext financeDbContext)
        {
            _financeDbContext = financeDbContext;
        }

        public async Task<List<PaymentData>> GetFuturePayments(Guid portfolioId)
        {
            var portfolio = await _financeDbContext.Portfolios
                .Include(p => p.PortfolioStocks)
                .ThenInclude(ps => ps.Stock)
                .ThenInclude(s => s.Dividends)
                .ThenInclude(s => s.Currency)
                .Include(p => p.PortfolioBonds)
                .ThenInclude(pb => pb.Bond)
                .ThenInclude(b => b.Coupons)
                .ThenInclude(s => s.Currency)
                .AsSingleQuery()
                .FirstOrDefaultAsync(p => p.Id == portfolioId);

            var futureDividends = GetFutureDividends(portfolio.PortfolioStocks);
            var futureCoupons = GetFutureCoupons(portfolio.PortfolioBonds);

            var payments = futureDividends
                .Concat(futureCoupons)
                .ToList();

            return payments;
        }

        private List<PaymentData> GetFutureDividends(List<PortfolioStock> stocks)
        {
            var futureDividends = new List<PaymentData>();
            foreach (var stock in stocks)
            {
                var stockDividends = stock.Stock.Dividends
                    .Where(d => d.RegistryCloseDate.Date >= DateTime.Now.Date)
                    .Select(d => new PaymentData
                    (
                        stock.Stock.ShortName,
                        stock.Stock.Ticket,
                        d.Value,
                        stock.Amount,
                        d.Value * stock.Amount,
                        d.RegistryCloseDate,
                        d.Currency
                    ));
                
                futureDividends.AddRange(stockDividends);
            }

            return futureDividends;
        }

        private List<PaymentData> GetFutureCoupons(List<PortfolioBond> bonds)
        {
            var futureCoupons = new List<PaymentData>();
            foreach (var bond in bonds)
            {
                var bondCoupons = bond.Bond.Coupons
                    .Where(c => c.CouponDate.Date >= DateTime.Now.Date)
                    .Select(d => new PaymentData
                    (
                        bond.Bond.ShortName,
                        bond.Bond.Ticket,
                        d.Value,
                        bond.Amount,
                        d.Value * bond.Amount,
                        d.CouponDate,
                        d.Currency
                    ));
                
                futureCoupons.AddRange(bondCoupons);
            }

            return futureCoupons;
        }
    }
}