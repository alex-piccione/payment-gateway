using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PaymentGateway.Core.Bank;
using PaymentGateway.Core.Models;
using PaymentGateway.DataAccess;
using PaymentGateway.Models;

namespace PaymentGateway.Core
{
    public interface IPaymentsProcessor
    {
        PaymentCreationResult CreatePayment(PaymentCreationData data);
        Payment GetPayment(string paymentId);
    }

    public class PaymentsProcessor : IPaymentsProcessor
    {
        private ILogger<PaymentsProcessor> logger;
        private IPaymentsRepository paymentsRepository;
        private IBankClient bankClient;

        public PaymentsProcessor(ILogger<PaymentsProcessor> logger, IPaymentsRepository paymentsRepository, IBankClient bankClient)
        {
            this.logger = logger;
            this.paymentsRepository = paymentsRepository;
            this.bankClient = bankClient;
        }

        public PaymentCreationResult CreatePayment(PaymentCreationData data)
        {
            try
            { 
                // generate the Payment Id
                data.PaymentId = Guid.NewGuid().ToString();

                var payment = bankClient.CreatePayment(data);
                paymentsRepository.Save(payment);

                // A validation error should return a Fail result
                //return PaymentCreationResult.Fail("Not enough funds");

                return PaymentCreationResult.Success(payment);
            }
            catch (Exception exc)
            {
                logger.LogError(exc, "Failed to process payment creation");
                return PaymentCreationResult.Fail("General error on creating payment");
            }
        }

        public Payment GetPayment(string paymentId)
        {
            return paymentsRepository.Get(paymentId);
        }
    }
}
