using Microsoft.Extensions.Options;
using Sigma.Integrations.Common.Enums;
using Sigma.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sigma.Integrations.Moex
{
    public class MoexApi
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<HangFireSettings> _hangFireSettings;

        public MoexApi(HttpClient httpClient, IOptions<HangFireSettings> hangFireSettings)
        {
            _httpClient = httpClient;
            _hangFireSettings = hangFireSettings;
        }

        public async Task<string> GetPaymentJson(PaymentTypes paymentType, string ticket)
        {
            var moexUrls = _hangFireSettings.Value.IntegrationUrls.Moex;

            var url = string.Empty;

            switch (paymentType)
            {
                case PaymentTypes.Coupon:
                    url = moexUrls.Coupons;
                    break;
                case PaymentTypes.Dividend:
                    url = moexUrls.Dividends;
                    break;
            }

            url = url.Replace("{ticket}", ticket);

            var data = await RequestTo(url);

            return data;
        }

        public async Task<string> GetAssetJson(AssetTypes assetType)
        {
            var moexUrls = _hangFireSettings.Value.IntegrationUrls.Moex;

            var url = string.Empty;

            switch (assetType)
            {
                case AssetTypes.Stock:
                    url = moexUrls.StockBoard;
                    break;
                case AssetTypes.Fond:
                    url = moexUrls.FondBoard;
                    break;
                case AssetTypes.Bond:
                    url = moexUrls.BondBoard;
                    break;
            }

            var data = await RequestTo(url);

            return data;
        }

        private async Task<string> RequestTo(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "{}";
        }
    }
}
