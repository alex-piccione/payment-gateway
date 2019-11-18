using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.WebApi.Metrics;

namespace PaymentGateway.WebApi.Controllers
{
    [ApiController, Route("metrics")]
    public class MetricsController : BaseController
    {
        public MetricsController(ILogger<HealthCheckController> logger) : base(logger)
        {

        }

        [HttpGet]
        public IActionResult Metrics()
        {
            logger.LogInformation("Metrics");
            return new JsonResult(MetricsDataCollector.Report) { StatusCode = 200 };
        }

    }
}