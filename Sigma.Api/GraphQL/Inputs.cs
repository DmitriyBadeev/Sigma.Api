﻿using System;

namespace Sigma.Api.GraphQL
{
    public record AddPortfolioInput(string Name, Guid TypeId);  
    public record RemovePortfolioInput(Guid PortfolioId);  
    public record UpdatePortfolioInput(Guid PortfolioId, string Name, Guid TypeId);  
}