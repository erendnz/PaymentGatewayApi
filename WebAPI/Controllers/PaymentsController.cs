using Business.DTOs.PaymentDTOs;
using Business.PaymentService;
using Core.Constants.Messages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/payments")] 
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IValidator<PaymentRequestDto> _validator;

        public PaymentsController(IPaymentService paymentService, IValidator<PaymentRequestDto> validator)
        {
            _paymentService = paymentService;
            _validator = validator;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeTransaction([FromBody] PaymentRequestDto request)
        {
            var validationResult = await _validator.ValidateAsync(request); // Validate the request using FluentValidation rules
            
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    error = ErrorMessages.ValidationError,
                    message = validationResult.Errors.First().ErrorMessage
                });
            }

            var result = await _paymentService.AuthorizeTransactionAsync(request);
            return CreatedAtAction(nameof(GetTransactionById), new { id = result.TransactionId }, result);
        }


        /// Retrieves a specific transaction by its unique identifier.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var result = await _paymentService.GetTransactionByIdAsync(id);

            if (result == null)
                return NotFound(new { error = "Not Found", message = ErrorMessages.TransactionNotFound });

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a paginated list of all transactions.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            // Security check to prevent massive data fetching
            if (pageSize > 100) pageSize = 100;

            var results = await _paymentService.GetAllTransactionsAsync(page, pageSize);

            return Ok(new
            {
                totalCount = results.Count(),
                page,
                pageSize,
                items = results
            });
        }

        [HttpPost("{id}/capture")]
        public async Task<IActionResult> CaptureTransaction(Guid id)
        {
            var success = await _paymentService.CaptureTransactionAsync(id);

            if (!success)
                return BadRequest(new { error = "Invalid operation", message = ErrorMessages.InvalidCapture });

            return Ok(new { message = "Transaction captured successfully." });
        }

        [HttpPost("{id}/void")]
        public async Task<IActionResult> VoidTransaction(Guid id)
        {
            var success = await _paymentService.VoidTransactionAsync(id);

            if (!success)
                return BadRequest(new { error = "Invalid operation", message = ErrorMessages.InvalidVoid });

            return Ok(new { message = "Transaction voided successfully." });
        }

        /// <summary>
        /// Retrieves the history for a specific transaction.
        /// </summary>
        [HttpGet("{id}/events")]
        public async Task<IActionResult> GetTransactionEvents(Guid id)
        {
            var transaction = await _paymentService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound(new { error = "Not Found", message = ErrorMessages.TransactionNotFound });

            var events = await _paymentService.GetTransactionEventsAsync(id);
            return Ok(events);
        }
    }
}