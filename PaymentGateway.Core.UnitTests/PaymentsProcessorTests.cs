using System;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using PaymentGateway.Core;
using Microsoft.Extensions.Logging;
using PaymentGateway.Core.Bank;
using PaymentGateway.Core.Models;
using PaymentGateway.Models;

namespace Core.UnitTests
{
    [Category("Business logic")]

    public class PaymentsProcessorTests
    {
        private PaymentsProcessor paymentProcessor;
        private ILogger<PaymentsProcessor> logger = new Mock<ILogger<PaymentsProcessor>>().Object;
        private Mock<IBankClient> bankClient;

        [SetUp]
        public void SetUp()
        {
            bankClient = new Mock<IBankClient>();
            paymentProcessor = new PaymentsProcessor(logger, bankClient.Object);
        }

        [Test]
        public void CreatePayment__when__BankClient_returns_a_Payment__should__return_Successfull_result()
        {
            var data = new PaymentCreationData();

            var bankPayment = new Payment()
            {
                Id = Guid.NewGuid().ToString()
            };

            bankClient.Setup(c => c.CreatePayment(data)).Returns(bankPayment);

            // act
            var result = paymentProcessor.CreatePayment(data);

            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Payment.Should().NotBeNull();
            result.Payment.Id.Should().Be(bankPayment.Id);
        }

        [Test]
        public void CreatePayment__when__BankClient_returns_an_error__should__return_Error_result()
        {
            var data = new PaymentCreationData();

            var bankPayment = new Payment()
            {
                Id = Guid.NewGuid().ToString()
            };

            bankClient.Setup(c => c.CreatePayment(data)).Throws<Exception>();
            
            // act
            var result = paymentProcessor.CreatePayment(data);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
            result.Payment.Should().BeNull();
        }
               
    }
}
