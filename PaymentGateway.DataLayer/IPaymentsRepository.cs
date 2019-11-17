using System;
using System.Collections.Generic;
using PaymentGateway.Models;

namespace PaymentGateway.DataLayer
{
    public interface IPaymentsRepository
    {
        void Save(Payment paymenr);
        Payment Get(string paymentId);

    }
}
