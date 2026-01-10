using System;
using System.Collections.Generic;
using System.Linq;
using Business.DTOs.PaymentDTOs;
using Business.Utilities;
using FluentValidation;

namespace Business.ValidationRules
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequestDto>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a 3-letter code.")
                .Must(c => new[] { "USD", "EUR", "GBP" }.Contains(c))
                .WithMessage("Only USD, EUR, and GBP are supported.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .Length(13, 19).WithMessage("Card number must be between 13 and 19 digits.")
                .Matches("^[0-9]*$").WithMessage("Card number must contain only digits.")
                .Must(x => x.IsValidLuhn()).WithMessage("Invalid card number (Luhn check failed).");

            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("Card holder name is required.")
                .MinimumLength(2).WithMessage("Card holder name must be at least 2 characters.");

            RuleFor(x => x.ExpirationMonth)
                .InclusiveBetween(1, 12).WithMessage("Expiration month must be between 1 and 12.");

            RuleFor(x => x.ExpirationYear)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("Expiration year must be current year or later.");

            RuleFor(x => x.Cvv)
                .NotEmpty().WithMessage("CVV is required.")
                .Length(3, 4).WithMessage("CVV must be 3 or 4 digits.")
                .Matches("^[0-9]*$").WithMessage("CVV must contain only digits.");

            RuleFor(x => x.OrderReference)
                .NotEmpty().WithMessage("Order reference is required.")
                .MaximumLength(50).WithMessage("Order reference cannot exceed 50 characters.");
        }
    }
}