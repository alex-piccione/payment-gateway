using System;
using PaymentGateway.Core.Models;
using PaymentGateway.Models;

namespace PaymentGateway.Core.Bank
{
    public class BankClientMock : IBankClient
    {

        public Payment CreatePayment(PaymentCreationData data)
        {
            return new Payment()
            {
                

            };
        }
    }
}
