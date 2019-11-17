using System;


namespace PaymentGateway.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public DateTime ExecutionDate { get; set; }

        public string CardNumber { get; set; }
        public string CardOwner { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
        public int CCV { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

    }
}
