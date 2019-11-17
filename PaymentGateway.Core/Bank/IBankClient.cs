using PaymentGateway.Core.Models;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Bank
{
    public interface IBankClient
    {
        Payment CreatePayment(PaymentCreationData data);
    }
}
