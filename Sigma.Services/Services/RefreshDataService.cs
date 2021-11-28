using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex;
using Sigma.Integrations.Moex.Models.Assets;
using Sigma.Integrations.Moex.Models.Interfaces;
using Sigma.Integrations.Moex.Models.Payments.Coupons;
using Sigma.Integrations.Moex.Models.Payments.Dividends;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services
{
    public class RefreshDataService : IRefreshDataService
    {
        private readonly FinanceDbContext _context;
        private readonly MoexIntegrationService _moexIntegrationService;

        private delegate void PaymentsToAsset(List<IPayment> payments, Guid assetId);

        public RefreshDataService(FinanceDbContext context, MoexIntegrationService moexIntegrationService)
        {
            _context = context;
            _moexIntegrationService = moexIntegrationService;
        }

        public async Task RefreshPayments()
        {
            // TODO: Рассмотреть в будущем возможность апдейта без удаления всех записей
            RemoveAllPayments();

            var bonds = _context.Bonds.ToList();
            foreach (var bond in bonds)
            {
                await GetAndSavePayment<Coupon, CouponResponse>(bond.Ticket, CouponsToBond, bond.Id);
            }

            var stocks = _context.Stocks.ToList();
            foreach (var stock in stocks)
            {
                await GetAndSavePayment<Dividend, DividendResponse>(stock.Ticket, DividendsToStock, stock.Id);
            }
        }

        private async Task GetAndSavePayment<TPayment, TResponse>(string ticket, PaymentsToAsset assign, Guid assetId)
            where TPayment : class, IPayment, IRequested
            where TResponse : class, IResponse
        {
            var payments = await _moexIntegrationService.GetPayments<TPayment, TResponse>(ticket);

            var iPayments = payments
                .Cast<IPayment>()
                .ToList();

            assign(iPayments, assetId);

            await _context.Set<TPayment>().AddRangeAsync(payments);
            await _context.SaveChangesAsync();
        }

        private void CouponsToBond(List<IPayment> payments, Guid bondId)
        {
            payments.ForEach(x => ((Coupon)x).BondId = bondId);
        }

        private void DividendsToStock(List<IPayment> payments, Guid stockId)
        {
            payments.ForEach(x => ((Dividend)x).StockId = stockId);
        }

        private void RemoveAllPayments()
        {
            _context.RemoveRange(_context.Dividends);
            _context.RemoveRange(_context.Coupons);

            _context.SaveChanges();
        }



        public async Task RefreshAssets()
        {
            // TODO: Рассмотреть в будущем возможность апдейта без удаления всех записей
            RemoveAllAssets();

            await GetAndSaveAsset<Stock, AssetResponse>();
            await GetAndSaveAsset<Fond, AssetResponse>();
            await GetAndSaveAsset<Bond, AssetResponse>();
        }

        private async Task GetAndSaveAsset<TAsset, TResponse>()
            where TAsset : class, IAsset, IRequested
            where TResponse : IResponse
        {
            var assets = await _moexIntegrationService.GetAssets<TAsset, TResponse>();
            await _context.Set<TAsset>().AddRangeAsync(assets);
            await _context.SaveChangesAsync();
        }

        private void RemoveAllAssets()
        {
            _context.RemoveRange(_context.Stocks);
            _context.RemoveRange(_context.Fonds);
            _context.RemoveRange(_context.Bonds);
            
            _context.SaveChanges();
        }
    }
}