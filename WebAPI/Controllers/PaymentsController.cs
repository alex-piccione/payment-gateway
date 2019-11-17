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
                    PaymentId = Guid.NewGuid().ToString()                    
                };
                return response;
            }
            catch (Exception exc)
            {                
                logger.LogError(exc, $"Failed to create Payment. Request: {request.ToLog()}");
                //return new StatusCodeResult(500) { 
                
                //}
                //return new StatusCodeResult(500);
                return GeneralError();
            }
        }

    }
}