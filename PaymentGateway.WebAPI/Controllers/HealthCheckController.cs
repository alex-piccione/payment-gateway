﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.WebApi.Controllers
{
    [ApiController, Route("health")]
    public class HealthCheckController : BaseController
    {
        public HealthCheckController(ILogger<HealthCheckController> logger) : base(logger)
        {

        }

        [HttpGet]
        public IActionResult Check()
        {
            logger.LogInformation("Check");

            if (Request.Headers["Accept"] == "application/json")
                return new JsonResult(new { status = "ok" }) { StatusCode = 200 };
            else
                return Ok("OK");
        }

    }
}