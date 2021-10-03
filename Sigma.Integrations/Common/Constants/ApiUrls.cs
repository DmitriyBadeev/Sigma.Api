using Sigma.Infrastructure.Helpers;
using Sigma.Integrations.Common.Enums;

namespace Sigma.Integrations.Common.Constants
{
    internal static class ApiUrls
    {
        public static class MoexUrls
        {
            private const string Shares = "shares";
            private const string Bonds = "bonds";

            public static string GetBoardUrl(MoexTradeModes moexTradeMode)
            {
                var assetType = string.Empty;
                var tradeMode = EnumHelper.GetEnumName(moexTradeMode);

                switch (moexTradeMode)
                {
                    case MoexTradeModes.Stock or MoexTradeModes.Fond:
                        assetType = Shares;
                        break;
                    case MoexTradeModes.Bond:
                        assetType = Bonds;
                        break;
                }

                return $"https://iss.moex.com/iss/engines/stock/markets/{assetType}/boards/{tradeMode}/securities.json?iss.meta=off&iss.only=securities,marketdata";
            }
        }
    }
}
