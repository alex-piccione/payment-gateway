using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;
using System;

namespace PaymentGateway.WebApi.Models
{
    public class PaymentResponse
    {
        public string Id { get; set; }
        public DateTime ExecutionDate { get; set; }

        public string CardNumber { get; set; }
        //public string CardOwner { get; set; }
        //public int ExpiryYear { get; set; }
        //public int ExpiryMonth { get; set; }
        //public int CCV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        internal static ActionResult<PaymentResponse> FromPayment(Payment payment)
        {
            return new PaymentResponse
            {
                Id = payment.Id,
                CardNumber = payment.CardNumber,
                ExecutionDate = payment.ExecutionDate,
                Amount = payment.Amount,
                Currency = payment.Currency
            };
        }
    }
}
