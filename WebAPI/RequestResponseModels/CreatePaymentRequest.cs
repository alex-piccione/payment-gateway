using System;

namespace PaymentGateway.WebAPI.Models
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
    }
}
