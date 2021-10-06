using System;

namespace Sigma.Api.GraphQL
{
    public record DefaultPayload(bool IsSuccess, string Message = "Запрос выполнен успешно");

    public record DefaultPayload<T>(bool IsSuccess, string Message = "Запрос выполнен успешно", T Result = default);

    public record PaymentData(
        string Name,
        string Ticket,
        decimal PaymentValue,
        int Amount,
        int AllPayment,
        DateTime RegistryCloseDate,
        string CurrencyId);

    public record ValuePercent(decimal Value, decimal Percent);

    public record CostWithInvestSum(decimal Cost, decimal InvestSum);

    public record StockReport(
        string Name,
        string Ticket,
        int Amount,
        decimal Price,
        decimal PriceChange,
        decimal AllPrice,
        decimal BoughtPrice,
        decimal PaperProfit,
        decimal PaperProfitPercent,
        PaymentData NearestDividend,
        decimal PaidDividends,
        string UpdateTime);

    public record FondReport(
        string Name,
        string Ticket,
        int Amount,
        double Price,
        double PriceChange,
        double AllPrice,
        double BoughtPrice,
        double PaperProfit,
        double PaperProfitPercent,
        string UpdateTime);

    public record BondReport(
        string Name,
        string Ticket,
        int Amount,
        double Price,
        double PriceChange,
        double AllPrice,
        double BoughtPrice,
        double PaperProfit,
        double PaperProfitPercent,
        PaymentData NearestPayment,
        double PaidPayments,
        string UpdateTime,
        DateTime AmortizationDate,
        bool HasAmortized);
}