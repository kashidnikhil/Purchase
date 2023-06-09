﻿using System.Threading.Tasks;
using Abp.Application.Services;
using MyTraining1101Demo.MultiTenancy.Payments.Dto;
using MyTraining1101Demo.MultiTenancy.Payments.Stripe.Dto;

namespace MyTraining1101Demo.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}