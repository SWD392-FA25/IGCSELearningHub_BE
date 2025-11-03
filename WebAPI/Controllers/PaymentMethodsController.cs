using System.Linq;
using Application.DTOs.Payments;
using Application.Exceptions;
using Application.Payments.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment-methods")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _paymentMethodService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActive()
        {
            var result = await _paymentMethodService.GetActiveAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id:int}/status")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] PaymentMethodStatusUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var result = await _paymentMethodService.SetStatusAsync(id, dto.IsActive);
            return StatusCode(result.StatusCode, result);
        }
    }
}
