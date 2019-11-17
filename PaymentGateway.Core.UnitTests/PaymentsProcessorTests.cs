using System;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using PaymentGateway.Core;
using PaymentGateway.Core.Bank;
using PaymentGateway.Core.Models;
using PaymentGateway.Models;
using PaymentGateway.DataLayer;

namespace Core.UnitTests
{
    [Category("Business logic")]

    public class PaymentsProcessorTests
    {
        private PaymentsProcessor paymentProcessor;
        private ILogger<PaymentsProcessor> logger = new Mock<ILogger<PaymentsProcessor>>().Object;
        private Mock<IPaymentsRepository> paymentsRepository;
        private Mock<IBankClient> bankClient;

        [SetUp]
        public void SetUp()
        {
            paymentsRepository = new Mock<IPaymentsRepository>();
            bankClient = new Mock<IBankClient>();
            paymentProcessor = new PaymentsProcessor(logger, paymentsRepository.Object, bankClient.Object);
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
            result.PaymentId.Should().Be(bankPayment.Id);
        }

        [Test]
        public void CreatePayment__when__BankClient_returns_a_Payment__should__save_to_repository()
        {
            var data = new PaymentCreationData();

            var bankPayment = new Payment()
            {
                Id = Guid.NewGuid().ToString()
            };

            bankClient.Setup(c => c.CreatePayment(data)).Returns(bankPayment);
            
            // act
            var result = paymentProcessor.CreatePayment(data);

            paymentsRepository.Verify(r => r.Save(It.IsAny<Payment>()), Times.AtLeastOnce);
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
            result.PaymentId.Should().BeNull();
        }


        [Test]
        public void GetPayment__when__record_not_exists__should__return_null()
        {
            paymentsRepository.Setup(r => r.Get(It.IsAny<string>())).Returns<Payment>(null);

            // act
            var payment = paymentProcessor.GetPayment("not exists");

            payment.Should().BeNull();
        }

        [Test]
        public void GetPayment__when__record_exists__should__return_Payment()
        {
            paymentsRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(new Payment());

            // act
            var payment = paymentProcessor.GetPayment("not exists");

            payment.Should().NotBeNull();
        }
    }
}
