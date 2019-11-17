using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.WebAPI.Models;

namespace PaymentGateway.WebAPI.Controllers
{
    [ApiController, Route("payments")]
    public class PaymentsController : BaseController
    {

        public PaymentsController(ILogger<PaymentsController> logger) : base(logger)
        {

        }


        [HttpPost]
        public ActionResult<CreatePaymentResponse> Create(CreatePaymentRequest request)
        {
            logger.LogInformation("CreatePaymentRequest");

            try
            {
                var response = new CreatePaymentResponse { 
                    PaymentId = Guid.NewGuid().ToString()                    
                };
                return response;
            }
            catch (Exception exc)
            {                
                logger.LogError(exc, $"Failed to create Payment. Request: {request.ToLog()}");
                return GeneralError();
            }
        }

    }
}