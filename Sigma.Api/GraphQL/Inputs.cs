﻿using System;
using Sigma.Core.Enums;

namespace Sigma.Api.GraphQL
{
    public record AddPortfolioInput(string Name, Guid TypeId);  
    public record RemovePortfolioInput(Guid PortfolioId);  
    public record UpdatePortfolioInput(Guid PortfolioId, string Name, Guid TypeId);

    public record AssetOperationInput(
        string Ticket,
        int Amount,
        decimal Price,
        Guid CurrencyId,
        DateTime Date,
        Guid PortfolioId,
        AssetType AssetType,
        AssetAction AssetAction);

    public record CurrencyOperationInput(
        Guid CurrencyId,
        decimal Total,
        DateTime Date,
        OperationType OperationType,
        Guid PortfolioId,
        string Ticket,
        int? Amount);

    public enum CandleInterval
    {
        Week = 7,
        Day = 24,
        Month = 31,
        Hour = 60,
    }
}