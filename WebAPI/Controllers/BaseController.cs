using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using PaymentGateway.WebAPI.Models;

namespace PaymentGateway.WebAPI.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ILogger logger;

        public BaseController(ILogger logger)
        {
            this.logger = logger;
        }

        //internal ErrorResponse GeneralError() => new ErrorResponse("General error");

        protected JsonResult GeneralError([ActionResultStatusCode] int statusCode = 500) 
            => new JsonResult(null) {
                StatusCode = statusCode,
                Value =  new { status="error", error="General Error"}
            }; 
    }
}