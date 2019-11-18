using System;
using System.Collections.Generic;

using PaymentGateway.DataLayer;
using PaymentGateway.Models;

namespace PaymentGateway.Core.Mocking
{
    /// <summary>
    /// In memory storage of Payment records. 
    /// Number of records is capped to Capacity. A new add over the max capacity will raise an Exception.
    /// </summary>
    public class PaymentsRepositoryMock : IPaymentsRepository
    {
        public int Capacity { get; set; }

        private IDictionary<string, Payment> inMemoryStore;

        public PaymentsRepositoryMock()
        {
            Capacity = 1000000;
            inMemoryStore = new Dictionary<string, Payment>(/*Capacity*/);
        }

        public Payment Get(string paymentId)
        {
            return inMemoryStore[paymentId];
        }

        public void Save(Payment payment)
        {
            if (inMemoryStore.Count > Capacity) throw new Exception($"Max capacity ({Capacity} records) of in memory storage reached.");

            if (inMemoryStore.ContainsKey(payment.Id))
                inMemoryStore[payment.Id] = payment;
            else
                inMemoryStore.Add(payment.Id, payment);
        }
    }
}
