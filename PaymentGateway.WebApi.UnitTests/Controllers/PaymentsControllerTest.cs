using System;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using PaymentGateway.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using PaymentGateway.Core;
using PaymentGateway.WebApi.Models;

namespace WebApi.UnitTests.Controllers
{
    [Category("Web API")]
    public class PaymentsControllerTest
    {
        private PaymentsController controller;
        private ILogger<PaymentsController> logger = new Mock<ILogger<PaymentsController>>().Object;
        private Mock<IPaymentsProcessor> paymentsProcessor = new Mock<IPaymentsProcessor>();
        

        [SetUp]
        public void SetUp()
        {            
            paymentsProcessor = new Mock<IPaymentsProcessor>();
            controller = new PaymentsController(logger, paymentsProcessor.Object);
        }

        [Test]
        public void Create__should__return_PaymentResponse()
        {
            var request = new CreatePaymentRequest();
            var response = controller.Create(request)?.Value as CreatePaymentResponse;

            response.Should().NotBeNull();
            response.PaymentId.Should().NotBeNullOrEmpty();
        }

    }
}
