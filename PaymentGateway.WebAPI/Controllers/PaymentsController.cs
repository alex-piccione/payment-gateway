using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Core;
using PaymentGateway.WebApi.Models;

namespace PaymentGateway.WebApi.Controllers
{
    [ApiController, Route("payments")]
    public class PaymentsController : BaseController
    {
        private IPaymentsProcessor paymentsProcessor;

        public PaymentsController(ILogger<PaymentsController> logger, IPaymentsProcessor paymentsProcessor) : base(logger)
        {
            this.paymentsProcessor = paymentsProcessor;
        }


        [HttpPost]
        public ActionResult<CreatePaymentResponse> Create(CreatePaymentRequest request)
        {
            logger.LogInformation("CreatePaymentRequest");

            try
            {
                var paymentCreationData = request.ToPaymentCreationData();
                var result = paymentsProcessor.CreatePayment(paymentCreationData);

                var response = new CreatePaymentResponse { 
                    PaymentId = result.PaymentId
                };
                //return Ok(response);
                return Created($"payments/{result.PaymentId}", response);
            }
            catch (Exception exc)
            {                
                logger.LogError(exc, $"Failed to create Payment. Request: {request.ToLog()}");
                return GeneralError();
            }
        }

        [HttpGet, Route("{paymentId}")]
        public ActionResult<PaymentResponse> Get([FromRoute]string paymentId)
        {
            logger.LogInformation("GetPayment");

            try
            {
                var payment = paymentsProcessor.GetPayment(paymentId);

                if (payment == null) return new NotFoundResult();
                else return PaymentResponse.FromPayment(payment);
            }
            catch (Exception exc)
            {
                logger.LogError(exc, $"Failed to retrieve Payment. PaymentId: {paymentId}");
                return GeneralError();
            }
        }

    }
}