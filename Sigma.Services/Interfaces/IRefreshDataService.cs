using System.Threading.Tasks;

namespace Sigma.Services.Interfaces
{
    public interface IRefreshDataService
    {
        Task RefreshPayments();
        Task RefreshAssets();
    }
}