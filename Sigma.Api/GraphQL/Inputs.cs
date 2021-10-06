using System;

namespace Sigma.Api.GraphQL
{
    public record AddPortfolioInput(string Name, Guid TypeId);  
    public record RemovePortfolioInput(Guid PortfolioId);  
    public record UpdatePortfolioInput(Guid PortfolioId, string Name, Guid TypeId);

    public record BuyAssetInput(
        int PortfolioId,
        string Ticket,
        int Price,
        int Amount,
        int AssetTypeId,
        DateTime Date);

    public record SellAssetInput(
        int PortfolioId,
        string Ticket,
        int Price,
        int Amount,
        int AssetTypeId,
        DateTime Date);

    public record RefillBalanceInput(
        int PortfolioId,
        int Price,
        DateTime Date);

    public record WithdrawalBalanceInput(
        int PortfolioId,
        int Price,
        DateTime Date);
}