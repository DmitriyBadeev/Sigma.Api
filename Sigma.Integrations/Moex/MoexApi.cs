using System.Net.Http;
using System.Threading.Tasks;
using Sigma.Integrations.Common.Constants;
using Sigma.Integrations.Common.Enums;

namespace Sigma.Integrations.Moex
{
    public class MoexApi
    {
        private readonly HttpClient _httpClient;

        public MoexApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetBoardJson(MoexTradeModes tradeMode)
        {
            var url = ApiUrls.MoexUrls.GetBoardUrl(tradeMode);
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
