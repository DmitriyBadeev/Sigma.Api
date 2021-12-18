using System;
using System.Collections.Generic;
using MediatR;

namespace Sigma.Api.GraphQL
{
    public record DefaultPayload(bool IsSuccess, string Message = "Запрос выполнен успешно");

    public record DefaultPayload<T>(bool IsSuccess, string Message = "Запрос выполнен успешно", T Result = default) : IRequest<DefaultPayload>;

    public record StockCandle(
        double Open,
        double Close,
        double High,
        double Low,
        double Value,
        double Volume,
        long Date);

    public record TimeValue(
        long Date,
        int Value);

    public record CostGraphData(
        int PortfolioId,
        string PortfolioName,
        List<TimeValue> Data);
}