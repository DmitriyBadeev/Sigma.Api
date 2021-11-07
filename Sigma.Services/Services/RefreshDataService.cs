using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services
{
    public class RefreshDataService : IRefreshDataService
    {
        private readonly FinanceDbContext _context;
        private readonly MoexIntegrationService _moexIntegrationService;

        public RefreshDataService(FinanceDbContext context, MoexIntegrationService moexIntegrationService)
        {
            _context = context;
            _moexIntegrationService = moexIntegrationService;
        }

        public async Task RefreshBoards()
        {
            // TODO: Рассмотреть в будущем возможность апдейта без удаления всех записей
            RemoveAllAssets();

            await GetAndSaveAsset<Stock>();
            await GetAndSaveAsset<Fond>();
            await GetAndSaveAsset<Bond>();
        }

        private async Task GetAndSaveAsset<TAsset>()
            where TAsset : class, IAsset
        {
            var assets = await _moexIntegrationService.GetAssets<TAsset>();
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