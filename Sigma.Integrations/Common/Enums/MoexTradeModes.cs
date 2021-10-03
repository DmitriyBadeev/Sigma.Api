using System.ComponentModel;

namespace Sigma.Integrations.Common.Enums
{
    public enum MoexTradeModes
    {
        [Description("TQBR")]
        Stock,

        [Description("TQTF")]
        Fond,

        [Description("TQOB")]
        Bond
    }
}
