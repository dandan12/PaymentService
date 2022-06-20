using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PaymentService.Repositories.Interface;
using PaymentService.Services.Interface;

namespace PaymentService.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentRepository paymentRepository, IPaymentService paymentService)
        {
            _paymentRepository = paymentRepository;
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("{referenceNumber}/validate")]
        public IActionResult ValidateTransaction([FromRoute] string referenceNumber, [FromQuery] decimal amount)
        {
            var payment = _paymentRepository.GetPaymentByReferenceNumAndAmount(referenceNumber, amount);
            return Ok(payment != null);
        }

        [HttpPatch]
        [Route("{referenceNumber}/complete")]
        public async Task<IActionResult> CompleteTransaction([FromRoute] string referenceNumber)
        {
            var entity = _paymentRepository.GetPaymentByReferenceNumber(referenceNumber);
            if (entity == null) return NotFound();

            var payment = await _paymentService.CompleteTransaction(entity);
            return Ok(payment);
        }
    }
}
