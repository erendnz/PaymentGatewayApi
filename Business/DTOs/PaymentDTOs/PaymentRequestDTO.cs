using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.PaymentDTOs
{
    public record PaymentRequestDto(
        decimal Amount,
        string Currency,
        string CardNumber,
        string CardHolderName,
        int ExpirationMonth,
        int ExpirationYear,
        string Cvv,
        string OrderReference
    );
}
