using PaymentGateway.Core.Models;
using System;

namespace PaymentGateway.WebApi.Models
{
    public class CreatePaymentRequest
    {        
        public string CardNumber { get; set; }
        public string CardOwner { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
        public int CCV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string ToLog()
        {
            // avoid logging sensible data
            return $"[CardNumber:{CardNumber}, CardOwner:{CardOwner}, Amount:{Amount}, Currency:{Currency}]";
        }

        internal PaymentCreationData ToPaymentCreationData()
        {
            return new PaymentCreationData {
                CardNumber = CardNumber,
                CardHolder = CardOwner,
                ExpiryYear = ExpiryYear,
                ExpiryMonth = ExpiryMonth,
                CCV = CCV,
                Amount = Amount,
                Currency = Currency
            };
        }
    }
}
