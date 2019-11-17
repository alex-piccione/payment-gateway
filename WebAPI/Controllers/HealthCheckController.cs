using System;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.WebAPI.Controllers
{
    
    [ApiController, Route("health")]
    public class HealthCheckController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "OK";
        }

    }
}