
using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using PaymentGateway.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace WebApiUnitTests
{

    [Category("Web API")]
    public class HealthCheckControllerTest
    {
        private ILogger<HealthCheckController> logger = new Mock<ILogger<HealthCheckController>>().Object;

        
        [Test]
        public void Check__should__return_HTTP_StatusCode_OK() 
        {
            var controller = new HealthCheckController(logger);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/json";

            var result = controller.Check() as JsonResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);   
        }

    }
}
