using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;
using System;

namespace PaymentGateway.WebApi.Metrics
{
    public static class MetricsDataCollector
    {


        public static DateTime UpSince { get; set; }
        //public static string Status { get; set; }
        public static int TotalErrors { get; set; }

        public static long CreatedPayments { get; set; }
        public static long FailedPayments { get; set; }
        public static long LastPaymentCreationTime { get; set; }
        

        public static void Initialize() {
            UpSince = DateTime.UtcNow;
        }

        public static void IncreaseErrors() => TotalErrors++;
        public static void IncreaseCreatedPayments() => CreatedPayments++;
        public static void IncreaseFailedPayments() => FailedPayments++;


        public static dynamic Report =>
            new { 
                UpSince = UpSince,
                //Status = Status,
                TotalErrors = TotalErrors,

                CreatedPayments= CreatedPayments,
                FailedPayments = FailedPayments,
                LastPaymentCreationTime = LastPaymentCreationTime,
            };      

    }

}