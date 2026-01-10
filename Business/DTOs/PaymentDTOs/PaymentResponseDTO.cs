using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.PaymentDTOs
{
    public record PaymentResponseDto(
        Guid TransactionId,
        string Status,
        decimal Amount,
        string Currency,
        string MaskedCardNumber,
        DateTime CreatedAt
    );
}
