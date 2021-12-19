using System.Collections.Generic;
using System.Text.Json;

namespace Sigma.Integrations.Moex.Models.Payments.Coupons
{
    public class Coupons
    {
        public List<string> columns { get; set; }
        public List<List<JsonElement>> data { get; set; }
    }
}
