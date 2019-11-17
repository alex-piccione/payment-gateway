using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace PaymentGateway.WebAPI.Models
{
    public class ErrorResponse : StatusCodeResult
    {
        public string Error { get; set; }

        public ErrorResponse(string error, [ActionResultStatusCode] int statusCode = 500) : base(statusCode)
        {
            Error = error;
        }        
    }
}
