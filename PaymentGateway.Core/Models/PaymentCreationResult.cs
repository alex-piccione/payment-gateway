using System;
using PaymentGateway.Models;

namespace PaymentGateway.Core.Models
{
    public class PaymentCreationResult
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string PaymentId { get; set; }        


        public static PaymentCreationResult Success(Payment payment)
            => new PaymentCreationResult {
                IsSuccess = true,
                PaymentId = payment.Id,
            };

        public static PaymentCreationResult Fail(string error)
            => new PaymentCreationResult
            {
                IsSuccess = false,
                Error = error
            };
    }
}
