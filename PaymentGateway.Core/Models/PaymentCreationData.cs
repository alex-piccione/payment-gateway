using System;

namespace PaymentGateway.Core.Models
{
    public class PaymentCreationData
    {
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
        public int CCV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
