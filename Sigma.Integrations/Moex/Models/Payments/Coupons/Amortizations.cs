using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sigma.Integrations.Moex.Models.Payments.Coupons
{
    public class Amortizations
    {
        public List<string> columns { get; set; }
        public List<List<JsonElement>> data { get; set; }
    }
}
