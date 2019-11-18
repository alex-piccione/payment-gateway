using PaymentGateway.Core.Models;
using System;

namespace PaymentGateway.WebApi.Models
{
    public class CreatePaymentRequest
    {        
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
        public int CCV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string ToLog()
        {
            // avoid logging sensible data
            return $"[CardNumber:{CardNumber}, CardHolder:{CardHolder}, Amount:{Amount}, Currency:{Currency}]";
        }

        internal PaymentCreationData ToPaymentCreationData()
        {   
            return new PaymentCreationData {
                CardNumber = CardNumber,
                CardHolder = CardHolder,
                ExpiryYear = ExpiryYear,
                ExpiryMonth = ExpiryMonth,
                CCV = CCV,
                Amount = Amount,
                Currency = Currency
            };
        }
    }
}
