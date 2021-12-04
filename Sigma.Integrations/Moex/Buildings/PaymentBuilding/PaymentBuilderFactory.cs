using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding.Builders;
using Sigma.Integrations.Moex.Models.Interfaces;
using System;
using Sigma.Integrations.Moex.Buildings.Common;

namespace Sigma.Integrations.Moex.Buildings.PaymentBuilding
{
    public class PaymentBuilderFactory
    {
        public IRequestedBuilder<TPayment, TResponse> GetPaymentBuilder<TPayment, TResponse>()
            where TPayment : IPayment, IRequested
            where TResponse : IResponse
        {
            Type paymentBuilderType = null;

            switch (typeof(TPayment).Name)
            {
                case nameof(Dividend):
                    paymentBuilderType = typeof(DividendBuilder);
                    break;
                case nameof(Coupon):
                    paymentBuilderType = typeof(CouponBuilder);
                    break;
            }

            if (paymentBuilderType != null)
            {
                var paymentBuilder = (IRequestedBuilder<TPayment, TResponse>)Activator.CreateInstance(paymentBuilderType);

                return paymentBuilder;
            }

            return null;
        }
    }
}
