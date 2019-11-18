using System;
using PaymentGateway.Core.Bank;
using PaymentGateway.Core.Models;
using PaymentGateway.Models;

namespace PaymentGateway.Core.Mocking
{
    public class BankClientMock : IBankClient
    {

        public BankClientMock()
        {

        }

        public Payment CreatePayment(PaymentCreationData data)
        {
            return new Payment()
            {
                Id = data.PaymentId,
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
