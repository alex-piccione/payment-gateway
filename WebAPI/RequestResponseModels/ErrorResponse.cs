using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace PaymentGateway.WebAPI.Models
{
    public class ErrorResponse : JsonResult //, IHttpActionResult  // StatusCodeResult, IActionResult
    {
        public string Error { get; set; }

        public ErrorResponse(string error, [ActionResultStatusCode] int statusCode = 500) : base(error)
        {
            Error = error;          
        }

        //public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        //{
        //    var response = new HttpResponseMessage()
        //    {
        //        Content = new StringContent($@"{{""status"":""error"", ""error"":""{Error}"" }}"),
        //        //RequestMessage = request
        //    };
        //    return Task.FromResult(response);
        //}
    }
}
