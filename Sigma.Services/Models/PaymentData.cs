using System;
using Sigma.Core.Entities;

namespace Sigma.Services.Models
{
    public record PaymentData(
        string AssetName,
        string Ticket,
        decimal PaymentValue,
        int Amount,
        decimal Total,
        DateTime Date,
        Currency Currency);
}