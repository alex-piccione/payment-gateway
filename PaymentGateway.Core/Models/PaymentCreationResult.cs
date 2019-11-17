using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class PaymentCreationResult
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public Payment Payment { get; set; }


        public static PaymentCreationResult Success(Payment payment)
            => new PaymentCreationResult {
                IsSuccess = true,
                Payment = payment
            };

        public static PaymentCreationResult Fail(string error)
            => new PaymentCreationResult
            {
                IsSuccess = false,
                Error = error
            };
    }
}
