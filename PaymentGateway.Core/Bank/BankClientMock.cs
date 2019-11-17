using System;
using PaymentGateway.Core.Models;
using PaymentGateway.Models;

namespace PaymentGateway.Core.Bank
{
    public class BankClientMock : IBankClient
    {
        public string KnownPaymentId { get; set; }

        public BankClientMock(string paymentId)
        {
            KnownPaymentId = paymentId;
        }

        public Payment CreatePayment(PaymentCreationData data)
        {
            return new Payment()
            {
                Id = KnownPaymentId,
                CardNumber = data.CardNumber,
                CardHolder = data.CardHolder,
                ExecutionDate = DateTime.UtcNow,
                ExpiryYear = data.ExpiryYear,
                ExpiryMonth = data.ExpiryMonth,
                CCV = data.CCV,
                Amount = data.Amount,
                Currency = data.Currency
            };
        }
    }
}
