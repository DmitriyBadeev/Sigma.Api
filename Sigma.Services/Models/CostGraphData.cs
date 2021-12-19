using System;
using System.Collections.Generic;

namespace Sigma.Services.Models
{
    public record CostGraphData(
        Guid PortfolioId,
        string PortfolioName,
        List<TimeValue> Data);
    
    public record TimeValue(
        long Date,
        decimal Value);
}