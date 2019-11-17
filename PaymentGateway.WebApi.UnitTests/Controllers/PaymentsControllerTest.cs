using System;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using PaymentGateway.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using PaymentGateway.Core;
using PaymentGateway.WebApi.Models;
using PaymentGateway.Core.Models;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Models;

namespace PaymentGateway.WebApi.UnitTests.Controllers
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
        public void Create__when__Processor_returns_Success__should__return_Success_and_PaymentId()
        {           
            var request = new CreatePaymentRequest();
            string paymentId = Guid.NewGuid().ToString();
            paymentsProcessor.Setup(p => p.CreatePayment(It.IsAny<PaymentCreationData>())).Returns(
                new PaymentCreationResult { IsSuccess = true, PaymentId = paymentId }
            );

            // act
            var response = controller.Create(request)?.Value as CreatePaymentResponse;
            
            response.Should().NotBeNull();
            response.PaymentId.Should().Be(paymentId);
        }

        [Test]
        public void Create__should__call_PaymentsProcessor_with_right_data()
        {
            var request = new CreatePaymentRequest {
                CardNumber = "1234-4567-1234-4567",
                CardOwner = "Owner",
                CCV = 1234,
                ExpiryYear = 2025,
                ExpiryMonth = 1,
                Amount = 1.23m,
                Currency = "GBP"
            };            

            paymentsProcessor.Setup(c => c.CreatePayment(It.IsAny<PaymentCreationData>()))
                .Callback<PaymentCreationData>(data =>
                {
                    Assert.AreEqual(request.CardNumber, data.CardNumber);
                    Assert.AreEqual(request.CardOwner, data.CardHolder);
                    Assert.AreEqual(request.CCV, data.CCV);
                    Assert.AreEqual(request.ExpiryYear, data.ExpiryYear);
                    Assert.AreEqual(request.ExpiryMonth, data.ExpiryMonth);
                    Assert.AreEqual(request.Amount, data.Amount);
                    Assert.AreEqual(request.Currency, data.Currency);

                }).Returns<object>(null).Verifiable();

            // act
            var response = controller.Create(request)?.Value as CreatePaymentResponse;

            paymentsProcessor.Verify();
        }


        [Test]
        public void GetPayment__when__should__return_Success()
        {
            var payment = new Payment()
            {
                Id = Guid.NewGuid().ToString(),
                CardNumber = "1234-5678-1234-4567",
                CardHolder = "Owner",
                ExecutionDate = DateTime.UtcNow,
            };

            paymentsProcessor.Setup(p => p.GetPayment(It.IsAny<string>())).Returns(payment);

            // act 
            var response = controller.Get(payment.Id);

            response.Should().NotBeNull();
            var paymentResponse = response.Value.As<PaymentResponse>();
            paymentResponse.Id.Should().Be(payment.Id);
            paymentResponse.CardNumber.Should().Be(payment.CardNumber);
            paymentResponse.ExecutionDate.Should().BeSameDateAs(payment.ExecutionDate);
        }

        [Test]
        public void GetPayment__when__Processor_fails__should__return_a_Error_Response()
        {
            string paymentId = Guid.NewGuid().ToString();
            paymentsProcessor.Setup(p => p.GetPayment(It.IsAny<string>())).Throws<Exception>();

            // act 
            var response = controller.Get(paymentId);

            response.Should().NotBeNull();
            (response.Result as JsonResult).StatusCode.Should().Be(500);
        }

        [Test]
        public void GetPayment__when__Payment_is_not_found__should__return_404()
        {
            paymentsProcessor.Setup(p => p.GetPayment(It.IsAny<string>())).Returns<Payment>(null);

            // act 
            var response = controller.Get("not exists");

            response.Should().NotBeNull();
            (response.Result as NotFoundResult).StatusCode.Should().Be(404);
        }

    }
}
