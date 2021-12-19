using System.Collections.Generic;
using System.Text.Json;
using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Models.Candles
{
    public class CandleResponse : IResponse
    {
        public CandlesData candles { get; set; }
    }
    
    public class CandlesData
    {
        public List<string> columns { get; set; }
        public List<List<JsonElement>> data { get; set; }
    }
}