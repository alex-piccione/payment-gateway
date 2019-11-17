using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core
{
    public interface IPaymentsProcessor
    {
        PaymentCreationResult CreatePayment(PaymentCreationData data);
    }

    public class PaymentsProcessor : IPaymentsProcessor
    {
        public PaymentCreationResult CreatePayment(PaymentCreationData data)
        {
            throw new NotImplementedException();
        }
    }
}
